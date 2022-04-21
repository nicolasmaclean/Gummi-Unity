using System;
using UnityEngine;

namespace Gummi
{
    /// <summary>
    /// The attached property will be hidden in the editor if <see cref="Target"/> is null.
    /// For greater maintainability of your code, use nameof() for a property's name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple=false, Inherited=true)]
    public class HideIfNotNullAttribute : PropertyAttribute
    {
        public readonly string Target;

        public HideIfNotNullAttribute(string target) => Target = target;
    }
}