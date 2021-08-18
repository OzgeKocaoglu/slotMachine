using UnityEngine;
using Zenject;


public interface IGameManager
{
    void StartGame();
}

public class GameManager: IGameManager
{

    [Inject]
    ISlotMachine _slotMachine;

    public void StartGame()
    {
        UIManager.On_SpinClick += SpinGame;
        Debug.Log("Start is started");
        _slotMachine.StartSlotMachine();
    }
    
    public void SpinGame()
    {
        _slotMachine.SpinSlotMachine();
    }

}
