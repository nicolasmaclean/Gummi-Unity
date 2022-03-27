using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gummi.Pattern.MVC
{
    /// <summary>
    /// Base class for SubControllers with reference to Root Controller.
    /// </summary>
    public abstract class SubController : MonoBehaviour
    {
        [HideInInspector]
        public RootController root;

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
    public abstract class SubController<T> : SubController where T : UIRoot
    {
        public T UI => ui;

        [SerializeField]
        protected T ui;

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