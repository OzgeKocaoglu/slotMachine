using System;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour{

    private int numberOfcomboItems;

    private void Awake()
    {
        SlotMachine.On_StateChanged += SlotMachineStateChange;
    }
    private void OnDestroy()
    {
        SlotMachine.On_StateChanged -= SlotMachineStateChange;
    }


    private void SlotMachineStateChange(SlotMachineState _state)
    {
        if(_state == SlotMachineState.Rolling)
        {
            Debug.Log("Reels are rolling");
        }
    }
}