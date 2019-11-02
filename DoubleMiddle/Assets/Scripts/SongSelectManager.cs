using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class SongSelectManager : MonoBehaviour
{
    public Image musicImageUI;
    public Text musicTitleUI;
    public Text artistNameUI;
    public Text bpmUI;

    private int musicIndex;
    private int musicCount = 3;

    //회원가입 결과 UI
    public Text userUI;

    private void UpdateSong(int musicIndex)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        //리소스에서 비트 텍스트 파일 불러오기
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + musicIndex.ToString());
        StringReader stringReader = new StringReader(textAsset.text);
        // 파일 읽고 UI업데이트
        musicTitleUI.text = stringReader.ReadLine();
        artistNameUI.text = stringReader.ReadLine();
        bpmUI.text = "BPM: " + stringReader.ReadLine().Split(' ')[0];

        //리소스에서 음악파일 불러와 재생
        AudioClip audioClip = Resources.Load<AudioClip>("Beats/" + musicIndex.ToString());
        audioSource.clip = audioClip;
        audioSource.Play();

        //이미지 파일 불러오기
        musicImageUI.sprite = Resources.Load<Sprite>("Beats/" + musicIndex.ToString());
    }

    public void Right()
    {
        musicIndex = musicIndex + 1;
        if (musicIndex > musicCount) musicIndex = 1;
        UpdateSong(musicIndex);
    }
    public void Left()
    {
        musicIndex = musicIndex - 1;
        if (musicIndex < 1) musicIndex = musicCount;
        UpdateSong(musicIndex);
    }

    void Start()
    {
        userUI.text = "ID: " + PlayerInformation.auth.CurrentUser.Email;
        musicIndex = 1;
        UpdateSong(musicIndex);
    }

    public void GameStart()
    {
        PlayerInformation.selectedMusic = musicIndex.ToString();
        SceneManager.LoadScene("GameScene");
    }

    public void LogOut()
    {
        PlayerInformation.auth.SignOut();
        SceneManager.LoadScene("LoginScene");
    }
}
