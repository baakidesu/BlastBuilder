using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

public class LevelController : MonoBehaviour
{
    [HideInInspector] public LevelScriptableObject levelDataFromScriptableObject;
    
    #region Privates

    private DropFillController _dropFillController;
    
    [SerializeField] private GameObject shuffleSprite;
    
    private LevelData levelData;
    private ItemFactory _itemFactory;
    private AudioController _audioController;
    private GameGrid _gameGrid;

    #endregion

    #region Injections

    [Inject]
    void Construct(ItemFactory itemFactory, DropFillController dropFillController, AudioController audioController, GameGrid gameGrid)
    {
        _itemFactory = itemFactory;
        _dropFillController = dropFillController;
        _audioController = audioController;
        _gameGrid = gameGrid;
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
        RefreshRate refreshRate = Screen.currentResolution.refreshRateRatio;
        Application.targetFrameRate = (refreshRate.denominator >= 90) ? 90 : 60; 
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
        };
        return levelInfo;
    } 
    public void PrepareLevel()
    {
        levelData = new LevelData(_gameGrid.levelInfo);

        for (int i = 0; i < _gameGrid.levelInfo.gridHeight; ++i)
        {
            for (int j = 0; j < _gameGrid.levelInfo.gridWidth; ++j)
            {
                var cell = _gameGrid.Cells[j,i];

                var itemType = levelData.GridData[_gameGrid.levelInfo.gridHeight - i-1, j];
                var item = _itemFactory.CreateItem(itemType, _gameGrid.itemsParent);
                if (item == null) continue;
                
                cell.item = item;
                item.transform.position = cell.transform.position;
            }
        }
    } 
    private void InitializeDropFilController()
    {
        _dropFillController.Initialize(_gameGrid, levelData);
    }
    public void ResetGrid()
    {
        _audioController.PlaySoundEffect(SoundEffects.Shuffle);
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
        for (int i = 0; i < _gameGrid.levelInfo.gridHeight; ++i)
        {
            for (int j = 0; j < _gameGrid.levelInfo.gridWidth; ++j)
            {
                var cell = _gameGrid.Cells[j, i];
                
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
        for (int i = 0; i < _gameGrid.levelInfo.gridHeight; ++i)
        {
            for (int j = 0; j < _gameGrid.levelInfo.gridWidth; ++j)
            {
                var cell = _gameGrid.Cells[j, i];
                
                var itemType = LevelData.GetRandomCubeItemType(); 
                var item = _itemFactory.CreateItem(itemType, _gameGrid.itemsParent);
            
                if (item == null) continue;

                cell.item = item;
                item.transform.position = cell.transform.position;
            }
        }
    }
}