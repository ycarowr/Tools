namespace Tools
{
    public partial class DialogSystem
    {
        private abstract class DialogSubComponent
        {
            protected DialogSubComponent(IDialogSystem system)
            {
                DialogSystem = system;
            }

            public IDialogSystem DialogSystem { get; }
        }
    }
}