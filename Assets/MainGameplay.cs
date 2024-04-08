using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainGameplay : MonoBehaviour
{

    [Header("General")]
    InputSystem input;
    public Tilemap selectedTilemap;
    public Camera cam;
    public Text moneyText;
    public GameObject mainCanvas;
    public GameObject gameOverCanvas;
    public Text WaveText;

    [Header("Debug")]
    public static GameObject selectedBuilding;
    public static GameObject ghostBuilding;

    void Awake(){
        input = new InputSystem();
        input.Enable();
    }
    void Start(){
        Time.timeScale = 1;
        input.MainGameplay.MouseAction1.performed += _ => PlaceBuilding();  
        
        MoneyGenerator.moneyText = moneyText; //setting a static variable
        MoneyGenerator.RestartMoney();
    }
    void Update()
    {
        if(ghostBuilding != null){
            Vector2 mousePosition = input.MainGameplay.MousePosition.ReadValue<Vector2>();
            mousePosition = cam.ScreenToWorldPoint(mousePosition);
            Vector3Int cellPos = selectedTilemap.WorldToCell(mousePosition);
            if(selectedTilemap.HasTile(cellPos)){
                ghostBuilding.SetActive(true);
                ghostBuilding.transform.position = selectedTilemap.GetCellCenterWorld(cellPos);
            }
            else{
                ghostBuilding.SetActive(false);
            }
        }
    }
    void PlaceBuilding(){
        if(selectedBuilding == null) return;
            Vector2 mousePosition = input.MainGameplay.MousePosition.ReadValue<Vector2>();
            mousePosition = cam.ScreenToWorldPoint(mousePosition);
            Vector3Int cellPos = selectedTilemap.WorldToCell(mousePosition);
            if(selectedTilemap.HasTile(cellPos)){
                int _cost = selectedBuilding.GetComponent<GeneralTurretInformation>().cost;
                if(MoneyGenerator.money >= _cost){
                    MoneyGenerator.UpdateMoney(-_cost);
                    GameObject _building = Instantiate(selectedBuilding, selectedTilemap.GetCellCenterWorld(cellPos), Quaternion.identity);
                }
            }
        Destroy(ghostBuilding);
        selectedBuilding = null;
    }
    void OnEnabled(){
        input.Enable();
    }
    void OnDisabled(){
        input.Disable();
    }
    public void SelectBuilding(GameObject _building){
        selectedBuilding = _building;
        Destroy(ghostBuilding);
        ghostBuilding = Instantiate(selectedBuilding);
        ghostBuilding.GetComponent<Collider2D>().enabled = false;
        ghostBuilding.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        ghostBuilding.transform.GetChild(1).gameObject.SetActive(true);
    }
    public void GameOver(){
        input.Disable();
        Time.timeScale = 0;
        mainCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        WaveText.text = "Wave: " + EnemySpawner.curWaveNumber.ToString();
    }
    public void OpenScene(string _sceneName){
        SceneManager.LoadScene(_sceneName);
    }
}
