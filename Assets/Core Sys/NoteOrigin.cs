using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NoteOrigin : MonoBehaviour
{
    //DONT EDIT
    public string[] channel_names = {"TestNote", "TestNote2", "Tap","Hold", "Slide", "Miss", "Tap Hold" , "Tap Slide" , "Tap Release"};
    public GameObject[] prefabs; //index should correspond to channel_names index
    public Scoring score;
    //so our general flow: read from a file to instantiate a few big ol deques of notes.
    //add note objects to the deques back, so that when they are being read, they are read from the front.
    //each frame, check the time, and generate notes from the deque until their start time is past the current time. 
    //"automation notes" will contain a string corresponding to a function, and strings corresponding to args for those functions.


    Dictionary<string,Queue<GameObject>> channels; //the key for a note type and its channel should be identical.
    Dictionary<string, GameObject> noteTypes;
    float lastSpawnTime;



    void Awake() //midi to beatmap happens at start and requires this so, it must be awake
    {
        channels =  new Dictionary<string,Queue<GameObject>>(); //the key for a note type and its channel should be identical.
        noteTypes = new Dictionary<string, GameObject>();
        score = GetComponent<Scoring>();
        for(int i = 0 ; i < channel_names.Length ; i++)
        {
            Queue<GameObject> Q = new Queue<GameObject>();
            channels.Add(channel_names[i],Q);
            noteTypes.Add(channel_names[i],prefabs[i]);
        }

        //GenerateBeatmap(TestNotes);
        lastSpawnTime = 0;
    }
    //enqueues a note into testNotes 
    //void func(params object[] stuff) {} -> func('a', "B" , 48.3, 2)
    //void func(int a =0, int b=0, int c=0) {} ->  func(a: 3, c:2)
    //this will need to take variable args at some point.
    public void EnqueueNote(string channelName ,int position, float spawnTime, float tempo, int lifetime, object[] args = null)
    {
        if(channels[channelName] == null) { Debug.Log("Channel Name Not found : " + channelName + "." ) ; }
        GameObject go = Instantiate(noteTypes[channelName],new Vector3(0,0,-100),Quaternion.identity);
//what setup do we do?
        switch(channelName) 
        {
            case "TestNote":
                go.GetComponent<NoteParent>().Setup(position,spawnTime,tempo,lifetime);
                break;
            case "TestNote2":
                go.GetComponent<NoteParent>().Setup(position,spawnTime,tempo,lifetime);
                break;
            case "Tap":
                go.GetComponent<NoteParent>().Setup(position,spawnTime,tempo,lifetime);
                break;
            case "Slide":
                go.GetComponent<NoteParent>().Setup(position,spawnTime,tempo,lifetime);
                break;
            case "Hold":
                go.GetComponent<HoldNote>().Setup(position,spawnTime,tempo,lifetime,args);
                break;
            case "Miss":
                go.GetComponent<NoteParent>().Setup(position,spawnTime,tempo,lifetime);
                break;
            case "Tap Hold":
                go.GetComponent<NoteParent>().Setup(position,spawnTime,tempo,lifetime);
                break;
            case "Tap Slide":
                go.GetComponent<NoteParent>().Setup(position,spawnTime,tempo,lifetime);
                break;
            case "Tap Release":
                go.GetComponent<NoteParent>().Setup(position,spawnTime,tempo,lifetime);
                break;
            default:
                Debug.Log("Switch statement: case not found");
                break;
        }
        score.N ++;
        go.SetActive(false);
        channels[channelName].Enqueue(go);
    }

    // Update is called once per frame
    void Update()
    {
        foreach( Queue<GameObject> channel in channels.Values)
        {
            if(channel.Count > 0)
            {
                while(channel.Peek().GetComponent<NoteParent>().SpawnTime <= Time.time)
                {
                    channel.Peek().SetActive(true);
                    channel.Dequeue();
                    if(channel.Count==0)
                        break;
                }
            }
        }
    }
}
