using UnityEngine;
using Zenject;


public interface IGameManager
{
    void StartGame();
}

public class GameManager: IGameManager
{
    private ISlotMachine _slotMachine;

    [Inject]
    public void Setup(ISlotMachine slotMachine)
    {
        _slotMachine = slotMachine;
    }

    public void StartGame()
    {
        _slotMachine.StartSlotMachine();
        Debug.Log("Start Game");
    }
}
