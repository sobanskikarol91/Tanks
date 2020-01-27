using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game")]
public class GameSettings : ScriptableObject 
{
    public AudioClip winSnd;
    public AudioClip loseSnd;
}