using ToolManager;

namespace ToolManagerTest
{
    [ToolWindowAtt(true)]
    public class TestToolWindow3 : BaseToolWindow
    {
        private const string NAME = "TestToolWindow3";
        public override string Id => NAME;
        public override string Title => NAME;

        private void OnGUI()
        {
            AddLabel("window 3");
        }
    }
}
