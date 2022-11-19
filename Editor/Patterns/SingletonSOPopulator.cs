using System.Collections.Generic;
using System.Linq;
using Gummi.Patterns;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;
using Type = System.Type;
using Assembly = System.Reflection.Assembly;

namespace GummiEditor.Patterns
{
    [InitializeOnLoad]
    public static class SingletonSOPopulator
    {
        static SingletonSOPopulator()
        {
            Populate();
        }

        static void Populate()
        {
            Assembly assembly = GetAssemblyByName("Game");
            // exit, there is no Game assembly
            if (assembly == null) return;
            
            foreach (var type in FindSubClassesOf<SingletonSOBase>(assembly))
            {
                Object[] instances = Resources.LoadAll("", type);
                if (instances.Length > 0) continue;

                CreateAssetInResources(type);
            }
        }
        
        static Assembly GetAssemblyByName(string name)
        {
            return System.AppDomain.CurrentDomain.GetAssemblies().
                SingleOrDefault(assembly => assembly.GetName().Name == name);
        }

        static void CreateAssetInResources(Type type)
        {
            // create resources folder if not already
            const string resources = "Assets/Resources";
            if (!AssetDatabase.IsValidFolder(resources))
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
            }
                
            string fileName = ObjectNames.NicifyVariableName(type.Name);
            var obj = ScriptableObject.CreateInstance(type);
            AssetDatabase.CreateAsset(obj, $"{resources}/{ fileName }.asset");
            
            Debug.Log($"Created Scriptable Object Singleton '{ fileName }' in the Resources folder.");
        }

        static IEnumerable<Type> FindSubClassesOf<T>(Assembly assembly)
        {
            var baseType = typeof(T);

            return from t in assembly. GetTypes()
                where t.IsClass && !t.IsAbstract
                where t.IsSubclassOf(baseType)
                select t;
        }
    }
}
