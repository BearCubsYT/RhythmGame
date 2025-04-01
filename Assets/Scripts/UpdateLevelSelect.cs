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
    [SerializeField] private TextMeshProUGUI SongnameText;
    [SerializeField] private CanvasRenderer Contant;
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
        int count = 0;
        int row = 0;
        int col = 0;

        foreach (TextAsset Map in jsonFiles)
        {
            MapJson MapObject = JsonUtility.FromJson<MapJson>(Map.ToString());
            GameObject SongButton = Instantiate(ButtonPrefab, new Vector3(-420 + (col * 800), 1480 - (row * 300), 0), Quaternion.identity);
            
            TextMeshProUGUI ButtonSong = SongButton.GetComponentInChildren<TextMeshProUGUI>();
            ButtonSong.text = MapObject.name;

            SongButton.transform.SetParent(Contant.transform, false);
            SongButton.SetActive(true);
            
            count++;
            
            if (count % 2 == 0)
            {
                row++;
            }

            if (col == 0)
            {
                col = 1;
            }
            else
            {
                col = 0;
            }
        }

    }

    public void DisplayStats(string x)
    {


        NoteCountText.text = "Note Count: " + "placeholder";
        SongnameText.text = "Music: " + "placeholder";
        StatsPrefab.SetActive(true);
    }

    private void Start()
    {
        GenerateLevelButtons();
    }
}
