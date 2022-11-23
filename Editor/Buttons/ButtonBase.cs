using System.Reflection;
using Gummi;
using UnityEditor;
using UnityEngine;

namespace GummiEditor.Buttons
{
    public abstract class ButtonBase
    {
        protected readonly MethodInfo Method;
        protected readonly string DisplayName;
        
        readonly bool _disabled;
        readonly int _spacing;

        internal static ButtonBase Create(MethodInfo method, ButtonAttribute buttonAttribute)
        {
            var parameters = method.GetParameters();
            if (parameters.Length > 0)
            {
                Debug.LogError($"{ method.Name } has parameters, but ButtonAttribute does not support methods with parameters.");
                return null;
            }
            
            return new ButtonWithoutParameters(method, buttonAttribute);
        }

        protected ButtonBase(MethodInfo method, ButtonAttribute buttonAttribute)
        {
            Method = method;
            DisplayName = string.IsNullOrEmpty(buttonAttribute.Label) ? ObjectNames.NicifyVariableName(method.Name) : buttonAttribute.Label;

            _spacing = buttonAttribute.Spacing;
            _disabled = buttonAttribute.Mode switch
            {
                PlayModeMask.Always => false,
                PlayModeMask.InPlayMode => !EditorApplication.isPlaying,
                PlayModeMask.NotInPlayMode => EditorApplication.isPlaying,
                _ => true,
            };
        }

        public void Draw(object[] targets)
        {
            if (_spacing > 0) GUILayout.Space(_spacing);
            
            EditorGUI.BeginDisabledGroup(_disabled);
            
            var backgroundColor = GUI.backgroundColor;
            
            DrawInternal(targets);
            
            GUI.backgroundColor = backgroundColor;
            
            EditorGUI.EndDisabledGroup();
        }

        protected abstract void DrawInternal(object[] targets);
    }
}