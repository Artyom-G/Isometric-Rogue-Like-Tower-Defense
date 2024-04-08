using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTurret : MonoBehaviour
{
    [System.Serializable]
    public class AnimationArray{
        public Sprite sprite;
        public Vector3 projectileInitialLocation;
        public bool shoot = false;
        public bool shootWithReload = false;
    }
    [System.Serializable]
    public class AnimationMaster{
        public string name;
        public float aimAngle;
        public AnimationArray[] Reload = new AnimationArray[1];
        public AnimationArray[] Shoot = new AnimationArray[1];
    }
    [Header("Animation")]
    public AnimationMaster[] animation; 
    public float[] ReloadTimer = new float[1];
    public float[] ShootTimer = new float[1];
    
    [Header("References")]
    public SpriteRenderer sr;
    public ProjectileShootTurrets pst;
    public LaserShootTurret lst;
    public MoneyGenerator mg;

    [Header("Debug")]
    public int animationIndex = 0;
    public int shootIndex = 0;
    public enum State {Idle, Reload, Shoot};
    public State state = State.Reload;

    public IEnumerator ReloadAnimation(int _index = 0){
        sr.sprite = animation[animationIndex].Reload[_index].sprite;
        yield return new WaitForSeconds(ReloadTimer[_index]);
        if(_index + 1 < ReloadTimer.Length) StartCoroutine(ReloadAnimation(_index + 1));
    }

    public delegate void ShootAction();
    public event ShootAction OnShoot;
    public void Shoot(){
        if(OnShoot != null){
            OnShoot();
        }
    }
    public IEnumerator ShootAnimation(int _index = 0){
        sr.sprite = animation[animationIndex].Shoot[_index].sprite;
        if(animation[animationIndex].Shoot[_index].shoot) Shoot();
        yield return new WaitForSeconds(ShootTimer[_index]);
        if(_index + 1 < ShootTimer.Length && !animation[animationIndex].Shoot[_index].shootWithReload) StartCoroutine(ShootAnimation(_index + 1));
        else {
            shootIndex = _index + 1;
            if(shootIndex >= animation[animationIndex].Shoot.Length) shootIndex = 0;
            state = State.Reload;
            StartCoroutine(ReloadAnimation());
        }
    }
}
