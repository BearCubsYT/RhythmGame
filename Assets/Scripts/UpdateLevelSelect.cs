using System;
using System.Runtime.CompilerServices;
using TMPro;
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
        ButtonPrefab.SetActive(true);

        foreach (TextAsset Map in jsonFiles)
        {
            MapJson MapString = JsonUtility.FromJson<MapJson>(Map.ToString());
            Debug.Log(MapString.name);
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
