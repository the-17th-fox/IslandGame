using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Island : MonoBehaviour
{
    [SerializeField] private Text TimberAmountText;
    [SerializeField] private Text FoodAmountText;
    [SerializeField] private Text IronAmountText;
    [SerializeField] private Text IronOreAmountText;
    [SerializeField] private Text CoalAmountText;
    [SerializeField] private Text MoneyAmountText;
    [SerializeField] private Text TreesAmountText;

    [HideInInspector] public Resource Timber;
    [HideInInspector] public Resource Food;
    [HideInInspector] public Resource Iron;
    [HideInInspector] public Resource IronOre;
    [HideInInspector] public Resource Coal;
    [HideInInspector] public Resource Money;
    [HideInInspector] public Resource Trees;

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

        IronOre = gameObject.AddComponent<Resource>();
            IronOre.CreateNewResource("Iron ore");

        Coal = gameObject.AddComponent<Resource>();
            Coal.CreateNewResource("Coal");

        Money = gameObject.AddComponent<Resource>();
            Money.CreateNewResource("Money", InitAmount: 1000, BasicGenerationSpeed: 0.5f, isMarketable: false);

        Trees = gameObject.AddComponent<Resource>();
            Trees.CreateNewResource("Trees", InitAmount: 100, BasicGenerationSpeed: 0.5f, MaxAmount: 1000, isMarketable: false);

        resources = new[] { Timber, Food, Iron, IronOre, Coal, Money, Trees };
        statistics = new[] { TimberAmountText, FoodAmountText, IronAmountText, IronOreAmountText, CoalAmountText, MoneyAmountText, TreesAmountText };
    }

    private void Update()
    {
        Resource.UpdateStatistics(statistics, resources);
        //Resource.BasicResourcesGeneration(resources);
        //Debug.Log($"Money:{Math.Round(Money._amount, 2)}, Population:{Math.Round(Population._amount, 2)}, Trees:{Math.Round(Trees._amount, 2)}, Iron: {Math.Round(Iron._amount, 2)} Timber: {Math.Round(Timber._amount, 2)}");
    }
}

