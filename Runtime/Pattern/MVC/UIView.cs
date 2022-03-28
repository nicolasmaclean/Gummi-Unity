using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gummi.Pattern.MVC
{
    public class UIView : MonoBehaviour
    {
        /// <summary>
        /// Show UI
        /// </summary>
        public virtual void ShowRoot()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hide UI
        /// </summary>
        public virtual void HideRoot()
        {
            gameObject.SetActive(false);
        }
    }
}