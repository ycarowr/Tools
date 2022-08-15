using ToolManager;
using UnityEngine;

namespace ToolManagerTest
{
    public class TestToolWindow1 : BaseToolWindow
    {
        private const string NAME = "TestToolWindow1";
        public override string Id => NAME;
        public override string Title => NAME;
        private bool m_Toogle;
        private string m_TextArea = "text area";
        private Color m_Color = Color.white;
        private AnimationCurve m_Curve = new AnimationCurve();
        private float m_Float;
        private int m_Int;
        private Object m_Object;
        private Gradient m_Gradient = new Gradient();
        private bool m_FoldOut;

        private void OnGUI()
        {
            AddButton("button 1", () => { });
            AddLabel("label 1");
            AddButton("button 2", () => { });
            AddHelpBox("help 1", UnityEditor.MessageType.None);
            AddHelpBox("help 2", UnityEditor.MessageType.Info);
            AddHelpBox("help 3", UnityEditor.MessageType.Warning);
            AddHelpBox("help 4", UnityEditor.MessageType.Error);

            IdentRight();
            m_Toogle = AddToggle("toggle 1", m_Toogle);
            m_FoldOut = AddFoldout("foldout 1", m_FoldOut);
            m_TextArea = AddTextArea(m_TextArea);
            m_Color = AddColorField("color 1", m_Color);
            IdentRight();
            m_Curve = AddAnimationCurveField("curve 1", m_Curve);
            m_Float = AddFloatField("float 1", m_Float);
            IdentLeft();
            m_Int = AddIntField("int 1", m_Int);
            m_Object = AddObjectField("", m_Object, typeof(Object));
            IdentLeft();
            m_Gradient = AddGradientField("gradient 1", m_Gradient);

        }
    }
}
