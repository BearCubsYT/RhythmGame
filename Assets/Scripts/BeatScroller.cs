using System.Runtime.CompilerServices;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float noteSpeed;
    [SerializeField] private GameObject notes;

    public bool gameActive = false;
    public bool gameOver = false;

    // Update is called once per frame
    void Update()
    {
        if (gameActive) {
            Debug.Log(Time.deltaTime);
            notes.transform.position -= new Vector3(noteSpeed * Time.deltaTime, 0f, 0f);

        }
    }
}
