using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelView : MonoBehaviour
{
    public delegate void ReelHandler(int id, Combo _currentCombo);
    public static ReelHandler On_ReelViewSpinning;

    [SerializeField] private int id;
    [SerializeField] private float spinTime;
    [SerializeField] private List<ItemView> items;

    [Header("Spin Variables")]
    Vector3 initialPosition;
    private float rotationSpeed;
    private float maxRotationSpeed = 20f;
    private float acceleration = 2f;
    private float deceleration = 3f;
    private float speedCoefficient = 3f;
    private int spinableObjectCount = 5;
    private float currentTime;
    private List<string> spinVariables;

    Coroutine SpinCouroutine = null, Stopping = null;


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
        foreach (var item in items)
        {
            item.ActivateBlur();
        }
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
            if(spinTime - currentTime > spinTime/2)
            {
                rotationSpeed = Mathf.Clamp(rotationSpeed + Time.deltaTime * acceleration * speedCoefficient, 0, maxRotationSpeed);
                this.transform.position = Vector3.MoveTowards(transform.position,
                (new Vector3(transform.position.x, -7.5f, transform.position.z)),
                rotationSpeed * Time.deltaTime);
            }
            else
            {
                //Slowing
                foreach (var item in items)
                {
                    item.DeactivateBlur();
                }
                Debug.Log("I'm reel: " + id + "and my transform is " + GetCurrentVariableLocalTransform());
                rotationSpeed = Mathf.Clamp(rotationSpeed - Time.deltaTime * deceleration  * speedCoefficient, 0, maxRotationSpeed);
                this.transform.position = Vector3.MoveTowards(transform.position,
                (new Vector3(transform.position.x, -GetCurrentVariableLocalTransform(), transform.position.z)),
                rotationSpeed * Time.deltaTime);
                if (transform.position.y == -GetCurrentVariableLocalTransform())
                {
                    Debug.Log("Wait");
                    yield return null;
                }
            }
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    private float GetCurrentVariableLocalTransform()
    {
        for (int i = 0; i < spinableObjectCount; i++)
        {
            if (items[i].Equals(spinVariables[id-1]))
            {
                Debug.Log("Local transform:: " + items[i].LocalTransform.y);
                return items[i].LocalTransform.y;
            }
        }
        return 0;

    }
}
