using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class MatchController : Singleton<MatchController>
{
    private void Start()
    {
        _visitedCells = new bool[_gameGrid.colums, _gameGrid.rows];
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 3f) //Limiting the timer because of battery optimisation
        {
            if (NoMoreMatches()) _levelController.ResetGrid();
            timer = 0f;
        }
    }

    #region Injections

    [Inject]
    private void Construct(LevelController levelController, GameController gameController, GameGrid gameGrid)
    {
        _gameGrid = gameGrid;
        _levelController = levelController;
        _gameController = gameController;
    }

    #endregion

    private bool NoMoreMatches()
    {
        for (var y = 0; y < _gameGrid.rows; y++)
        for (var x = 0; x < _gameGrid.colums; x++)
        {
            var cell = _gameGrid.Cells[x, y];

            if (cell == null || cell.item == null) continue;

            var validCells = FindMatches(cell, cell.item.GetMatchType());
            var validNormalItemCount = CountMatchedNormalItems(validCells);

            if (validNormalItemCount >= _minimumNumberOfSameColors) return false;
        }

        return true;
    }

    public List<Cell> FindMatches(Cell cell, MatchType matchType)
    {
        var matchedItems = new List<Cell>();
        ResetVisitedCells();
        FindMatchesRecursively(cell, matchType, matchedItems);

        return matchedItems;
    }

    private void ResetVisitedCells()
    {
        for (var x = 0; x < _visitedCells.GetLength(0); x++)
        for (var y = 0; y < _visitedCells.GetLength(1); y++)
            _visitedCells[x, y] = false;
    }

    private void
        FindMatchesRecursively(Cell cell, MatchType matchType,
            List<Cell> matchedItems) //Flood Fill Algorithm that uses DFS.
    {
        if (cell == null) return; //don't search if it is null

        var x = cell.x;
        var y = cell.y;

        if (_visitedCells[x, y]) return;

        if (cell.item != null && cell.item.GetMatchType() == matchType && cell.item.GetMatchType() != MatchType.None)
        {
            _visitedCells[x, y] = true;
            matchedItems.Add(cell);

            if (!cell.item.canClickable) return;

            var neighbours = cell.neighbours;
            if (neighbours.Count == 0) return;

            for (var i = 0; i < neighbours.Count; i++) FindMatchesRecursively(neighbours[i], matchType, matchedItems);
        }
    }

    public int CountMatchedNormalItems(List<Cell> cells)
    {
        var _count = 0;
        foreach (var cell in cells)
            if (cell.item.canClickable)
                _count++;
        return _count;
    }

    public void ExplodeMatchingCells(Cell cell)
    {
        var previousCells = new List<Cell>();

        var validItems = FindMatches(cell, cell.item.GetMatchType());
        var validNormalItemCount = validItems.Count;
        var validNormalItems = CountMatchedNormalItems(validItems);

        if (validNormalItems < _minimumNumberOfSameColors) return;

        for (var i = 0; i < validItems.Count; i++)
        {
            var explodedCell = validItems[i];

            ExplodeValidCellsInNeighbours(explodedCell, previousCells);

            var item = explodedCell.item;
            item.Execute();
        }

        if (validNormalItemCount is > 4 and < 8) //A State 
            _gameController.points += validNormalItemCount * 2;
        else if (validNormalItemCount is > 7 and < 10) //B State
            _gameController.points += validNormalItemCount * 3;
        else if (validNormalItemCount > 9) //C State 
            _gameController.points += validNormalItemCount * 5;
        else if (validNormalItems == 2)
            _gameController.points += 1;
        else if (validNormalItems == 3)
            _gameController.points += 2;
        else if (validNormalItems == 4) _gameController.points += 5;
        _ = _gameController.DecreaseMovesAsync();
    }

    private void ExplodeValidCellsInNeighbours(Cell cell, List<Cell> previousCells)
    {
        var explodedCellsInNeighbours = cell.neighbours;

        for (var j = 0; j < explodedCellsInNeighbours.Count; j++)
        {
            var neighbourCell = explodedCellsInNeighbours[j];
            var neighbourCellItem = neighbourCell.item;

            if (neighbourCellItem != null && !previousCells.Contains(neighbourCell))
            {
                previousCells.Add(neighbourCell);

                if (neighbourCellItem.canExplode) neighbourCellItem.Execute();
            }
        }
    }

    #region Privates

    private GameGrid _gameGrid;
    [SerializeField] private GameObject _gameGridObject;
    private LevelController _levelController;
    private MatchController _matchController;
    private GameController _gameController;

    private bool[,] _visitedCells;
    private readonly int _minimumNumberOfSameColors = 2;
    private float timer;

    #endregion
}