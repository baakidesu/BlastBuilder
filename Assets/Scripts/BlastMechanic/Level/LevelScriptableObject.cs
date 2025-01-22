using UnityEngine;

[CreateAssetMenu(fileName = "LevelScriptableObject", menuName = "ScriptableObjects/Level")]
public class LevelScriptableObject : ScriptableObject
{
    public int levelNumber;
    public int colorCount;
    public int gridWidth;
    public int gridHeight;
    public int moveCount;
    public int pointToGather;
    public bool didWin = false;
}
