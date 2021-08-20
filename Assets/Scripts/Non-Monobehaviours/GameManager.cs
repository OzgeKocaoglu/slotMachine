using UnityEngine;
using Zenject;
using System.Collections.Generic;


public interface IGameManager
{
    void StartGame();
}

public class GameManager: IGameManager
{

    [Inject]
    ISlotMachine _slotMachine;

    [Inject]
    IDataManager _dataManager;

    public void StartGame()
    {
        _slotMachine.StartSlotMachine(3, _dataManager.Init());
        UIManager.On_SpinClick += SpinGame;
    }
    
    public void SpinGame()
    {
        _slotMachine.SpinSlotMachine();
    }

}
