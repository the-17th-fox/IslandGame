using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public class Resource
    {
        public string Name
        {
            get => _name;
            private set => _name = value;
        }
        private string _name; // Resource name

        public float Amount
        {
            get => _amount;
            set
            {
                if (value < 0)
                    _amount = 0;
                if (value > float.MaxValue)
                    _amount = float.MaxValue;
                else
                    _amount = value;
            }
        }
        private float _amount; // Resource amount
        
        public bool IsMarketable
        {
            get => _isMarketable;
            private set => _isMarketable = value;
        }
        private bool _isMarketable; // Is resource marketable?

        public float MaxAmount
        {
            get => _maxAmount;
            private set
            {
                if (_maxAmount < 0)
                    _maxAmount = 0;
                else if (_maxAmount > float.MaxValue)
                    _maxAmount = float.MaxValue;
                else
                    _maxAmount = value;
            }
        }
        private float _maxAmount; // Max resource amount

        public float BasicGenerationSpeed
        {
            get => _basicGenerationSpeed;
            private set
            {
                if (BasicGenerationSpeed > float.MaxValue)
                    _basicGenerationSpeed = float.MaxValue;
                else if (BasicGenerationSpeed < 0)
                    _basicGenerationSpeed = float.MinValue;
                else
                    _basicGenerationSpeed = value;
            }
        }
        private float _basicGenerationSpeed; // Basic resource generation speed

        public Resource(string Name, float MaxAmount = float.MaxValue, float InitAmount = 0, float BasicGenerationSpeed = 0, bool isMarketable = true)
        {
            this.Name = Name;
            this.MaxAmount = MaxAmount;
            this.Amount = InitAmount;
            this.BasicGenerationSpeed = BasicGenerationSpeed;
            this.IsMarketable = isMarketable;
        }
    }

    [Header("List of statistics fields:")]
    [SerializeField] private Text _timberAmountText;
    [SerializeField] private Text _foodAmountText;
    [SerializeField] private Text _ironAmountText;
    [SerializeField] private Text _ironOreAmountText;
    [SerializeField] private Text _coalAmountText;
    [SerializeField] private Text _moneyAmountText;
    [SerializeField] private Text _treesAmountText;

    [HideInInspector] public Resource Timber = new Resource("Timber");
    [HideInInspector] public Resource Food = new Resource("Food");
    [HideInInspector] public Resource Iron = new Resource("Iron");
    [HideInInspector] public Resource IronOre = new Resource("Iron ore");
    [HideInInspector] public Resource Coal = new Resource("Coal");
    [HideInInspector] public Resource Money = new Resource("Money", InitAmount: 1000, BasicGenerationSpeed: 0.5f, isMarketable: false);
    [HideInInspector] public Resource Trees = new Resource("Trees", InitAmount: 100, BasicGenerationSpeed: 0.5f, MaxAmount: 1000, isMarketable: false);

    private Resource[] _resourcesArray; // Array of all available resources 
    private Text[] _statisticsArray;    // Array of all text statistics

    private void Start()
    {
        _resourcesArray = new[] { Timber, Food, Iron, IronOre, Coal, Money, Trees };
        _statisticsArray = new[] { _timberAmountText, _foodAmountText, _ironAmountText, _ironOreAmountText, _coalAmountText, _moneyAmountText, _treesAmountText };
    }

    private void Update()
    {
        UpdateStatistics(_statisticsArray, _resourcesArray);
        BasicResourcesGeneration(_resourcesArray);
    }

    /// <summary>
    /// Updates statistics and resurces info
    /// </summary>
    /// <param name="statistics">Recieves the array of text statistics fields</param>
    /// <param name="resources">Recieves the array of all resources</param>
    static private void UpdateStatistics(Text[] statistics, Resource[] resources)
    {
        if (statistics.Length != resources.Length)
        {
            Debug.LogError("UpdateStatistics : statistics.Length != resources.Length");
            return;
        }

        for (int i = 0; i < statistics.Length; i++)
        {
            statistics[i].text = $"{Math.Round(resources[i].Amount, 1)}";
        }
    }

    /// <summary>
    /// Generates basical resource
    /// </summary>
    /// <param name="resources">Recieves the array of all resources</param>
    private static void BasicResourcesGeneration(ResourceManager.Resource[] resources)
    {
        foreach (ResourceManager.Resource resource in resources)
        {
            if (resource.BasicGenerationSpeed != 0 && resource.Amount < resource.MaxAmount)
            {
                resource.Amount += resource.BasicGenerationSpeed * Time.deltaTime;
            }
        }
    }
}