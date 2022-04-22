using UnityEngine;
using UnityEditor;
using Gummi;

namespace GummiEditor.Drawer
{
    [CustomPropertyDrawer(typeof(HideIfNotNullAttribute))]
    public class HideIfNotNullDrawer : PropertyDrawer
    {
        bool visible = true;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedObject obj = property.serializedObject;
            HideIfNotNullAttribute attr = attribute as HideIfNotNullAttribute;


            // check if property is null
            SerializedProperty prop = obj.FindProperty(attr.Target);
            visible = prop == null || prop.objectReferenceValue == null;

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