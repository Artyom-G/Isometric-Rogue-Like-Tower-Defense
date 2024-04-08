using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FindTargetTurret))]
public class FieldShootTurret : MonoBehaviour
{
    [Header("General")]
    public GameObject FieldPrefab;

    [Header("References")]
    public AimTurret aim;
    public AnimatorTurret anim;
    
    //General Turret Information
    GeneralTurretInformation gti;
    int damagePerHit;
    float timeBetweenHits;
    float hit_timer = 0;

    FindTargetTurret findTarget;
    GeneralEnemyInformation gei;

    [Header("Debug")]
    public Transform Target;
    public Vector3 oldTarget = new Vector3(-200, -100, 0);

    void Awake(){
        gti = GetComponent<GeneralTurretInformation>();
        findTarget = GetComponent<FindTargetTurret>();
        damagePerHit = gti.damagePerHit;
        timeBetweenHits = gti.timeBetweenHits;

        anim.OnShoot += Shoot;
        anim.StartCoroutine(anim.ReloadAnimation());
    }
    void Update(){
        Target = findTarget.Target;
        if(Target != null) oldTarget = Target.position;
        aim.Aim(oldTarget);

        hit_timer += Time.deltaTime;
        if(hit_timer >= timeBetweenHits){
            if(Target != null){
                hit_timer = 0;
                anim.state = AnimatorTurret.State.Shoot;
                anim.StopAllCoroutines();
                anim.StartCoroutine(anim.ShootAnimation(anim.shootIndex));
            }
            else{
                anim.state = AnimatorTurret.State.Idle;
            }   
        }
    }
    public void Shoot(){
        GameObject _field = Instantiate(FieldPrefab);
        _field.transform.position = anim.animation[anim.animationIndex].Shoot[anim.shootIndex].projectileInitialLocation + transform.position;
        AreaEffect _ae = _field.GetComponent<AreaEffect>();
        _ae.damage = damagePerHit;
        _ae.TurretHost = gti;
    }
    void OnDisable(){
        anim.OnShoot -= Shoot;
    }
}
