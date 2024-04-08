using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyGenerator : MonoBehaviour
{
    [Header("References")]
    public AnimatorTurret anim;
    public static Text moneyText; //Being set by MainGameplay at Start()
    GeneralTurretInformation gti;

    [Header("Debug")]
    public int generateMoney;
    public float timeBetweenMoneyGeneration = 0;
    public float timeBetweenMoneyGenerationTimer = 0;
    
    //Statics
    public static int startingMoneyVal = 35;  
    public static int money;

    void Awake(){
        gti = GetComponent<GeneralTurretInformation>();
        timeBetweenMoneyGeneration = gti.timeBetweenMoneyGeneration;
        generateMoney = gti.generateMoney;

        anim.OnShoot += GenerateMoney;
        anim.StartCoroutine(anim.ReloadAnimation());
    }
    void Update(){
        timeBetweenMoneyGenerationTimer += Time.deltaTime;
        if(timeBetweenMoneyGenerationTimer >= timeBetweenMoneyGeneration){
            timeBetweenMoneyGenerationTimer = 0;

            anim.state = AnimatorTurret.State.Shoot; //Shoot, meaning generate money
            anim.StopAllCoroutines();
            anim.StartCoroutine(anim.ShootAnimation(anim.shootIndex));
        }
        else{
            anim.state = AnimatorTurret.State.Idle;
        }   
    }
    public void GenerateMoney(){        //Used by AnimatorTurret
        UpdateMoney(generateMoney);
        timeBetweenMoneyGenerationTimer = 0;
    }
    public static void UpdateMoney(int _moneyChange = 0){
        money += _moneyChange;
        moneyText.text = "$" + money.ToString();
    }
    public static void RestartMoney(){
        money = startingMoneyVal;
        UpdateMoney();
    }
    void OnDisable(){
        anim.OnShoot -= GenerateMoney;
    }
}
