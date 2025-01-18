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
    private LevelScriptableObject levelDataFromScriptableObject;
    [SerializeField] private GameGrid gameGrid;
    
    private LevelData levelData;
    private ItemFactory _itemFactory;

    #endregion

    #region Injections

    [Inject]
    void Construct(ItemFactory itemFactory)
    {
        _itemFactory = itemFactory;
        //, GameGrid _gameGrid
        //gameGrid = _gameGrid;
    }
    
    #endregion
    private void Start()
    {
        LimitFps();// I limit the fps for battery life.
        PrepareLevel();
        InitializeDropFilController();
    }

    private void LimitFps()
    {
        int refreshRate = Screen.currentResolution.refreshRate;
        Application.targetFrameRate = (refreshRate >= 90) ? 90 : 60; 
    }

    public LevelInfo GetLevelInfo(int levelIndex)
    {
        string levelName = "Level" + levelIndex.ToString();
        levelDataFromScriptableObject = Resources.Load<LevelScriptableObject>("Levels/" + levelName);
        
        LevelInfo levelInfo = new LevelInfo
        {
            levelNumber = levelDataFromScriptableObject.levelNumber,
            gridWidth = levelDataFromScriptableObject.gridWidth,
            gridHeight = levelDataFromScriptableObject.gridHeight,
            moveCount = levelDataFromScriptableObject.moveCount,
            grid = levelDataFromScriptableObject.grid
        };
        return levelInfo;
    }

    private void PrepareLevel()
    {
        levelData = new LevelData(gameGrid.levelInfo);

        for (int i = 0; i < gameGrid.levelInfo.gridHeight; ++i)
        {
            for (int j = 0; j < gameGrid.levelInfo.gridWidth; ++j)
            {
                var cell = gameGrid.Cells[j,i];

                var itemType = levelData.GridData[gameGrid.levelInfo.gridHeight - i-1, j];
                var item = ItemFactory.Instance.CreateItem(itemType, gameGrid.itemsParent);
                if (item == null) continue;
                
                cell.item = item;
                item.transform.position = cell.transform.position;
            }
        }
    }

    private void InitializeDropFilController()
    {
        DropFillController.Instance.Initialize(gameGrid, levelData);
    }
}