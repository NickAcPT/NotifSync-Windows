using System;

namespace NotifSync.UI.Router
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    sealed class TargetedStyleAttribute : Attribute
    {
        public TargetedStyleAttribute(string style)
        {
            Style = style;
        }

        public string Style { get; }
    }
}