using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    public int noteType;

    void Start()
    {
        
    }

    
    void Update()
    {
        //노트를 아래로 움직이게 함
        transform.Translate(Vector3.down * GameManager.instance.noteSpeed);
    }
}
