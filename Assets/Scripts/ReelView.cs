using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ReelView : MonoBehaviour
{
    public delegate void ReelHandler(int id);
    public static ReelHandler On_ReelViewSpinning;
    [SerializeField] private int id;
    private Vector3 positionOffSet;
    [SerializeField] private float spacing;
    [SerializeField] private float spinTime;
    [SerializeField] private List<GameObject> items;
    private int spinCount;
    private int bottom, top;


    private void Awake()
    {
        On_ReelViewSpinning += ReelSpin;
        positionOffSet = this.transform.position;
        bottom = (int)-spacing * ((items.Count / 2) -1);
        top = (int)spacing * ((items.Count / 2) - 1);
        spinCount = 0;
    }

    private void OnDestroy()
    {
        On_ReelViewSpinning -= ReelSpin;
    }


    private void ReelSpin(int id)
    {
        if(this.id == id)
        {
            InvokeRepeating("Spinning", id * 0.5f, 0.3f);

        }
    }

    void Spinning()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.DOMoveY(items[i].transform.position.y - spacing, 0.3f);
        }

    }
}
