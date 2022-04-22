using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gummi
{
    /// <summary>
    /// The attached property will be hidden in the editor if <see cref="Target"/> is null.
    /// For greater maintainability of your code, use nameof() for a property's name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple=false, Inherited=true)]
    public class HideIfAttribute : VisibleBaseAttribute
    {
        public HideIfAttribute(string target) : base(target, toggleVisibility: true) { }

        public HideIfAttribute(string target, object benchmark) : base(target, benchmark, toggleVisibility: true) { }

#if UNITY_EDITOR
        public override bool Visible(SerializedProperty property)
        {
            // check if property is null
            SerializedObject obj = property.serializedObject;
            SerializedProperty prop = obj.FindProperty(Target);

            if (prop == null)
            {
                Debug.LogError($"Unable to find property, \"{Target}\". \"{property.name}\" will always be shown.");
                return true;
            }

            return !EqualToBenchmark(prop);
        }
#endif
    }
}