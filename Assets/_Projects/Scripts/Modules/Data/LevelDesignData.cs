using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDesignData", menuName = "ScriptableObjects/LevelDesignData", order = 1)]
public class LevelDesignData : ScriptableObject
{
    public List<LevelDesign> _levelList = new List<LevelDesign>();
    public int maxWave => _levelList.Count;
}


[Serializable]
public class LevelDesign
{
    public int level;
    public List<KeyValuePair<GameObject, int>> enemyList = new List<KeyValuePair<GameObject, int>>();
}
