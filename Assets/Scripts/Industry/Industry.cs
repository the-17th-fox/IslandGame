using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Industry
{
    public string Name
    {
        get => _name;
        set => _name = value;
    }
    private string _name;                   // Industry name

    public byte Level
    {
        get => _lvl;
        set
        {
            if (value < MIN_LVL)
            {
                Debug.LogWarning($"Industry,LVL: Value({value}) is less than MIN({MIN_LVL})");
                _lvl = MIN_LVL;
            }
            else if (value > MAX_LVL)
            {
                Debug.LogWarning($"Industry,LVL: Value({value}) is bigger than MAX({MAX_LVL})");
                _lvl = MAX_LVL;
            }
            else if (value >= MIN_LVL && value <= MAX_LVL)
                _lvl = value;
        }
    }
    private byte _lvl;                      // Industry level

    public float Effectiveness
    {
        get => _effectiveness;
        set
        {
            if (value < MIN_EFFECTIVENESS)
            {
                Debug.LogWarning($"Industry,Effectiveness: Value({value}) is less than MIN({MIN_EFFECTIVENESS})");
                _effectiveness = MIN_EFFECTIVENESS;
            }
            else if (value > float.MaxValue)
            {
                Debug.LogWarning($"Industry,Effectiveness: Value({value}) is bigger than MAX({float.MaxValue})");
                _effectiveness = float.MaxValue;
            }
            else
                _effectiveness = value;
        }
    }
    private float _effectiveness;           // Industry effectiveness

    public float ProductionProportion
    {
        get => _productionProportion;
        set
        {
            if (value < MIN_PRODUCTION_PROPORTION)
            {
                Debug.LogWarning($"Industry,Prod.proportion: Value({value}) is less than MIN({MIN_PRODUCTION_PROPORTION})");
                _productionProportion = MIN_PRODUCTION_PROPORTION;
            }
            else if (value > float.MaxValue)
            {
                Debug.LogWarning($"Industry,Prod.proportion: Value({value}) is bigger than MAX({float.MaxValue})");
                _productionProportion = float.MaxValue;
            }
            else
                _productionProportion = value;
        }
    }
    private float _productionProportion;    // Basic produced resources amount

    public float Staffing
    {
        get => _staffing;
        set
        {
            if (value < MIN_STAFFING)
            {
                Debug.LogWarning($"Industry,Staffing: Value({value}) is less than MIN({MIN_STAFFING})");
                _staffing = MIN_STAFFING;
            }
            else if (value > MAX_STAFFING)
            {
                Debug.LogWarning($"Industry,Staffing: Value({value}) is bigger than MAX({MAX_STAFFING})");
                _staffing = MAX_STAFFING;
            }
            else
                _staffing = value;
        }
    }
    private float _staffing;                // Staffing

    public uint EmployeesAmount
    {
        get => _employeesAmount;
        set
        {
            if (value < MIN_EMPLOYEES)
            {
                Debug.LogWarning($"{Name}, Industry,Empl.amount: Value({value}) is less than MIN({MIN_EMPLOYEES})");
                _employeesAmount = MIN_EMPLOYEES;
            }
            else if (value > TotalWorkplacesAmount)
            {
                Debug.LogWarning($"{Name}, Empl.amount: Value({value}) is bigger than MAX({TotalWorkplacesAmount})");
                _employeesAmount = TotalWorkplacesAmount;
            }
            else
                _employeesAmount = value;
        }
    }
    private uint _employeesAmount;          // Employees amount

    public uint WorkplacesPerLVL
    {
        get => _workplacesPerLVL;
        set
        {
            if (value < MIN_WORKPLACES_PER_LVL)
            {
                Debug.LogWarning($"Industry,WorkplacesPerLvl: Value({value}) is less than MIN({MIN_WORKPLACES_PER_LVL})");
                _workplacesPerLVL = MIN_WORKPLACES_PER_LVL;
            }
            else if (value > uint.MaxValue)
            {
                Debug.LogWarning($"Industry,WorkplacesPerLvl: Value({value}) is bigger than MAX({uint.MaxValue})");
                _workplacesPerLVL = uint.MaxValue;
            }
            else
                _workplacesPerLVL = value;
        }
    }
    private uint _workplacesPerLVL;         // Workplaces amount/per LVL

    public uint TotalWorkplacesAmount
    {
        get => _totalWorkplacesAmount;
        set
        {
            if (value < uint.MinValue)
            {
                Debug.LogWarning($"Industry,TotalWorkplacesAmount: Value({TotalWorkplacesAmount}) is less than MIN({uint.MinValue})");
                _totalWorkplacesAmount = uint.MinValue;
            }
            else if (value > uint.MaxValue)
            {
                Debug.LogWarning($"Industry,TotalWorkplacesAmount: Value({value}) is bigger than MAX({uint.MaxValue})");
                _totalWorkplacesAmount = uint.MaxValue;
            }
            else
                _totalWorkplacesAmount = value;
        }
    }
    private uint _totalWorkplacesAmount;    // Total workplaces amount

    public bool IsEnabled
    {
        get => _isEnabled;
        private set
        {
            _isEnabled = value;
        }
    }
    private bool _isEnabled;                // Activity status

    public Text[] Statistics
    {
        get => _statistics;
        set
        {
            if (value == null)
                Debug.LogWarning($"IndustryBasse: {Name}, Statistics == null");
            else
                _statistics = value;
        }
    }
    private Text[] _statistics;             // Array of text statistic elemenets

    public string[] IndustryLVLNames
    {
        get => _industryLVLNames;
        set
        {
            if(value == null)
                Debug.LogWarning($"IndustryBasse: {Name}, IndustryLVLNames == null");
            _industryLVLNames = value;
        }
    }
    private string[] _industryLVLNames;     // Array of industry names

    public const byte MIN_LVL = 1;
    public readonly byte MAX_LVL;
    public const float MIN_STAFFING = 0.0f;
    public const float MAX_STAFFING = 1.0f;
    public const float MIN_EFFECTIVENESS = 0.0f;
    public const uint MIN_EMPLOYEES = 0;
    public const float MIN_PRODUCTION_PROPORTION = 0.0f;
    public const uint MIN_WORKPLACES_PER_LVL = 1;
    public const uint MIN_WORKPLACES = 0;

    public Industry(float ProductionProportion, byte MAX_LVL, Text[] Statistics, string[] IndustryLVLNames, uint WorkplacesPerLVL = 1, float Effectiveness = 1,
                    byte Level = 1, uint EmployeesAmount = 0, bool IsEnabled = true, bool DebugLog = false)
    {
        this.MAX_LVL = MAX_LVL;
        this.Level = Level;
        this.Effectiveness = Effectiveness;
        this.ProductionProportion = ProductionProportion;
        this.EmployeesAmount = EmployeesAmount;
        this.WorkplacesPerLVL = WorkplacesPerLVL;
        this.IsEnabled = IsEnabled;
        this.IndustryLVLNames = IndustryLVLNames;
        this.Statistics = Statistics;

        UpdateNameByLVL();
        EmployeesRecalculation();
    }

    private Population _population; // Population info

    ////////////////////////////////////////////////////// INDUSTRY SPHERE AND STATS

    /// <summary>
    /// Activates current industry sphere
    /// </summary>
    public void SetIndustryEnabled() => IsEnabled = true;

    /// <summary>
    /// Deactivates current industry sphere
    /// </summary>
    public void SetIndustryDisabled() => IsEnabled = false;

    /// <summary>
    /// Updates all text fields related to industry info
    /// </summary>
    public void UpdateStatistics()
    {
        Statistics[0].text = Name;                                              // Name
        Statistics[1].text = $"{Level} / {MAX_LVL}";                            // Level
        Statistics[2].text = $"{Effectiveness * 100}%";                         // Effectiveness
        Statistics[3].text = $"{ProductionProportion}";                         // Produced resource amount
        Statistics[4].text = $"{EmployeesAmount} / {TotalWorkplacesAmount}";    // Active employees amount
    }

    ////////////////////////////////////////////////////// METHODS RELATED TO EMPLOYEES

    /// <summary>
    /// Updates employees and staffing data
    /// </summary>
    private void EmployeesRecalculation()
    {
        TotalWorkplacesAmountUpdate();
        StaffingUpdate();
    }

    /// <summary>
    /// Recalculates staffing
    /// </summary>
    private void StaffingUpdate() => Staffing = (float)(EmployeesAmount) / (float)(Level * WorkplacesPerLVL);

    /// <summary>
    /// Recalculates the max number of workplaces
    /// </summary>
    private void TotalWorkplacesAmountUpdate() => TotalWorkplacesAmount = WorkplacesPerLVL * Level;

    /// <summary>
    /// Sets a new number of employees
    /// </summary>
    /// <param name="NewEmployeesAmount"></param>
    public void SetNewEmployeesAmount(uint NewEmployeesAmount)
    {
        EmployeesAmount = NewEmployeesAmount;
        EmployeesRecalculation();
    }

    ////////////////////////////////////////////////////// INDUSTRY LEVEL AND NAME

    /// <summary>
    /// Increases the industry level
    /// </summary>
    /// <param name="diffiniteNum">The number by which the level increases</param>
    public void IncreaseLevel(byte diffiniteNum = 1)
    {
        Level += diffiniteNum;
        UpdateNameByLVL();
    }

    /// <summary>
    /// Decreases the industry level
    /// </summary>
    /// <param name="diffiniteNum">The number by which the level decreases</param>
    public void DecreaseLevel(byte diffiniteNum = 1)
    {
        Level -= diffiniteNum;
        UpdateNameByLVL();
    }

    /// <summary>
    /// Updates the name of the industry depending on it's level
    /// </summary>
    private void UpdateNameByLVL() => Name = IndustryLVLNames[Level - 1];

    ////////////////////////////////////////////////////// PRODUCTION

    /// <summary>
    /// Returns the coefficient of producible resources
    /// </summary>
    /// <returns></returns>
    private float ProducedResourcesAmount() => ConsumableResourcesAmount() * ProductionProportion;

    /// <summary>
    /// Returns the coefficient of consumable resources
    /// </summary>
    /// <returns></returns>
    private float ConsumableResourcesAmount() => Staffing * Staffing;

    /// <summary>
    /// Decreases amount of cunsumableRes., increases amount of producedRes.
    /// </summary>
    public void IndustryResourceProduction(ResourceManager.Resource[] ConsumableResources, ResourceManager.Resource[] ProducedResources, bool isLoggingEnabled = false)
    {
        bool isResourcesEnough = true;
        foreach (ResourceManager.Resource consumableResource in ConsumableResources)
        {
            if (consumableResource.Amount < ConsumableResourcesAmount())
            {
                if (isLoggingEnabled)
                    Debug.Log($"IndustryResourceProduction : not enough {consumableResource.Name} [{consumableResource.Amount}/{ConsumableResourcesAmount()}] to get produced resource(s).");
                isResourcesEnough = false;
            }
        }

        if (!isResourcesEnough)
            return;

        foreach (ResourceManager.Resource consumableResource in ConsumableResources)
        {
            consumableResource.Amount -= ConsumableResourcesAmount() * Time.deltaTime;
        }

        foreach (ResourceManager.Resource producedResource in ProducedResources)
        {
            producedResource.Amount += ProducedResourcesAmount() * Time.deltaTime;
        }
    }
}