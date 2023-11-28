// Handles game session starting from Main Menu all the way in combat / session
// Will call DBManager to load up to sync previous runs and scores
// INCLUDE DO NOT DESTROY
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour{
    public static GameManager instance;
    public string SESSION_NAME;
    public Image IMG_LOAD;
    public Image IMG_FADE;
    public GameObject TEXT_LEADERBOARD;
    public GameObject BTN_RESTART;
    public GameObject BTN_MAINMENU;
    public TextMeshProUGUI TEXT_FINALTIME;
    public TextMeshProUGUI TEXT_SESSIONNAME;
    public bool GAMEOVER = false;
    public bool inSESSION = false;
    [Header("Leaderboard")]
    public List<GameObject> LEADERBOARD = new List<GameObject>();
    void Start(){
        if(instance == null){
            instance = this;
        }else{Destroy(gameObject);}
        DontDestroyOnLoad(gameObject);
    }

    public void SETGAMEOVER(){
        GAMEOVER = true;
        TEXT_FINALTIME.text = ScoreManager.instance.GetTime.ToString("#.000");
        TEXT_SESSIONNAME.text = $"{SESSION_NAME}";
        Cursor.lockState = CursorLockMode.None;
        DatabaseManager.database.AddLeader(SESSION_NAME, ScoreManager.instance.GetTime);
        DatabaseManager.database.RefreshLeaders();
        StartCoroutine("Fade");
    }
    IEnumerator Fade(){
        float time = 0f;
        float duration = 2f;
        float alpha;

        alpha = GAMEOVER ? .75f : 0f;
        Color FadeColor = new Color(IMG_FADE.color.r,IMG_FADE.color.g,IMG_FADE.color.b, alpha);
        
        TEXT_FINALTIME.gameObject.SetActive(GAMEOVER);
        TEXT_SESSIONNAME.gameObject.SetActive(GAMEOVER);

        if(!GAMEOVER){
            BTN_MAINMENU.SetActive(GAMEOVER);
            BTN_RESTART.SetActive(GAMEOVER);
            TEXT_LEADERBOARD.gameObject.SetActive(GAMEOVER);
        }
        
        while (time < duration){
            time += Time.deltaTime;
            IMG_FADE.color = Color.Lerp(IMG_FADE.color, FadeColor, time / duration);
            yield return null;
        }

        BTN_RESTART.SetActive(GAMEOVER);
        BTN_MAINMENU.SetActive(GAMEOVER);
        TEXT_LEADERBOARD.gameObject.SetActive(GAMEOVER);

        if(GAMEOVER){RefreshLeaderBoard();}
    }
    void RefreshLeaderBoard(){
        DatabaseManager.database.CollectLeaders();
        for (int i = 0; i < DatabaseManager.database.LeadItem.Count; i++){
            // Set the name
            LEADERBOARD[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText($"{i+1}. {DatabaseManager.database.LeadItem[i].lead_username}");
            // Set the time
            float user_time = float.Parse(DatabaseManager.database.LeadItem[i].lead_usertime);
            LEADERBOARD[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(user_time.ToString("#.000"));
        }
    }
    public void LoadMainMenu(){
        GAMEOVER = false;
        StartCoroutine("LoadScene", 0);
    }
    public void LoadPlay(){
        GAMEOVER = false;
        StartCoroutine("LoadScene", 1);
    }
    public void Restart(){
        GAMEOVER = false;
        StartCoroutine("Fade");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator LoadScene(int indx){
        float time = 0f;
        float duration = .75f;
        float alpha = 1f;
        Color FadeInColor = new Color(IMG_FADE.color.r,IMG_FADE.color.g,IMG_FADE.color.b, alpha);
        Color FadeOutColor = new Color(IMG_FADE.color.r,IMG_FADE.color.g,IMG_FADE.color.b, 0f);
        
        TEXT_SESSIONNAME.gameObject.SetActive(GAMEOVER);
        TEXT_FINALTIME.gameObject.SetActive(GAMEOVER);
        BTN_MAINMENU.SetActive(GAMEOVER);
        BTN_RESTART.SetActive(GAMEOVER);
        TEXT_LEADERBOARD.gameObject.SetActive(GAMEOVER);

        while (time < duration){
            time += Time.deltaTime;
            IMG_LOAD.color = Color.Lerp(IMG_FADE.color, FadeInColor, time / duration);
            yield return null;
        }
        
        yield return new WaitUntil(()=> IMG_LOAD.color.a == 1f);
        IMG_FADE.color = FadeOutColor;
        SceneManager.LoadScene(indx);
        time = 0f;

        while (time < duration){
            time += Time.deltaTime;
            IMG_LOAD.color = Color.Lerp(IMG_LOAD.color, FadeOutColor, time / duration);
            yield return null;
        }
    }
}