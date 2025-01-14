using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LevelController : MonoBehaviour
{
    #region Privates

    private DropFillController dropFillController;
    private LevelScriptableObject levelData;
    private GameGrid gameGrid;

    #endregion

    #region Injections

    /*[Inject]
    void Construct(GameGrid _gameGrid)
    {
        gameGrid = _gameGrid;
    }*/
    
    #endregion


    private void Start()
    {
        //GetLevelInfo(1);
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

        Debug.Log(levelName);
        return levelInfo;
    }

    private void PrepareLevel()
    {
        //levelData = newLevelData()
    }
}