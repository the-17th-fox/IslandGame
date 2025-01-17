﻿using System;
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
                //Debug.LogWarning($"{Name},Effectiveness: Value({value}) is less than MIN({MIN_EFFECTIVENESS})");
                _effectiveness = MIN_EFFECTIVENESS;
                //Debug.LogWarning($"{Name},Effectiveness: Eff({_effectiveness}");
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

    public int EmployeesAmount
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
    private int _employeesAmount;          // Employees amount

    public int WorkplacesPerLVL
    {
        get => _workplacesPerLVL;
        set
        {
            if (value < MIN_WORKPLACES_PER_LVL)
            {
                Debug.LogWarning($"{Name},WorkplacesPerLvl: Value({value}) is less than MIN({MIN_WORKPLACES_PER_LVL})");
                _workplacesPerLVL = MIN_WORKPLACES_PER_LVL;
            }
            else if (value > int.MaxValue)
            {
                Debug.LogWarning($"{Name},WorkplacesPerLvl: Value({value}) is greater than MAX({int.MaxValue})");
                _workplacesPerLVL = int.MaxValue;
            }
            else
                _workplacesPerLVL = value;
        }
    }
    private int _workplacesPerLVL;         // Workplaces amount/per LVL

    public int TotalWorkplacesAmount
    {
        get => _totalWorkplacesAmount;
        set
        {
            if (value < int.MinValue)
            {
                Debug.LogWarning($"{Name},TotalWorkplacesAmount: Value({TotalWorkplacesAmount}) is less than MIN({int.MinValue})");
                _totalWorkplacesAmount = int.MinValue;
            }
            else if (value > int.MaxValue)
            {
                Debug.LogWarning($"{Name},TotalWorkplacesAmount: Value({value}) is greater than MAX({int.MaxValue})");
                _totalWorkplacesAmount = int.MaxValue;
            }
            else
                _totalWorkplacesAmount = value;
        }
    }
    private int _totalWorkplacesAmount;    // Total workplaces amount

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

    public static int AllIndustriesEmployees = 0;

    public const byte MIN_LVL = 1;
    public readonly byte MAX_LVL;
    public const float MIN_STAFFING = 0.0f;
    public const float MAX_STAFFING = 1.0f;
    public const float MIN_EFFECTIVENESS = 0.1f;
    public const float MAX_EFFECTIVENESS = float.MaxValue;
    public const int MIN_EMPLOYEES = 0;
    public const float MIN_PRODUCTION_PROPORTION = 0.0f;
    public const int MIN_WORKPLACES_PER_LVL = 1;
    public const int MIN_WORKPLACES = 0;

    public IndustryCore(float ProductionProportion, byte MAX_LVL, IndustryInfo IndustryInfo, string[] IndustryLVLNames, int WorkplacesPerLVL = 1, float Effectiveness = 1,
                    byte Level = 1, int EmployeesAmount = 0, bool IsEnabled = true, bool DebugLog = false)
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
        _industryInfo.EmployeesAmount = 0;

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
        ThisIndustryInfo.EffectivenessText = $"{Math.Round(Effectiveness * 100, 1)}%";                             // Effectiveness
        ThisIndustryInfo.ProductionText = $"{Math.Round(ProducedResourcesAmount(), 1)}";                           // Produced resource amount
        ThisIndustryInfo.EmployeesAmountText = $"{EmployeesAmount} / {TotalWorkplacesAmount}";      // Active employees amount
    }

    public void GetDebugLog()
    {
        Debug.Log($"{Name} | LVL({Level}) | EMPL({EmployeesAmount}/{TotalWorkplacesAmount}/DATA {_industryInfo.EmployeesAmount}) | STFF({Staffing}) | EFF({Effectiveness}) | PROD_AM({ProducedResourcesAmount()})");
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
        EffectivenessUpdate();
    }

    /// <summary>
    /// Recalculates staffing
    /// </summary>
    private float StaffingUpdate() => Staffing = (float)(EmployeesAmount) / (float)(Level * WorkplacesPerLVL);

    /// <summary>
    /// Recalculates the max number of workplaces
    /// </summary>
    private void TotalWorkplacesAmountUpdate() => TotalWorkplacesAmount = WorkplacesPerLVL * Level;

    /// <summary>
    /// Recalculates the number of employees
    /// </summary>
    private void EmployeesAmountUpdate()
    {
        // TODO: Сделать чтобы при убыли населения уменьшались и рабочие

        Population.EmployablePopulationUpdate();
        if (Population._EmployedPopulation < AllIndustriesEmployees && EmployeesAmount > 0)
        {
            int freeEmployees = (int)(Population._WorkablePopulation - Population._EmployedPopulation);
            int employeesBoundaries = (int)Population._EmployedPopulation;

            int deltaEmployees = EmployeesAmount - employeesBoundaries;
            if (deltaEmployees < 0)
                Debug.LogWarning(deltaEmployees);

            if (AllIndustriesEmployees > employeesBoundaries && freeEmployees == 0)
            {
                if (deltaEmployees > EmployeesAmount)
                    DecreaseEmployeesAmount(1);
                else
                    DecreaseEmployeesAmount(deltaEmployees);
                EmployeesAmount = _industryInfo.EmployeesAmount;
            }
        }

        else if (EmployeesAmount != _industryInfo.EmployeesAmount)
            EmployeesAmount = _industryInfo.EmployeesAmount;
    }

    /// <summary>
    /// Recalculates the effectiveness
    /// </summary>
    private float EffectivenessUpdate() => Effectiveness = 2 * (Population._EducationLevel * Population._MedicineLevel);

    /// <summary>
    /// Decreases eployees amount
    /// </summary>
    /// <param name="value"></param>
    private void DecreaseEmployeesAmount(int value)
    {
        _industryInfo.EmployeesAmount -= value;
        AllIndustriesEmployees -= value;
    }

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