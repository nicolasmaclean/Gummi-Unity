using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gummi.Utility
{
    public class MDDocumentation : MonoBehaviour
    {
        [SerializeField, TextArea(15, 15)]
        [Tooltip("Supports levels 1, 2, and 3 markdown headers along with plain text. Click the button below to render it!")]
        internal string description = string.Empty;
    }
}