using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gummi.Pattern.MVC
{
    /// <summary>
    /// Base class for every UI element and view.
    /// </summary>
    public class UIView : MonoBehaviour
    {
        /// <summary>
        /// Show view or element
        /// </summary>
        public virtual void ShowView()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hide view or element
        /// </summary>
        public virtual void HideView()
        {
            gameObject.SetActive(false);
        }
    }
}