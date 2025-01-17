using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class HintController : MonoBehaviour
{
    [SerializeField]private GameGrid _gameGrid;

    /*[Inject]
    void Construct(GameGrid gameGrid)
    {
        _gameGrid = gameGrid;
    }*/

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

                var validCells = MatchController.Instance.FindMatches(cell, cell.item.GetMatchType());
                var validNormalItemCount = MatchController.Instance.CountMatchedNormalItems(validCells);
                
                visitedCells.AddRange(validCells);

                for (int i = 0; i < validNormalItemCount; i++)
                {
                    var thisItem = validCells[i].item;

                    //CheckHint(thisItem, validNormalItemCount);
                    HintSpriteUpdate(thisItem, validNormalItemCount);
                }
            }
        }
    }

    private void SpriteUpdateForHint(Item item, int count)
    {
        item.HintUpdateToSprite(item.itemType, count);
    }
    

    private void CheckHint(Item item, int count)
    {
        if (count > 1)
        {
            //if (item.Particle != null) return;
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
