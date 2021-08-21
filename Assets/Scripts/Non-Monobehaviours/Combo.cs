using UnityEngine;
using System;
using System.Collections.Generic;


class Combo
{
    private int periot;
    private int probability;
    private int id;
    private bool isCameOut;
    public int[][] _numberOflist;

    public int Periot
    {
        get
        {
            return periot;
        }
    }
    public int ID
    {
        get
        {
            return id;
        }
    }

    public bool CameOut
    {
        get
        {
          return  isCameOut;
        }
        set
        {
            isCameOut = value;
        }
    }

    public int Probability
    {
        get
        {
            return probability;
        }
    }

    public void SetListVariables(List<int> _variables)
    {
        for(int i = 0; i<_variables.Count; i++)
        {
            if(_variables[i] == ID)
            {
                for(int j = 0; j < _numberOflist.Length; j++)
                {
                    
                }

            }
        }
    }


    public Combo(int periot, int probability, int id)
    {
        this.periot = periot;
        this.probability = probability;
        this.id = id;
        _numberOflist = new int[100/periot][];
        isCameOut = false;
    }

}

