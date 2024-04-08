using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEnemyInformation : MonoBehaviour
{
    //[Header("Effects")]
    [System.Serializable]
    public class Effect{
        public string name;
        public bool enabled = false;
        public float duration = 0;
        public Effect(string _name){
            name = _name;
        }
        public Effect(Effect _effect){
            name = _effect.name;
            enabled = _effect.enabled;
            duration = _effect.duration;
        }
    }
    public Effect[] Effects = new Effect[] {new Effect("Ice"), new Effect("Fire"), new Effect("Holy")};
    public Effect[] effects;

    [Header("Debug")]
    public Vector3 nextPos;
    public Vector3 dir;

    void Awake(){
        effects = Effects;
        for(int i = 0; i < effects.Length; i++){
            effects[i] = new Effect(effects[i]);
        }
    }

    public void ApplyEffect(int _effectIndex, float _effectDuration){
        if(_effectIndex < 0) return;
        if(effects[_effectIndex].duration < _effectDuration) effects[_effectIndex].duration = _effectDuration;
        if(!effects[_effectIndex].enabled){
            effects[_effectIndex].enabled = true;
            StartCoroutine(DecayEffect(effects[_effectIndex]));
        }
        effects[_effectIndex].enabled = true;
    }
    public IEnumerator DecayEffect(Effect _effect){
        while(_effect.duration > 0){
            _effect.duration -= 0.2f;
            yield return new WaitForSeconds(0.2f);
        }
        _effect.duration = 0;
        _effect.enabled = false;
        yield return null;
    }
}
