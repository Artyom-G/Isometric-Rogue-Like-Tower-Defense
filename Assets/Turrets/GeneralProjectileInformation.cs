using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralProjectileInformation : MonoBehaviour
{

    [Header("Debug")]
    public Transform Target;
    
    //For predicting enemy movement
    public Vector3 targetPos;
    public Vector3 targetDir;

    public GeneralTurretInformation TurretHost;
    
    public int damagePerHit;

}
