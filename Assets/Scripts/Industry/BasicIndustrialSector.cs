using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicIndustrialSector : MonoBehaviour
{
    public byte _lvl { private set; get; }// уровень отрасли
    //private string _name; // название отрасли
    public float _effectiveness { private set; get; } // коэффициент эффективности производства
    public float _productionProportion { private set; get; } // пропорция вход-выход
    public float _staffing { private set; get; } // укомплектованность рабочими (от 0 до 1)
    public uint _employeesAmount { private set; get; } // кол-во работников
    public uint _workplacesPerLVL { private set; get; } // кол-во рабочих мест на каждый уровень
    public bool _isEnabled { private set; get; } // включена ли пром-ность    

    [SerializeField]
    private Island island;

    public void CreateNewIndustry(byte Level, float Effectiveness, float ProductionProportion, uint EmployeesAmount, uint WorkplacesPerLVL, bool isEnabled)
    {
        _lvl = Level;
        _effectiveness = Effectiveness;
        _productionProportion = ProductionProportion;
        _employeesAmount = EmployeesAmount;
        _workplacesPerLVL = WorkplacesPerLVL;
        _isEnabled = isEnabled;
    }

    /// <summary>
    /// Перерасчитывает укомплектованность
    /// </summary>
    public void StaffingUpdate()
    {
        _staffing = (float)(_employeesAmount) / (float)(_lvl * _workplacesPerLVL);

        if (_staffing > 1 || _staffing < 0)
            Debug.LogError($"Staffing = [{_staffing}/1.0]");
    }

    /// <summary>
    /// Кол-во произведенных ресурсов
    /// </summary>
    /// <returns></returns>
    private float ProductionAmount() => ProductionCost() * _productionProportion;

    /// <summary>
    /// Возвращает кол-во ресурсов, которое нужно затратить/прибавить при производстве за раз
    /// </summary>
    /// <returns></returns>
    private float ProductionCost() => _staffing * _effectiveness;

    /// <summary>
    /// Устанавливает новое кол-во работников
    /// </summary>
    /// <param name="NewEmployedAmount"></param>
    public void SetNewEmployeesAmount(uint NewEmployedAmount) => _employeesAmount = NewEmployedAmount;

    /// <summary>
    /// Повышает уровень отрасли
    /// </summary>
    /// <param name="DeltaLVL"></param>
    public void IncreaseLevel(int DeltaLVL)
    {
        if ((int)_lvl + DeltaLVL > byte.MaxValue) 
        {
            Debug.Log("Already has a MAX LVL");
            return;
        }
        _lvl +=(byte) DeltaLVL;
    }

    /// <summary>
    /// Понижает уровень отрасли
    /// </summary>
    /// <param name="DeltaLVL"></param>
    public void DecreaseLevel(int DeltaLVL)
    {
        if ((int)_lvl - DeltaLVL < byte.MinValue)
        {
            Debug.Log("Already has a MIN LVL");
            return;
        }
        _lvl -= (byte)DeltaLVL;
    }

    /// <summary>
    /// Производит ресурс(ы) _producedResources[] за счёт ресурс(ов) _consumableResources[]
    /// </summary>
    public void IndustryResourceProduction(Resource[] ConsumableResources, Resource[] ProducedResources)
    {
        if (Timer.SecondGone())
        {
            foreach (Resource neededResource in ConsumableResources)
            {
                if (neededResource._amount < ProductionCost())
                {
                    Debug.Log($"Not enough: {neededResource._name} [{neededResource._amount}/{ProductionCost()}] to get produced resource(s).");
                    return;
                }
            }

            foreach (Resource consumableResource in ConsumableResources)
            {
                consumableResource._amount -= ProductionCost();
            }

            foreach (Resource producedResource in ProducedResources)
            {
                producedResource._amount += ProductionAmount();
            }
        }
    }
}
