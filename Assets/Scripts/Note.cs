using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Note : MonoBehaviour
{
    // Note Booleans
    private bool canBePressed = false;
    public bool isFront = false;
    private bool wasPressed = false;
    private bool alreadyRan = false;

    // Other Game Objects
    [SerializeField] private GameObject trigger;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject note;
    [SerializeField] private GameObject song;

    // Game Numbers
    private float distance = 0f;
    private float centerOfBoard = 5.00f;

    // How Much Score to Add Per Score Type
    private int perfectPlusScore = 1000;
    private int perfectScore = 800;
    private int greatScore = 600;
    private int goodScore = 500;
    
    // Animator
    Animator anim;

    // Lists of Every Note
    private List<string> blueNotes;
    private List<string> yellowNotes;
    private List<string> greenNotes;
    private List<string> redNotes;

    // Runs at Start of Program
    void Start()
    {
        // Gets Animator and Animations For Current Note
        anim = gameObject.GetComponent<Animator>();
        // Grabs List From Other Files and Makes Another Here
        blueNotes = gameManager.GetComponent<Stats>().blueNotes;
        yellowNotes = gameManager.GetComponent<Stats>().yellowNotes;
        greenNotes = gameManager.GetComponent<Stats>().greenNotes;
        redNotes = gameManager.GetComponent<Stats>().redNotes;
    }

    // Runs When Note Collides With Another Object and Sets What it Collides With To "col"
    void OnTriggerEnter(Collider col)
    {
        // Checks If Collision is With My Collider
        if(col.gameObject == trigger)
        {
            canBePressed = true;
        }
    }

    // Checks if Game is Over
    void GameOver()
    {
        // Checks if the Tag on Note is the "Last Note" Tag
        if (note.CompareTag("Last Note"))
        {
            // Sets the Game Over Boolean in BeatScroller Script to True
            gameManager.GetComponent<BeatScroller>().gameOver = true;
        }
    }

    // Runs When the Note Leaves the Trigger and Sets What it Exits To "col"
    void OnTriggerExit(Collider col)
    {
        // Checks if What it Leaves is the Collider
        if (col.gameObject == trigger)
        {
            canBePressed = false;
            // Makes Sure it Wasn't Pressed
            if (!wasPressed)
            {
                // Resets Streak and Multiplier Arrays in UI Script Attached to Game Manager
                UI.GetComponent<UI>().streak = 0;
                UI.GetComponent<UI>().multiplier = 0;
                // Adds One to Missed Hits Array in Stats Script Attached to Game Manager
                gameManager.GetComponent<Stats>().missedHits += 1;
                // Removes Note From Its Array in Stats Script Attached to Game Manager
                gameManager.GetComponent<Stats>().blueNotes.Remove(note.name);
                gameManager.GetComponent<Stats>().yellowNotes.Remove(note.name);
                gameManager.GetComponent<Stats>().greenNotes.Remove(note.name);
                gameManager.GetComponent<Stats>().redNotes.Remove(note.name);
            }
            GameOver();
            Object.Destroy(note);
        }
    }

    // Reusable Method For Each Note
    void WasPressed(List<string> notesList)
    {
        wasPressed = true;
        notesList.Remove(note.name);
        //anim.SetTrigger("Active");
    }

    // Checks if User is Pressing a Note
    void Pressing()
    {
        if (canBePressed)
        {
            // Checks Note Specific Key is Pressed, and the Position is the Notes Specific Position. Also That it is the Front Note For Its Color.
            if (Input.GetKeyDown(KeyCode.D) && transform.position.z == 3.5 && isFront)
            {
                // Passes In Blue Notes Array Into Was Pressed Function
                WasPressed(blueNotes);
            }
            if (Input.GetKeyDown(KeyCode.F) && transform.position.z == 1.125 && isFront)
            {
                // Passes In Yellow Notes Array Into Was Pressed Function
                WasPressed(yellowNotes);
            }
            if (Input.GetKeyDown(KeyCode.J) && transform.position.z == -1.125 && isFront)
            {
                // Passes In Green Notes Array Into Was Pressed Function
                WasPressed(greenNotes);
            }
            if (Input.GetKeyDown(KeyCode.K) && transform.position.z == -3.5 && isFront)
            {
                // Passes In Red Notes Array Into Was Pressed Function
                WasPressed(redNotes);
            }
        } 
    }

    // Reusable Method For Each Note
    void ApplyScore(int score, int count)
    {
        // Adds One to Streak and Multiplier Arrays in UI Script Attached to Game Manager
        UI.GetComponent<UI>().score += score * UI.GetComponent<UI>().multiplier;
        UI.GetComponent<UI>().streak += 1;
        count += 1;
    }

    void ScoreSystem()
    {
        if (wasPressed && !alreadyRan)
        {
            alreadyRan = true;
            distance = centerOfBoard - transform.position.x;
            distance = Mathf.Abs(distance);


            if (distance <= 0.25)
            {
                ApplyScore(perfectPlusScore, gameManager.GetComponent<Stats>().perfectPlusHits);
            }
            else if (distance > 0.25 && distance <= 0.5)
            {
                ApplyScore(perfectScore, gameManager.GetComponent<Stats>().perfectHits);
            }
            else if (distance > 0.5 && distance <= 0.75)
            {
                ApplyScore(greatScore, gameManager.GetComponent<Stats>().greatHits);
            }
            else if (distance > 0.75)
            {
                ApplyScore(goodScore, gameManager.GetComponent<Stats>().goodHits);
            }
        }
    }

    void MultiplierSystem()
    {
        if (UI.GetComponent<UI>().streak >= 3 && UI.GetComponent<UI>().streak < 6)
        {
            UI.GetComponent<UI>().multiplier = 2;
        }
        else if (UI.GetComponent<UI>().streak >= 6 && UI.GetComponent<UI>().streak < 9)
        {
            UI.GetComponent<UI>().multiplier = 4;
        }
        else if (UI.GetComponent<UI>().streak >= 9)
        {
            UI.GetComponent<UI>().multiplier = 6;
        }
        else
        {
            UI.GetComponent<UI>().multiplier = 1;
        }
    }

    void Update()
    {
        if (gameManager.GetComponent<BeatScroller>().gameActive == true)
        {
            song.SetActive(true);
            UI.SetActive(true);
            Pressing();
            ScoreSystem();
            MultiplierSystem();
        }
    }   
}