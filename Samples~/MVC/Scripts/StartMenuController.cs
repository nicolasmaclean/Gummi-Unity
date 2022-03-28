using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gummi.Pattern.MVC;

namespace Gummi.Samples.MVC
{
    public class StartMenuController : SubController<GameState, StartMenuView>
    {
        public override void Engage()
        {
            base.Engage();

            ui.OnPlayClicked += StartGame;
            ui.OnQuitClicked += QuitGame;
        }

        public override void Disengage()
        {
            base.Disengage();

            ui.OnPlayClicked -= StartGame;
            ui.OnQuitClicked -= QuitGame;
        }

        void StartGame()
        {
            root.ChangeController(GameState.Game);
        }

        void QuitGame()
        {
            Debug.Log("Quitting Game");
            Application.Quit();
        }
    }
}