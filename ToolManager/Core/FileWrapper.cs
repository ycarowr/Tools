using System;
using UnityEditor;

namespace ToolManager
{
    public sealed class FileWrapper
    {
        private const string ASSETS = "Assets";
        private const string SELECT = "Select";
        private const string DELETE = "Delete";
        private const string REMOVE_FOLDER = "Remove Folder";
        private const string FILE = "File: ";
        private const string NO_FILE = "Empty";
        private const string EMPTY = "";
        private const char SLASH = '/';

        private readonly string ParentFolderName;
        private readonly string Extension;
        private readonly Action OnLoad;
        private readonly string FullAssetPath;
        private readonly string FolderPath;

        public FileWrapper(BaseToolWindow window, string parentFolderName, string fileName, string extension, Action onLoad)
        {
            ParentFolderName = parentFolderName;
            OnLoad = onLoad;
            Extension = extension;
            FullAssetPath = $"{ASSETS}/{ParentFolderName}/{fileName}.{Extension}";
            FolderPath = $"{ASSETS}/{ParentFolderName}";

            bool isValid = LoadFile() != null;
            if (isValid)
            {
                window.AddLabel(FILE + FullAssetPath);
            }
            else
            {
                window.AddLabel(FILE + NO_FILE);
            }

            window.AddButton(SELECT, SelectFile);
            window.AddButton(DELETE, DeleteFile);
            window.AddButton(REMOVE_FOLDER, RemoveFolder);
        }

        public void SelectFile()
        {
            CreateFolders();
            string selectedPath = EditorUtility.OpenFilePanel(EMPTY, EMPTY, Extension);
            FileUtil.CopyFileOrDirectory(selectedPath, FullAssetPath);
            AssetDatabase.Refresh();
            OnLoad.Invoke();
        }

        public void DeleteFile()
        {
            AssetDatabase.DeleteAsset(FullAssetPath);
        }

        public void RemoveFolder()
        {
            AssetDatabase.DeleteAsset(FolderPath);
        }

        private object LoadFile()
        {
            return AssetDatabase.LoadAssetAtPath(FullAssetPath, typeof(object));
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
    }
}