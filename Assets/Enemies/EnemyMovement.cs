using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(GeneralEnemyInformation))]
[RequireComponent(typeof(HealthManager))]
public class EnemyMovement : MonoBehaviour
{

    [Header("General")]
    [Range(0, 3)]
    public float movementSpeed = 0.1f;
    [Range(0, 20)]
    public int difficultyValue = 1;

    [Header("Debug")]
    public float processedMovementSpeed;

    public Tilemap map;

    public TileBase nextTile;
    public TileBase curTile;
    public TileBase lastTile;

    public Vector3Int nextPosition;
    public Vector3Int curPosition;
    public Vector3Int lastPosition;
    public Vector3 nextPosRealWorld;
    public Tile PathTile;
    
    int roundTo = 10;
    bool goToNextTile = false;
    GeneralEnemyInformation gei;
    HealthManager hm;

    void Start()
    {
        gei = GetComponent<GeneralEnemyInformation>();
        hm = GetComponent<HealthManager>();
        processedMovementSpeed = movementSpeed;
        
        FindNextTile();
    }

    void Update()
    {
        //Reset all stats before processing them
        processedMovementSpeed = movementSpeed;
        hm.processedDefense = hm.defense;
        
        if(gei.effects[0].enabled){ //FREEZE
            processedMovementSpeed = movementSpeed / 2;
            
            //Fire Check
            if(!gei.effects[2].enabled){
                Debug.Log("When FREEZE is applied without FIRE, enemy DEF is boosted");
                hm.processedDefense *= 2;
            }
        }
        if(gei.effects[1].enabled){ //FIRE
            hm.StartCoroutine(hm.HealthOverTime(gameObject, 1, 2f, 5));      //change attacker from gameObject to real attacker, change status effect stats
            
            //Water Check
            if(!gei.effects[0].enabled){
                Debug.Log("When FIRE is applied without WATER, enemy SPD is boosted");
                processedMovementSpeed *= 2f;
            }
        }
        if(gei.effects[2].enabled){ //WATER
            //Add effect: down def
            hm.processedDefense /= 2;

            //Freeze Check
            if(!gei.effects[0].enabled){
                Debug.Log("When WATER is applied without FREEZE, enemy HP is restored over time");
                hm.StartCoroutine(hm.HealthOverTime(gameObject, -1, 0.5f, 5));      //change attacker from gameObject to real attacker, change status effect stats
            }
        }

        if(goToNextTile){
            goToNextTile = false;
            FindNextTile();
        }
        else{
            MoveToTile();
        }
    }
    void FindNextTile(){
        //Find Current Position
        curPosition = map.WorldToCell(transform.position);
        curTile = map.GetTile(curPosition);

        //Find Neighbouring Cells
        Vector3Int[] neighbourPositions = new Vector3Int[4];
        neighbourPositions[0] = new Vector3Int(curPosition.x - 1, curPosition.y, curPosition.z);
        neighbourPositions[1] = new Vector3Int(curPosition.x + 1, curPosition.y, curPosition.z);
        neighbourPositions[2] = new Vector3Int(curPosition.x, curPosition.y + 1, curPosition.z);
        neighbourPositions[3] = new Vector3Int(curPosition.x, curPosition.y - 1, curPosition.z);
        
        //Check if Path Tile
        List<Vector3Int> possibleTiles = new List<Vector3Int>();
        for(int i = 0; i < neighbourPositions.Length; i++){
            if(map.HasTile(neighbourPositions[i])){
                if(map.GetTile(neighbourPositions[i]).name == PathTile.name && neighbourPositions[i] != lastPosition){
                    possibleTiles.Add(neighbourPositions[i]);
                } 
            }
        } 
        if(possibleTiles.Count > 0)
        {
            nextPosition = possibleTiles[Random.Range(0, possibleTiles.Count)];
            nextPosRealWorld = map.GetCellCenterWorld(nextPosition);
            //gei.nextPos = nextPosRealWorld;
            gei.nextPos = transform.position; //Used to be next pos but then changed into cur pos and cur dir
            lastPosition = curPosition;
        }else{
            Camera.main.GetComponent<MainGameplay>().GameOver();
        }
    }
    void MoveToTile(){
        Vector3 _dir = (nextPosRealWorld - transform.position);
        gei.dir = _dir.normalized * processedMovementSpeed;
        Vector3 _moveBy = _dir.normalized * processedMovementSpeed * Time.deltaTime;
        if(_moveBy.sqrMagnitude > _dir.sqrMagnitude || _moveBy.sqrMagnitude == 0){
            transform.position = nextPosRealWorld;
            goToNextTile = true;
        } 
        else transform.position += _moveBy;
    }
    void OnDestroy(){
        EnemySpawner.enemyTotalQuantity -= 1;
    }
}
