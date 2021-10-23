using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "IndustryData", menuName = "New Industry Data")]
public class IndustryInfo : ScriptableObject
{
    [SerializeField] public IndustryCore industry;

    [Header("Main parameters:")]
        public string[] IndustryNamesByLevel;
        public GameObject[] PrefabsByLevel;
        public byte Level;
        public float ProductionProportion;
        public int WorkplacesPerLVL;
        public float Effectiveness;
        public bool IsEnabled;
        public byte MAX_LVL;
        public bool DebugLog;

        [HideInInspector] public int EmployeesAmount = 0;

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
