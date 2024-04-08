using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    
    [Header("General")]
    public Tilemap map;
    [System.Serializable]
    public class Enemy{
        public string name;
        public Transform enemy;
        public int difficultyValue = 1; 
        public int enemyQuantity = 1;
        
        public Enemy(Transform _enemy, int _difficultyValue, int _enemyQuantity){
            enemy = _enemy;
            difficultyValue = _difficultyValue;
            enemyQuantity = _enemyQuantity;
        }
    }
    public Enemy[] enemyTypes = new Enemy[1];

    [System.Serializable]
    public class Wave{
        public string name;
        public List<Enemy> enemy = new List<Enemy>();
        [Range(0, 10)]
        public float timeBetweenEnemySpawn = 1;
        public bool nextLevel = false;
    }
    public Wave[] StarterWaves = new Wave[4];
    public float timeBetweenWaves = 5;
    [Range(1, 2)]
    public float exponentialBaseDifficulty = 1.2f;

    //% of an enemy group per wave
    [Range(0, 1)]
    public float enemyGroupPercentagePerWaveMax = 0.6f;
    [Range(0, 1)]
    public float enemyGroupPercentagePerWaveMin = 0.4f;

    [Header("Debug")]
    //Level
    public int Level = 1;
    int HighLevel;

    //Wave
    public int startingWaveNumber = 1;
    public static int curWaveNumber = 0;
    public Wave curWave;
    public float waveCountDown;
    public static int enemyTotalQuantity;
    public int curDifficulty = 10;

    //State
    public enum SpawnState {Spawning, Waiting, CountingDown};
    public SpawnState state = SpawnState.CountingDown;

    void Start()
    {
        waveCountDown = timeBetweenWaves;
        state = SpawnState.Waiting;
        curWaveNumber = startingWaveNumber - 1;
    }

    void Update()
    {

        if (state == SpawnState.Waiting)
        {
            if (enemyTotalQuantity <= 0)
            {
                state = SpawnState.CountingDown;
                waveCountDown = timeBetweenWaves;
            }
            else
            {
                return;
            }
        }

        if (waveCountDown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                if(curWaveNumber >= StarterWaves.Length){
                    //curDifficulty = Random.Range(1, 10); 
                    //f(x)=10*x^1.2 where f(x) is difficulty and x is wave
                    curDifficulty = (int) Mathf.Floor(10 * Mathf.Pow(exponentialBaseDifficulty, (float) (curWaveNumber - StarterWaves.Length))); //Could condensed
                    curWave = CreateRandomizedWave(curDifficulty);
                }
                else{
                    curWave = StarterWaves[curWaveNumber];
                }
                StartCoroutine(SpawnWave(curWave));    
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave(Wave _wave){
        curWaveNumber++;
        state = SpawnState.Spawning;
        if(PlayerPrefs.GetInt("HighestWave") < curWaveNumber){
            PlayerPrefs.SetInt("HighestWave", curWaveNumber);
        }
        //Determine total enemy count (for determining if all enemies were killed)
        foreach(Enemy _ in _wave.enemy){
            enemyTotalQuantity += _.enemyQuantity;
        }

        //Iterate to spawn enemies
        foreach(Enemy _enemy in _wave.enemy){
            for(int i = 0; i < _enemy.enemyQuantity; i++){
                SpawnEnemy(_enemy.enemy);
                yield return new WaitForSeconds(_wave.timeBetweenEnemySpawn);
            }
        }

        state = SpawnState.Waiting;
        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        enemy.GetComponent<EnemyMovement>().map = map;
        Instantiate(enemy, transform.position, Quaternion.identity);
    }

    Wave CreateRandomizedWave(int _difficultyValue){
        Wave _wave = new Wave();
        int _trueDifficultyValue = 0;
        float _enemyGroupPercentagePerWaveMax = enemyGroupPercentagePerWaveMax;
        float _enemyGroupPercentagePerWaveMin = enemyGroupPercentagePerWaveMin;

        while(_trueDifficultyValue < _difficultyValue){
            //Create a group of enemies (accounting for the difficulty of the wave)
            //Pick random enemy type that isnt over 20% of the of difficulty value (enemy difficulty > difficulty value * 0.20f)
            Enemy _enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
            while(_enemyType.difficultyValue * 5 > _difficultyValue){
                _enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
            }
            //Decide on a quantity of the enemy with Group% of the Wave's Total in terms of Difficulty (also always have at least 1 of that enemy)
            if((float)(_difficultyValue - _trueDifficultyValue)/_trueDifficultyValue < _enemyGroupPercentagePerWaveMax){ //rearrage for values for performance 
                _enemyGroupPercentagePerWaveMax = (float)(_difficultyValue - _trueDifficultyValue)/_trueDifficultyValue;
                _enemyGroupPercentagePerWaveMin = 0;
            }
            int _enemyGroupDifficulty = (int) Mathf.Floor((float) _difficultyValue * Random.Range(_enemyGroupPercentagePerWaveMin, _enemyGroupPercentagePerWaveMax));
            int _enemyQuantityPerGroup = (int) Mathf.Floor((float) _enemyGroupDifficulty / _enemyType.difficultyValue) + 1;
            
            //Determine the difficulty of the whole group (quantity * difficulty of one enemy)
            _enemyGroupDifficulty = _enemyQuantityPerGroup * _enemyType.difficultyValue;
            _trueDifficultyValue += _enemyGroupDifficulty;
            _wave.enemy.Add(new Enemy(_enemyType.enemy, _enemyGroupDifficulty, _enemyQuantityPerGroup));
        }
        _wave.timeBetweenEnemySpawn = 0.3f; //Add randomization to spawn rate later
        _wave.name = "Wave " + curWaveNumber.ToString();
        return _wave;
    }
}
