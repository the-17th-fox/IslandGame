using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//interface IIndustrySector
//{
//    void CreateSector(params int[] list);
//}

public class BasicIndustrySector : MonoBehaviour
{
    public byte _level { protected set; get; } // уровень отрасли
    public string _name { protected set; get; } // название отрасли
    public float _effectivenes { protected set; get; } // коэффициент эффективности производства
    public float _productionProportion { protected set; get; } // пропорция вход-выход
    public float _staffing { protected set; get; } // укомплектованность рабочими (от 0 до 1)
    public uint _employees { protected set; get; } // кол-во работников

    public List <byte> _neededResourceID { set; get; } // необходимые ресурсы для производства
    private const uint _workplacePerLVL= 1000; // кол-во рабочих мест на каждый уровень

    public void CreateNewSector(string Name, byte Level, float Effectivenes, float ProductionCoefficient, uint Employees, params byte[] IDsResource )
    {
        _name = Name;
        _level = Level;
        _effectivenes = Effectivenes;
        _productionProportion = ProductionCoefficient;
        _employees = Employees;
        _staffing = _employees / (_workplacePerLVL * _level);
        _neededResourceID = new List<byte>();

        foreach (byte ID in IDsResource)
        {
            _neededResourceID.Add(ID);
        }
    }
    public void StaffingUpdate() // перерасчёт укомплектованности
    {
        _staffing= _employees/(_workplacePerLVL * _level); 
    }
    public float ProductionAmount() // кол-во произведенных ресурсов
    {
        return ProductionCost() * _productionProportion; 
    }
    public float ProductionCost() // то, сколько ресурсов будет потрачено
    {
        return _staffing * _effectivenes; //дописать стракт что бы связать коофицент и IDResource?
    }
    public void IncreaseLevel(int DeltaLVL) // повышение уровня отрасли
    {
        if ((int)_level + DeltaLVL > byte.MaxValue) 
        {
            Debug.Log("__ already got max level");
            return;
        }
        _level +=(byte) DeltaLVL;
    }
    public void DecreaseLevel(int DeltaLVL) // понижение уровня отрасли
    {
        if ((int)_level - DeltaLVL < byte.MinValue)
        {
            Debug.Log("__ already has minimal level");
            return;
        }
        _level -= (byte)DeltaLVL;

    }
    public void SetNewEmployeesAmount(uint NewEmployedAmount)  // установить новое кол-во работников
    {
        _employees = NewEmployedAmount;
    }
}

//public class TimberIndustrySector : BasicIndustrySector, IIndustrySector
//{
//    public void CreateSector(params int[] list)
//    {
//        createNewSector("Timber idustry sector", 1, 1, 0.7f, 0);
//        for (int i = 0; i < list.Length; i++)
//        {
//            _IDneededResource.Add(list[i]);
//        }
//    }
//}
