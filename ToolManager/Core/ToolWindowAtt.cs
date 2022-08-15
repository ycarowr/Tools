using System;

namespace ToolManager
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ToolWindowAtt : Attribute
    {
        public bool IsDisabled { get; }
        public ToolWindowAtt(bool isEnabled)
        {
            IsDisabled = isEnabled;
        }
    }
}