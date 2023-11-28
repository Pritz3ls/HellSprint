using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MainManager : MonoBehaviour
{
    public GameObject UI_SESSION;
    public GameObject UI_MAIN;
    public GameObject UI_LEADERBOARD;
    public GameObject UI_HOWTOPLAY;
    public GameObject UI_CREDITS;
    public TextMeshProUGUI TEXT_SESSION_INPUT;
    public TextMeshProUGUI TEXT_VERSION;
    [Header("Leaderboard")]
    public List<GameObject> LEADERBOARD = new List<GameObject>();

    // Start is called before the first frame update
    void Start(){
        if(!GameManager.instance.inSESSION){UI_SESSION.SetActive(true);
        }else{UI_SESSION.SetActive(false);}

        CollectLeaderBoard();
        TEXT_VERSION.text = $"build.version {Application.version}";
    }
    void CollectLeaderBoard(){
        DatabaseManager.database.CollectLeaders();
        for (int i = 0; i < DatabaseManager.database.LeadItem.Count; i++){
            // Set the name
            LEADERBOARD[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText($"{i+1}. {DatabaseManager.database.LeadItem[i].lead_username}");
            // Set the time
            float user_time = float.Parse(DatabaseManager.database.LeadItem[i].lead_usertime);
            LEADERBOARD[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(user_time.ToString("#.000"));
        }
    }
    public void PLAY(){
        GameManager.instance.LoadPlay();
    }
    public void SWITCH_MENU(int index = 0){
        UI_MAIN.SetActive(false);
        UI_LEADERBOARD.SetActive(false);
        UI_CREDITS.SetActive(false);
        UI_HOWTOPLAY.SetActive(false);
        switch (index){
            case 1: UI_LEADERBOARD.SetActive(true);break;
            case 2: UI_CREDITS.SetActive(true);break;
            case 3: UI_HOWTOPLAY.SetActive(true);break;
            case 4: Application.Quit(); break;
            default: UI_MAIN.SetActive(true);break;
        }
    }

    public void ENTER_SESSION(){
        GameManager.instance.SESSION_NAME = TEXT_SESSION_INPUT.text;
        GameManager.instance.inSESSION = true;
        UI_SESSION.SetActive(false);
    }
}
