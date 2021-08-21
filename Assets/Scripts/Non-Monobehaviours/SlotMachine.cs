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
    void StartSlotMachine(int reelCount, List<ProbabilityTableVariable> _datas);
    void SpinSlotMachine();
}


public class SlotMachine : ISlotMachine
{
    private SlotMachineState _state;
    private List<Combo> _combos;
    private List<Reel> _reels;
    private List<int> _comboList;
    private Dictionary<int, int> _probabilityData;
    private int spinCount = 0;
    private float currentProbability = 0;
    private int periot = 100;
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


    private void InitSlotMachine(int reelCount, List<ProbabilityTableVariable> _datas)
    {
        _combos = new List<Combo>();
        _reels = new List<Reel>();
        _comboList = new List<int>(periot);
        for(int i = 1; i<= 3; i++)
        {
            _reels.Add(new Reel(i));
        }
        foreach (var data in _datas)
        {
            Combo combo = new Combo(data.periot, data.spinProbability, data.id);
            _combos.Add(combo);
        }

        //spinCount'u playerprefs'den al.
        if (spinCount % periot == 0)
        {
            Debug.Log("New set is creating");
            for (int i = 0; i < _combos.Count; i++)
            {
                for (int j = 0; j < _combos[i].Probability; j++)
                {
                    _comboList.Add(_combos[i].ID);
                }
            }
        }

        Utils.Shuffle(_comboList);

        foreach(var combo in _comboList)
        {
            Debug.Log(combo);
        }
        //Listeyi istenilen þekilde düzenle.
        //Array'e çevir
        //make spacing
        //check if true
        //



    }

    void ISlotMachine.StartSlotMachine(int reelCount, List<ProbabilityTableVariable> _datas)
    {
        InitSlotMachine(reelCount, _datas);
    }

    void ISlotMachine.SpinSlotMachine()
    {
        State = SlotMachineState.Rolling;

        //Calculate weights
        //periotxprobability
        //get min periot and find maximum probability cumulative
        //set cameoutvariable false
        //and keep going
        List<float> _prop = new List<float>();

        foreach(Combo _combo in _combos)
        {
            _prop.Add(_combo.Probability);
        }
        Debug.Log(GetItemByProbability(_prop));

      
    }

    private Combo GetItemByPeriot(int periot)
    {
        for(int i = 0; i<_combos.Count; i++)
        {
            if(_combos[i].Periot == periot)
            {
                return _combos[i];
            }
        }
        return null;
    }

    private int CalculateWeight(int probability, int periot)
    {
        return probability * periot;
    }


    private int GetMinPeriot(int spinCount)
    {
        int minPeriot = 20;
        for (int i = 0; i < _combos.Count; i++)
        {
            if ((_combos[i].Periot < minPeriot))
            {
                minPeriot = _combos[i].Periot;
            }
        }

        return minPeriot;
    }

    List<float> cumulativeProbability;

    //This function is called with the Item probability array and it'll return the index of the item
    // for example the list can look like [10,25,30] so the first item has 10% of showing and next one has 25% and so on
    public int GetItemByProbability(List<float> probability) //[50,10,20,20]
    {
        //if your game will use this a lot of time it is best to build the arry just one time
        //and remove this function from here.
        if (!MakeCumulativeProbability(probability))
            return -1; //when it return false then the list excceded 100 in the last index

        float rnd = UnityEngine.Random.Range(1, 101); //Get a random number between 0 and 100

        for (int i = 0; i < probability.Count; i++)
        {
            if (rnd <= cumulativeProbability[i]) //if the probility reach the correct sum
            {
                return i; //return the item index that has been chosen 
            }
        }
        return -1; //return -1 if some error happens
    }

    //this function creates the cumulative list
    bool MakeCumulativeProbability(List<float> probability)
    {
        float probabilitiesSum = 0;

        cumulativeProbability = new List<float>(); //reset the Array

        for (int i = 0; i < probability.Count; i++)
        {
            probabilitiesSum += probability[i]; //add the probability to the sum
            cumulativeProbability.Add(probabilitiesSum); //add the new sum to the list

            //All Probabilities need to be under 100% or it'll throw an exception
            if (probabilitiesSum > 100f)
            {
                Debug.LogError("Probabilities exceed 100%");
                return false;
            }
        }
        return true;
    }

}
