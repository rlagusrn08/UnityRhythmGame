using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    public int noteType;
    private GameManager.judges judge;
    private KeyCode keyCode; //테스트를 위한 키 입력
    void Start()
    {
        if (noteType == 1) keyCode = KeyCode.G;
        else if (noteType == 2) keyCode = KeyCode.H;
        else if (noteType == 3) keyCode = KeyCode.J;
        else if (noteType == 4) keyCode = KeyCode.Space;
    }

    
    void Update()
    {
        //노트를 아래로 움직이게 함
        transform.Translate(Vector3.down * GameManager.instance.noteSpeed);

        //사용자 키 입력 시
        if (Input.GetKey(keyCode))
        {
            //판정 테스트
            Debug.Log(judge);
            // 판정 선에 닿기 시작한 이후로는 노트 제거
            if(judge != GameManager.judges.NONE)
            {
                Destroy(gameObject);
            }
        }
    }

    //각 노트의 현재 위치에 따른 판정 수행
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Bad Line")
        {
            judge = GameManager.judges.BAD;
        }
        else if(other.gameObject.tag == "Good Line")
        {
            judge = GameManager.judges.GOOD;
        }
        else if (other.gameObject.tag == "Perfect Line")
        {
            judge = GameManager.judges.PERFECT;
        }
        else if (other.gameObject.tag == "Miss Line")
        {
            judge = GameManager.judges.MISS;
            Destroy(gameObject);
            Debug.Log(judge);
        }
        
    }
}
