using UnityEngine;

namespace Gummi.Utility
{
    [System.Serializable]
    public struct FloatSquared
    {
        public float Value => _value;
        public float ValueSQ => _valueSQ;
        
        [SerializeField, Min(0)] internal float _value;
        [SerializeField] internal float _valueSQ;

        public FloatSquared(float value)
        {
            _value = value;
            _valueSQ = value * value;
        }
    }
}