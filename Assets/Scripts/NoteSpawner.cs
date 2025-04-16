using System.Collections.Generic;
using UnityEngine;

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
    private readonly float initialPos = 20f * 2 / 1.3f;

    void Start()
    {
        AssignCurrentFile();
        Debug.Log(StaticData.jsonFile);
        Debug.Log(StaticData.jsonFile.units);
        Debug.Log(StaticData.jsonFile.velocities);
        noteSpeed = (StaticData.jsonFile.units["0"] / StaticData.jsonFile.velocities["0"]["0"]) * 2 * 1.3f;
        gameManager.GetComponent<BeatScroller>().noteSpeed = noteSpeed;
        SpawnNotes();
    }

    void AssignCurrentFile()
    {
        currentJson = StaticData.jsonFile;
        audioSource.clip = songs[0];
}

    void SpawnNotes()
    {

        //Debug.Log(currentJson.notes.Length);

        var times = 0;
        foreach (StaticData.ANote note in currentJson.notes)
        {
            note.MultiplyNotePosition(2);
            note.MultiplyNoteLength(2);

            Debug.Log(note.position);
            Debug.Log(note.length);

            if (note.length == 2)
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

        //Debug.Log(blueNotes.transform.childCount + redNotes.transform.childCount + greenNotes.transform.childCount + yellowNotes.transform.childCount);
    }

    void TapNoteSpawner(int noteCount, GameObject prefab, StaticData.ANote note, string noteName, Transform noteTransform, List<string> noteArray, float zPos)
    {
        currentNote = Instantiate(
            prefab,
            new Vector3(note.position + initialPos, 0.8f, zPos), 
            Quaternion.identity
        );
        currentNote.name = $"{noteName} ({noteCount})";
        currentNote.transform.parent = noteTransform;
        currentNote.tag = "Note";
        noteArray.Add(currentNote.name);
        //Debug.Log(note.length);
    }

    void HoldNoteSpawner(int noteCount, GameObject prefab, StaticData.ANote note, string noteName, Transform noteTransform, List<string> noteArray, float zPos)
    {
        currentNote = Instantiate(
            prefab, 
            new Vector3(note.position + initialPos + note.length / 2, 0.8f, zPos), // (float.Parse(note.position) + float.Parse(note.length)) - (float.Parse(note.length) / 2) * noteSpeed
            Quaternion.identity
        );
        currentNote.name = $"{noteName} ({noteCount})";
        currentNote.transform.parent = noteTransform;
        currentNote.tag = "HoldNote";
        noteArray.Add(currentNote.name);
        currentNote.GetComponent<BoxCollider>().size = new Vector3(note.length, 1, 1);
        foreach (Transform childTransform in currentNote.transform)
        {
            if (childTransform.name == "Base")
            {
                childTransform.transform.localScale = new Vector3(note.length, 0.4f, 2f);
            }
            else
            {
                childTransform.transform.localScale = new Vector3(note.length - 0.75f, 0.2f, 1.5f);
            }
        }
    }
}

