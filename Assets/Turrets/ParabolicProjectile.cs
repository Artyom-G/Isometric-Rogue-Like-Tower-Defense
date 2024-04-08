using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicProjectile : MonoBehaviour
{
    [Header("General")]
    [Range(0, 5)]
    public float desiredHeight = 7;
    [Range(0, 10f)]
    public float speed = 1f;
    public GameObject explosionPrefab;
    public Vector2 landingRandomization = new Vector2(0.25f, 0.125f);

    [Header("Debug")]
    public Vector3 LaunchPos = Vector3.zero;
    public Vector3 MaxPointApproximate;
    public Vector3 LandingPos;

    public float a;
    public float b;
    public float c;

    public Vector3 LaunchPosRealWorld;
    public Vector3 LandingPosRealWorld;
    public float timeIndex = 0;
    public int xDir = 1;
    float processedSpeed;
    
    GeneralProjectileInformation gpi;

    void Start()
    {
        gpi = GetComponent<GeneralProjectileInformation>();
        LaunchPosRealWorld = transform.position;
        LandingPosRealWorld = gpi.targetPos;
        LandingPosRealWorld += gpi.targetDir;
        LandingPosRealWorld += new Vector3(Random.Range(-landingRandomization.x, landingRandomization.x), Random.Range(-landingRandomization.y, landingRandomization.y), 0);

        LandingPos = LandingPosRealWorld - LaunchPosRealWorld;
        
        MaxPointApproximate = new Vector3(LandingPos.x / 2, desiredHeight, 0);

        //https://www.desmos.com/calculator/gjjc6tydvq
        a = (LandingPos.x * MaxPointApproximate.y - MaxPointApproximate.x * LandingPos.y) / (MaxPointApproximate.x * MaxPointApproximate.x * LandingPos.x - LandingPos.x * LandingPos.x * MaxPointApproximate.x);
        b = (MaxPointApproximate.y - a * MaxPointApproximate.x * MaxPointApproximate.x) / MaxPointApproximate.x;
        c = 0;

        if(b < 0) xDir = -1;

        processedSpeed = LandingPos.x * speed * 0.01f;
    }

    void FixedUpdate()
    {
        if(Mathf.Abs(LaunchPosRealWorld.x - transform.position.x) < Mathf.Abs(LandingPos.x)){
            transform.position = LaunchPosRealWorld + ParabolicMovement(timeIndex);
            timeIndex += processedSpeed;
        }
        else{
            GameObject explostion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explostion.GetComponent<AreaEffect>().TurretHost = gpi.TurretHost; //Consider giving explosion the GeneralProjectileInformation script instead of this
            Destroy(gameObject);
        }
    }
    
    Vector3 ParabolicMovement(float _time){
        return new Vector3(_time, (a * _time * _time + b * _time + c), 0);
    }
}
