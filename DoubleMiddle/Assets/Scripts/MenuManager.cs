using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    bool pause = false;
    float temp;
    public GameObject menuCanvas;
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        temp = GameManager.instance.noteSpeed;
        menuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameReset()
    {
        SceneManager.LoadScene("GameScenes");
        Time.timeScale = 1;
    }

    public void pauseSwitch()
    {
        if (pause)
        {
            pause = false;
            Time.timeScale = 1;
            GameManager.instance.pauseMusic();
            GameManager.instance.noteSpeed = temp;
            menuCanvas.SetActive(false);
        }
        else
        {
            pause = true;
            Time.timeScale = 0;
            GameManager.instance.pauseMusic();
            GameManager.instance.noteSpeed = 0.0f;
            menuCanvas.SetActive(true);
        }
    }

    public void SongSelect()
    {
        SceneManager.LoadScene("SongSelect");
        Time.timeScale = 1;
    }
}
