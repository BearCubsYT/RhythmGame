using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] public StaticData.MapJson currentMap;

    private void GenerateLevelButtons()
    {
        int count = 0;
        int row = 0;
        int col = 0;

        foreach (TextAsset Map in jsonFiles)
        {
            StaticData.MapJson MapObject = JsonUtility.FromJson<StaticData.MapJson>(Map.ToString());
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

    private void DisplayStats(StaticData.MapJson MapObject)
    {
        currentMap = MapObject;
        SongNameText.text = "Song: " + MapObject.name;
        NoteCountText.text = "Note Count: " + MapObject.notes.Length;
        StatsPrefab.SetActive(true);
    }

    public void ChangeScene()
    {
        StaticData.jsonFile = currentMap;
        SceneManager.LoadScene(2);
    }

    private void Start()
    {
        GenerateLevelButtons();
    }
}
