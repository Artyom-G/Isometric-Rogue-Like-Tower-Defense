using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    [Header("General")]
    public int maxHealth;
    [Tooltip("Flat Damage Reduction")]      //Does nothing atm
    public int armor = 0;
    [Tooltip("Linear Damage Reduction")]
    public float defense = 1;
    
    [Header("On Death")]
    public AnimationCurve deathScale;
    float _deathTimeTimer = 0;
    float _deathTime = 0.3f;
    float _waitForSeconds = 0.05f;
    bool isDead = false;
    public AudioManager deathAudio;
    
    [Header("Debug")]
    public int processedHealth;
    public float processedDefense = 1;

    void Awake(){
        processedHealth = maxHealth;
    }

    public void OnHit(GameObject attacker, int damage){
        if(damage > 0){
            damage = (int) ((float) damage / defense);
            damage -= armor;
            if(damage < 0) damage = 0;
        }
        processedHealth -= damage;
        if(processedHealth <= 0 && !isDead){
            StartCoroutine(Death());
        }
    }

    public IEnumerator Death(){
        isDead = true;
        deathAudio.PlayAudioOnce();
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<EnemyMovement>().enabled = false;
        gameObject.GetComponent<EnemyMovement>().movementSpeed = 0.01f;
        while(_deathTimeTimer < _deathTime){ //Death will take 10 secs
            float deathScaleEvaluated = deathScale.Evaluate(_deathTimeTimer / _deathTime);
            transform.localScale = new Vector3(deathScaleEvaluated, deathScaleEvaluated, 1);
            _deathTimeTimer += _waitForSeconds;
            yield return new WaitForSeconds(_waitForSeconds);
        }
        Destroy(gameObject);
    }

    public IEnumerator HealthOverTime(GameObject attacker, int hitDamage, float hitDelay, int hitAmount, int hitIndex=0){
        yield return new WaitForSeconds(hitDelay);
        Debug.Log("Testing Health Over Time. Line 60");
        OnHit(attacker, hitDamage);
        if(hitIndex < hitAmount) StartCoroutine(HealthOverTime(attacker, hitDamage, hitDelay, hitAmount, hitIndex + 1));
    } 

}
