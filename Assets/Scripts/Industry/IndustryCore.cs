using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndustryCore
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
                Debug.LogWarning($"{Name},LVL: Value({value}) is less than MIN({MIN_LVL})");
                _lvl = MIN_LVL;
            }
            else if (value > MAX_LVL)
            {
                Debug.LogWarning($"{Name},LVL: Value({value}) is greater than MAX({MAX_LVL})");
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
                Debug.LogWarning($"{Name},Effectiveness: Value({value}) is less than MIN({MIN_EFFECTIVENESS})");
                _effectiveness = MIN_EFFECTIVENESS;
            }
            else if (value > MAX_EFFECTIVENESS)
            {
                Debug.LogWarning($"{Name},Effectiveness: Value({value}) is greater than MAX({MAX_EFFECTIVENESS})");
                _effectiveness = MAX_EFFECTIVENESS;
            }
            else
                _effectiveness = value;
        }
    }
    private float _effectiveness;           // Industry effectiveness (in the future will be changed by technologies)

    public float ProductionProportion
    {
        get => _productionProportion;
        set
        {
            if (value < MIN_PRODUCTION_PROPORTION)
            {
                Debug.LogWarning($"{Name},Prod.proportion: Value({value}) is less than MIN({MIN_PRODUCTION_PROPORTION})");
                _productionProportion = MIN_PRODUCTION_PROPORTION;
            }
            else if (value > float.MaxValue)
            {
                Debug.LogWarning($"{Name},Prod.proportion: Value({value}) is greater than MAX({float.MaxValue})");
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
                Debug.LogWarning($"{Name},Staffing: Value({value}) is less than MIN({MIN_STAFFING})");
                _staffing = MIN_STAFFING;
            }
            else if (value > MAX_STAFFING)
            {
                Debug.LogWarning($"{Name},Staffing: Value({value}) is greater than MAX({MAX_STAFFING})");
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
                Debug.LogWarning($"{Name}, Empl.amount: Value({value}) is greater than MAX({TotalWorkplacesAmount})");
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
                Debug.LogWarning($"{Name},WorkplacesPerLvl: Value({value}) is less than MIN({MIN_WORKPLACES_PER_LVL})");
                _workplacesPerLVL = MIN_WORKPLACES_PER_LVL;
            }
            else if (value > uint.MaxValue)
            {
                Debug.LogWarning($"{Name},WorkplacesPerLvl: Value({value}) is greater than MAX({uint.MaxValue})");
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
                Debug.LogWarning($"{Name},TotalWorkplacesAmount: Value({TotalWorkplacesAmount}) is less than MIN({uint.MinValue})");
                _totalWorkplacesAmount = uint.MinValue;
            }
            else if (value > uint.MaxValue)
            {
                Debug.LogWarning($"{Name},TotalWorkplacesAmount: Value({value}) is greater than MAX({uint.MaxValue})");
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

    public IndustryInfo ThisIndustryInfo
    {
        get => _industryInfo;
        set
        {
            if (value == null)
                Debug.LogWarning($"IndustryBase: {Name}, IndustryInfo == null");
            else
                _industryInfo = value;
        }
    }
    private IndustryInfo _industryInfo; // Object that's stores all info-fields of difinite industry

    public string[] IndustryLVLNames
    {
        get => _industryLVLNames;
        set
        {
            if(value == null)
                Debug.LogWarning($"IndustryBase: {Name}, IndustryLVLNames == null");
            _industryLVLNames = value;
        }
    }
    private string[] _industryLVLNames;     // Array of industry names

    public bool DebugLog
    {
        get => _debugLog;
        set => _debugLog = value;
    }
    private bool _debugLog;

    public const byte MIN_LVL = 1;
    public readonly byte MAX_LVL;
    public const float MIN_STAFFING = 0.0f;
    public const float MAX_STAFFING = 1.0f;
    public const float MIN_EFFECTIVENESS = 0.1f;
    public const float MAX_EFFECTIVENESS = float.MaxValue;
    public const uint MIN_EMPLOYEES = 0;
    public const float MIN_PRODUCTION_PROPORTION = 0.0f;
    public const uint MIN_WORKPLACES_PER_LVL = 1;
    public const uint MIN_WORKPLACES = 0;

    public IndustryCore(float ProductionProportion, byte MAX_LVL, IndustryInfo IndustryInfo, string[] IndustryLVLNames, uint WorkplacesPerLVL = 1, float Effectiveness = 1,
                    byte Level = 1, uint EmployeesAmount = 0, bool IsEnabled = true, bool DebugLog = false)
    {
        this.IndustryLVLNames = IndustryLVLNames;
        Name = IndustryLVLNames[0]; // Temp name for debbuging, will be replaced in the end

        this.MAX_LVL = MAX_LVL;
        this.Level = Level;
        this.WorkplacesPerLVL = WorkplacesPerLVL;
        TotalWorkplacesAmountUpdate();
        this.EmployeesAmount = EmployeesAmount;

        this.Effectiveness = Effectiveness;
        this.ProductionProportion = ProductionProportion;
        this.IsEnabled = IsEnabled;
        
        this.ThisIndustryInfo = IndustryInfo;

        UpdateNameByLVL();
        StaffingUpdate();

        this.DebugLog = DebugLog;
    }

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
        ThisIndustryInfo.NameText = Name;                                                           // Name
        ThisIndustryInfo.LevelText = $"{Level} / {MAX_LVL}";                                        // Level
        ThisIndustryInfo.EffectivenessText = $"{Effectiveness * 100}%";                             // Effectiveness
        ThisIndustryInfo.ProductionText = $"{ProducedResourcesAmount()}";                                // Produced resource amount
        ThisIndustryInfo.EmployeesAmountText = $"{EmployeesAmount} / {TotalWorkplacesAmount}";      // Active employees amount
    }

    public void GetDebugLog()
    {
        Debug.Log($"{Name} | LVL({Level}) | EMPL({EmployeesAmount}/{TotalWorkplacesAmount}) | STFF({Staffing}) | EFF({Effectiveness}) | PROD_AM({ProductionProportion})");
    }

    ////////////////////////////////////////////////////// METHODS RELATED TO EMPLOYEES

    /// <summary>
    /// Updates employees and staffing data
    /// </summary>
    public void EmployeesRecalculation()
    {
        TotalWorkplacesAmountUpdate();
        EmployeesAmountUpdate();
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
    /// Recalculates the number of employees
    /// </summary>
    private void EmployeesAmountUpdate() => EmployeesAmount = (uint)Population.DeployEmployablePopulation(TotalWorkplacesAmount);

    /// <summary>
    /// Sets a new number of employees
    /// </summary>
    /// <param name="NewEmployeesAmount"></param>
    public void SetNewEmployeesAmount(uint NewEmployeesAmount)
    {
        EmployeesAmount = NewEmployeesAmount;
        EmployeesRecalculation();
    }

    /// <summary>
    /// Recalculates the effectiveness
    /// </summary>
    private void EffectivenessUpdate() => Effectiveness = 2 * (Population._EducationLevel * Population._MedicineLevel);

    ////////////////////////////////////////////////////// INDUSTRY LEVEL AND NAME

    /// <summary>
    /// Increases the industry level
    /// </summary>
    /// <param name="definiteNum">The number by which the level increases</param>
    public void IncreaseLevel(byte definiteNum = 1)
    {
        Level += definiteNum;
        UpdateNameByLVL();
    }

    /// <summary>
    /// Decreases the industry level
    /// </summary>
    /// <param name="definiteNum">The number by which the level decreases</param>
    public void DecreaseLevel(byte definiteNum = 1)
    {
        Level -= definiteNum;
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
    private float ConsumableResourcesAmount() => Effectiveness * Staffing;

    /// <summary>
    /// Decreases amount of cunsumableRes., increases amount of producedRes.
    /// </summary>
    public void ResourceProduction(ResourceManager.Resource[] ConsumableResources, ResourceManager.Resource[] ProducedResources, bool isLoggingEnabled = false)
    {
        bool isResourcesEnough = true;
        foreach (ResourceManager.Resource consumableResource in ConsumableResources)
        {
            if (consumableResource.Amount < ConsumableResourcesAmount())
            {
                if (isLoggingEnabled)
                    Debug.Log($"ResourceProduction : not enough {consumableResource.Name} [{consumableResource.Amount}/{ConsumableResourcesAmount()}] to get produced resource(s).");
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