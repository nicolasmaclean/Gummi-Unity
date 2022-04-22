using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Gummi.Pattern.MVC;

namespace Gummi.Samples.MVC
{
    public class GameOverView : UIView
    {
        public UnityAction OnReplayClicked;
        public UnityAction OnMenuClicked;

        [SerializeField]
        Text _txt_score;

        [SerializeField]
        Text _txt_time;

        public void ReplayClick()
        {
            OnReplayClicked?.Invoke();
        }

        public void MenuClicked()
        {
            OnMenuClicked?.Invoke();
        }

        public void UpdateScore(int score)
        {
            _txt_score.text = score.ToString();
        }

        public void UpdateTime(float time)
        {
            _txt_time.text = string.Format("{0:#00}:{1:00.000}", (int)(time / 60), (time % 60));
        }
    }
}