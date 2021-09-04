using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    static float _Time = -1; 
    static public bool SecondGone() 
    {
        if (_Time <= 0)
        {
            _Time = 1;
            //Debug.Log("timer");
            return true;
        }
        else 
        {
            _Time -= Time.deltaTime;
            return false;
        }
    } 
}
