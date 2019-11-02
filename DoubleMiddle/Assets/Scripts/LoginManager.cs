using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class LoginManager : MonoBehaviour
{
    private FirebaseAuth auth;

    //이메일 및 패스워드 UI
    public InputField emailInputField;
    public InputField passwordInputField;

    public Text messageUI;

   
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        messageUI.text = "";
    }
    
    public void Login()
    {
        messageUI.text = "로그인 확인중...";
        string email = emailInputField.text;
        string password = passwordInputField.text;
        var content = TaskScheduler.FromCurrentSynchronizationContext();
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(
            task =>
            {
                if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
                {
                    PlayerInformation.auth = auth;
                    SceneManager.LoadScene("SongSelectScene");
                }
                else
                {
                    messageUI.text = "계정 또는 비밀번호를 확인해주세요.";
                }
            },content
            );
    }

    public void GoToJoin()
    {
        SceneManager.LoadScene("JoinScene");
    }
}
