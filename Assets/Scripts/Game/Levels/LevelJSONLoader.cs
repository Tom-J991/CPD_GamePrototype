// Level JSON Loader
// by: Halen Finlay
// date: 08/09/2023
// last modified: 13/09/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;

public class LevelData
{
    public float time { get; }
    public float speed { get; }
    public float[] targets { get; }
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
        Debug.Log($"Time Limit: {data?.time}");
        Debug.Log($"Speed Target: {data?.speed}");
        Debug.Log($"Score Targets: {data?.targets[0]}, {data?.targets[1]}, {data?.targets[2]}");
    }
}
