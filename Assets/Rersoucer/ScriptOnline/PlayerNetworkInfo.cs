using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerNetworkInfo : INetworkStruct
{
    //public string name;
    public float currentHealth;
    public float mana;
    public float score;
}
