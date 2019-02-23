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

    Dictionary<string,Queue<GameObject>> channels; //the key for a note type and its channel should be identical.
    Dictionary<string, GameObject> noteTypes;
    float lastSpawnTime;

    void Awake() //midi to beatmap happens at start and requires this so, it must be awake
    {
        TestNotes = new Queue<GameObject>();
        channels =  new Dictionary<string,Queue<GameObject>>(); //the key for a note type and its channel should be identical.
        noteTypes = new Dictionary<string, GameObject>();
        channels.Add("TestNote",TestNotes);
        noteTypes.Add("TestNote", TestNote);
        //GenerateBeatmap(TestNotes);
        lastSpawnTime = 0;
    }
    //enqueues a note into testNotes 
    //void func(params object[] stuff) {} -> func('a', "B" , 48.3, 2)
    //void func(int a =0, int b=0, int c=0) {} ->  func(a: 3, c:2)
    //this will need to take variable args at some point.
    public void EnqueueNote(string channelName ,int position, float spawnTime, float tempo, int lifetime)
    {
        if(lastSpawnTime > spawnTime) { Debug.Log("Beatmap problem : Successive notes not chronologically sequential. " 
            + spawnTime + " , " + lastSpawnTime);}
        if(channels[channelName] == null) { Debug.Log("Channel Name Not found : " + channelName + "." ) ; }
        if(noteTypes[channelName] == null) { Debug.Log("Channel Name Not found : " + channelName + "." ) ; }

        GameObject go = Instantiate(noteTypes[channelName],Vector3.zero,Quaternion.identity);

        switch(channelName)
        {
            case "TestNote":
                go.GetComponent<NoteParent>().Setup(position,spawnTime,tempo,lifetime);
                break;
            default:
                Debug.Log("Switch statement: case not found");
                break;
        }
        
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
