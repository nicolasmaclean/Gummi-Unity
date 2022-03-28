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
        public virtual void EngageController()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Disengage controller.
        /// </summary>
        public virtual void DisengageController()
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
        public override void EngageController()
        {
            base.EngageController();
            ui.ShowRoot();
        }

        /// <summary>
        /// Disengage controller.
        /// </summary>
        public override void DisengageController()
        {
            base.DisengageController();
            ui.HideRoot();
        }
    }
}