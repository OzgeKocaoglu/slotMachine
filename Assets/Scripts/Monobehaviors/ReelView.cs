using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ReelView : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private float spinTime;
    [SerializeField] private List<ItemView> items;

    private Vector3 initialPosition;
    private float rotationSpeed;
    private float maxRotationSpeed = 20f;
    private float acceleration = 2f;
    private float deceleration = 5f;
    private float speedCoefficient = 3f;
    private float currentTime;
    private List<string> spinVariables;

    Coroutine SpinCouroutine = null, Stopping = null;

    public delegate void ReelHandler(int id, Combo _currentCombo);
    public static ReelHandler On_ReelViewSpinning;

    private void Awake()
    {
        On_ReelViewSpinning += ReelSpin;
        initialPosition = this.transform.position;
        spinVariables = new List<string>();
    }
    private void OnDestroy()
    {
        On_ReelViewSpinning -= ReelSpin;
    }
    private void ReelSpin(int id, Combo _currentCombo)
    {
        if(this.id == id)
        {
           
            for (int i = 0; i<_currentCombo.Names.Length; i++)
            {
                spinVariables.Add(_currentCombo.Names[i]);
                Debug.Log(_currentCombo.Names[i]);
            }
            StartCoroutine(WaitUntilSpinEnd(spinTime, id));
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
            if(currentTime > spinTime / 4)
            {
                foreach (var item in items)
                {
                    item.ActivateBlur();
                }
            }
            if (transform.position.y <= -7.5f)
            {
                this.transform.position = initialPosition;
            }
            rotationSpeed = Mathf.Clamp(rotationSpeed + Time.deltaTime * acceleration * speedCoefficient, 0, maxRotationSpeed);
            this.transform.position = Vector3.MoveTowards(transform.position,
            (new Vector3(transform.position.x, -7.5f, transform.position.z)),
            rotationSpeed * Time.deltaTime);
            if (spinTime - currentTime < spinTime / 2)
            {
                foreach (var item in items)
                {
                    item.DeactivateBlur();
                }
                Stopping = StartCoroutine(Stop());
            }
            currentTime += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator Stop()
    {
        if(id == 2 || id == 1)
        {
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            yield return new WaitForSeconds(1f);

        }
        rotationSpeed = Mathf.Clamp(rotationSpeed - Time.deltaTime * deceleration * speedCoefficient, 0, maxRotationSpeed);

        while (true)
        {
            this.transform.position = Vector3.MoveTowards(transform.position,
            (new Vector3(transform.position.x, -GetCurrentVariableLocalTransform(), transform.position.z)),
            rotationSpeed * Time.deltaTime);
            yield return null;
        }

    }

    private float GetCurrentVariableLocalTransform()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Equals(spinVariables[id-1]))
            {
                return items[i].LocalTransform.y;
            }
        }
        return 0;
    }
}
