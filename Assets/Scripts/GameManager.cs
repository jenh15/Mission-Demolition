using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("_Scene_0");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
