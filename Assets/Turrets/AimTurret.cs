using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTurret : MonoBehaviour
{
    [Header("References")]
    public AnimatorTurret anim;

    [Header("Debug")]
    public Transform Target;
    public float aimAngle;

    public void Aim(Vector3 _target){
        aimAngle = Vector2.Angle(Vector2.up, _target - transform.position);
        if (_target.x < transform.position.x) aimAngle = 360 - aimAngle;
        AimAngle(aimAngle);
    }
    public void AimAngle(float _angle = 269.536352f){       //Default Aim Angle is in the South-West Corner: 270 - arcsin(1/sqrt(5))
        if(_angle < anim.animation[0].aimAngle || _angle > anim.animation[anim.animation.Length-1].aimAngle){
            anim.animationIndex = 0;
            return;
        }
        for(int i = 1; i < anim.animation.Length; i++){
            if(anim.animation[i].aimAngle > _angle){
                anim.animationIndex = i;
                break;
            }
        }
    }
}
