using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class HintController : MonoBehaviour
{
    private GameGrid _gameGrid;

    [Inject]
    void Construct(GameGrid gameGrid)
    {
        _gameGrid = gameGrid;
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
