using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class DropFillController : MonoBehaviour
{
    private bool isActive;
    private GameGrid _gameGrid;
    private Cell[] fillingCells;
    private ItemFactory _itemFactory;

    [Inject]
    void Construct(ItemFactory itemFactory)
    {
        _itemFactory = itemFactory;
    } 
    private void Update()
    {
        if (!isActive) return;
        
        DoFalls();
        DoFills();
    } 
    public void Initialize(GameGrid gameGrid, LevelData levelData)
    {
        _gameGrid = gameGrid;
        FindFillingCells();
        StartFall();
    } 
    private void DoFills()
    {
        for (int i = 0; i < fillingCells.Length; i++)
        {
            var cell = fillingCells[i];

            if (cell.item == null)
            {
                cell.item = _itemFactory.CreateItem(LevelData.GetRandomCubeItemType(), _gameGrid.itemsParent); //itemFactory

                float posY = 0;
                var targetCell = cell.GetFallTarget().firstCellBelow;

                if (targetCell != null)
                {
                    if (targetCell.item != null)
                    {
                        posY = targetCell.item.transform.position.y + 1;
                    }
                }

                Vector3 pos = cell.transform.position;
                pos.y += 2;
                
                if (pos.y <= posY)
                {
                    pos.y = posY;
                }

                if (cell.item == null) continue;
                
                cell.item.transform.position = pos;
                cell.item.Fall();
            }
        }
    } 
    private void FindFillingCells()
    {
        var listOfCells = new List<Cell>();

        for (int y = 0; y < _gameGrid.rows; y++)
        {
            for (int x = 0; x < _gameGrid.colums; x++)
            {
                var cell = _gameGrid.Cells[x, y];

                if (cell != null && cell.isFillingCell)
                {
                    listOfCells.Add(cell);
                }
            }
        }
        fillingCells = listOfCells.ToArray();
    } 
    public void DoFalls()
    {
        
        for (int y = 0; y < _gameGrid.rows; y++)
        {
            for (int x = 0; x < _gameGrid.colums; x++)
            {
                var cell = _gameGrid.Cells[x, y];

                if (cell.item != null && cell.firstCellBelow != null && cell.firstCellBelow.item == null)
                    cell.item.Fall();
            }
        }
    } 
    public void StartFall() { isActive = true; }
}
