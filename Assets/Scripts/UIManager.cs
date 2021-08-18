using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public delegate void UIHandler();
    public static UIHandler On_SpinClick;

    public void Spin()
    {
        On_SpinClick?.Invoke();
        Debug.Log("Spining");
    }
}
