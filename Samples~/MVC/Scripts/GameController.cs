using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gummi.Pattern.MVC;

namespace Gummi.Samples.MVC
{
    public class GameController : SubController<GameState, GameView>
    {
        float gameTime = 0;

        public override void Engage()
        {
            gameTime = 0;
            ui.UpdateTime(0);

            ui.OnFinishClicked += FinishGame;
            ui.OnMenuClicked += GoToMenu;

            base.Engage();
        }

        public override void Disengage()
        {
            base.Disengage();

            ui.OnMenuClicked -= GoToMenu;
            ui.OnFinishClicked -= FinishGame;
        }

        void Update()
        {
            gameTime += Time.deltaTime;
            ui.UpdateTime(gameTime);
        }

        void FinishGame()
        {
            int gameScore = Mathf.CeilToInt(gameTime * Random.Range(0.0f, 10.0f));
            PlayerPrefs.SetInt("gameScore", gameScore);
            PlayerPrefs.SetFloat("gameTime", gameTime);

            root.ChangeController(GameState.GameOver);
        }

        void GoToMenu()
        {
            root.ChangeController(GameState.StartMenu);
        }
    }
}