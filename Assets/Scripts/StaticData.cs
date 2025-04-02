using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData : MonoBehaviour 
{
    [System.Serializable]
    public class ANote
    {
        public string note;
        public float position;
        public float length;

        public ANote(string note, float position, float length)
        {
            this.note = note;
            this.position = position;
            this.length = length;
        }
        
        public void MultiplyNotePosition(int Multiplier)
        {
            this.position *= Multiplier;
        }
        public void MultiplyNoteLength(int Multiplier)
        {
            this.length *= Multiplier;
        }
    }

    [System.Serializable]
    public class MapJson
    {
        public string name;
        public string artist;
        public int seconds;
        public int units;

        public ANote[] notes;

        public MapJson(string name, string artist, int seconds, int units, ANote[] notes)
        {
            this.name = name;
            this.artist = artist;
            this.seconds = seconds;
            this.units = units;
            this.notes = notes;
        }
    }

    public static MapJson jsonFile;
}
