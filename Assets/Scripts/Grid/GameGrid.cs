using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class GameGrid : MonoBehaviour
{
    #region Privates

    private LevelController levelController;

    #endregion
    
    
    #region Publics

    public LevelInfo levelInfo;
    
    public int Rows { get; private set; }
    public int Columns { get; private set; }

    public Cell[,] Cells { get; private set; }

    #endregion

    #region Injections

    [Inject]
    void Construct(LevelController _levelController)
    {
        levelController = _levelController;
    }

    #endregion

    private void Awake()
    {
        LoadLevelInfo();
    }

    private void LoadLevelInfo()
    {
        //int levelIndex = PlayerPrefs.GetInt("Level"); //TODO

        int levelIndex = 1;
        levelInfo = levelController.GetLevelInfo(levelIndex); 
        
    }
}
