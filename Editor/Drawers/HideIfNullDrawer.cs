using UnityEngine;
using UnityEditor;
using Gummi;

namespace GummiEditor.Drawer
{
    [CustomPropertyDrawer(typeof(HideIfNullAttribute))]
    public class HideIfNullDrawer : PropertyDrawer
    {
        bool visible = true;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedObject obj = property.serializedObject;
            HideIfNullAttribute attr = attribute as HideIfNullAttribute;

            // check if property is null
            SerializedProperty prop = obj.FindProperty(attr.Target);
            visible = prop == null || !(prop.objectReferenceValue == null);

            if (prop == null)
            {
                Debug.LogError($"Unable to find property, \"{attr.Target}\". \"{property.name}\" will always be shown.");
            }

            // show property
            if (visible)
            {
                EditorGUI.indentLevel++;
                EditorGUI.PropertyField(position, property, label);
                EditorGUI.indentLevel--;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return visible ? base.GetPropertyHeight(property, label) : 0;
        }
    }
}