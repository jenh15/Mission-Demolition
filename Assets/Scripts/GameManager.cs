using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public bool gameOver = false;
    // Start is called before the first frame update

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        gameOver = true;
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
