using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class LevelController : MonoBehaviour
{
    #region Privates

    private DropFillController dropFillController;
    private LevelScriptableObject levelData;
    private GameGrid gameGrid;

    #endregion

    #region Publics

    

    #endregion

    #region Injections

    [Inject]
    void Construct(DropFillController _dropFillController, GameGrid _gameGrid)
    {
        //Debug.Log("LevelManager constructed");
        dropFillController = _dropFillController;
        gameGrid = _gameGrid;
    }


    #endregion


    private void Start()
    {
        GetLevelInfo(1);
    }

    public LevelInfo GetLevelInfo(int levelIndex)
    {
        string levelName = "Level" + levelIndex.ToString();
        levelData = Resources.Load<LevelScriptableObject>(levelName);
        
        LevelInfo levelInfo = new LevelInfo
        {
            levelNumber = levelData.levelNumber,
            gridWidth = levelData.gridWidth,
            gridHeight = levelData.gridHeight,
            moveCount = levelData.moveCount,
            grid = levelData.grid
        };

        return levelInfo;
    }
}