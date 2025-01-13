using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelScriptableObject", menuName = "ScriptableObjects/Level")]
public class LevelScriptableObject : ScriptableObject
{
    [SerializeField]
    int LevelNumber;
    public int levelNumber {get => LevelNumber; private set => LevelNumber = value;}
    
    [SerializeField]
    int GridWidth;
    public int gridWidth {get => GridWidth; private set => GridWidth = value;}
    
    [SerializeField]
    int GridHeight;
    public int gridHeight {get => GridHeight; private set => GridHeight = value;}
    
    [SerializeField]
    int MoveCount;
    public int moveCount {get => MoveCount; private set => MoveCount = value;}
    
    [SerializeField]
    string[] Grid;
    public string[] grid {get => Grid; private set => Grid = value;}
    
}
