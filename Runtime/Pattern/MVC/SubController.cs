using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gummi.Pattern.MVC
{
    /// <summary>
    /// Base class for SubControllers with reference to Root Controller.
    /// </summary>
    public abstract class SubController<TAppState> : MonoBehaviour
        where TAppState : System.Enum
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
        where TAppState : System.Enum
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