using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeInput : MonoBehaviour
{
    public float touchRadius = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Collider2D[] overlaps = Physics2D.OverlapCircleAll(
                Camera.main.ScreenToWorldPoint( Input.mousePosition )
                ,touchRadius
                );//,LayerMask.NameToLayer("note"));
            foreach(Collider2D col in overlaps)
            {
                col.gameObject.GetComponent<NoteParent>().touch(TouchPhase.Began);
            }
        }
        if (Input.GetMouseButton(0))
        {
            Collider2D[] overlaps = Physics2D.OverlapCircleAll(
                Camera.main.ScreenToWorldPoint(Input.mousePosition)
                , touchRadius
                );//,LayerMask.NameToLayer("note"));
            foreach (Collider2D col in overlaps)
            {
                col.gameObject.GetComponent<NoteParent>().touch(TouchPhase.Stationary);
            }
        }

    }
}
