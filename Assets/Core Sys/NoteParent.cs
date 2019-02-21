using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NoteParent : MonoBehaviour
{
    [HideInInspector]
    public GameObject Origin;
    [HideInInspector]
    public EdgeManager Edges;
    public int position;//0-127
    public float SpawnTime;//this can be interpreted as beat
    public float tempo;
    public int beatLife;
    [NonSerialized]
    public bool setup ;

    // Start is called before the first frame update
    public void Awake()
    {
        Origin= GameObject.Find("Origin");
        Edges = Origin.GetComponent<EdgeManager>();
    }

    public void Setup(int position, float SpawnTime,float tempo, int beatLife)
    {
        this.position = position;
        this.SpawnTime = SpawnTime;
        this.tempo = tempo;
        this.beatLife = beatLife;
        setup = true;//flag for whether the note was properly set up
    }

    public void OnEnable()
    {
        if(Edges.origin.offset!=0)
        {
            position = (position - (Edges.origin.offset % 128) + 128) % 128; //hooray for wayne
            transform.position = Vector3.Lerp(Edges.origin.points[position],Edges.terminal.points[position],0);
        }
    }
    
    public void Update()
    {
        float progress = (Time.time - SpawnTime ) / (beatLife/tempo*60);//potential weirdness if not spawning at proper time(ie super speed)
        transform.position = Vector3.Lerp(Edges.origin.points[position],Edges.terminal.points[position],progress);
        if(progress>1.1f) // fine tune for allowance for hitting notes, etc.
        {
            Destroy(gameObject);
        }
    }
}
