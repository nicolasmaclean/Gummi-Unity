using UnityEditor;
using Object = UnityEngine.Object;

namespace GummiEditor.Buttons
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Object), editorForChildClasses: true)]
    internal class ButtonEditor : Editor
    {
        ButtonsDrawer _drawer;

        void OnEnable()
        {
            _drawer = new ButtonsDrawer(target);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            _drawer.DrawButtons(targets);
        }
    }
}