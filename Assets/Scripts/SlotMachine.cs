using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SlotMachineState
{
    Idle,
    Starting,
    Rolling,
    Stopping
}

public class SlotMachine : MonoBehaviour
{
    private SlotMachineState _state;

    public delegate void SlotMachineHandler(SlotMachineState State);
    public static SlotMachineHandler On_StateChanged;


    public SlotMachineState State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
            On_StateChanged?.Invoke(_state);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            State = SlotMachineState.Rolling;
        }
    }

}
