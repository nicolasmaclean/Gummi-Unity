using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gummi.Pattern.MVC;
using UnityEngine.Events;

namespace Gummi.Samples.MVC
{
    public class StartMenuView : UIView
    {
        public UnityAction OnPlayClicked;
        public UnityAction OnQuitClicked;

        public void PlayClicked()
        {
            OnPlayClicked?.Invoke();
        }

        public void QuitClicked()
        {
            OnQuitClicked?.Invoke();
        }
    }
}