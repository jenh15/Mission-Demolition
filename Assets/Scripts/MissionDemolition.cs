using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameMode {
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour {
    static private MissionDemolition S;
    
    [Header("Inscribed")]
    public TextMeshProUGUI uitLevel;
    public TextMeshProUGUI uitShots;
    public TextMeshProUGUI uiNumStars;
    public Vector3 castlePos;
    public GameObject[] castles;
    public GameManager gameManager;
    public GameObject[] levelStarIcons;

    [Header("Dynamic")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";
    public int starsThisLevel = 0;
    public int totalStars = 0;

    void Start(){
        S=this;

        level = 0;
        shotsTaken = 0;
        levelMax = castles.Length;
    }

    public void StartLevel(){
        if(castle != null){
            Destroy(castle);
        }
        //Destroy old Projectiles if they exist
        Projectile.DESTROY_PROJECTILES();

        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;

        Goal.goalMet = false;

        starsThisLevel = 0;
        S.levelStarIcons[0].SetActive(false);

        UpdateGUI();

        mode = GameMode.playing;

        // Zoom out to show both
        FollowCam.SWITCH_VIEW(FollowCam.eView.both);
    }

    void UpdateGUI(){
        uitLevel.text = "Level: "+(level+1)+ " of "+levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
        uiNumStars.text = "Stars: " + totalStars;
    }

    void Update(){
        UpdateGUI();

        //Check level
        if((mode == GameMode.playing)&&Goal.goalMet){
            mode = GameMode.levelEnd;
            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel(){
        level++;
        if (level == levelMax)
        {
            level = levelMax - 1;
            gameManager.GameOver();
            return;
        }
        
        StartLevel();
    }

    static public void SHOT_FIRED(){
        S.shotsTaken++;
    }

    static public GameObject GET_CASTLE()
    {
        return S.castle;
    }
    
    static public void StarCollected()
    {
        S.starsThisLevel++;
        S.totalStars++;

        if (S.levelStarIcons[S.starsThisLevel - 1] != null)
        {
            S.levelStarIcons[S.starsThisLevel - 1].SetActive(true);
        }
    }
}