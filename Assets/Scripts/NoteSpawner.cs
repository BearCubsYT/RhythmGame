using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class NoteSpawner : MonoBehaviour
{
    public TextAsset[] jsonFiles;
    private TextAsset currentJsonFile;

    // Sound
    [SerializeField] private AudioSource audioSource;
    public AudioClip[] songs;

    //Note Variables
    private GameObject currentNote;
    private int blueNoteCount;
    private int yellowNoteCount;
    private int greenNoteCount;
    private int redNoteCount;

    // Game Manager
    [SerializeField] private GameObject gameManager;
    private float noteSpeed;

    // Note Prefabs
    [SerializeField] private GameObject bluePrefab;
    [SerializeField] private GameObject yellowPrefab;
    [SerializeField] private GameObject greenPrefab;
    [SerializeField] private GameObject redPrefab;
    [SerializeField] private GameObject blueHoldPrefab;
    [SerializeField] private GameObject yellowHoldPrefab;
    [SerializeField] private GameObject greenHoldPrefab;
    [SerializeField] private GameObject redHoldPrefab;

    // Note Empty Game Objects
    [SerializeField] private GameObject blueNotes;
    [SerializeField] private GameObject yellowNotes;
    [SerializeField] private GameObject greenNotes;
    [SerializeField] private GameObject redNotes;

    [System.Serializable]
    private class NoteArray
    {
        public Note[] notes;
    }

    [System.Serializable]
    private class Note
    {
        public string note;
        public string position;
        public string length;

        public Note(string note, string position, string length)
        {
            this.note = note;
            this.position = position;
            this.length = length;
        }
    }

    void Start()
    {
        AssignCurrentFile();
        gameManager.GetComponent<BeatScroller>().noteSpeed = 25;
        noteSpeed = 5;
        SpawnNotes();
    }

    void AssignCurrentFile()
    {
        currentJsonFile = jsonFiles[0];
        //var times = 0;
        //foreach (var song in songs)
        //{//{
        //    if (song.name == currentJsonFile.name)
        //    {
        //        audioSource.clip = songs[times];
        //        break;
        //    }
        //    times++;
        //}
    }

    void SpawnNotes()
    {
        NoteArray notes = JsonUtility.FromJson<NoteArray>(currentJsonFile.ToString());
        var times = 0;
        foreach (Note note in notes.notes)
        {
            if (note.length == "1")
            {
                if (note.note == "0")
                {
                    TapNoteSpawner(blueNoteCount, bluePrefab, note, "Blue Note", blueNotes.transform, gameManager.GetComponent <Stats>().blueNotes, 3.5f);
                }
                if (note.note == "1")
                {
                    TapNoteSpawner(yellowNoteCount, yellowPrefab, note, "Yellow Note", yellowNotes.transform, gameManager.GetComponent<Stats>().yellowNotes, 1.125f);
                }
                if (note.note == "2")
                {
                    TapNoteSpawner(greenNoteCount, greenPrefab, note, "Green Note", greenNotes.transform, gameManager.GetComponent<Stats>().greenNotes, -1.125f);
                }
                if (note.note == "3")
                {
                    TapNoteSpawner(redNoteCount, redPrefab, note, "Blue Note", redNotes.transform, gameManager.GetComponent<Stats>().redNotes, -3.5f);
                }
            }
            else
            {
                if (note.note == "0")
                {
                    HoldNoteSpawner(blueNoteCount, blueHoldPrefab, note, "Blue Note", blueNotes.transform, gameManager.GetComponent<Stats>().blueNotes, 3.5f);
                }
                if (note.note == "1")
                {
                    HoldNoteSpawner(yellowNoteCount, yellowHoldPrefab, note, "Yellow Note", yellowNotes.transform, gameManager.GetComponent<Stats>().yellowNotes, 1.125f);
                }
                if (note.note == "2")
                {
                    HoldNoteSpawner(greenNoteCount, greenHoldPrefab, note, "Green Note", greenNotes.transform, gameManager.GetComponent<Stats>().greenNotes, -1.125f);
                }
                if (note.note == "3")
                {
                    HoldNoteSpawner(redNoteCount, redHoldPrefab, note, "Red Note", redNotes.transform, gameManager.GetComponent<Stats>().redNotes, -3.5f);
                }
            }
            times++;
            if (times == notes.notes.Length)
            {
                if (currentNote.CompareTag("Note"))
                {
                    currentNote.tag = "Last Note";
                }
                else
                {
                    currentNote.tag = "Last HoldNote";
                }
            }
        }
    }

    void TapNoteSpawner(int noteCount, GameObject prefab, Note note, string noteName, Transform noteTransform, List<string> noteArray, float zPos)
    {
        noteCount++;
        currentNote = Instantiate(prefab, new Vector3(float.Parse(note.position) * noteSpeed + 5, 0.8f, zPos), Quaternion.identity);
        currentNote.name = $"{noteName} ({noteCount})";
        currentNote.transform.parent = noteTransform;
        currentNote.tag = "Note";
        noteArray.Add(currentNote.name);
    }

    void HoldNoteSpawner(int noteCount, GameObject prefab, Note note, string noteName, Transform noteTransform, List<string> noteArray, float zPos)
    {
        noteCount++;
        currentNote = Instantiate(prefab, new Vector3((float.Parse(note.position + note.length) - (float.Parse(note.length) / 2)) * gameManager.GetComponent<BeatScroller>().noteSpeed, 0.8f, zPos), Quaternion.identity);
        currentNote.name = $"{noteName} ({noteCount})";
        currentNote.transform.parent = noteTransform;
        currentNote.tag = "HoldNote";
        currentNote.transform.localScale = new Vector3(float.Parse(note.length), 1, 1);
        noteArray.Add(currentNote.name);
    }
}

