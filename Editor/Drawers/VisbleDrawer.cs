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
            if (!attr.Visible(property))
            {
                if (attr.AffectEnabled)
                {
                    // show disabled property
                    using (new EditorGUI.IndentLevelScope())
                    {
                        var previousGUIState = GUI.enabled;
                        GUI.enabled = false;
                        EditorGUI.PropertyField(position, property, label);
                        GUI.enabled = previousGUIState;
                    }
                }

                return;
            }

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
            return attr.AffectVisiblity && !attr.Visible(property) ? 0 : base.GetPropertyHeight(property, label);
        }
    }
}