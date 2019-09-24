using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    bool pause = false;

    public GameObject menuCanvas;
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {

        menuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameReset()
    {
        SceneManager.LoadScene("SampleScene"); //임시 나중에 이름 수정
        Time.timeScale = 1;
    }

    public void pauseSwitch()
    {
        if (pause)
        {
            pause = false;
            Time.timeScale = 1;
            GameManager.instance.pauseMusic();
            GameManager.instance.noteSpeed = 0.09f;
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

    public void startMenu()
    {
        
    }
}
