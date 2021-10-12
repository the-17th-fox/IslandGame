using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Industry : MonoBehaviour
{
    //public ResourceManager _resourceManager;
    //public ResourceManager.Resource[] _resources = { _resourceManager.Timber, _resourceManager.Food, _resourceManager.Iron, _resourceManager.IronOre, _resourceManager.Coal, _resourceManager.Money, _resourceManager.Trees };

    private IndustryCore _industry;

    [Space][SerializeField] private string[] _industryNamesByLevel;

    [Space][SerializeField] private GameObject[] _prefabsByLevel;

    [Space][SerializeField] private IndustryInfo _industryInfo;

    [Header("Resources manager:")]
    [Tooltip("0 - Timber \n1 - Food \n2 - Iron \n3 - IronOre \n4 - Coal \n5 - Money \n6 - Trees")]
    [SerializeField] private byte[] _producibleResourceIndexes;
    [Tooltip("0 - Timber \n1 - Food \n2 - Iron \n3 - IronOre \n4 - Coal \n5 - Money \n6 - Trees")]
    [SerializeField] private byte[] _consumableResourceIndexes;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
