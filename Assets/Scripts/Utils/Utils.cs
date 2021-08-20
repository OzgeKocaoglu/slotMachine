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


public static void Shuffle<T>(this IList<T> list)
{
    RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
    int n = list.Count;
    while (n > 1)
    {
        byte[] box = new byte[1];
        do provider.GetBytes(box);
        while (!(box[0] < n * (Byte.MaxValue / n)));
        int k = (box[0] % n);
        n--;
        T value = list[k];
        list[k] = list[n];
        list[n] = value;
    }
}
    public class Item<T>
    {
        public T value;
        public float weight;

        public static float GetTotalWeight<T>(Item<T>[] p_itens)
        {
            float __toReturn = 0;
            foreach (var item in p_itens)
            {
                __toReturn += item.weight;
            }

            return __toReturn;
        }
    }

    
}
