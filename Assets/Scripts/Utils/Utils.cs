using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Security.Cryptography;
using System;

public static class Utils
{
    public static List<T> GetAllInstances<T>() where T : ScriptableObject
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
        List<T> a = new List<T>(guids.Length);
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a.Add(AssetDatabase.LoadAssetAtPath<T>(path));
        }

        return a;
    }
    
}
