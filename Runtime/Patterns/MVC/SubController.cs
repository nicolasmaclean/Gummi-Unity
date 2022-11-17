using System;
using UnityEngine;

namespace Gummi.Patterns
{
    /// <summary>
    /// Base class for SubControllers with reference to Root Controller.
    /// </summary>
    public abstract class SubController<TAppState> : MonoBehaviour
        where TAppState : Enum
    {
        [HideInInspector]
        public RootController<TAppState> root;

        /// <summary>
        /// Engage controller.
        /// </summary>
        public virtual void Engage()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Disengage controller.
        /// </summary>
        public virtual void Disengage()
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Extending SubController class with generic reference UI Root.
    /// </summary>
    public abstract class SubController<TAppState, TUIView> : SubController<TAppState>
        where TAppState : Enum
        where TUIView : UIView
    {
        public TUIView UI => ui;

        [SerializeField]
        protected TUIView ui;

        /// <summary>
        /// Engage controller.
        /// </summary>
        public override void Engage()
        {
            base.Engage();
            ui?.ShowRoot();

            if (ui == null)
            {
                Debug.LogError($"{name}: missing UI reference.");
            }
        }

        /// <summary>
        /// Disengage controller.
        /// </summary>
        public override void Disengage()
        {
            base.Disengage();
            ui?.HideRoot();
        }
    }
}