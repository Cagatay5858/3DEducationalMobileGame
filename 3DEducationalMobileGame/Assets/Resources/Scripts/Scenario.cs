using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Educational/Scenario")]
public class Scenario : ScriptableObject
{
    public List<ScenarioStep> steps;
}
