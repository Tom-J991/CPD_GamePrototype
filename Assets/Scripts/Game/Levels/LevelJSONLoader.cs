// Level JSON Loader
// by: Halen Finlay
// date: 08/09/2023
// last modified: 21/09/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;

[System.Serializable]
public class LevelData
{
    public float time;
    public float speed;
    public float[] targets;
    public HighScore[] highscores;
}

public class LevelJSONLoader : MonoBehaviour
{
    public LevelData data;

    // Start is called before the first frame update
    void Awake()
    {
        LoadLevelData();
    }

    private void LoadLevelData()
    {
        // https://www.newtonsoft.com/json/help/html/SerializingJSON.htm
        data = JsonConvert.DeserializeObject<LevelData>(File.ReadAllText("Assets/Level Data/level0.json"));
    }
}
