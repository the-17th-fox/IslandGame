using UnityEngine;
using UnityEngine.UI;

public class MiningIndustry : MonoBehaviour
{
    private Industry miningIndustry;

    [Header("This industry fields:")] // 
    [SerializeField] private GameObject _firstLVL_prefab;

    [Header("List of statistics fields:")]
    [SerializeField] public Text NameText;
    [SerializeField] public Text LVLText;
    [SerializeField] public Text EffectivenessText;
    [SerializeField] public Text ProductionProportionText;
    [SerializeField] public Text EmployeesText;

    private Text[] _statistics;
    private string[] _industryNamesByLVL;
    private ResourceManager.Resource[] _consumableResources; 
    private ResourceManager.Resource[] _producedResources; 

    public ResourceManager _resourceManager;

    private void Start()
    {
        _statistics = new[] { NameText, LVLText, EffectivenessText, ProductionProportionText, EmployeesText };
        _industryNamesByLVL = new[] { "MINE" };

        miningIndustry = new Industry(ProductionProportion: 1, MAX_LVL: 1, Statistics: _statistics, IndustryLVLNames: _industryNamesByLVL);
        miningIndustry.SetNewEmployeesAmount(5);

        _consumableResources = new[] { _resourceManager.Money };
        _producedResources = new[] { _resourceManager.Coal, _resourceManager.IronOre };
    }

    private void Update()
    {
        if (miningIndustry.IsEnabled)
        {
            miningIndustry.UpdateStatistics();
            miningIndustry.IndustryResourceProduction(_consumableResources, _producedResources);
        }
    }
}
