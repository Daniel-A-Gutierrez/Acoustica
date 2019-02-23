using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Smf;
using Melanchall.DryWetMidi.Smf.Interaction;
using Melanchall.DryWetMidi.Tools;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Standards;
using Melanchall.DryWetMidi.Devices;
using Melanchall;

using System.Linq;
[RequireComponent(typeof (NoteOrigin))]
public class MidiToBeatmap : MonoBehaviour
{
    // Start is called before the first frame update
    void somemethod()
    {
        MidiFile mf = MidiFile.Read("Assets/Midis/e.mid");
        var tempoMap = mf.GetTempoMap();
        foreach(TrackChunk tc in mf.GetTrackChunks())
        {
            using (var notesManager = new NotesManager(tc.Events))
            {
                NotesCollection notes = notesManager.Notes;
                IEnumerator<Note> ien = notes.GetEnumerator();
                foreach(Note n in notes)
                {
                    MetricTimeSpan metricTime = TimeConverter.ConvertTo<MetricTimeSpan>(n.Time, tempoMap);
                    GetComponent<NoteOrigin>().EnqueueNote( "TestNote"/* tc.ChunkId*/,n.NoteNumber, 
                                metricTime.Milliseconds/1000f, tempoMap.Tempo.AtTime(n.Time).BeatsPerMinute , lifetime:4);
                    /* for later 
                    switch(tc.ChunkId)
                    {
                        case "TestNote":
                            GetComponent<NoteOrigin>().EnqueueNote( "TestNote" '''tc.ChunkId''' ,n.NoteNumber, 
                                metricTime.Milliseconds/1000f, tempoMap.Tempo.AtTime(n.Time).BeatsPerMinute , lifetime:4);
                            break;
                        default:
                            Debug.Log("ChunkId not found");
                            break;
                    }
                    */
                }
            }
        }
    }
    void Start()
    {
        somemethod();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
