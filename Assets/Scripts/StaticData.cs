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
        public Dictionary<string, int> units;
        public Dictionary<string, Dictionary<string, int>> velocities;

        public ANote[] notes;

        public MapJson(string name, string artist, Dictionary<string, int> units, Dictionary<string, Dictionary<string, int>> track_velocities, ANote[] notes)
        {
            this.name = name;
            this.artist = artist;
            this.units = units;
            this.velocities = track_velocities;
            this.notes = notes;
        }
    }

    public static MapJson jsonFile;
}
