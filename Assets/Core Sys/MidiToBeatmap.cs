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
        MidiFile mf = MidiFile.Read("Assets/Midis/multiChannelTest.mid");
        var tempoMap = mf.GetTempoMap();
        foreach(TrackChunk tc in mf.GetTrackChunks())
        {
            using (var notesManager = new NotesManager(tc.Events))
            {
                NotesCollection notes = notesManager.Notes;
                IOrderedEnumerable<Note> sortedNotes = notes.OrderBy(n => n.Time);
                foreach(Note n in sortedNotes)
                {
                    MetricTimeSpan metricTime = TimeConverter.ConvertTo<MetricTimeSpan>(n.Time, tempoMap);
                    SequenceTrackNameEvent trackNameEvent = (SequenceTrackNameEvent) tc.Events[0]; //this may fail if the first event isnt a trackname
                    var bpm = tempoMap.Tempo.AtTime(n.Time).BeatsPerMinute;
                    string trackName = trackNameEvent.Text;
                    switch(trackName)
                    {
                        case "TestNote":
                            GetComponent<NoteOrigin>().EnqueueNote( trackName ,n.NoteNumber, 
                                metricTime.TotalMicroseconds/1000000f, tempoMap.Tempo.AtTime(n.Time).BeatsPerMinute , lifetime:4);
                            break;
                        case "TestNote2":
                            GetComponent<NoteOrigin>().EnqueueNote( trackName ,n.NoteNumber, 
                                metricTime.TotalMicroseconds/1000000f, tempoMap.Tempo.AtTime(n.Time).BeatsPerMinute , lifetime:4);
                            break;
                        default:
                            Debug.Log("ChunkId not found: " + trackName);
                            break;
                    }
                    
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
