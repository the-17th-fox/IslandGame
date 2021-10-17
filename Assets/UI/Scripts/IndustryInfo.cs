using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "IndustryData", menuName = "New Industry Data")]
public class IndustryInfo : ScriptableObject
{
    [SerializeField] public IndustryCore industry;

    [Header("Main parameters:")]
        [SerializeField] public string[] _industryNamesByLevel;
        [SerializeField] public GameObject[] _prefabsByLevel;
        [SerializeField] public byte _level;
        [SerializeField] public float _productionProportion;
        [SerializeField] public uint _workplacesPerLVL;
        [SerializeField] public float _effectiveness;
        [SerializeField] public bool _isEnabled;
        [SerializeField] public byte _MAX_LVL;
        [SerializeField] public uint _employeesAmount;

    [Header("Resources manager:")]
        [Tooltip("0 - Timber \n1 - Food \n2 - Iron \n3 - IronOre \n4 - Coal \n5 - Money \n6 - Trees")]
            [SerializeField] public byte[] _producibleResourcesIndexes;
        [Tooltip("0 - Timber \n1 - Food \n2 - Iron \n3 - IronOre \n4 - Coal \n5 - Money \n6 - Trees")]
            [SerializeField] public byte[] _consumableResourcesIndexes;

    [Header("Statistics fiels:")]
        [Space] public Sprite IndustrySprite;
        [HideInInspector] public string NameText;
        [HideInInspector] public string LevelText;
        [HideInInspector] public string EffectivenessText;
        [HideInInspector] public string ProductionText;
        [HideInInspector] public string EmployeesAmountText;
}
