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

public interface ISlotMachine
{
    void StartSlotMachine();
}


public class SlotMachine : ISlotMachine
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

    void ISlotMachine.StartSlotMachine()
    {
        Debug.Log("Started slot machine");
    }
}
