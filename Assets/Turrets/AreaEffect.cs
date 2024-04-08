using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AreaEffect : MonoBehaviour
{
    [Header("General")]
    public float lifeSpan = 0.5f;
    public Gradient colorChange;
    public Gradient colorChangeOutline;
    public AnimationCurve collisionEnabler;

    [Header("References")]
    public Tilemap field;
    public Tilemap fieldOutline;
    public PolygonCollider2D collider;
    public AudioManager am;

    [Header("Debug")]
    public GeneralTurretInformation TurretHost;
    public bool destruct = true;
    public float lifeSpanTimer = 0;
    public int damage = 1;

    //Private
    bool activated = false;

    void Start(){
        am.PlayAudioOnce();
    }

    void Update(){
        float lifeEvaluate = lifeSpanTimer/lifeSpan;
        field.color = colorChange.Evaluate(lifeEvaluate);
        fieldOutline.color = colorChange.Evaluate(lifeEvaluate);
        collider.enabled = collisionEnabler.Evaluate(lifeEvaluate) >= 1 ? true : false;
        
        lifeSpanTimer += Time.deltaTime;
        if(lifeSpanTimer > lifeSpan){
            if(destruct) Destroy(gameObject);
            else lifeSpanTimer = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D _col){
        //if(activated) return;
        GameObject _obj = _col.transform.gameObject;
        _obj.GetComponent<HealthManager>().OnHit(gameObject, damage);
        GeneralEnemyInformation _gei = _obj.GetComponent<GeneralEnemyInformation>();
        _gei.ApplyEffect(TurretHost.effectIndex, TurretHost.effectDuration);
        activated = true;
    }
    void OnTriggerStay2D(Collider2D _col){
        //if(activated) return;
        GameObject _obj = _col.transform.gameObject;
        GeneralEnemyInformation _gei = _obj.GetComponent<GeneralEnemyInformation>();
        _gei.ApplyEffect(TurretHost.effectIndex, TurretHost.effectDuration);
        activated = true;
    }
}
