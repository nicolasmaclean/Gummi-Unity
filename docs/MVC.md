# Model, View, Controller (MVC) Base Classes

namespace: `Gummi.Pattern.MVC`

This space includes base classes used to follow a MVC control flow.

MVC relies on models to store/handle data, views to display data and recieve input, and controllers to perform core application logic and act as a middle man to models and views.

The MVC pattern is useful when an application has muliple 'states' or contexts that affect what the user sees or how their input is utilized. The separation of front-end/back-end and use of a single controller at any time encourages stronger encapsulation and greater maintainability.

# Common Architecture

- AppController
  - SubController
    - View
      - Visual 1
      - Visual 2
      - ...
  - SubController
    - ...
  - ...

The composition of a state begins with visuals presented to the user, which are grouped under a view. A view is a child of a SubController and a SubController of the AppController.

There may however many SubControllers or visual under a view as desired, but there should be a single AppController and a single View for each SubController.

# Example

There is a sample scene to help illustrate how the base classes could be utilized.

The steps I took to build the sample scene began with AppController.cs:

## App Controller

```C#
public enum GameState
{
    StartMenu, Game, GameOver
}

public class AppController : RootController<GameState>
{
    protected override SubController<GameState> GetController(GameState state)
    {
        throw new System.NotImplementedException();
    }
}
```

I defined an enum to includes a value for each state that the application may be in. In this specific example, the application has a start menu, game, and game over menu.

Next the AppController must derive from RootController with the enum of possible states. `AppController` will also need to implement the GetController method so that RootController can access any SubController of the app. We don't have any SubControllers yet, so we will throw an exception in the meantime.

## Start Menu

We must create a new SubController and a view of some sort for each new state that create for our app. This sample's visuals are created using purely unity's UI module, so we can use the `UIView` base class for our views.

```C#
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
```

The start menu for our application has two buttons that need to modify the state of our app, so `StartMenuView` contains methods that may be called when they are clicked. The view has no concern for what happens when a button is clicked, but simply lets anything else listening know that they have been clicked.

```C#
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
```

The `StartMenuController` modifies the application's state based on input taken through its `ui` which is a `StartMenuView` object. While this controller is active, it is listening to the `ui.OnPlayClicked` and `ui.OnQuitClicked` events. The `StartGame` method updates the `AppController`'s state to play the game and `QuitGame` quits the application.

```C#
public enum GameState
{
    StartMenu, Game, GameOver
}

public class AppController : RootController<GameState>
{
    [SerializeField]
    StartMenuController _startMenuController = null;

    protected override SubController<GameState> GetController(GameState state)
    {
        switch (state)
        {
            case GameState.StartMenu:
                return _startMenuController;

            case GameState.Game:
            case GameState.GameOver:
            default:
                throw new System.NotImplementedException();
        }
    }
}
```

At this point we can update the `AppController` to access the `StartMenu`'s SubController, but we still need to implement the other controllers before finishing the `AppController`.

I will not go through the rest of the scripts in the scene, but they follow the same pattern: derived SubController, derived UIView, update AppController.

While implementing `SubController`s, the `AppController` can function with partial access to controllers. It just needs access to the `_initialState`'s controller to work properly. The `AppController`, however, will not update its state if there is does not have access to a controller for a requested state. So With our above snippets of code, we could enter the Start Menu state, but trying to play the game would do nothing.

## Remarks

The PlayerPrefs in the `GameController` and `GameOverController` acts as the model in this example. If a custom data manager of some sort is used instead, like the credited blog post, that would be the model.

---

# class abstract RootController\<enum\>

A base class for a root controller of an app with `enum` being the possible states the app may be in.

`protected abstract SubController<T> GetController(T state)` must be implemented for `RootController` methods to access any `SubController`s corresponding to a value of `enum`. The example `AppController` utilizes serialized fields in Unity to store references to all `SubControllers` and connects `enum` values to the appropriate field.

A scene in Unity should probably have only one `RootController` instances.

# class abstract SubController\<enum\>

A base class for a bare-bones `SubController`, derived classes should handle their connection to a view and enable/disable it as the `SubController` is enabled/disabled.

SubController's should connect input processing logic to its view and manipulate the application's model's data.

# class abstract SubController\<enum, uiView\>

A base class build ontop of SubController\<enum\> that must have a connection to an instance of `uiView`, `ui`. The `ui` GameObject is enabled/disabled as the `SubController` is engaged/disengaged.

See SubController\<enum\> for more info.

# class UIView

Derived definitions may be used in the composition of SubController<enum, uiView> definition.

It should contain all logic necessary to render data and take user input necessary in this state.

---

## Credit

This code is heavily recycled from Patryk Galach's blog post [here](https://www.patrykgalach.com/2019/04/29/simple-mvc-for-unity/) and his bitbucket repo [here](https://bitbucket.org/gaello/simple-mvc/src/master/).

He has not published or mentioned the license for this code in the post or repo, but here is credit.
