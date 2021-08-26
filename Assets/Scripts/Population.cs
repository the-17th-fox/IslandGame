using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour
{
    private uint _population=10000;
    private double _IncreasePopulationModificator=0.0002;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _population+=
        
    }
    int IncreasePopulation() 
    {
        return (int)(_IncreasePopulationModificator * _population);
    }
}
