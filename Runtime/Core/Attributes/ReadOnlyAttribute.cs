// https://www.patrykgalach.com/2020/01/20/readonly-attribute-in-unity-editor/

using System;
using UnityEngine;

namespace Gummi
{
    /// <summary>
    /// Attribute disables the ability to edit properties and fields editor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ReadOnlyAttribute : PropertyAttribute { }
}