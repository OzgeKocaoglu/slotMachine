using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ItemType
{
    A,
    Seven,
    Jackpot,
    Wild,
    Bonus
}

public class ItemView : MonoBehaviour
{

    [SerializeField] private ItemType typeName;
    public string itemName;
    private Vector3 initialLocalTransform;

    public Vector3 LocalTransform
    {
        get
        {
            return initialLocalTransform;
        }
    }

    private void Awake()
    {
        initialLocalTransform = this.transform.localPosition;
        itemName = typeName.ToString();
    }

    public bool Equals(string obj)
    {
        return this.itemName == obj;
    }


}
