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
