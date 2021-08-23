
public class Combo
{
    private int periot;
    private int probability;
    private int id;
    private string[] names;

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
    public int Probability
    {
        get
        {
            return probability;
        }
    }
    public string[] Names
    {
        get
        {
            return names;
        }
    }
    public Combo(int periot, int probability, int id, string[] names)
    {
        this.periot = periot;
        this.probability = probability;
        this.id = id;
        this.names = names;
    }
}

