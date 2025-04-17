using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    public class Units
    {
        public int[] keys;
        public int[] values;
     
        public Dictionary<int, int> units;

        public Units(int[] keys, int[] values)
        {
            this.keys = keys;
            this.values = values;
            this.units = Deserialize(this.keys, this.values);
        }
    }

    [System.Serializable]
    public class Velocities
    {
        public int[] keys;
        public int[] values;

        public Dictionary<int, int> velocities;

        public Velocities(int[] keys, int[] values)
        {
            this.keys = keys;
            this.values = values;
            this.velocities = Deserialize(this.keys, this.values);
        }
    }

    [System.Serializable]
    public class MapJson
    {
        public string name;
        public string artist;
        public Units units;
        public Velocities velocities;

        public ANote[] notes;

        public MapJson(string name, string artist, Units units, Velocities track_velocities, ANote[] notes)
        {
            this.name = name;
            this.artist = artist;
            this.units = units;
            this.velocities = track_velocities;
            this.notes = notes;
        }
    }

    public static Dictionary<int, int> Deserialize(int[] keys, int[] values)
    {
        Dictionary<int, int> DeserializedDictionary = new();

        for (int i = 0; i < keys.Length; i++)
        {
            DeserializedDictionary[keys[i]] = values[i];
        }

        return DeserializedDictionary;
    }

    public static MapJson jsonFile;
}
