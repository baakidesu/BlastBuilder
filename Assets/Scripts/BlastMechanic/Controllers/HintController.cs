using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class HintController : MonoBehaviour
{
    private GameGrid _gameGrid;
    private MatchController _matchController;

    [Inject]
    void Construct(MatchController matchController, GameGrid gameGrid)
    {
        _gameGrid = gameGrid;
        _matchController = matchController;
    } 
    private void Update()
    {
        Hint();
    } 
    private void Hint()
    {
        var visitedCells = new List<Cell>();

        for (int y = 0; y < _gameGrid.rows; y++)
        {
            for (int x = 0; x < _gameGrid.colums; x++)
            {
                var cell = _gameGrid.Cells[x,y];

                if (cell.item == null || visitedCells.Contains(cell)) continue;

                var validCells = _matchController.FindMatches(cell, cell.item.GetMatchType());
                var validNormalItemCount = _matchController.CountMatchedNormalItems(validCells);
                
                visitedCells.AddRange(validCells);

                for (int i = 0; i < validNormalItemCount; i++)
                {
                    var thisItem = validCells[i].item;

                    HintSpriteUpdate(thisItem, validNormalItemCount);
                }
            }
        }
    }
    private void HintSpriteUpdate(Item item, int matchedCount)
    {
        if (matchedCount > 4) //A,B,C
        {
            item.HintUpdateToSprite(ItemType.Hint, matchedCount);

        }else //Default
        {
            item.HintUpdateToSprite(item.itemType, matchedCount);
        }
    }
}
