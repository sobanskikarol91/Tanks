using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Method : MonoBehaviour 
{
    public static string GetName(Action method)
    {
       return method.Method.Name;
    }
}