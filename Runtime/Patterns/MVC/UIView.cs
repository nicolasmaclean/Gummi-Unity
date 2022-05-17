using UnityEngine;

namespace Gummi.Patterns.MVC
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