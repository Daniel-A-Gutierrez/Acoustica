using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteOrigin : MonoBehaviour
{
    public GameObject TestNote;
    //so our general flow: read from a file to instantiate a few big ol deques of notes.
    //add note objects to the deques back, so that when they are being read, they are read from the front.
    //each frame, check the time, and generate notes from the deque until their start time is past the current time. 
    //"automation notes" will contain a string corresponding to a function, and strings corresponding to args for those functions.
    Queue<GameObject> TestNotes;
    void Start()
    {
        TestNotes = new Queue<GameObject>();
        GenerateBeatmap(TestNotes);
    }

    void GenerateBeatmap(Queue<GameObject> Notes)
    {
        int tempo = 60;
        int life = 4;
        for(int i = 0; i < 8000; i++)
        {
            GameObject go = Instantiate(TestNote,Vector3.zero,Quaternion.identity);
            GameObject go2 = Instantiate(TestNote,Vector3.zero,Quaternion.identity);
            go.GetComponent<NoteParent>().Setup(0 ,i/5f,tempo,life);
            go2.GetComponent<NoteParent>().Setup(64,i/5f,tempo,life);
            go.SetActive(false);
            go2.SetActive(false);
            Notes.Enqueue(go);
            Notes.Enqueue(go2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(TestNotes.Count > 0)
        {
            while(TestNotes.Peek().GetComponent<NoteParent>().SpawnTime <= Time.time)
            {
                GameObject g = TestNotes.Dequeue();
                g.SetActive(true);
                if(TestNotes.Count==0)
                    break;
            }
        }
        /* if(Random.Range(0,1f)>.9f)
        {
            Instantiate(TestNote,Vector3.zero,Quaternion.identity);
        }*/
    }
}
