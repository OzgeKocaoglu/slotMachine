using System.Collections.Generic;

public interface IDataManager
{
    List<ProbabilityTableVariable> Init();
}


public class DataManager : IDataManager
{

    public List<ProbabilityTableVariable> Init()
    {
        return Utils.GetAllInstances<ProbabilityTableVariable>();
    }
}
