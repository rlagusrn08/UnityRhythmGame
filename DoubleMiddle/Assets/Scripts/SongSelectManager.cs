using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Threading.Tasks;

public class SongSelectManager : MonoBehaviour
{
    public Image musicImageUI;
    public Text musicTitleUI;
    public Text artistNameUI;
    public Text bpmUI;

    public Text rank1UI;
    public Text rank2UI;
    public Text rank3UI;

    private int musicIndex;
    private int musicCount = 3;

    private DatabaseReference reference;
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

        
        var content = TaskScheduler.FromCurrentSynchronizationContext();
        rank1UI.text = "데이터 불러오는 중...";
        rank2UI.text = "데이터 불러오는 중...";
        rank3UI.text = "데이터 불러오는 중...";
        
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unitygameserverpractice.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.GetReference("ranks").Child(musicIndex.ToString());
        
        reference.OrderByChild("score").GetValueAsync().ContinueWith(
            task =>
            {
                if (task.IsCompleted)
                {
                    List<string> rankList = new List<string>();
                    List<string> emailList = new List<string>();
                    DataSnapshot snapshot = task.Result;
                    Debug.Log("데이터 불러오기 성공");
                    foreach (DataSnapshot data in snapshot.Children)
                    {
                        IDictionary rank = (IDictionary)data.Value;
                        emailList.Add(rank["email"].ToString());
                        rankList.Add(rank["score"].ToString());
                        Debug.Log(rank["email"].ToString() + " " + rank["score"].ToString());
                    }
                    Debug.Log(rankList.Count);
                    Debug.Log("이메일 배열 리버스");
                    emailList.Reverse();
                    Debug.Log("랭킹 배열 리버스");
                    rankList.Reverse();
                    Debug.Log("랭킹 텍스트 공백으로 처리");
                    rank1UI.text = "1위: NONE";
                    rank2UI.text = "2위: NONE";
                    rank3UI.text = "3위: NONE";
                    Debug.Log("랭킹 UI를 담을 배열 생성");
                    List<Text> textList = new List<Text>();
                    textList.Add(rank1UI);
                    textList.Add(rank2UI);
                    textList.Add(rank3UI);
                    int count = 1;
                    for (int i = 0; i < rankList.Count && i < 3; i++)
                    {
                        Debug.Log(count + "위: " + emailList[i] + " " + rankList[i]);
                        textList[i].text = count + "위: " + emailList[i] + " " + rankList[i] +"점";
                        count++;
                    }
                }
                else if (task.IsFaulted)
                {
                    Debug.Log("데이터 불러오기 실패");
                }

            }
            , content
        );

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
        reference = FirebaseDatabase.DefaultInstance.RootReference;
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
