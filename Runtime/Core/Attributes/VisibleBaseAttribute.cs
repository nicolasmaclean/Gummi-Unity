using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gummi
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class VisibleBaseAttribute : PropertyAttribute
    {
        public string Target { get; protected set; }
        public object Benchmark { get; protected set; }
        public bool WasGivenBenchmark { get; protected set; } = false;

        public bool AffectEnabled { get; private set; }
        public bool AffectVisiblity { get; private set; }

        public VisibleBaseAttribute(string target, object benchmark, bool toggleEnabled=false, bool toggleVisibility = false) : this(target, toggleEnabled, toggleVisibility)
        {
            WasGivenBenchmark = true;
            Benchmark = benchmark;
        }

        public VisibleBaseAttribute(string target, bool toggleEnabled = false, bool toggleVisibility = false)
        {
            Target = target;
            AffectEnabled = toggleEnabled;
            AffectVisiblity = toggleVisibility;
        }

#if UNITY_EDITOR
        public abstract bool Visible(SerializedProperty property);

        /// <summary>
        /// Compares <paramref name="property"/> to <see cref="Target"/>'s value. Default comparisions are made if no benchmark was provided.
        /// </summary>
        /// <param name="property"></param>
        /// <returns> true if property's and target's values are equal </returns>
        public bool EqualToBenchmark(SerializedProperty property)
        {
            try
            {
                switch (property.propertyType)
                {
                    case SerializedPropertyType.Integer:
                    case SerializedPropertyType.Enum:
                    case SerializedPropertyType.LayerMask:
                        if (WasGivenBenchmark)
                        {
                            return property.intValue.Equals(Benchmark);
                        }

                        return property.intValue != 0;

                    case SerializedPropertyType.Boolean:
                        if (WasGivenBenchmark)
                        {
                            return property.boolValue.Equals(Benchmark);
                        }

                        return property.boolValue;

                    case SerializedPropertyType.Float:
                        if (WasGivenBenchmark)
                        {
                            return property.floatValue.Equals(Benchmark);
                        }

                        return property.floatValue != 0;

                    case SerializedPropertyType.String:
                        if (WasGivenBenchmark)
                        {
                            if (property.stringValue == null)
                            {
                                return Benchmark == null;
                            }

                            return property.stringValue.Equals(Benchmark);
                        }

                        return property.stringValue != null;

                    case SerializedPropertyType.Color:
                        if (WasGivenBenchmark)
                        {
                            if (Benchmark.GetType() != typeof(float[]) || ((float[])Benchmark).Length != 4)
                            {
                                Debug.LogError("Invalid Benchmark was provided to compare with color. Please provide a float[] of length 4.");
                                return true;
                            }

                            float[] arr = (float[])Benchmark;
                            Color bench = new Color(arr[0], arr[1], arr[2], arr[3]);
                            return property.colorValue.Equals(bench);
                        }

                        return property.colorValue != new Color(0, 0, 0, 0);

                    case SerializedPropertyType.Vector2:
                        if (WasGivenBenchmark)
                        {
                            if (Benchmark.GetType() != typeof(float[]) || ((float[])Benchmark).Length != 2)
                            {
                                Debug.LogError("Invalid Benchmark was provided to compare with vector. Please provide a float[] of length 2.");
                                return true;
                            }

                            float[] arr = (float[])Benchmark;
                            Vector2 bench = new Vector2(arr[0], arr[1]);
                            return property.vector2Value.Equals(bench);
                        }

                        return property.vector2Value != null;

                    case SerializedPropertyType.Vector3:
                        if (WasGivenBenchmark)
                        {
                            if (Benchmark.GetType() != typeof(float[]) || ((float[])Benchmark).Length != 3)
                            {
                                Debug.LogError("Invalid Benchmark was provided to compare with vector. Please provide a float[] of length 3.");
                                return true;
                            }

                            float[] arr = (float[])Benchmark;
                            Vector3 bench = new Vector3(arr[0], arr[1], arr[2]);
                            return property.vector3Value.Equals(bench);
                        }

                        return property.vector3Value != null;

                    case SerializedPropertyType.Vector4:
                        if (WasGivenBenchmark)
                        {
                            if (Benchmark.GetType() != typeof(float[]) || ((float[])Benchmark).Length != 4)
                            {
                                Debug.LogError("Invalid Benchmark was provided to compare with vector. Please provide a float[] of length 4.");
                                return true;
                            }

                            float[] arr = (float[])Benchmark;
                            Vector4 bench = new Vector4(arr[0], arr[1], arr[2], arr[3]);
                            return property.vector4Value.Equals(bench);
                        }

                        return property.vector4Value != null;

                    case SerializedPropertyType.Vector2Int:
                        if (WasGivenBenchmark)
                        {
                            if (Benchmark.GetType() != typeof(int[]) || ((int[])Benchmark).Length != 2)
                            {
                                Debug.LogError("Invalid Benchmark was provided to compare with vector. Please provide a int[] of length 2.");
                                return true;
                            }

                            int[] arr = (int[])Benchmark;
                            Vector2Int bench = new Vector2Int(arr[0], arr[1]);
                            return property.vector2IntValue.Equals(bench);
                        }

                        return property.vector2IntValue != null;

                    case SerializedPropertyType.Vector3Int:
                        if (WasGivenBenchmark)
                        {
                            if (Benchmark.GetType() != typeof(int[]) || ((int[])Benchmark).Length != 3)
                            {
                                Debug.LogError("Invalid Benchmark was provided to compare with vector. Please provide a int[] of length 3.");
                                return true;
                            }

                            int[] arr = (int[])Benchmark;
                            Vector3Int bench = new Vector3Int(arr[0], arr[1], arr[1]);
                            return property.vector3IntValue.Equals(bench);
                        }

                        return property.vector3IntValue != null;

                    case SerializedPropertyType.ObjectReference:
                        if (WasGivenBenchmark)
                        {
                            if (property.objectReferenceValue == null)
                            {
                                return Benchmark == null;
                            }

                            return property.objectReferenceValue.Equals(Benchmark);
                        }

                        return property.objectReferenceValue != null;

                    case SerializedPropertyType.ExposedReference:
                        if (WasGivenBenchmark)
                        {
                            if (property.exposedReferenceValue == null)
                            {
                                return Benchmark == null;
                            }

                            return property.exposedReferenceValue.Equals(Benchmark);
                        }

                        return property.objectReferenceValue != null;

                    default:
                        Debug.LogError($"{property.propertyType} is not supported by VisibleBaseAttribute and its derivative classes.");
                        return true;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"{property.name}: Target property does not contain a value for the requested Propertytype." + "\n" + e);
            }

            return true;
        }
#endif
    }
}