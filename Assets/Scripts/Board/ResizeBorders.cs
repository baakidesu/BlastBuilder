using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class ResizeBorders : MonoBehaviour
{
    private GameGrid gameGrid;
    
    [Inject]
    void Construct(GameGrid _gameGrid)
    {
        Debug.Log("ResizeBorders constructed");
        gameGrid = _gameGrid;
    }
    
    private const float widhtPadding = 0.35f;
    private const float heightPadding = 0.45f;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        
        Debug.Log("ResizeBorders işlemleri");
        float newWidth = gameGrid.levelInfo.gridWidth + widhtPadding;
        float newHeight = gameGrid.levelInfo.gridHeight + heightPadding;
        
        spriteRenderer.size = new Vector2(newWidth, newHeight);
        
    }
}
