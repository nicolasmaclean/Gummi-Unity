using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gummi.Pattern.MVC
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"> Enum to map SubControllers to their use. </typeparam>
    public abstract class RootController<T> : RootController where T : System.Enum
    {
        public T CurrentState;

        [Header("Controllers")]
        [SerializeField]
        T _initialState;

        void Start()
        {
            SubController controller;

            // provide SubControllers a reference to this
            foreach (T state in System.Enum.GetValues(typeof(T)))
            {
                controller = GetController(state);

                if (controller == null)
                {
                    Debug.LogWarning($"{name}: missing controller for {nameof(state)}. If this.ChangeController " +
                        $"attempts to engage this controller, the currently active controller will not be disengaged.");
                }
                else
                {
                    controller.root = this;
                }
            }

            // activate initial controller
            controller = GetController(_initialState);
            if (controller == null)
            {
                Debug.LogError($"{name}: missing controller for the initial state, {nameof(_initialState)}. This component will be disabled.");
                this.enabled = true;
                return;
            }

            ChangeController(_initialState);
        }

        /// <summary>
        /// Access a SubController.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected abstract SubController GetController(T state);

        /// <summary>
        /// Method used by subcontrollers to change game phase.
        /// </summary>
        /// <param name="state">Controller type.</param>
        public void ChangeController(T state)
        {
            if (!this.enabled)
            {
                Debug.LogWarning($"{name}: this component is disabled, ChangeController will do nothing.");
                return;
            }

            SubController controller = GetController(state);

            if (controller == null)
            {
                Debug.LogError($"{this.GetType().Name}: missing controller for {nameof(state)}. Root will remain in its current state, {CurrentState}.");
            }
            else
            {
                return;
            }

            // Reseting subcontrollers.
            DisengageControllers();

            // activate requested
            controller.EngageController();

            CurrentState = state;
        }

        /// <summary>
        /// Disables all attached subcontrollers.
        /// </summary>
        public void DisengageControllers()
        {
            if (!this.enabled)
            {
                Debug.LogWarning($"{name}: this component is disabled, DisengageControllers will do nothing.");
                return;
            }

            foreach (T controllerEnum in System.Enum.GetValues(typeof(T)))
            {
                SubController controller = GetController(controllerEnum);
                controller.DisengageController();
            }
        }
    }

    /// <summary>
    /// Only <see cref="RootController{T}"/> should ever inherit from this.
    /// Classes may refer to this to remove the need to specify T.
    /// </summary>
    public abstract class RootController : MonoBehaviour { }
}