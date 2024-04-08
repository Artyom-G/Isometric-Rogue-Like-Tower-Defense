using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralTurretInformation : MonoBehaviour
{
    [Header("Turret")]
    [Range(0, 10.0f)]
    public float timeBetweenHits = 1.0f;
    public int cost;
    [Range(0, 10)]
    public int damagePerHit = 1;

    //Effects
    [Range(-1, 3)]
    [Tooltip("-1. NONE\n0. Ice\n1. Fire\n2. Holy")]
    public int effectIndex;
    [Range(0, 10.0f)]
    public float effectDuration;

    [Header("Money Generator")]
    public int generateMoney = 0;
    public float timeBetweenMoneyGeneration = 0;

    [Header("Debug")]
    public List<Transform> ReachableTargets = new List<Transform>();

    public void ApplyEffect(GeneralEnemyInformation gei){
        if(effectIndex >= 0) gei.ApplyEffect(effectIndex, effectDuration);
    }
}
