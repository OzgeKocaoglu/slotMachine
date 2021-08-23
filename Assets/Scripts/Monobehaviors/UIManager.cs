using UnityEngine;

public class UIManager : MonoBehaviour
{
    public delegate void UIHandler();
    public static UIHandler On_SpinClick;

    public void Spin()
    {
        Debug.Log("Spining");
        On_SpinClick?.Invoke();
    }
}
