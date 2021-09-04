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

    [SerializeField]
    private float _originalWoodAmount = 0;
    [SerializeField]
    private float _originalFoodAmount = 0;
    [SerializeField]
    private float _originalMoneyAmount = 0;
    [SerializeField]
    private float _originalPopulationAmount = 10000;

    Resource Wood;
    Resource Food;
    Resource Money;
    Resource Population;

    //WoodFactory woodFactory;
    // 
    BasicIndustrySector TimberIndustry;
    /// 



    Resource[] resources;
    Text[] statistics;

    private void Start()
    { 
        Wood = gameObject.AddComponent<Resource>();
            Wood.CreateNewResource("Wood", _originalWoodAmount, BasicGenerationSpeed: 0.7f);

        Food = gameObject.AddComponent<Resource>();
            Food.CreateNewResource("Food", _originalFoodAmount);

        Money = gameObject.AddComponent<Resource>();
            Money.CreateNewResource("Money", _originalMoneyAmount, BasicGenerationSpeed: 0.2f, isMarketable:false);

        Population = gameObject.AddComponent<Resource>();
            Population.CreateNewResource("Population", _originalPopulationAmount, BasicGenerationSpeed: 0.03f, isMarketable:false);

        //woodFactory = gameObject.AddComponent<WoodFactory>();
        //
        TimberIndustry = gameObject.AddComponent<BasicIndustrySector>();
            TimberIndustry.CreateNewSector("Timer", Level: 1, Effectivenes: 1, ProductionCoefficient: 0.7f, Employees: 1000, 0, 1);
        ///

        resources = new[] { Wood, Food, Money, Population };
        statistics = new[] { WoodAmountText, FoodAmountText, MoneyAmountText, PopulationAmountText };
    }

    private void Update()
    {
        //foreach (var resource in resources)
        //{
        //    resource.ResourceGeneration((uint)(Population._amount / 100 * 50));
        //}

        //woodFactory.Production(ref Wood, ref Money);
        Money.IndusrialResourceGeneration(TimberIndustry,resources);
        
        Resource.UpdateStatistics(statistics, resources);

    }
}

