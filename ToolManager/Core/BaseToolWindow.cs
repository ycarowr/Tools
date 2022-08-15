using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ToolManager
{
    public abstract class BaseToolWindow : EditorWindow
    {
        public abstract string Id { get; }
        public abstract string Title { get; }

        public void Open()
        {
            Type type = GetType();
            EditorWindow window = GetWindow(type);
            titleContent = new GUIContent(Title);
            window.Show();
            Focus();
        }

        public void Space(int amount)
        {
            GUILayout.Space(amount);
        }

        public void AddButton(string label, Action callback)
        {
            if (GUILayout.Button(label))
            {
                callback?.Invoke();
            }
        }

        public void AddLabel(string label)
        {
            GUILayout.Label(label);
        }

        public bool AddToggle(string label, bool value)
        {
            return EditorGUILayout.Toggle(label, value);
        }

        public string AddTextArea(string label)
        {
            return EditorGUILayout.TextArea(label);
        }

        public void AddHelpBox(string label, MessageType type)
        {
            EditorGUILayout.HelpBox(label, type);
        }

        public Color AddColorField(string label, Color color)
        {
            return EditorGUILayout.ColorField(label, color);
        }

        public AnimationCurve AddAnimationCurveField(string label, AnimationCurve curve)
        {
            return EditorGUILayout.CurveField(label, curve);
        }

        public float AddFloatField(string label, float value)
        {
            return EditorGUILayout.FloatField(label, value);
        }

        public int AddIntField(string label, int value)
        {
            return EditorGUILayout.IntField(label, value);
        }

        public UnityEngine.Object AddObjectField(string label, UnityEngine.Object value, Type type)
        {
            return EditorGUILayout.ObjectField(label, value, type, false);
        }

        public Gradient AddGradientField(string label, Gradient value)
        {
            return EditorGUILayout.GradientField(label, value);
        }

        public void AddSeparator()
        {
            EditorGUILayout.Separator();
        }

        public bool AddFoldout(string label, bool foldout)
        {
            return EditorGUILayout.Foldout(foldout, label);
        }

        public FileWrapper AddFile(string parentFolderName, string fileName, string extension, Action onLoad)
        {
            return new FileWrapper(this, parentFolderName, fileName, extension, onLoad);
        }

        public int IdentRight()
        {
            return ++EditorGUI.indentLevel;
        }

        public int IdentLeft()
        {
            return --EditorGUI.indentLevel;
        }
    }
}