// Heavily recycled/adapted from Fydar's code on a Unity forum thread.
// https://forum.unity.com/threads/editor-tool-better-scriptableobject-inspector-editing.484393/

using System;
using System.Collections.Generic;
using Gummi;
using UnityEditor;
using UnityEngine;

namespace GummiEditor.Drawers
{
    [CustomPropertyDrawer(typeof(SOExposedAttribute), true)]
    public class SOExposedDrawer : PropertyDrawer
    {
        #region Style Setup
        enum BackgroundStyles
        {
            None,
            HelpBox,
            Darken,
            Lighten
        }

        /// <summary>
        /// The spacing on the inside of the background rect.
        /// </summary>
        const float INNER_SPACING = 6f;

        /// <summary>
        /// The spacing on the outside of the background rect.
        /// </summary>
        const float OUTER_SPACING = 4f;

        /// <summary>
        /// The style the background uses.
        /// </summary>
        static readonly BackgroundStyles BACKGROUND_STYLE = BackgroundStyles.HelpBox;

        /// <summary>
        /// The colour that is used to darken the background.
        /// </summary>
        static readonly Color DARKEN_COLOUR = new Color(0.0f, 0.0f, 0.0f, 0.2f);

        /// <summary>
        /// The colour that is used to lighten the background.
        /// </summary>
        static readonly Color LIGHTEN_COLOUR = new Color(1.0f, 1.0f, 1.0f, 0.2f);
        #endregion

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float totalHeight = EditorGUIUtility.singleLineHeight;

            // use default height
            if (property.objectReferenceValue == null || !property.isExpanded)
            {
                return totalHeight;
            }

            SerializedObject targetObject = new SerializedObject(property.objectReferenceValue);
            SerializedProperty field = targetObject.GetIterator();

            field.NextVisible(true);

            while (field.NextVisible(false))
            {
                totalHeight += EditorGUI.GetPropertyHeight(field, true) + EditorGUIUtility.standardVerticalSpacing;
            }

            totalHeight += INNER_SPACING * 2;
            totalHeight += OUTER_SPACING * 2;

            return totalHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // scriptable object source
            Rect fieldRect = new Rect(position);
            fieldRect.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(fieldRect, property, label, true);
            if (property.objectReferenceValue.IsNull()) return;

            // foldout
            property.isExpanded = EditorGUI.Foldout(fieldRect, property.isExpanded, GUIContent.none, true);
            if (!property.isExpanded) return;

            SerializedObject targetObject = new SerializedObject(property.objectReferenceValue);

            #region Format Field Rects
            List<Rect> propertyRects = new List<Rect>();
            Rect marchingRect = new Rect(fieldRect);

            Rect bodyRect = new Rect(fieldRect);
            bodyRect.xMin += EditorGUI.indentLevel * 14;
            bodyRect.yMin += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing + OUTER_SPACING;

            SerializedProperty field = targetObject.GetIterator();
            field.NextVisible(true);

            marchingRect.y += INNER_SPACING + OUTER_SPACING;

            while (field.NextVisible(false))
            {
                marchingRect.y += marchingRect.height + EditorGUIUtility.standardVerticalSpacing;
                marchingRect.height = EditorGUI.GetPropertyHeight(field, true);
                propertyRects.Add(marchingRect);
            }

            marchingRect.y += INNER_SPACING;
            bodyRect.yMax = marchingRect.yMax;
            #endregion

            DrawBackground(bodyRect);

            #region Draw Fields
            using (new EditorGUI.IndentLevelScope())
            {
                int index = 0;
                field = targetObject.GetIterator();
                field.NextVisible(true);

                //Replacement for "editor.OnInspectorGUI ();" so we have more control on how we draw the editor
                while (field.NextVisible(false))
                {
                    try
                    {
                        EditorGUI.PropertyField(propertyRects[index], field, true);
                    }
                    catch (StackOverflowException)
                    {
                        field.objectReferenceValue = null;
                        Debug.LogError("Detected self-nesting causing a StackOverflowException, avoid using " +
                                       "the same object inside a nested structure.");
                    }

                    index++;
                }

                targetObject.ApplyModifiedProperties();
            }
            #endregion
        }

        static void DrawBackground(Rect rect)
        {
            switch (BACKGROUND_STYLE)
            {
                case BackgroundStyles.HelpBox:
                    EditorGUI.HelpBox(rect, "", MessageType.None);
                    break;

                case BackgroundStyles.Darken:
                    EditorGUI.DrawRect(rect, DARKEN_COLOUR);
                    break;

                case BackgroundStyles.Lighten:
                    EditorGUI.DrawRect(rect, LIGHTEN_COLOUR);
                    break;
            }
        }
    }
}