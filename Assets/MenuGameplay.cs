using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGameplay : MonoBehaviour
{

    public Text highestWaveText;

    void Start(){
        highestWaveText.text = "Highest Wave: " + PlayerPrefs.GetInt("HighestWave").ToString();
    }

    public void OpenScene(string _sceneName){
        SceneManager.LoadScene(_sceneName);
    }

    public void ToggleObject(GameObject _obj){
        _obj.SetActive(!_obj.activeSelf);
    }
}
