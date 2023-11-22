// Record Live Scores and temporary save them
// Handles ingame scores and runtime
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour{
    public static ScoreManager instance;
    [Header("SCORE UI ELEMENTS")]
    public TextMeshProUGUI timerText;
    float TIME = 1.00f;
    void Start(){
        instance = this;
    }
    void Update(){
        if(GameManager.instance.GAMEOVER){timerText.gameObject.SetActive(false); return;}
        UPDATESCORE();
    }
    void UPDATESCORE(){
        TIME += Time.deltaTime;
        timerText.text = TIME.ToString("#.000");
    }
    public float GetTime{
        get{return TIME;}
    }
}
