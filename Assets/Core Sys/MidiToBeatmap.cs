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

public class MidiToBeatmap : MonoBehaviour
{
    // Start is called before the first frame update
    void somemethod()
    {
        MidiFile mf = MidiFile.Read("Assets/Midis/e.mid");
        TrackChunk tc = (TrackChunk)mf.GetTrackChunks().First();
        using (var notesManager = new NotesManager(tc.Events))
        {
            NotesCollection notes = notesManager.Notes;
            IEnumerator<Note> ien = notes.GetEnumerator();
            foreach(Note n in notes)
            {
                //n = ien.Current;
                print(n.NoteNumber);
            }
            //var cSharpNotes = notes.Where(n => n.NoteName == Melanchall.DryWetMidi.MusicTheory.NoteName.CSharp);
            //notes.Add(new Note(Melanchall.DryWetMidi.MusicTheory.NoteName.A, 4)
            //{
            //    Time = 123,
            //    Length = 456,
            //    Velocity = (SevenBitNumber)45
            //});
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
