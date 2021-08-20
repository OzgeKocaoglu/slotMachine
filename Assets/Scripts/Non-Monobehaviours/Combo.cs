using UnityEngine;


class Combo
{
    private int periot;
    private int probability;
    private int id;
    private bool isCameOut; 

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


    public Combo(int periot, int probability, int id)
    {
        this.periot = periot;
        this.probability = probability;
        this.id = id;
        isCameOut = false;
    }


}

