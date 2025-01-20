using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;
public class GameGrid : MonoBehaviour
{
    #region Privates

    [SerializeField] private LevelController levelController;
    [SerializeField] private Cell cellPrefab;
    
    #endregion
    
    #region Publics

    public LevelInfo levelInfo;
    public int _moveCount; 
    public int rows { get; private set; } 
    public int colums { get; private set; }
    public Cell[,] Cells { get; private set; }
    
    public Transform cellsParent;
    public Transform itemsParent;
    public Transform particlesParent;

    #endregion

    private void Awake()
    {
        LoadLevelInfo();
        InitCells(); //Initialize Cells
        PrepareCells();
    }

    private void InitCells() 
    {
        Cells = new Cell[colums, rows];
        ResizeBoard(rows, colums);
        CreateCells();
    } 
    private void PrepareCells()
    {
        for (int y = 0; y < rows; y++)
            for (int x = 0; x < colums; x++)
            {
                Cells[x, y].Prepare(x, y, this);
            }
    }
    private void CreateCells()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < colums; j++)
            {
                Cells[j, i] = Instantiate(cellPrefab, Vector3.zero, Quaternion.identity, cellsParent);
            }
        }
        
    } 
    private void ResizeBoard(int rows, int colums)
    {
        Transform currentTransform = this.transform;
        
        float newX = (9 - colums) * 0.5f;
        float newY = (9 - rows) * 0.5f;
        
        transform.localPosition = new Vector3(newX, newY, currentTransform.position.z);

    } 
    private void LoadLevelInfo()
    {
        int levelIndex = PlayerPrefs.GetInt("Level");
        levelInfo = levelController.GetLevelInfo(levelIndex);

        _moveCount = levelInfo.moveCount;
        rows = levelInfo.gridHeight;
        colums = levelInfo.gridWidth;
    }
}
