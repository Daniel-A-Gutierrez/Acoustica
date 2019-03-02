using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PresentScore : MonoBehaviour
{
    Scoring score ;
    TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("Origin").GetComponent<Scoring>();
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float s = (100f*score.hits/(score.N*1.0f));
        tmp.SetText("" + s.ToString("0.00") + "%");
    }
}
