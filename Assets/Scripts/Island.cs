using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Island : MonoBehaviour
{
    [SerializeField]
    private Text WoodAmountText;
    [SerializeField]
    private Text FoodAmountText;
    [SerializeField]
    private Text MoneyAmountText;
    [SerializeField]
    private Text PopulationAmountText;

    public Resource Timber;
    public Resource Food;
    public Resource Money;
    public Resource Trees;
    public Resource Population;

    private Resource[] resources;
    private Text[] statistics;

    private void Start()
    { 
        Timber = gameObject.AddComponent<Resource>();
            Timber.CreateNewResource("Древесина", 20);

        Food = gameObject.AddComponent<Resource>();
            Food.CreateNewResource("Еда", 35);

        Money = gameObject.AddComponent<Resource>();
            Money.CreateNewResource("Деньги", 100, BasicGenerationSpeed: 0.05f, isMarketable:false);

        Population = gameObject.AddComponent<Resource>();
            Population.CreateNewResource("Население", 500, BasicGenerationSpeed: 0.03f, isMarketable:false);

        Trees = gameObject.AddComponent<Resource>();
        Trees.CreateNewResource("Деревья", 400, BasicGenerationSpeed: 0.001f);

        resources = new[] { Timber, Food, Money, Population };
        statistics = new[] { WoodAmountText, FoodAmountText, MoneyAmountText, PopulationAmountText };
    }

    private void Update()
    {
        Resource.UpdateStatistics(statistics, resources);

    }
}

