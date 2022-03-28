using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gummi.Pattern.MVC;

namespace Gummi.Samples.MVC
{
    public class AppController : RootController<GameState>
    {
        [SerializeField]
        StartMenuController _startMenuController = null;

        [SerializeField]
        GameController _gameController = null;

        [SerializeField]
        GameOverController _gameOverController = null;

        protected override SubController<GameState> GetController(GameState state)
        {
            switch (state)
            {
                case GameState.StartMenu:
                    return _startMenuController;

                case GameState.Game:
                    return _gameController;

                case GameState.GameOver:
                    return _gameOverController;

                default:
                    return null;
            }
        }
    }

    public enum GameState
    {
        StartMenu = 0, Game = 1, GameOver = 2
    }
}