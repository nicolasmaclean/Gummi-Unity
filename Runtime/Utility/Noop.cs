using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gummi.Utility
{
    public class Noop : MonoBehaviour { }
    
    #if UNITY_EDITOR
    [CustomEditor(typeof(Noop))]
    public class NoopEditor : Editor
    {
        public override void OnInspectorGUI() { }
    }
    #endif
}