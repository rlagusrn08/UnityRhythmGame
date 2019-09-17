﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    //하나의 노트에 대한 정보를 담는 노트(Note) 클래스 정의
    class Note
    {
        public int noteType { get; set; }
        public int order { get; set; }
        public Note(int noteType,int order)
        {
            this.noteType = noteType;
            this.order = order;
        }
    }

    IEnumerator AwaitMakeNote(Note note)
    {
        int noteType = note.noteType;
        int order = note.order;
        yield return new WaitForSeconds(order * beatInterval);
        MakeNote(note);
    }

    public GameObject[] Notes;

    private ObjectPooler noteObjectPooler;
    private List<Note> notes = new List<Note>();
    private float x, z, startY = 5.0f;
    private float beatInterval = 1f;

    void MakeNote(Note note)
    {
        GameObject obj = noteObjectPooler.getObject(note.noteType);
        //설정된 시작 라인으로 노트를 이동.
        x = obj.transform.position.x;
        z = obj.transform.position.z;
        obj.transform.position = new Vector3(x, startY, z);
        obj.GetComponent<NoteBehavior>().Initialize();
        obj.SetActive(true);
    }

    void Start()
    {
        //노트 테스트
        noteObjectPooler = gameObject.GetComponent<ObjectPooler>();
        notes.Add(new Note(1, 1));
        notes.Add(new Note(2, 1));
        notes.Add(new Note(3, 3));
        notes.Add(new Note(4, 3));
        notes.Add(new Note(1, 5));
        notes.Add(new Note(2, 6));
        notes.Add(new Note(3, 7));
        notes.Add(new Note(4, 8));
        notes.Add(new Note(2, 9));
        notes.Add(new Note(4, 10));
        //모든 노트를 정해진 시간에 출발하도록 설정
        for(int i = 0; i<notes.Count; i++)
        {
            StartCoroutine(AwaitMakeNote(notes[i]));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
