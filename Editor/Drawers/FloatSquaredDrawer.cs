using UnityEditor;
using UnityEngine;
using Gummi.Utility;

namespace GummiEditor.Drawers
{
    [CustomPropertyDrawer(typeof(FloatSquared))]
    public class FloatSquaredDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative(nameof(FloatSquared._value));
            return EditorGUI.GetPropertyHeight(valueProperty);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative(nameof(FloatSquared._value));

            EditorGUI.BeginChangeCheck();
            
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(position, valueProperty, label, true);
            EditorGUI.EndProperty();

            if (EditorGUI.EndChangeCheck())
            {
                float value = valueProperty.floatValue;
                var valueSQProperty = property.FindPropertyRelative(nameof(FloatSquared._valueSQ));
                valueSQProperty.floatValue = value * value;
            }
        }
    }
}