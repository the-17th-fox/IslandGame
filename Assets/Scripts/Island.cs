using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Island : MonoBehaviour
{
    [SerializeField]
    private Text TimberAmountText;
    [SerializeField]
    private Text FoodAmountText;
    [SerializeField]
    private Text MoneyAmountText;
    [SerializeField]
    private Text PopulationAmountText;

    public Resource Timber;
    public Resource Food;
    public Resource Iron;
    public Resource Money;
    public Resource Trees;
    public Resource Population;

    private Resource[] resources;
    private Text[] statistics;

    private void Awake()
    { 
        Timber = gameObject.AddComponent<Resource>();
            Timber.CreateNewResource("Timber");

        Food = gameObject.AddComponent<Resource>();
            Food.CreateNewResource("Food");

        Iron = gameObject.AddComponent<Resource>();
            Iron.CreateNewResource("Iron");            

        Money = gameObject.AddComponent<Resource>();
            Money.CreateNewResource("Money", InitAmount: 1000, BasicGenerationSpeed: 0.5f, isMarketable: false);

        Population = gameObject.AddComponent<Resource>();
            Population.CreateNewResource("Population", InitAmount: 500, BasicGenerationSpeed: 0.5f, isMarketable:false);

        Trees = gameObject.AddComponent<Resource>();
            Trees.CreateNewResource("Trees", InitAmount: 100, BasicGenerationSpeed: 0.5f, MaxAmount: 1000);

        resources = new[] { Timber, Food, Iron, Money, Population, Trees };
        statistics = new[] { TimberAmountText, FoodAmountText, MoneyAmountText, PopulationAmountText };
    }

    private void Update()
    {
        //Resource.UpdateStatistics(statistics, resources);
        Resource.BasicResourcesGeneration(resources);
        //Debug.Log($"Money:{Math.Round(Money._amount, 2)}, Population:{Math.Round(Population._amount, 2)}, Trees:{Math.Round(Trees._amount, 2)}, Iron: {Math.Round(Iron._amount, 2)} Timber: {Math.Round(Timber._amount, 2)}");
    }
}

