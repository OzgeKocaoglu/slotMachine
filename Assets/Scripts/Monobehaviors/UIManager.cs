using UnityEngine;

public class UIManager : MonoBehaviour
{
    public delegate void UIHandler();
    public static UIHandler On_SpinClick, On_ActivateAgain;
    public GameObject button;

    private void Awake()
    {
        On_ActivateAgain += ActivateButton;
    }
    private void OnDestroy()
    {
        On_ActivateAgain += ActivateButton;
    }
    public void Spin()
    {
        On_SpinClick?.Invoke();
    }
    public void ActivateButton()
    {
        button.SetActive(true);
    }
}
