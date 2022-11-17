// from https://gist.github.com/fguillen/a929a1d003a20bc727d8efe228b5dda4
// which is from https://www.youtube.com/watch?v=6kWUGEQiMUI&ab_channel=whateep
// ReSharper disable StaticMemberInGenericType

using System;
using System.IO;
using Gummi.Patterns;
using UnityEngine;

/// <summary>
/// ScriptableObject Singleton base class that provides lazy-loading from local instances or /Assets/Resources.
/// Saving must be manually called with <see cref="Save"/>.
/// </summary>
/// <typeparam name="T"> The ScriptableObject type deriving from this class. </typeparam>
public abstract class SingletonSO<T> : SingletonSOBase where T : SingletonSO<T>
{
    /// <summary>
    /// Singleton with built-in save functionality. Default data is stored in /Assets/Resources/. Player's data
    /// is stored using <see cref="Application.persistentDataPath"/>.
    /// </summary>
    /// <exception cref="System.Exception"> Unable to find Scriptable Object Singleton. </exception>
    public static T Instance
    {
        get
        {
            if (!_instance)
            {
                Load();
            }
            
            return _instance;
        }
    }
    static T _instance;

    public static void Load()
    {
        _instance = ReadDataFromResources();
    }
    
    /// <summary>
    /// Attempts to read default data from /Assets/Resources/.
    /// </summary>
    /// <returns> Object representation of data. </returns>
    /// <exception cref="Exception"> There is more or less than 1 instance in /Assets/Resources/. </exception>
    static T ReadDataFromResources()
    {
        Debug.Log("loading");
        // search resources
        T[] assets = Resources.LoadAll<T>("");

        // validation
        if(assets == null || assets.Length < 1)
        {
            throw new Exception($"Did not find Singleton Scriptable Object of type: { typeof(T).Name }");
        }
        if (assets.Length > 1)
        {
            throw new Exception($"More than 1 instance of Singleton Scriptable Object of type: { typeof(T).Name } found");
        }

        return assets[0];
    }
}