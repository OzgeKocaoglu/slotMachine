using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelView : MonoBehaviour
{
    public delegate void ReelHandler(int id, Combo _currentCombo);
    public static ReelHandler On_ReelViewSpinning;

    [SerializeField] private int id;
    [SerializeField] private float spinTime;
    [SerializeField] private List<GameObject> items;

    [Header("Spin Variables")]
    Vector3 initialPosition;
    private float rotationSpeed;
    private float maxRotationSpeed = 20f;
    private float acceleration = 2f;
    private float deceleration = 3f;
    private float speedCoefficient = 3f;
    private float currentTime;

    Coroutine SpinCouroutine = null, Stopping = null;


    private void Awake()
    {
        On_ReelViewSpinning += ReelSpin;
        initialPosition = this.transform.position;
    }

    private void OnDestroy()
    {
        On_ReelViewSpinning -= ReelSpin;
    }

    private void ReelSpin(int id, Combo _currentCombo)
    {
        if(this.id == id)
        {
            StartCoroutine(WaitUntilSpinEnd(spinTime, id));
            BlurToggle();
        }
    }

    IEnumerator WaitUntilSpinEnd(float spinTime, int id)
    {
        SpinCouroutine = StartCoroutine(Spin(id, spinTime));
        yield return new WaitForSeconds(spinTime);
        StopCoroutine(SpinCouroutine);
    }

    IEnumerator Spin(int id, float spinTime)
    {
        yield return new WaitForSeconds(id * Random.Range(0.1f,0.2f));

        while (true)
        {
            if (transform.position.y <= -7.5f)
            {
                this.transform.position = initialPosition;
            }
            if(spinTime - currentTime > 1f)
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


    private void BlurToggle()
    {
        foreach(var item in items)
        {
            GameObject childItem = item.gameObject.transform.GetChild(0).gameObject;
            if (childItem != enabled)
            {
                childItem.SetActive(true);
            }
            else
            {
                childItem.SetActive(false);
            }
        }
    }
}
