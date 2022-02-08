using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager; 
    public List<Stage> stage = new List<Stage>();
    void Awake()
    {
        levelManager = this;
    }
    [System.Serializable]
    public class Stage
    {
        public GameObject stage;
        public List<Enemy> enemy;
    }
}
