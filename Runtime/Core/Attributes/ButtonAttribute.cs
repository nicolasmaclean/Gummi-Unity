// This heavily recycled and simplified from Brandon Coffey
// From this specific project: https://github.com/utd-sgdp/SGDP-2023/tree/main/Assets/_Game/Scripts/Editor/Attributes/Buttons

using System;

namespace Gummi
{
    [AttributeUsage(validOn: AttributeTargets.Method)]
    public sealed class ButtonAttribute : Attribute
    {
        public string Label = string.Empty;
        public PlayModeMask Mode = PlayModeMask.Always;
        public int Spacing = 0;
    }
}
