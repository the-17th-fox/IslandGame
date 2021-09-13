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

    private const byte MIN_LVL = 1;
    private const byte MAX_LVL = 4;
    
    private const float MIN_STAFFING = 0.0f;
    private const float MAX_STAFFING = 1.0f;

    private const float MIN_EFFECTIVENESS = 0.0f;

    private const uint MIN_EMPLOYEES = 0;
    private const float MIN_PRODUCTION_PROPORTION = 0.0f;
    private const uint MIN_WORKPLACES_PER_LVL = 0;
    private const uint MIN_WORKPLACES = 0;

    public void CreateNewIndustry(string Name, uint WorkplacesPerLVL, float ProductionProportion, float Effectiveness = 1, byte Level = 1, uint EmployeesAmount = 0,  bool isEnabled = true)
    {
        _isEnabled = isEnabled;
        _name = Name;   

        if (Level < MIN_LVL || Level > MAX_LVL)
        {
            Debug.LogError($"CreateNewIndustry : Level({Level}) < MinLVL{MIN_LVL} or > MaxLVL{MAX_LVL}");
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

    public void SetIndustryEnabled() => _isEnabled = true;

    public void SetIndustryDisabled() => _isEnabled = false;

    public bool IsEnabled() => _isEnabled ? true : false;

/////////////////////////// РАБОТНИКИ

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

    /////////////////////////// УРОВЕНЬ ПРОИЗВОДСТВА

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
    }

    /////////////////////////// ПРОИЗВОДСТВО

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
    public void IndustryResourceProduction(Resource[] ConsumableResources, Resource[] ProducedResources, BasicIndustrialSector industry)
    {
        if (Timer.SecondGone())
        {
            bool isResourcesEnough = true;
            foreach (Resource neededResource in ConsumableResources)
            {
                if (neededResource._amount < ConsumableResourcesAmount())
                {
                    Debug.Log($"{industry._name}: not enough {neededResource._name} [{neededResource._amount}/{ConsumableResourcesAmount()}] to get produced resource(s).");
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
