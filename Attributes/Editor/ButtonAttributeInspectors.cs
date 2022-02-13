using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace YWR.Tools
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Object), true)]
    public class ButtonAttributeInspectors : UnityEditor.Editor
    {
        MethodInfo[] Methods => target.GetType()
            .GetMethods(BindingFlags.Instance |
                        BindingFlags.Static |
                        BindingFlags.NonPublic |
                        BindingFlags.Public);

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DrawMethods();
        }

        void DrawMethods()
        {
            if (Methods.Length < 1)
                return;

            foreach (MethodInfo method in Methods)
            {
                ButtonAttribute buttonAttribute = (ButtonAttribute) method
                    .GetCustomAttribute(typeof(ButtonAttribute));

                if (buttonAttribute != null)
                    DrawButton(buttonAttribute, method);
            }
        }

        public void DrawButton(ButtonAttribute buttonAttribute, MethodInfo method)
        {
            string label = buttonAttribute.Label ?? method.Name;

            if (GUILayout.Button(label))
                method.Invoke(target, null);
        }
    }
}