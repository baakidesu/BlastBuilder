using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class LevelData
{
    public ItemType[,] GridData { get; protected set; }

    private static readonly Random _random = new Random();
    private static int _colorIndex;

    public static int GetRandomNumber(int min, int max) // Faster than Unity's random.
    {
        lock (_random) // Sync
        {
            return _random.Next(min, max);
        }
    }

    public LevelData(LevelInfo levelInfo)
    {
        GridData = new ItemType[levelInfo.gridHeight, levelInfo.gridWidth];

        if (levelInfo.colorCount <= 6 && levelInfo.colorCount >= 1)
        {
            _colorIndex = levelInfo.colorCount + 1;
        }
        else
        {
            _colorIndex = 7;
        }
        
        for (int i = levelInfo.gridHeight - 1; i >= 0; --i)
        {
            for (int j = 0; j < levelInfo.gridWidth; ++j)
            {
                GridData[i, j] = ((ItemType[])Enum.GetValues(typeof(ItemType)))[GetRandomNumber(1, _colorIndex)];
            }
        }
    }

    public static ItemType GetRandomCubeItemType()
    {
        return ((ItemType[])Enum.GetValues(typeof(ItemType)))[GetRandomNumber(1, _colorIndex)];
    }
}