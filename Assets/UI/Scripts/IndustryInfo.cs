using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "IndustryData", menuName = "New Industry Data")]
public class IndustryInfo : ScriptableObject
{
    public Sprite IndustrySprite;
    [HideInInspector] public string NameText;
    [HideInInspector] public string LevelText;
    [HideInInspector] public string EffectivenessText;
    [HideInInspector] public string ProductionText;
    [HideInInspector] public string EmployeesAmountText;
}
