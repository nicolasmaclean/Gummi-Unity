using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Gummi.Pattern.MVC;

namespace Gummi.Samples.MVC
{
    public class GameView : UIView
    {
        public UnityAction OnFinishClicked;
        public UnityAction OnMenuClicked;

        [SerializeField]
        private Text _txt_time;

        public void FinishClick()
        {
            OnFinishClicked?.Invoke();
        }

        public void MenuClicked()
        {
            OnMenuClicked?.Invoke();
        }

        public void UpdateTime(float time)
        {
            _txt_time.text = string.Format("{0:#00}:{1:00.000}", (int)(time / 60), (time % 60));
        }
    }
}