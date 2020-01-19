using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;

public class EnviromentManager : MonoBehaviour, IRestart
{
    [SerializeField] WoodWall woodWall;

    public void Restart()
    {
        woodWall.Restart();
    }
}