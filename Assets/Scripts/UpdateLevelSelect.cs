using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UpdateLevelSelect : MonoBehaviour
{
    [SerializeField] private GameObject StatsPrefab;
    [SerializeField] private UnityEngine.UI.Button ButtonPrefab;
    [SerializeField] private TextMeshProUGUI NoteCountText;
    [SerializeField] private TextMeshProUGUI SongNameText;
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
            UnityEngine.UI.Button SongButton = Instantiate(ButtonPrefab, new Vector3(-420 + (col * 800), 1480 - (row * 300), 0), Quaternion.identity);
            
            SongButton.onClick.AddListener(() => DisplayStats(MapObject));

            TextMeshProUGUI ButtonSong = SongButton.GetComponentInChildren<TextMeshProUGUI>();
            ButtonSong.text = MapObject.name;

            SongButton.transform.SetParent(Contant.transform, false);
            
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

    private void DisplayStats(MapJson MapObject)
    {
        NoteCountText.text = "Note Count: " + MapObject.units.ToString();
        SongNameText.text = "Music: " + MapObject.name;
        StatsPrefab.SetActive(true);
    }

    private void Start()
    {
        GenerateLevelButtons();
    }
}
