using UnityEngine;

[CreateAssetMenu(fileName = "LevelScriptableObject", menuName = "ScriptableObjects/Level")]
public class LevelScriptableObject : ScriptableObject
{
    public int levelNumber;
    public int gridWidth;
    public int gridHeight;
    public int moveCount;
    public string grid;

}
