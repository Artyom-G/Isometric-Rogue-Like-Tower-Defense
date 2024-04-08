using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    [Header("General")]
    [Range(0, 5)]
    public float speed = 1;

    [Header("Debug")]
    public Transform Target;
    GeneralProjectileInformation gpi;
    public int damagePerHit = 1;

    void Start(){
        gpi = GetComponent<GeneralProjectileInformation>();
        Target = gpi.Target;
        damagePerHit = gpi.damagePerHit;
    }

    void Update()
    {
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = Target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }

    void HitTarget()
    {
        GameObject _hitObj = Target.transform.parent.gameObject;
        _hitObj.GetComponent<HealthManager>().OnHit(gameObject, damagePerHit);
        _hitObj.GetComponent<GeneralEnemyInformation>().ApplyEffect(gpi.TurretHost.effectIndex, gpi.TurretHost.effectDuration);
        Destroy(gameObject);
    }
}
