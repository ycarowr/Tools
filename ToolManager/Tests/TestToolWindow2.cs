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

            AddFile("Configs", "config", "json", () => { });
            AddFile("Configs/InnerConfigs", "config_0", "json", () => { });
            AddFile("Configs/InnerConfigs", "config_1", "json", () => { });
            AddFile("Configs/InnerConfigs/InnerInnerConfigs", "config", "json", () => { });
            
            AddFile("Textures", "image_0", "png", () => { });
            AddFile("Textures", "image_1", "png", () => { });
            AddFile("Textures", "image_2", "png", () => { });
        }
    }
}
