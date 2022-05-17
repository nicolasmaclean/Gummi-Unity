using System;
using UnityEngine;

namespace Gummi.Patterns.MVC
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"> Enum to map SubControllers to their use. </typeparam>
    public abstract class RootController<T> : MonoBehaviour
        where T : Enum
    {
        // TODO: make this readonly to the editor
        public T CurrentState;

        [Header("Controllers")]
        [SerializeField]
        T _initialState = default(T);

        void Start()
        {
            SubController<T> controller;

            // provide SubControllers a reference to this
            foreach (T state in Enum.GetValues(typeof(T)))
            {
                controller = GetController(state);

                if (controller == null)
                {
                    Debug.LogWarning($"{name}: missing controller for {Enum.GetName(typeof(T), state)}. If this.ChangeController " +
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
                this.enabled = false;
                return;
            }

            ChangeController(_initialState);
        }

        /// <summary>
        /// Access a SubController.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected abstract SubController<T> GetController(T state);

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

            SubController<T> controller = GetController(state);

            if (controller == null)
            {
                Debug.LogError($"{this.GetType().Name}: missing controller for {nameof(state)}. Root will remain in its current state, {CurrentState}.");
                return;
            }

            // reseting subcontrollers
            DisengageControllers();

            // activate requested controller
            controller.Engage();
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

            foreach (T controllerEnum in Enum.GetValues(typeof(T)))
            {
                SubController<T> controller = GetController(controllerEnum);
                controller?.Disengage();
            }
        }
    }
}