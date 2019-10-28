using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //싱글 톤
    public static GameManager instance { get; set; }
    public void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    //노트들의 스피드 정하기
    public float noteSpeed;

    public GameObject scoreUI;
    public float score;
    private Text scoreText;
    
    // 콤보 UI
    public GameObject comboUI;
    private int combo;
    private Text comboText;
    private Animator comboAnimator;
    public int maxCombo;

    //판정 이미지
    public GameObject judgeUI;
    public Sprite[] judgeSprites;
    private Image judgementSpriteRenderer;
    private Animator judgementSpriteAnimator;

    //라인 빛나는 이펙트
    public GameObject[] trails;
    private SpriteRenderer[] trailSpriteRenderers;

    /*
     * 판정변수
     * BAD : 1
     * GOOD : 2
     * PERFECT : 3
     * MISS : 4
     */
    public enum judges { NONE = 0, BAD, GOOD, PERFECT, MISS};


    //음악 처리
    private AudioSource audioSource;
    public string music = "1";

    //오토
    public bool autoPerfect;

    //음악 실행
    void MusicStart()
    {
        //리소스에서 음악을 불러와 재생.
        AudioClip audioClip = Resources.Load<AudioClip>("Beats/" + music);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    //음악 정지
    public bool pause = false;
    public void pauseMusic()
    {
        if (pause)
        {
            pause = false;
            audioSource.UnPause();
        }
        else
        {
            pause = true;
            audioSource.Pause();
        }
    }

    void Start()
    {
        Invoke("MusicStart", 1); //1초 후 음악 재생

        judgementSpriteRenderer = judgeUI.GetComponent<Image>();
        judgementSpriteAnimator = judgeUI.GetComponent<Animator>();
        scoreText = scoreUI.GetComponent<Text>();
        comboText = comboUI.GetComponent<Text>();
        comboAnimator = comboUI.GetComponent<Animator>();

        judgeSprites = new Sprite[4];
        judgeSprites[0] = Resources.Load<Sprite>("Sprites/Bad");
        judgeSprites[1] = Resources.Load<Sprite>("Sprites/Good");
        judgeSprites[2] = Resources.Load<Sprite>("Sprites/Miss");
        judgeSprites[3] = Resources.Load<Sprite>("Sprites/Perfect");

        trailSpriteRenderers = new SpriteRenderer[trails.Length];
        for(int i= 0; i < trails.Length; i++)
        {
            trailSpriteRenderers[i] = trails[i].GetComponent<SpriteRenderer>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        //사용자가 누른 라인 빛나게 처리
        if (Input.GetKey(KeyCode.G) && !pause) ShineTrail(0);
        if (Input.GetKey(KeyCode.H) && !pause) ShineTrail(1);
        if (Input.GetKey(KeyCode.J) && !pause) ShineTrail(2);
        if (Input.GetKey(KeyCode.Space) && !pause) ShineTrail(3);
        for(int i = 0; i < trailSpriteRenderers.Length; i++)
        {
            Color color = trailSpriteRenderers[i].color;
            color.a -= 0.01f;
            trailSpriteRenderers[i].color = color;
        }
    }

    //특정한 키를 눌러 라인을 빛나게함
    public void ShineTrail(int index)
    {
        Color color = trailSpriteRenderers[index].color;
        color.a = 0.32f;
        trailSpriteRenderers[index].color = color;
    }

    //노트 판정 이후에 판정 결과를 화면에 보여준다.
    void showJudgement()
    {
        string scoreFormat = "0";
        scoreText.text = score.ToString(scoreFormat);

        judgementSpriteAnimator.SetTrigger("Show");
        if (combo >= 2)
        {
            comboText.text = "COMBO " + combo.ToString();
            comboAnimator.SetTrigger("Show");
        }
        else
        {
            comboAnimator.SetTrigger("Hide");
        }
        if (maxCombo < combo)
        {
            maxCombo = combo;
        }
    }

    public void processJudge(judges judge, int noteType)
    {
        if (judge == judges.NONE) return;
        if (judge == judges.MISS)
        {
            judgementSpriteRenderer.sprite = judgeSprites[2];
            combo = 0;
        }
        else if (judge == judges.BAD)
        {
            judgementSpriteRenderer.sprite = judgeSprites[0];
            combo = 0;
            score += 50;
        }
        else
        {
            if (judge == judges.GOOD)
            {
                judgementSpriteRenderer.sprite = judgeSprites[1];
                score += 70;
            }
            else if (judge == judges.PERFECT)
            {
                judgementSpriteRenderer.sprite = judgeSprites[3];
                score += 100;
            }
            combo += 1;
            score += (float)combo * 0.1f;
        }
        showJudgement();
    }
}
