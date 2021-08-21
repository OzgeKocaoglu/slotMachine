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
    Vector3 initialPosition;
    private float rotationSpeed;
    private float maxRotationSpeed = 20f;
    private float acceleration = 2f;
    private float deceleration = 3f;
    private float speedCoefficient = 3f;
    private float currentTime;


    private void Awake()
    {
        On_ReelViewSpinning += ReelSpin;
        bottom = items[0].transform.position.y;
        top = items[items.Count - 1].transform.position.y;
        spinCount = 0;
        initialPosition = this.transform.position;
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

            StartCoroutine(WaitUntilSpinEnd(spinTime, id));
            BlurActive();
        }
        
    }

    IEnumerator WaitUntilSpinEnd(float spinTime, int id)
    {
        Debug.Log("Spinning Started");
        StartCoroutine(Spin(id, spinTime));
        yield return new WaitForSeconds(spinTime);
        //StopCoroutine("Spin");
        StopAllCoroutines();
        Debug.Log("Spinning Ended");
    }

    IEnumerator Spin(int id, float spinTime)
    {
        yield return new WaitForSeconds(id * Random.Range(0.1f,0.2f));
        while (true)
        {

            if(transform.position.y <= -7.5f)
            {
                this.transform.position = initialPosition;
            }
            if(spinTime - currentTime > 5f)
            {
                rotationSpeed = Mathf.Clamp(rotationSpeed + Time.deltaTime * acceleration * speedCoefficient, 0, maxRotationSpeed);

            }
            else
            {
                rotationSpeed = Mathf.Clamp(rotationSpeed - Time.deltaTime * deceleration  * speedCoefficient, 0, maxRotationSpeed);

            }
            this.transform.position = Vector3.MoveTowards(transform.position,
                    (new Vector3(transform.position.x, -7.5f, transform.position.z)),
                    rotationSpeed * Time.deltaTime);
            currentTime += Time.deltaTime;

            yield return null;
        }
    }


    void BlurActive()
    {
        foreach(var item in items)
        {
            item.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
