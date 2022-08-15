using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ToolManager
{
    public sealed class ToolManagerWindow : EditorWindow
    {
        private const string MENU_PATH_WINDOW = "Tools/ToolManager";
        private const string WINDOW_TITLE = "ToolManager";
        private static readonly GUIContent WINDOW_TITLE_CONTENT = new GUIContent(WINDOW_TITLE);
        private static readonly List<BaseToolWindow> REGISTRY = new List<BaseToolWindow>();


        [MenuItem(MENU_PATH_WINDOW)]
        public static void Open()
        {
            GetWindow<ToolManagerWindow>().Show();
        }

        private void OnGUI()
        {
            for (int i = 0, count = REGISTRY.Count; i < count; ++i)
            {
                BaseToolWindow tool = REGISTRY[i];
                if (GUILayout.Button(tool.Title))
                {
                    tool.Open();
                }
            }
        }

        private void OnEnable()
        {
            Initialize();
        }

        private void Initialize()
        {
            titleContent = WINDOW_TITLE_CONTENT;
            REGISTRY.Clear();
            List<Type> enabledWindowTypes = CollectValidWindowTypes();
            for (int i = 0, count = enabledWindowTypes.Count; i < count; ++i)
            {
                ScriptableObject instance = CreateInstance(enabledWindowTypes[i]);
                BaseToolWindow windowInstance = (BaseToolWindow)instance;
                if (windowInstance != null)
                {
                    REGISTRY.Add(windowInstance);
                }
            }
        }

        private static List<Type> CollectValidWindowTypes()
        {
            List<Type> enabledWindowTypes = new List<Type>();
            Type[] allTypes = CollectTypesFromAssembly();
            for (int i = 0, count = allTypes.Length; i < count; ++i)
            {
                Type type = allTypes[i];
                if (type.IsSubclassOf(typeof(BaseToolWindow)))
                {
                    ToolWindowAtt toolWindowAtt = type.GetCustomAttribute<ToolWindowAtt>();
                    if (toolWindowAtt == null || !toolWindowAtt.IsDisabled)
                    {
                        enabledWindowTypes.Add(type);
                    }
                }
            }
            return enabledWindowTypes;
        }

        private static Type[] CollectTypesFromAssembly()
        {
            return Assembly.GetAssembly(typeof(BaseToolWindow)).GetTypes();
        }
    }
}
