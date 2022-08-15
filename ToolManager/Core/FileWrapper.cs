using System;
using UnityEditor;

namespace ToolManager
{
    public sealed class FileWrapper
    {
        private const string ASSETS = "Assets";
        private const string SELECT = "Select";
        private const string DELETE = "Delete";
        private const string CLEAN = "Clean";
        private const string FILE = "File: ";
        private const string NO_FILE = "Empty";
        private const string EMPTY = "";
        private const char SLASH = '/';

        public string ParentFolderName { get; }
        public string Name { get; }
        public string Extension { get; }
        public Action OnLoad { get; }
        private BaseToolWindow ParentWindow { get; }
        private string FullAssetPath => $"{ASSETS}/{ParentFolderName}/{Name}.{Extension}";
        private string FolderPath => $"{ASSETS}/{ParentFolderName}";
        private bool IsValid => LoadFile() != null;

        public FileWrapper(BaseToolWindow window, string parentFolderName, string fileName, string extension, Action onLoad)
        {
            ParentWindow = window;
            ParentFolderName = parentFolderName;
            OnLoad = onLoad;
            Name = fileName;
            Extension = extension;

            if (IsValid)
            {
                ParentWindow.AddLabel(FILE + FullAssetPath);
            }
            else
            {
                ParentWindow.AddLabel(FILE + NO_FILE);
            }

            ParentWindow.AddButton(SELECT, Select);
            ParentWindow.AddButton(DELETE, Delete);
            ParentWindow.AddButton(CLEAN, Clean);
        }

        private object LoadFile()
        {
            return AssetDatabase.LoadAssetAtPath(FullAssetPath, typeof(object));
        }

        public void Select()
        {
            CreateFolders();
            string selectedPath = EditorUtility.OpenFilePanel(EMPTY, EMPTY, Extension);
            FileUtil.CopyFileOrDirectory(selectedPath, FullAssetPath);
            AssetDatabase.Refresh();
            OnLoad.Invoke();
        }

        private void CreateFolders()
        {
            if (!AssetDatabase.IsValidFolder(FolderPath))
            {
                string[] split = ParentFolderName.Split(SLASH);
                string fullPath = ASSETS;
                for (int i = 0, count = split.Length; i < count; ++i)
                {
                    string folder = split[i];
                    if (!AssetDatabase.IsValidFolder(fullPath + SLASH + folder))
                    {
                        AssetDatabase.CreateFolder(fullPath, folder); 
                    }
                    fullPath += SLASH + folder;
                }
            }
        }

        public void Delete()
        {
            AssetDatabase.DeleteAsset(FullAssetPath);
        }

        public void Clean()
        {
            AssetDatabase.DeleteAsset(FolderPath);
        }
    }
}