using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNote : NoteParent 
{
    // Start is called before the first frame update
    public new void Awake()
    {
        base.Awake();
    }

    public new void Update()
    {
        base.Update();
        if(progress >= 1  + mstolerance/1000f/(beatLife/tempo*60f) )
        {   
            if(State != "missed")
            {
                miss();
            }
            State = "missed";
        }
        else if(progress + mstolerance/1000f/(beatLife/tempo*60f) >= 1 )
            State = "hittable";
    }

    public override void miss()
    {

    }

    
    public override void hit()
    {
        print("Hit Note!");
        score.hits ++;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public override void touch(TouchPhase phase)
    {
        print("touched");
        if(State == "hittable" & phase == TouchPhase.Began)
        {
            hit();
        }
    }

}
