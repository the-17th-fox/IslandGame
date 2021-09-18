using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicIndustrialSector : MonoBehaviour
{
    private string _name; // название индустрии
    private byte _lvl; // уровень отрасли
    private float _effectiveness;
    private float _productionProportion; // коэффициент пропорции ресурса на выход
    private float _staffing; // укомплектованность рабочими (от 0 до 1)
    private uint _employeesAmount; // кол-во работников
    private uint _workplacesPerLVL; // кол-во рабочих мест на каждый уровень
    private uint _totalWorkplacesAmount; // кол-во рабочих мест в в сумме
    private bool _isEnabled; // включена ли пром-ность

    private string[] IndustryLVLNames; // имя сферы с каждым уровнем

    private const byte MIN_LVL = 1;
    private byte MAX_LVL;
    
    private const float MIN_STAFFING = 0.0f;
    private const float MAX_STAFFING = 1.0f;

    private const float MIN_EFFECTIVENESS = 0.0f;

    private const uint MIN_EMPLOYEES = 0;
    private const float MIN_PRODUCTION_PROPORTION = 0.0f;
    private const uint MIN_WORKPLACES_PER_LVL = 0;
    private const uint MIN_WORKPLACES = 0;

    /// <summary>
    /// Метод для создания сферы и установки значений для полей.
    /// </summary>
    /// <param name="WorkplacesPerLVL"></param>
    /// <param name="ProductionProportion"></param>
    /// <param name="MaxLevelConst"></param>
    /// <param name="Effectiveness"></param>
    /// <param name="Level"></param>
    /// <param name="EmployeesAmount"></param>
    /// <param name="isEnabled"></param>
    public void CreateNewIndustry(uint WorkplacesPerLVL, float ProductionProportion, byte MaxLevelConst,
                                    float Effectiveness = 1, byte Level = 1, uint EmployeesAmount = 0,  bool isEnabled = true)
    {
        _isEnabled = isEnabled;

        if (MaxLevelConst < MIN_LVL || MaxLevelConst > byte.MaxValue)
        {
            Debug.LogError($"CreateNewIndustry : MaxLevelConst({MaxLevelConst}) < MIN_LVL({MIN_LVL}) or > byteMaxValue");
            SetIndustryDisabled();
        }
        MAX_LVL = MaxLevelConst;
        IndustryLVLNames = new string[MAX_LVL];

        if (Level < MIN_LVL || Level > MAX_LVL)
        {
            Debug.LogError($"CreateNewIndustry : Level({Level}) | MinLVL({MIN_LVL}) | MaxLVL({MAX_LVL})");
            SetIndustryDisabled();
        }
        _lvl = Level;

        if (ProductionProportion < MIN_PRODUCTION_PROPORTION || ProductionProportion > float.MaxValue)
        {
            Debug.LogError($"CreateNewIndustry : ProductionProportion({ProductionProportion}) < MinProdProp({MIN_PRODUCTION_PROPORTION}) or > floatMaxValue");
            SetIndustryDisabled();
        }
        _productionProportion = ProductionProportion;

        if (EmployeesAmount < MIN_EMPLOYEES || EmployeesAmount > uint.MaxValue)
        {
            Debug.LogError($"CreateNewIndustry : EmployeesAmount({EmployeesAmount}) < MinEmployees({MIN_EMPLOYEES}) or > uintMaxValue");
            SetIndustryDisabled();
        }
        _employeesAmount = EmployeesAmount;

        if(WorkplacesPerLVL < MIN_WORKPLACES_PER_LVL || WorkplacesPerLVL > uint.MaxValue)
        {
            Debug.LogError($"CreateNewIndustry : WorkplacesPerLVL({WorkplacesPerLVL}) < MinWorkplacesPerLVL({MIN_WORKPLACES_PER_LVL}) or > uintMaxValue");
            SetIndustryDisabled();
        }
        _workplacesPerLVL = WorkplacesPerLVL;

        if (Effectiveness < MIN_EFFECTIVENESS || Effectiveness > float.MaxValue)
        {
            Debug.LogError($"CreateNewIndustry : Effectiveness({Effectiveness}) < MinEffectiveness({MIN_EFFECTIVENESS}) or > floatMaxValue");
            SetIndustryDisabled();
        }
        _effectiveness = Effectiveness;

        EmployeesRecalculation();
    }

////////////////////////////////////////////////////// АКТИВНОСТЬ СФЕРЫ

    /// <summary>
    /// Активировать сферу
    /// </summary>
    public void SetIndustryEnabled() => _isEnabled = true;

    /// <summary>
    /// Деактивировать сферы
    /// </summary>
    public void SetIndustryDisabled() => _isEnabled = false;

    /// <summary>
    /// Проверка на активность сферы
    /// </summary>
    /// <returns></returns>
    public bool IsEnabled()
    {
        if (IndustryLVLNames == null)
        {
            Debug.LogError($"IsEnabled : IndustryLVLNamesArray = null.");
            SetIndustryDisabled();
        }

        if (_isEnabled) return true;
        else return false;
    }

////////////////////////////////////////////////////// МЕТОДЫ, КАСАЮЩИЕСЯ РАБОТНИКОВ

    /// <summary>
    /// Пересчитывает кол-во работников, делает проверку на корректность данных
    /// </summary>
    private void EmployeesRecalculation()
    {
        TotalWorkplacesAmountUpdate();
        if (_employeesAmount < MIN_EMPLOYEES || _employeesAmount > _totalWorkplacesAmount)
        {
            Debug.LogError($"EmployeesRecalculation : EmployeesAmount({_employeesAmount}) | MinEmployees({MIN_EMPLOYEES}) | Workplaces({_totalWorkplacesAmount})");
            SetIndustryDisabled();
        }

        StaffingUpdate();
        if (_staffing < MIN_STAFFING || _staffing > MAX_STAFFING)
        {
            Debug.LogError($"EmployeesRecalculation : Staffing({_staffing}) | MinStaffing({MIN_STAFFING}) | MaxStaffing({MAX_STAFFING})");
            SetIndustryDisabled();
        }     
    }

    /// <summary>
    /// Перерасчитывает укомплектованность
    /// </summary>
    private void StaffingUpdate() => _staffing = (float) (_employeesAmount) / (float) (_lvl* _workplacesPerLVL);

    /// <summary>
    /// Перерасчитывает максимальное кол-во мест
    /// </summary>
    private void TotalWorkplacesAmountUpdate() => _totalWorkplacesAmount = _workplacesPerLVL * _lvl;

    /// <summary>
    /// Устанавливает новое кол-во работников
    /// </summary>
    /// <param name="NewEmployeesAmount"></param>
    public void SetNewEmployeesAmount(uint NewEmployeesAmount)
    {
        if (_employeesAmount + NewEmployeesAmount > _totalWorkplacesAmount || NewEmployeesAmount < MIN_EMPLOYEES)
        {
            Debug.LogError($"SetNewEmployeesAmount : NewEmployeesAmount({NewEmployeesAmount}) | TotalWorkplacesAmount({_totalWorkplacesAmount}) | MinEmployees({MIN_EMPLOYEES})");
            return;
        }
        _employeesAmount = NewEmployeesAmount;
        EmployeesRecalculation();
    }

////////////////////////////////////////////////////// УРОВЕНЬ ПРОИЗВОДСТВА И НАЗВАНИЕ СФЕРЫ

    /// <summary>
    /// Повышает уровень отрасли
    /// </summary>
    /// <param name="DeltaLVL"></param>
    public void IncreaseLevel()
    {
        if (_lvl + 1 > MAX_LVL)
        {
            Debug.LogWarning("Already has a MAX LVL");
            return;
        }
        _lvl += 1;
        UpdateNameByLVL(IndustryLVLNames);
    }

    /// <summary>
    /// Понижает уровень отрасли
    /// </summary>
    /// <param name="DeltaLVL"></param>
    public void DecreaseLevel()
    {
        if (_lvl - 1 < MIN_LVL)
        {
            Debug.Log("Already has a MIN LVL");
            return;
        }
        _lvl -= 1;
        UpdateNameByLVL(IndustryLVLNames);
    }

    /// <summary>
    /// Создает массив имён сферы производства для каждого уровня
    /// </summary>
    /// <param name="levelNames"></param>
    public void CreateArrayOfNamesByLVL(params string[] levelNames)
    {
        if (levelNames.Length != MAX_LVL)
        {
            Debug.LogError($"CreateIndustryNameByLVL : Not all levels got their names. NamesAmount: {levelNames.Length} | Levels: {MAX_LVL}");
            return;
        }

        for (int i = 0; i < levelNames.Length; i++)
        {
            IndustryLVLNames[i] = levelNames[i];
        }

        UpdateNameByLVL(IndustryLVLNames);
    }

    /// <summary>
    /// Обновляет имя сферы в зависимости от уровня
    /// </summary>
    /// <param name="industryLVLNames"></param>
    /// <param name="isLoggingEnabled"></param>
    private void UpdateNameByLVL(string[] industryLVLNames, bool isLoggingEnabled = false)
    {
        _name = industryLVLNames[_lvl - 1];
        if (isLoggingEnabled) Debug.Log($"UpdateNameByLVL : Name: '{_name}' | Level: '{_lvl}'");
    }

////////////////////////////////////////////////////// ПРОИЗВОДСТВО

    /// <summary>
    /// Кол-во произведенных ресурсов
    /// </summary>
    /// <returns></returns>
    private float ProducedResourcesAmount() => ConsumableResourcesAmount() * _productionProportion;

    /// <summary>
    /// Возвращает кол-во ресурсов, которое нужно затратить/прибавить при производстве за раз
    /// </summary>
    /// <returns></returns>
    private float ConsumableResourcesAmount() => _staffing * _effectiveness;

    /// <summary>
    /// Производит ресурс(ы) _producedResources[] за счёт ресурс(ов) _consumableResources[]
    /// </summary>
    public void IndustryResourceProduction(Resource[] ConsumableResources, Resource[] ProducedResources, bool isLoggingEnabled = false)
    {
        if (Timer.SecondGone())
        {
            bool isResourcesEnough = true;
            foreach (Resource consumableResource in ConsumableResources)
            {
                if (consumableResource._amount < ConsumableResourcesAmount())
                {
                    if(isLoggingEnabled)
                        Debug.Log($"IndustryResourceProduction : not enough {consumableResource._name} [{consumableResource._amount}/{ConsumableResourcesAmount()}] to get produced resource(s).");
                    isResourcesEnough = false;
                }
            }

            if (!isResourcesEnough) return;

            foreach (Resource consumableResource in ConsumableResources)
            {
                consumableResource._amount -= ConsumableResourcesAmount();
            }

            foreach (Resource producedResource in ProducedResources)
            {
                producedResource._amount += ProducedResourcesAmount();
            }
        }
    }
}
