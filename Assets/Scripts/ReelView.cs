using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ReelView : MonoBehaviour
{
    public delegate void ReelHandler(int id);
    public static ReelHandler On_ReelViewSpinning;
    [SerializeField] private int id;
    [SerializeField] private float spacing;
    [SerializeField] private float spinTime;
    [SerializeField] private List<GameObject> items;
    private int spinCount;
    private float bottom, top;


    private void Awake()
    {
        On_ReelViewSpinning += ReelSpin;
        bottom = items[0].transform.position.y;
        top = items[items.Count - 1].transform.position.y;
        spinCount = 0;
        Debug.Log("top " + top);
        Debug.Log("bottom " + bottom);
    }

    private void OnDestroy()
    {
        On_ReelViewSpinning -= ReelSpin;
    }


    private void ReelSpin(int id)
    {
        if(this.id == id)
        {

            StartCoroutine(WaitUntilSpinEnd(spinTime));
            BlurActive();
        }
        //Ýstenilen süre geçtikten sonra //Couroutine
        
    }

    IEnumerator WaitUntilSpinEnd(float spinTime)
    {
        StartCoroutine(Spinning());
        Debug.Log("Spinning Started");
        yield return new WaitForSeconds(spinTime);
        Debug.Log("Spinning Ended");
        StopCoroutine(Spinning());
    }


    IEnumerator Spinning()
    {
        while (true)
        {
            for (int i = 0; i < items.Count; i++)
            {
                this.transform.DOMoveY(items[i].transform.position.y - spacing, 0.3f).OnComplete(() => {
                    Debug.Log("Completed");

                    items[i].transform.position = new Vector2(items[i].transform.position.x, 12.5f);

                });
            }
        }
        yield return new WaitForSeconds(1f);
    }

    //Couroutine
    //void Spinning()
    //{
 
    //}


    void BlurActive()
    {
        foreach(var item in items)
        {
            item.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
