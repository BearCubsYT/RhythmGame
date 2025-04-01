using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class UpdateLevelSelect : MonoBehaviour
{
    [SerializeField] private GameObject StatsPrefab;
    [SerializeField] private GameObject ButtonPrefab;
    [SerializeField] private TextMeshProUGUI NoteCountText;
    [SerializeField] private TextMeshProUGUI SongNameText;
    [SerializeField] public TextAsset[] jsonFiles;

    [System.Serializable]
    private class MapJson
    {
        public string name;
        public string artist;
        public int seconds;
        public int units;

        public MapJson(string name, string artist, int seconds, int units)
        {
            this.name = name;
            this.artist = artist;
            this.seconds = seconds;
            this.units = units;
        }
    }

    private void GenerateLevelButtons()
    {
        float count = 0f;

        //if (count / 2)
        //{
            
        //}

        foreach (TextAsset Map in jsonFiles)
        {
            MapJson MapString = JsonUtility.FromJson<MapJson>(Map.ToString());
            //GameObject SongButton = Instantiate(ButtonPrefab, new Vector3(-420 + ((count % 2) * 750), 1480 - (Math.Floor(count / 2.0f) * 200), 0), Quaternion.identity);
            count++;
        }

    }

    public void DisplayStats()
    {
        NoteCountText.text = "Note Count: " + "placeholder";
        SongNameText.text = "Music: " + "placeholder";
        StatsPrefab.SetActive(true);
    }

    private void Start()
    {
        GenerateLevelButtons();
    }
}
