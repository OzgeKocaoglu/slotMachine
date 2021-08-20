using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Data", menuName = "Slot Machine/ProbabilityTableVariable", order =1)]
public class ProbabilityTableVariable : ScriptableObject
{
    public int id;
    public List<string> slotVariables;
    public int spinProbability;
    public int periot;
}
