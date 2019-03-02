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
using System;
using System.Linq;
[RequireComponent(typeof (NoteOrigin))]
public class MidiToBeatmap : MonoBehaviour
{
    void LoadBeatmapFromMidi(string midipath)
    {
        MidiFile mf = MidiFile.Read(midipath);
        var tempoMap = mf.GetTempoMap();
        
        foreach(TrackChunk tc in mf.GetTrackChunks())
        {
            using (var notesManager = new NotesManager(tc.Events))
            {
                NotesCollection notes = notesManager.Notes;
                IOrderedEnumerable<Note> sortedNotes = notes.OrderBy(n => n.Time);
                SequenceTrackNameEvent trackNameEvent;
                //this may fail if the first event isnt a trackname
                try //this will take us to the next track chunk in the case that this one doesnt have a name event
                {
                    trackNameEvent = (SequenceTrackNameEvent) tc.Events[0];
                }
                catch(InvalidCastException e)
                {
                    Debug.Log(e.ToString());
                    continue;
                }
                string trackName = trackNameEvent.Text;
                if(trackName == "" || trackName == null) {Debug.Log("Trackname is null");}
                foreach(Note n in sortedNotes)
                {
                    MetricTimeSpan metricTime = TimeConverter.ConvertTo<MetricTimeSpan>(n.Time, tempoMap);
                    var bpm = tempoMap.Tempo.AtTime(n.Time).BeatsPerMinute;
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
                        case "Tap":
                            GetComponent<NoteOrigin>().EnqueueNote( trackName ,n.NoteNumber, 
                                metricTime.TotalMicroseconds/1000000f, tempoMap.Tempo.AtTime(n.Time).BeatsPerMinute , lifetime:4);
                            break;
                        case "Hold":
                            MetricTimeSpan duration = TimeConverter.ConvertTo<MetricTimeSpan>(n.Length,tempoMap);
                            object[] args = {duration.TotalMicroseconds/1000000f};
                            GetComponent<NoteOrigin>().EnqueueNote( trackName ,n.NoteNumber, 
                                metricTime.TotalMicroseconds/1000000f, tempoMap.Tempo.AtTime(n.Time).BeatsPerMinute , lifetime:4, args);
                            break;
                        case "Slide":
                            GetComponent<NoteOrigin>().EnqueueNote( trackName ,n.NoteNumber, 
                                metricTime.TotalMicroseconds/1000000f, tempoMap.Tempo.AtTime(n.Time).BeatsPerMinute , lifetime:4);
                            break;
                        case "Miss":
                            GetComponent<NoteOrigin>().EnqueueNote( trackName ,n.NoteNumber, 
                                metricTime.TotalMicroseconds/1000000f, tempoMap.Tempo.AtTime(n.Time).BeatsPerMinute , lifetime:4);
                            break;
                        case "Tap Hold":
                            GetComponent<NoteOrigin>().EnqueueNote( trackName ,n.NoteNumber, 
                                metricTime.TotalMicroseconds/1000000f, tempoMap.Tempo.AtTime(n.Time).BeatsPerMinute , lifetime:4);
                            break;
                        case "Tap Slide":
                            GetComponent<NoteOrigin>().EnqueueNote( trackName ,n.NoteNumber, 
                                metricTime.TotalMicroseconds/1000000f, tempoMap.Tempo.AtTime(n.Time).BeatsPerMinute , lifetime:4);
                            break;
                        case "Tap Release":
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
    //dependent on enqueue note, cannot be in awake. 
    void Start()
    {
        LoadBeatmapFromMidi("Assets/Midis/Quaoar Beatmap.mid");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
