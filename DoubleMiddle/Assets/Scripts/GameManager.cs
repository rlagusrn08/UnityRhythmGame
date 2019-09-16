using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    /*
     * 판정변수
     * BAD : 1
     * GOOD : 2
     * PERFECT : 3
     * MISS : 4
     */
    public enum judges { NONE = 0, BAD, GOOD, PERFECT, MISS};

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
