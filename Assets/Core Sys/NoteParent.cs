using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NoteParent: MonoBehaviour 
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
    public float progress;
    public float mstolerance;
    //awakened, 
    public String State;
    protected Scoring score;

    public void Awake()
    {
        State = "awakened";
        progress = 0f;
        Origin= GameObject.Find("Origin");
        Edges = Origin.GetComponent<EdgeManager>();
        score = Origin.GetComponent<Scoring>();
    }

    public virtual void Setup(int position, float HitTime, float tempo, int beatLife,params object[] args)
    {
        this.position = position;
        this.SpawnTime = HitTime- beatLife/tempo;
        this.tempo = tempo;
        this.beatLife = beatLife;
        State = "setup";
    }

    public void OnEnable()
    {
        if(Edges.origin.offset!=0)// match the position to point relative to current cycle offset
        {
            position = (position - (Edges.origin.offset % 128) + 128) % 128; //hooray for wayne
            transform.position = Edges.origin.points[position];
        }
        State = "enabled";
    }
    /* so i want each note to have 3 or 4 states. 
    1 : activated, progressing towards terminal
    2 : hit : plays animation, notifies something, destroys self
    3 : missed :  continues moving, changes color or plays other animation, notifies something */
    public void Update()
    {
    
        progress = (Time.time - SpawnTime ) / (beatLife/tempo*60);      //potential weirdness if not spawning at proper time(ie super speed)
        transform.position = Vector3.LerpUnclamped(Edges.origin.points[position],Edges.terminal.points[position],progress); 
        if(progress>2.0f) // fine tune for allowance for hitting notes, etc.
        {
            Destroy(gameObject);
        }
    }

    public virtual void miss() {}
    public virtual void hit(){}
    public virtual void touch(TouchPhase phase){}

    /*So im thinking perhaps, put a fat edge collider on the terminal line and 
    tapping the screen in the edge collider notifies the bar which then 
    determines which vert is closest to the tap,
    circle casts aroudn that vert and notifies each note in the radius, which then 
    decides what to do based on progress.
    
    so this would work for tap notes, but we also have hold and slide. we need to pass
    the touch phase, and check touch input continuously. actually wait this approach works
    if i check for touch input in the "on collision enter" method. 
    
    the simplest method might be to detect touch input, circle cast from it to the note,
    if the note's progress is within a threshold count it as hit.
    */ 



}
