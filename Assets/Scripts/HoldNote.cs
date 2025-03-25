using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldNote : MonoBehaviour
{
    [SerializeField] private bool canBePressed = false;
    public bool isHoldFront = false;
    [SerializeField] private GameObject trigger;
    [SerializeField] private bool wasPressed = false;
    [SerializeField] private bool wasReleased = false;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject notes;
    private bool alreadyRan = false;

    private float pressDistance = 0f;
    private float releaseDistance = 0f;
    private float distance;
    private float addingScore;
    private float centerOfBoard = 5f;

    private float len = 3f;

    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject holdNote;

    private int perfectPlusScore = 1000;
    private int perfectScore = 800;
    private int greatScore = 600;
    private int goodScore = 500;



    Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void GameOver()
    {
        if (holdNote.CompareTag("Last HoldNote"))
        {
            gameManager.GetComponent<BeatScroller>().gameOver = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == trigger)
        {
            canBePressed = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject == trigger)
        {
            canBePressed = false;
            if (!wasPressed)
            {
                Debug.Log("Miss");
                UI.GetComponent<UI>().streak = 0;
                UI.GetComponent<UI>().multiplier = 0;
                gameManager.GetComponent<Stats>().missedHits += 1;
                gameManager.GetComponent<Stats>().blueNotes.Remove(holdNote.name);
                gameManager.GetComponent<Stats>().yellowNotes.Remove(holdNote.name);
                gameManager.GetComponent<Stats>().greenNotes.Remove(holdNote.name);
                gameManager.GetComponent<Stats>().redNotes.Remove(holdNote.name);
            }
            else
            {

            }
            GameOver();
            if (transform.position.z == 3.5)
            {
                Object.Destroy(holdNote);
            }
            if (transform.position.z == 1.125)
            {
                Object.Destroy(holdNote);
            }
            if (transform.position.z == -1.125)
            {
                Object.Destroy(holdNote);
            }
            if (transform.position.z == -3.5)
            {
                Object.Destroy(holdNote);
            }
        }
    }

    public void Pressing()
    {
        if (canBePressed)
        {
            if (Input.GetKeyDown(KeyCode.D) && transform.position.z == 3.5 && isHoldFront)
            {
                wasPressed = true;
                pressDistance = -notes.transform.position.x;
                gameManager.GetComponent<Stats>().blueNotes.Remove(holdNote.name);
                anim.SetTrigger("Holding");
                distance = centerOfBoard - (transform.position.x - (.5f * len) - .5f);
                distance = Mathf.Abs(distance);
            }
            if (Input.GetKeyDown(KeyCode.F) && transform.position.z == 1.125 && isHoldFront)
            {
                wasPressed = true;
                pressDistance = -notes.transform.position.x;
                gameManager.GetComponent<Stats>().yellowNotes.Remove(holdNote.name);
                anim.SetTrigger("Holding");
                distance = centerOfBoard - (transform.position.x - (.5f * len) - .5f);
                distance = Mathf.Abs(distance);
            }
            if (Input.GetKeyDown(KeyCode.J) && transform.position.z == -1.125 && isHoldFront)
            {
                wasPressed = true;
                pressDistance = -notes.transform.position.x;
                gameManager.GetComponent<Stats>().greenNotes.Remove(holdNote.name);
                anim.SetTrigger("Holding");
                distance = centerOfBoard - (transform.position.x - (.5f * len) - .5f);
                distance = Mathf.Abs(distance);
            }
            if (Input.GetKeyDown(KeyCode.K) && transform.position.z == -3.5 && isHoldFront)
            {
                wasPressed = true;
                pressDistance = -notes.transform.position.x;
                gameManager.GetComponent<Stats>().redNotes.Remove(holdNote.name);
                anim.SetTrigger("Holding");
                distance = centerOfBoard - (transform.position.x - (.5f * len) - .5f);
                distance = Mathf.Abs(distance);
            }
        }
        if (wasPressed)
        {
            if (-notes.transform.position.x - pressDistance >= len)
            {
                releaseDistance = pressDistance + len;
                wasReleased = true;
            }
            if (Input.GetKeyUp(KeyCode.D) && transform.position.z == 3.5)
            {
                releaseDistance = -notes.transform.position.x;
                wasReleased = true;
            }
            if (Input.GetKeyUp(KeyCode.F) && transform.position.z == 1.125)
            {
                releaseDistance = -notes.transform.position.x;
                wasReleased = true;
            }
            if (Input.GetKeyUp(KeyCode.J) && transform.position.z == -1.125)
            {
                releaseDistance = -notes.transform.position.x;
                wasReleased = true;
            }
            if (Input.GetKeyUp(KeyCode.K) && transform.position.z == -3.5)
            {
                releaseDistance = -notes.transform.position.x;
                wasReleased = true;
            }
        }
    }

    public void ScoreSystem()
    {
        if (wasReleased && !alreadyRan)
        {
            alreadyRan = true;

            if (distance <= 0.25)
            {
                Debug.Log("Perfect+");
                UI.GetComponent<UI>().streak += 1;
                gameManager.GetComponent<Stats>().perfectPlusHits += 1;
                addingScore = releaseDistance - pressDistance;
                if (addingScore - Mathf.Round(addingScore) == .5f)
                {
                    UI.GetComponent<UI>().score += Mathf.RoundToInt((Mathf.Round(addingScore) + 1) * perfectPlusScore * UI.GetComponent<UI>().multiplier);
                }
                else
                {
                    UI.GetComponent<UI>().score += Mathf.RoundToInt(Mathf.Round(addingScore) * perfectPlusScore * UI.GetComponent<UI>().multiplier);
                }
            }
            else if (distance > 0.25 && distance <= 0.5)
            {
                Debug.Log("Perfect");
                UI.GetComponent<UI>().streak += 1;
                gameManager.GetComponent<Stats>().perfectHits += 1;
                addingScore = releaseDistance - pressDistance;
                if (addingScore - Mathf.Round(addingScore) == .5f)
                {
                    UI.GetComponent<UI>().score += Mathf.RoundToInt((Mathf.Round(addingScore) + 1) * perfectScore * UI.GetComponent<UI>().multiplier);
                }
                else
                {
                    UI.GetComponent<UI>().score += Mathf.RoundToInt(Mathf.Round(addingScore) * perfectScore * UI.GetComponent<UI>().multiplier);
                }
            }
            else if (distance > 0.5 && distance <= 0.75)
            {
                Debug.Log("Great");
                UI.GetComponent<UI>().streak += 1;
                gameManager.GetComponent<Stats>().greatHits += 1;
                addingScore = releaseDistance - pressDistance;
                if (addingScore - Mathf.Round(addingScore) == .5f)
                {
                    UI.GetComponent<UI>().score += Mathf.RoundToInt((Mathf.Round(addingScore) + 1) * greatScore * UI.GetComponent<UI>().multiplier);
                }
                else
                {
                    UI.GetComponent<UI>().score += Mathf.RoundToInt(Mathf.Round(addingScore) * greatScore * UI.GetComponent<UI>().multiplier);
                }
            }
            else if (distance > 0.75)
            {
                Debug.Log("Good");
                UI.GetComponent<UI>().streak += 1;
                gameManager.GetComponent<Stats>().goodHits += 1;
                addingScore = releaseDistance - pressDistance;
                if (addingScore - Mathf.Round(addingScore) == .5f)
                {
                    UI.GetComponent<UI>().score += Mathf.RoundToInt((Mathf.Round(addingScore) + 1) * goodScore * UI.GetComponent<UI>().multiplier);
                }
                else
                {
                    UI.GetComponent<UI>().score += Mathf.RoundToInt(Mathf.Round(addingScore) * goodScore * UI.GetComponent<UI>().multiplier);
                }
            }
        }
    }

    void Update()
    {
        Pressing();

        ScoreSystem();
    }
}
