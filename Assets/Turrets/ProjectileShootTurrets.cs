using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FindTargetTurret))]
public class ProjectileShootTurrets : MonoBehaviour
{
    [Header("General")]
    public GameObject projectilePrefab;

    [Header("References")]
    public AimTurret aim;
    public AnimatorTurret anim;
    public AudioManager am;
    
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
        aim.AimAngle();
        //anim.StartCoroutine(anim.ReloadAnimation());
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
        GameObject _projectile = Instantiate(projectilePrefab);
        _projectile.transform.position = anim.animation[anim.animationIndex].Shoot[anim.shootIndex].projectileInitialLocation + transform.position; 
        GeneralProjectileInformation _gpi = _projectile.GetComponent<GeneralProjectileInformation>();
        if (Target == null) Target = findTarget.Target;
        gei = Target.parent.gameObject.GetComponent<GeneralEnemyInformation>();
        _gpi.Target = Target;
        if(gei.nextPos != Vector3.zero) _gpi.targetPos = gei.nextPos;       //Assume that its impossible for the enemy to get to (0,0,0)
        else _gpi.targetPos = Target.position;
        _gpi.targetDir = gei.dir;
        _gpi.TurretHost = gti;
        _gpi.damagePerHit = damagePerHit;

        am.PlayAudioOnce();
    }
    void OnDisable(){
        anim.OnShoot -= Shoot;
    }
}
