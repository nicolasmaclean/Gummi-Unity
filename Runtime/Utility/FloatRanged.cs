using UnityEngine;

namespace Gummi.Utility
{
    [System.Serializable]
    public struct RangedFloat
    {
        public float Min;
        public float Max;

        public RangedFloat(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float Random() => UnityEngine.Random.Range(Min, Max);
        
        public static implicit operator RangedFloat(Vector2 vector)
        {
            return new RangedFloat(vector.x, vector.y);
        }

        public static implicit operator Vector2(RangedFloat range)
        {
            return new Vector2(range.Min, range.Max);
        }
    }
}