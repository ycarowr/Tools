using ToolManager;

namespace ToolManagerTest
{
    public class TestToolWindow2 : BaseToolWindow
    {
        private const string NAME = "TestToolWindow2";
        public override string Id => NAME;
        public override string Title => NAME;
        private void OnGUI()
        {
            AddButton("button 1", () => { });
            AddLabel("label 1");
            AddButton("button 2", () => { });
            AddButton("button 3", () => { });
        }
    }
}
