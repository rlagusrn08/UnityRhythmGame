using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    //2차원 리스트로 1,2,3,4 리스트에 10개씩 데이터 넣기

    public List<GameObject> Notes;
    private List<List<GameObject>> poolsOfNotes;
    public int noteCount = 20;
    private bool more = true;

    void Start()
    {
        poolsOfNotes = new List<List<GameObject>>();
        for(int i = 0; i < Notes.Count; i++) //노트 종류만큼 반복
        {
            poolsOfNotes.Add(new List<GameObject>());
            for(int j = 0; j < noteCount; j++)// 10개씩 생성
            {
                GameObject obj = Instantiate(Notes[i]);
                obj.SetActive(false);
                poolsOfNotes[i].Add(obj);
            }
        }
    }

    public void Judge(int noteType)
    {
        foreach(GameObject obj in poolsOfNotes[noteType - 1])
        {
            if (obj.activeInHierarchy)
            {
                obj.GetComponent<NoteBehavior>().Judge();
            }
        }
    }
    public GameObject getObject(int noteType)
    {
        foreach(GameObject obj in poolsOfNotes[noteType - 1])
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        if (more)
        {
            GameObject obj = Instantiate(Notes[noteType - 1]);
            poolsOfNotes[noteType - 1].Add(obj);
            return obj;
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
