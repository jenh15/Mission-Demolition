using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject gameOverUI, startUI;
    public bool gameOver = false;
    public bool gameStart = false;
    public MissionDemolition missionDemo;

    // Start is called before the first frame update

    public void StartGame()
    {
        gameStart = true;
        startUI.SetActive(false);
        missionDemo.StartLevel();
    }
    
    public void GameOver()
    {
        gameOverUI.SetActive(true);
        gameOver = true;
    }

    public void Restart()
    {
        gameOver = false;
        missionDemo.level = 0;
        missionDemo.shotsTaken = 0;
        missionDemo.totalStars = 0;
        for (int i = 0; i < 3; i++)
        {
            missionDemo.levelStarIcons[i].SetActive(false);
        }
        gameOverUI.SetActive(false);
        missionDemo.StartLevel();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
