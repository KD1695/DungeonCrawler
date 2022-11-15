using System;
using System.Collections;
using System.Collections.Generic;
using PCG;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] private int souls = 0;
    [SerializeField] private int level = 1;
    [SerializeField] private DungeonGenerator dungeonGenerator;

    public event Action<int> updatedSoulsCount; 
    public event Action<int> updatedLevelCount;

    void Awake()
    {
        DontDestroyOnLoad(this);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
    }

    private void Start()
    {
        dungeonGenerator.InitiateDungeonGeneration(level+2);
        SceneManager.activeSceneChanged += OnLevelWasLoaded;
    }

    private void OnLevelWasLoaded(Scene scene1, Scene scene2)
    {
        dungeonGenerator.InitiateDungeonGeneration(level+2);
    }

    public void RemoveSouls(int soulsToRemove)
    {
        souls -= soulsToRemove;
        updatedSoulsCount(souls);
    }

    public void AddSouls(int soulsToAdd)
    {
        souls += soulsToAdd;
        updatedSoulsCount(souls);
    }

    public void LevelUp()
    {
        level++;
        updatedLevelCount(level);
    }
}
