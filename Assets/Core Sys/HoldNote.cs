using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldNote : NoteParent
{
    float duration;
    // Start is called before the first frame update
    public override void Setup(int position, float HitTime,
         float tempo, int beatLife, params object[] args)
    {
        //args[0] will be duration
        this.position = position;
        this.SpawnTime = HitTime- beatLife/tempo;
        this.tempo = tempo;
        this.beatLife = beatLife;
        this.duration = (float)args[0];
        State = "setup";
    }
    //im thinking maybe i can have the xy tiling correspond to the tempo in world space.
    // Update is called once per frame
    void Update()
    {
        
    }
}
