using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class NoteSpawner : MonoBehaviour
{
    public TextAsset[] jsonFiles;
    private StaticData.MapJson currentJson;

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

    void Start()
    {
        AssignCurrentFile();
        noteSpeed = StaticData.jsonFile.units / StaticData.jsonFile.seconds;
        gameManager.GetComponent<BeatScroller>().noteSpeed = noteSpeed;
        SpawnNotes();
    }

    void AssignCurrentFile()
    {
        currentJson = JsonUtility.FromJson<StaticData.MapJson>(jsonFiles[0].ToString()); // StaticData.jsonFile;
        //var times = 0;
        //foreach (var song in songs)
        //{//{
        //    if (song.name == currentJson.name)
        //    {
        //        audioSource.clip = songs[times];
        //        break;
        //    }
        //    times++;
        //}
    }

    void SpawnNotes()
    {
        var times = 0;
        foreach (StaticData.ANote note in currentJson.notes)
        {
            if (note.length == "1")
            {
                if (note.note == "0")
                {
                    TapNoteSpawner(blueNoteCount, bluePrefab, note, "Blue Note", blueNotes.transform, gameManager.GetComponent <Stats>().blueNotes, 3.5f);
                    blueNoteCount++;
                }
                if (note.note == "1")
                {
                    TapNoteSpawner(yellowNoteCount, yellowPrefab, note, "Yellow Note", yellowNotes.transform, gameManager.GetComponent<Stats>().yellowNotes, 1.125f);
                    yellowNoteCount++;
                }
                if (note.note == "2")
                {
                    TapNoteSpawner(greenNoteCount, greenPrefab, note, "Green Note", greenNotes.transform, gameManager.GetComponent<Stats>().greenNotes, -1.125f);
                    greenNoteCount++;
                }
                if (note.note == "3")
                {
                    TapNoteSpawner(redNoteCount, redPrefab, note, "Red Note", redNotes.transform, gameManager.GetComponent<Stats>().redNotes, -3.5f);
                    redNoteCount++;
                }
            }
            else
            {
                if (note.note == "0")
                {
                    HoldNoteSpawner(blueNoteCount, blueHoldPrefab, note, "Blue Note", blueNotes.transform, gameManager.GetComponent<Stats>().blueNotes, 3.5f);
                    blueNoteCount++;
                }
                if (note.note == "1")
                {
                    HoldNoteSpawner(yellowNoteCount, yellowHoldPrefab, note, "Yellow Note", yellowNotes.transform, gameManager.GetComponent<Stats>().yellowNotes, 1.125f);
                    yellowNoteCount++;
                }
                if (note.note == "2")
                {
                    HoldNoteSpawner(greenNoteCount, greenHoldPrefab, note, "Green Note", greenNotes.transform, gameManager.GetComponent<Stats>().greenNotes, -1.125f);
                    greenNoteCount++;
                }
                if (note.note == "3")
                {
                    HoldNoteSpawner(redNoteCount, redHoldPrefab, note, "Red Note", redNotes.transform, gameManager.GetComponent<Stats>().redNotes, -3.5f);
                    redNoteCount++;
                }
            }
            times++;
            if (times == currentJson.notes.Length)
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

    void TapNoteSpawner(int noteCount, GameObject prefab, StaticData.ANote note, string noteName, Transform noteTransform, List<string> noteArray, float zPos)
    {
        currentNote = Instantiate(
            prefab,
            new Vector3(float.Parse(note.position) * noteSpeed + 5, 0.8f, zPos), 
            Quaternion.identity
        );
        currentNote.name = $"{noteName} ({noteCount})";
        currentNote.transform.parent = noteTransform;
        currentNote.tag = "Note";
        noteArray.Add(currentNote.name);
    }

    void HoldNoteSpawner(int noteCount, GameObject prefab, StaticData.ANote note, string noteName, Transform noteTransform, List<string> noteArray, float zPos)
    {
        currentNote = Instantiate(
            prefab, 
            new Vector3((float.Parse(note.position) + float.Parse(note.length)) - (float.Parse(note.length) / 2) * noteSpeed + 5, 0.8f, zPos), 
            Quaternion.identity
        );
        currentNote.name = $"{noteName} ({noteCount})";
        currentNote.transform.parent = noteTransform;
        currentNote.tag = "HoldNote";
        currentNote.transform.localScale = new Vector3(float.Parse(note.length), 1, 1);
        noteArray.Add(currentNote.name);
    }
}

