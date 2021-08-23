using System.Collections.Generic;
using UnityEngine;
using Zenject;


public enum SlotMachineState
{
    Idle,
    Starting,
    Rolling,
    Stopping
}

public interface ISlotMachine
{
    void StartSlotMachine(int reelCount, List<ProbabilityTableVariable> _datas);
    void SpinSlotMachine();
}

public class SlotMachine : ISlotMachine
{
    private SlotMachineState _state;
    private List<Combo> _combos;
    private List<Reel> _reels;
    private int[] _comboList;
    private int spinCount = 0;
    private int periot = 100;

    public delegate void SlotMachineHandler(SlotMachineState State, Combo _currentCombo);
    public static SlotMachineHandler On_StateChanged;
    public Combo _currentCombo;

    [Inject]
    IDataManager _dataManager;


    public SlotMachineState State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
            On_StateChanged?.Invoke(_state, _currentCombo);
        }
    }

    private void InitSlotMachine(int reelCount, List<ProbabilityTableVariable> _datas)
    {
        _combos = new List<Combo>();
        _reels = new List<Reel>();
        _comboList = new int[periot];
        for(int i = 1; i<= 3; i++)
        {
            _reels.Add(new Reel(i));
        }
        foreach (var data in _datas)
        {
            Combo combo = new Combo(data.periot, data.spinProbability, data.id, data.slotVariables.ToArray());
            _combos.Add(combo);
        }
        _dataManager.GetData(out spinCount, out _comboList);
        if (spinCount % periot == 0)
        {
            Debug.Log("Creating new set");
            for (int i = 0; i < _combos.Count; i++)
            {
                for (int p = _combos[i].Periot; p <= 100; p += _combos[i].Periot)
                {
                    int index = GenerateRandom(p, _combos[i].Periot);
                    if(_comboList[index] == 0)
                    {
                        _comboList[index] = _combos[i].ID;
                    }
                    else
                    {
                        index = FindNullIndexAtPeriot(p - _combos[i].Periot, p);
                        _comboList[index] = _combos[i].ID;
                    }
                }
            }
        }
        else
        {
            _currentCombo = GetCombo();
        }
    }
    void ISlotMachine.StartSlotMachine(int reelCount, List<ProbabilityTableVariable> _datas)
    {
        InitSlotMachine(reelCount, _datas);
    }
    void ISlotMachine.SpinSlotMachine()
    {
        State = SlotMachineState.Rolling;
        spinCount++;
        _dataManager.SaveData(_comboList, spinCount);
    }
    private Combo GetCombo()
    {
        for (int i = 0; i < _comboList.Length; i++)
        {
            if (_comboList[i] != 0)
            {
                Combo _combo = FindCombo(_comboList[i]);
                if(_combo != null)
                {
                    _comboList[i] = 0;
                    return _combo;
                }
                else
                {
                    Debug.LogError("Json file is empty, list is empty!");
                }

            }
        }
        return null;
    }
    private Combo FindCombo(int id)
    {
        foreach(var combo in _combos)
        {
            if (combo.ID == id)
                return combo;

        }
        return null;
    }
    private void ClearJson()
    {
        spinCount = 0;
    }
    private int GenerateRandom(int p, int coefficent)
    {
        return UnityEngine.Random.Range(p - coefficent, p);
    }
    private int FindNullIndexAtPeriot(int bottom, int top)
    {
        for (int i = bottom; i <= top; i++)
        {
            if (_comboList[i] == 0)
            {
                return i;
            }
        }
        return 0;
    }
}
