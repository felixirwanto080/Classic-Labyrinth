using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject pausePanel;
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text totalTimeText;
    [SerializeField] Hole hole;
    float timer=0f;
    // Start is called before the first frame update
    public void SetPause(bool isPause){
        if(isPause){
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            gameCanvas.SetActive(false);
        }
        else{
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            gameCanvas.SetActive(true);
        }
    }
    private void Awake(){
        Debug.Log("Max Level di Awake: " + PlayerPrefs.GetInt("maxlevel"));
        if(PlayerPrefs.GetInt("maxlevel")<=0){
            PlayerPrefs.SetInt("maxlevel", 1);
        }
    }    
    private void Start()
    {
        Debug.Log("Max Level di Start: " + PlayerPrefs.GetInt("maxlevel"));

        if(timeText==null || totalTimeText==null || gameOverPanel==null || timeText==null || totalTimeText==null || hole==null || pausePanel==null)
            return;
        
        Time.timeScale = 1;
        timeText.text = "";
        totalTimeText.text = "";
        timer=0;
        
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if(timeText==null || totalTimeText==null || gameOverPanel==null || timeText==null || totalTimeText==null || hole==null)
            return;

        timer += Time.deltaTime;
        timeText.text = "Time: " + timer.ToString("0.00") + "s";
            
        if(hole.Entered && gameOverPanel.activeInHierarchy == false){
            timeText.enabled = false;
            gameCanvas.SetActive(false);
            gameOverPanel.SetActive(true);
            totalTimeText.text = "Total Time: \n" + timer.ToString("0.00") + "s";
        }
    }
    public void BackToMainMenu(){
        SceneLoader.Load("MainMenu");
    }
    public void LevelSelect(){
        SceneLoader.Load("Level0");
    }
    public void Replay(){
        SceneLoader.ReloadLevel();
    }
    public void PlayNext(){
        SceneLoader.LoadNextLevel();
    }
    public void PlayLevel(string sceneName){
        Debug.Log("lagi aktif: " + sceneName);
        SceneLoader.Load(sceneName);
    }
}
