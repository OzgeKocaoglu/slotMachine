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
    private string itemName;
    private Vector3 initialLocalTransform;
    private GameObject blurChild;

    public Vector3 LocalTransform
    {
        get
        {
            return initialLocalTransform;
        }
    }
    private void Awake()
    {
        blurChild = transform.GetChild(0).gameObject;
        initialLocalTransform = this.transform.localPosition;
        itemName = typeName.ToString();
    }
    public bool Equals(string obj)
    {
        return this.itemName == obj;
    }
    public void ActivateBlur()
    {
        blurChild.SetActive(true);
    }
    public void DeactivateBlur()
    {
        blurChild.SetActive(false);
    }


}
