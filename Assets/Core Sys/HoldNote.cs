using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldNote : NoteParent
{
    public float duration;
    float progress2;
    // Start is called before the first frame update
    //
    // beatlife describes how long it takes to lerp from spawn time to terminal.
    //duration will describe how long the hold note lasts in seconds. args[] 0.
    LineRenderer lr; 
    BoxCollider2D box;
    public override void Setup(int position, float spawnTime,
         float tempo, int beatLife, object[] args = null)
    {
        //args[0] will be duration
        this.lr = GetComponent<LineRenderer>();
        box = GetComponent<BoxCollider2D>();
        this.position = position;
        this.SpawnTime = spawnTime;
        this.tempo = tempo;
        this.beatLife = beatLife;
        this.duration = (float)args[0];
        box.enabled = false; //it will be enabled by default when awakened, so disable till it hits the line.
        State = "setup";
    }


    //im thinking maybe i can have the xy tiling correspond to the tempo in world space.
    //
    public override void Update()
    {
        float t = Time.time;
        progress = (Time.time - SpawnTime ) / (60*beatLife/tempo);
        progress2 = (Time.time - (duration + SpawnTime) ) / (60*beatLife/tempo);  //allowed to be negative, in which case, point2 is zero.  

        if(progress2 >= 1 + mstolerance/1000f/(beatLife/tempo*60f) )
        {   
            if(State != "ended")
            {
                State = "ended";
            }
        }
        else if(progress + mstolerance/1000f/(beatLife/tempo*60f) >= 1 )
        {
            if(State!= "hittable")
            {
                State = "hittable";
                box.enabled = true;
                transform.position = Edges.terminal.points[position]; //the hitbox is centered on the position, so until its hittable keep the box
                //disabled, and when it is put it on the terminal line
            }
        }


        
        Vector3[] ends = new Vector3[lr.positionCount];
        lr.GetPositions(ends); 
        ends[0] = Vector3.Lerp(Edges.origin.points[position],Edges.terminal.points[position],progress2);
        ends[1] = Vector3.LerpUnclamped(Edges.origin.points[position],Edges.terminal.points[position],progress);
        lr.SetPositions(ends);
        if(progress2>1.0f)
        {
            Destroy(gameObject);
        }
    }

    public virtual void miss() {}
    public virtual void hit(){}
    public virtual void touch(TouchPhase phase){}
}
