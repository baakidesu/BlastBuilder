using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LevelController : MonoBehaviour
{
    #region Privates

    private DropFillController _dropFillController;
    private LevelScriptableObject levelDataFromScriptableObject;
    
    [SerializeField] private GameGrid gameGrid;
    [SerializeField] private GameObject shuffleSprite;
    
    private LevelData levelData;
    private ItemFactory _itemFactory;

    #endregion

    #region Injections

    [Inject]
    void Construct(ItemFactory itemFactory, DropFillController dropFillController)
    {
        _itemFactory = itemFactory;
        //, GameGrid _gameGrid
        //gameGrid = _gameGrid;
        _dropFillController = dropFillController;
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
    public void PrepareLevel()
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
        _dropFillController.Initialize(gameGrid, levelData);
    }
    public void ResetGrid()
    {
        AudioController.Instance.PlaySoundEffect(SoundEffects.Shuffle);
        shuffleSprite.transform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Yoyo);
        
        shuffleSprite.transform.DOScale(1, 0.5f)
            .OnComplete(() =>
            {
                shuffleSprite.transform.DOScale(0, 0.5f);
            });
        ClearAllItems(); 
        GenerateNewItems();
    }
    private void ClearAllItems()
    {
        for (int i = 0; i < gameGrid.levelInfo.gridHeight; ++i)
        {
            for (int j = 0; j < gameGrid.levelInfo.gridWidth; ++j)
            {
                var cell = gameGrid.Cells[j, i];
                
                if (cell.item != null)
                {
                    Destroy(cell.item.gameObject);
                    cell.item = null;
                }
            }
        }
    } 
    private void GenerateNewItems()
    {
        for (int i = 0; i < gameGrid.levelInfo.gridHeight; ++i)
        {
            for (int j = 0; j < gameGrid.levelInfo.gridWidth; ++j)
            {
                var cell = gameGrid.Cells[j, i];
                
                var itemType = LevelData.GetRandomCubeItemType(); 
                var item = ItemFactory.Instance.CreateItem(itemType, gameGrid.itemsParent);
            
                if (item == null) continue;

                cell.item = item;
                item.transform.position = cell.transform.position;
            }
        }
    }
}