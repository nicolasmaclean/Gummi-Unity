using UnityEditor;
using UnityEngine;
using Gummi;

namespace GummiEditor.Drawer
{
    [CustomPropertyDrawer(typeof(VisibleBaseAttribute), true)]
    public class VisbleDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            VisibleBaseAttribute attr = attribute as VisibleBaseAttribute;

            // skip drawing if not visible
            if (!attr.Visible(property)) return;

            // show property
            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // use default height if visible else 0
            VisibleBaseAttribute attr = attribute as VisibleBaseAttribute;
            return attr.Visible(property) ? base.GetPropertyHeight(property, label) : 0;
        }
    }
}