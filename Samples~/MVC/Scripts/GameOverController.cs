using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gummi.Pattern.MVC;

namespace Gummi.Samples.MVC
{
    public class GameOverController : SubController<GameState, GameOverView>
    {
        public override void Engage()
        {
            int gameScore = 0;
            if (PlayerPrefs.HasKey("gameScore"))
            {
                gameScore = PlayerPrefs.GetInt("gameScore");
            }
            else
            {
                Debug.LogWarning($"{name}: PlayerPrefs does not have data for gameScore, defaulting to 0.");
            }

            float gameTime = 0;
            if (PlayerPrefs.HasKey("gameTime"))
            {
                gameTime = PlayerPrefs.GetFloat("gameTime");
            }
            else
            {
                Debug.LogWarning($"{name}: PlayerPrefs does not have data for gameTime, defaulting to 0.");
            }

            ui.UpdateScore(gameScore);
            ui.UpdateTime(gameTime);

            ui.OnReplayClicked += ReplayGame;
            ui.OnMenuClicked += GoToMenu;

            base.Engage();
        }

        public override void Disengage()
        {
            base.Disengage();

            ui.OnMenuClicked -= GoToMenu;
            ui.OnReplayClicked -= ReplayGame;
        }

        void ReplayGame()
        {
            root.ChangeController(GameState.Game);
        }

        void GoToMenu()
        {
            root.ChangeController(GameState.StartMenu);
        }
    }
}