using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Cell : MonoBehaviour
{
    #region Publics
    public List<Cell> neighbours { get; private set; }
    public List<Cell> allArea { get; private set; }
    
    [HideInInspector] public Cell firstCellBelow;
    [HideInInspector] public bool isFillingCell;
    
    [HideInInspector] public int x;
    [HideInInspector] public int y;
    
    public GameGrid gameGrid { get; private set; }
    
    public TMP_Text cellText;

    #endregion

    #region Privates
    
    private Item _item;

    #endregion
    public Item item // For reaching the cell's item.
    {
        get
        {
            return _item;
        }
        set
        {
            if (_item == value) return;

            var oldItem = _item;
            _item = value;

            if (oldItem != null && Equals(oldItem.Cell, this))
                oldItem.Cell = null;
            
            if (value != null)
                value.Cell = this;
        }
    }

    public void Prepare(int _x, int _y, GameGrid _gameGrid)
    {
       gameGrid = _gameGrid;
       x = _x;
       y = _y;
       transform.localPosition = new Vector3(x, y);
       isFillingCell = (y == gameGrid.rows-1);

       UpdateName(); 
       UpdateNeighbours(); //This method is responsible for updating neighbours in up, down, left, right and assigns firstCellBelow
       UpdateAllNeighbours(); //This method is responsible for updating all neighbours

    }

    private void UpdateAllNeighbours()
    {
        allArea = GetNeighbours(Direction.Up, Direction.UpRight, Direction.Right, Direction.DownRight, Direction.Down, Direction.DownLeft, Direction.Left, Direction.UpLeft);
    }

    private void UpdateNeighbours() 
    {
        neighbours = GetNeighbours(Direction.Up, Direction.Down, Direction.Left, Direction.Right);
        firstCellBelow = GetAllNeighbourWithDirection(Direction.Down);
    }

    private List<Cell> GetNeighbours(params Direction[] directions)
    {
        var neighbours = new List<Cell>();

        foreach (var direction in directions)
        {
            var neighbour = GetAllNeighbourWithDirection(direction);
            if (neighbour != null) neighbours.Add(neighbour);
        }
        
        return neighbours;
    }

    private Cell GetAllNeighbourWithDirection(Direction direction)
    {
        var dumpX = x;
        var dumpY = y;

        switch (direction)
        {
            case Direction.None: break;
            case Direction.Right:     dumpX += 1;         break;
            case Direction.Left:      dumpX -= 1;         break;
            case Direction.UpRight:   dumpX += 1; dumpY += 1; break;
            case Direction.DownRight: dumpX += 1; dumpY -= 1; break;
            case Direction.UpLeft:    dumpX -= 1; dumpY += 1; break;
            case Direction.DownLeft:  dumpX -= 1; dumpY -= 1; break;
            case Direction.Up:                dumpY += 1; break;
            case Direction.Down:              dumpY -= 1; break;
        }

        if (dumpX >= gameGrid.colums || dumpY >= gameGrid.rows || dumpX < 0 || dumpY < 0) return null;
        
        return gameGrid.Cells[dumpX, dumpY];
        
    }

    public Cell GetFallTarget()
    {
        var targetCell = this;
        if (targetCell.firstCellBelow != null && targetCell.firstCellBelow.item == null )
        {
            targetCell = targetCell.firstCellBelow;
        }

        return targetCell;
    }

    public void CellTapped()
    {
        if (item == null)
        {
            return;
        }
        //TODO
        MatchController.Instance.ExplodeMatchingCells(this);
    }
    private void UpdateName()
    {
        var cellName = x + " " + y;
        cellText.text = cellName;
        gameObject.name = "Cell: " + cellName;
    }
}