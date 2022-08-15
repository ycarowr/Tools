using System;
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
            titleContent = new GUIContent(Title);
            Type type = GetType();
            EditorWindow window = GetWindow(type);
            window.Show();
            Focus();
        }

        protected void Space(int amount)
        {
            GUILayout.Space(amount);
        }

        protected void AddButton(string label, Action callback)
        {
            if (GUILayout.Button(label))
            {
                callback?.Invoke();
            }
        }

        protected void AddLabel(string label)
        {
            GUILayout.Label(label);
        }

        protected bool AddToggle(string label, bool value)
        {
            return EditorGUILayout.Toggle(label, value);
        }

        protected string AddTextArea(string label)
        {
            return EditorGUILayout.TextArea(label);
        }

        protected void AddHelpBox(string label, MessageType type)
        {
            EditorGUILayout.HelpBox(label, type);
        }

        protected Color AddColorField(string label, Color color)
        {
            return EditorGUILayout.ColorField(label, color);
        }

        protected AnimationCurve AddAnimationCurveField(string label, AnimationCurve curve)
        {
            return EditorGUILayout.CurveField(label, curve);
        }

        protected float AddFloatField(string label, float value)
        {
            return EditorGUILayout.FloatField(label, value);
        }

        protected int AddIntField(string label, int value)
        {
            return EditorGUILayout.IntField(label, value);
        }

        protected UnityEngine.Object AddObjectField(string label, UnityEngine.Object value, Type type)
        {
            return EditorGUILayout.ObjectField(label, value, type, false);
        }

        protected Gradient AddGradientField(string label, Gradient value)
        {
            return EditorGUILayout.GradientField(label, value);
        }

        protected void AddSeparator()
        {
            EditorGUILayout.Separator();
        }

        protected bool AddFoldout(string label, bool foldout)
        {
            return EditorGUILayout.Foldout(foldout, label);
        }

        protected int IdentRight()
        {
            return ++EditorGUI.indentLevel;
        }

        protected int IdentLeft()
        {
            return --EditorGUI.indentLevel;
        }
    }
}