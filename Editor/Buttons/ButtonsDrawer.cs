using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Gummi;

namespace GummiEditor.Buttons
{
    public class ButtonsDrawer
    {
        readonly List<ButtonBase> _buttons = new List<ButtonBase>();

        public ButtonsDrawer(object target)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            MethodInfo[] methods = target.GetType().GetMethods(flags);

            foreach (var method in methods)
            {
                ButtonAttribute buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();

                if (buttonAttribute == null) continue;

                ButtonBase button = ButtonBase.Create(method, buttonAttribute);
                if (button == null) continue;
                
                _buttons.Add(button);
            }
        }

        public void DrawButtons(IEnumerable<object> targets)
        {
            object[] enumerated = targets.ToArray();
            foreach (ButtonBase button in _buttons)
            {
                button.Draw(enumerated);
            }
        }
    }
}