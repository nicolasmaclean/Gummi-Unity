using System.Reflection;
using Gummi;
using UnityEngine;

namespace GummiEditor.Buttons
{
    public class ButtonWithoutParameters : ButtonBase
    {
        public ButtonWithoutParameters(MethodInfo method, ButtonAttribute buttonAttribute) : base(method, buttonAttribute) { }
        
        protected override void DrawInternal(object[] targets)
        {
            if (!GUILayout.Button(DisplayName)) return;

            foreach (object obj in targets)
            {
                Method.Invoke(obj, null);
            }
        }
    }
}