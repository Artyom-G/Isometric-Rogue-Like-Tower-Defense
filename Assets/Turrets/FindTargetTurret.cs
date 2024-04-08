using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTargetTurret : MonoBehaviour
{
    [Header("Debug")]
    //public List<Transform> ReachableTargets = new List<Transform>();
    public Transform Target;
    public Vector3 targetDir;

    //General Turret Information
    GeneralTurretInformation gri;

    void Awake(){
        gri = GetComponent<GeneralTurretInformation>();
    }
    void OnTriggerStay2D(Collider2D collision){
        //Add layer check
        Target = collision.transform.GetChild(0);
    }
    void OnTriggerExit2D(Collider2D collision){
        Target = null;
    }
}

