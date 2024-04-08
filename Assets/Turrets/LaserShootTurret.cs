using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShootTurret : MonoBehaviour
{
    [Header("General")] 
    public LineRenderer ln;

    [Header("References")]
    public AimTurret aim;           //Redundant at the moment
    public AnimatorTurret anim;
    public AudioManager am;

    [Header("Debug")]
    public Transform Target;
    public float aimAngle;
    Vector3 oldTarget = new Vector3(-200, -100, 0);
    float hitTimer = 0;
    float timeBetweenHits;

    GeneralTurretInformation gti;
    FindTargetTurret findTarget;
    GeneralEnemyInformation gei;
    int damagePerHit;

    void Start(){
        gti = GetComponent<GeneralTurretInformation>();
        damagePerHit = gti.damagePerHit;
        timeBetweenHits = gti.timeBetweenHits;

        findTarget = GetComponent<FindTargetTurret>();

        //anim.OnShoot += Shoot;
    }

    void Update(){
        Target = findTarget.Target;
        //Turn Off Shooting Effects
        if(Target == null) {
            ln.SetPosition(1, anim.animation[0].Shoot[0].projectileInitialLocation);
            am.PauseAudio();
        }
        //Turn On Shooting Effects
        else {
            ln.SetPosition(1, Target.position - transform.position);
            am.UnpauseAudio();
        }
        ln.SetPosition(0, anim.animation[0].Shoot[0].projectileInitialLocation);

        hitTimer += Time.deltaTime;
        if(hitTimer >= timeBetweenHits){
            if(Target != null){
                hitTimer = 0;
                Shoot();
            }
        }
    }
    void Shoot(){
        Target.transform.parent.GetComponent<HealthManager>().OnHit(gameObject, damagePerHit);
        GeneralEnemyInformation _gei = Target.parent.GetComponent<GeneralEnemyInformation>();
        _gei.ApplyEffect(gti.effectIndex, gti.effectDuration);
    }
    void OnDisable(){
        //anim.OnShoot -= Shoot;
    }
}
