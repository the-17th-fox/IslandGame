//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class Factories : MonoBehaviour
//{
//    [SerializeField]
//    protected byte thisFactoryID;

//    private WoodFactory woodFactory;
//    private FoodFactory foodFactory;

//    private object[] factories;

//    private void Start()
//    {
//        woodFactory = gameObject.AddComponent<WoodFactory>();
//        foodFactory = gameObject.AddComponent<FoodFactory>();

//        if (thisFactoryID < 0 || thisFactoryID > factories.Length - 1)
//        {
//            Debug.LogError("Неверный ID фабрики.");
//        }
//    }

//    private void Update()
//    {

//    }
//}

//public class BasicFactory : Factories
//{
//    protected byte FACTORY_ID;
//    protected byte _level;
//    protected float _effectivenes;
//    protected float _productionCoefficient;
//    protected byte _staffing;
//    protected uint _employees;
//    public virtual void Production(Resource[] resources)
//    { }
//}

//public class WoodFactory : BasicFactory
//{
//    new private byte FACTORY_ID = 0;
//    new private byte _level; //уровень - вляет на количество рабочих мест
//    new private float _effectivenes = 1;//эффективность работы каждого рабочего
//    new private float _productionCoefficient = 0.7f; //70% эффективность обратки
//    new private byte _staffing = 50;//укомплектованность рабочими
//    new private uint _employees = 1000;// количество рабочих в сфере

//    public override void Production(Resource[] resources)
//    {
//        if (resources[0]._amount < _level * Time.deltaTime)
//        { 
//            Debug.LogWarning("Ошибка в производстве фабрики дерева");
//            return;
//        }
//        //inputResource._amount -= _staffing * _effectivenes * _level * Time.deltaTime;
//        //outputResource._amount += ((_staffing * _productionCoefficient) * _effectivenes * _level) * Time.deltaTime;
//    }
//    public void StaffingUpdate(WoodFactory[] woodFactories) 
//    {
//        //uint generalLVL=0;
//        //foreach (var woodFactory in woodFactories)
//        //{
//        //    generalLVL += woodFactory._level;
//        //}
//        //uint workplaces = generalLVL * 1000;
//        //_staffing = (byte)(workplaces / _employees);
//    }
//}

//public class FoodFactory : BasicFactory
//{
//    new private byte FACTORY_ID = 1;
//    new private byte _level;
//    new private float _effetivenes = 1;
//    new private float _productionCoefficient;

//    public override void Production(Resource[] resources)
//    {
        
//    }
//}
