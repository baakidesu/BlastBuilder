using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelData
{
   public ItemType[,] GridData { get; protected set; }

   public LevelData(LevelInfo levelInfo)
   {
      GridData = new ItemType[levelInfo.gridHeight,levelInfo.gridWidth];

      for (int i = levelInfo.gridHeight-1; i>=0; --i)
         for (int j = 0; j < levelInfo.gridWidth; ++j)
         {
            GridData[i, j] = ((ItemType[])Enum.GetValues(typeof(ItemType)))[Random.Range(1, 7)];
         }
      
   }

   public static ItemType GetRandomCubeItemType()
   {
      return ((ItemType[]) Enum.GetValues(typeof(ItemType)))[Random.Range(1, 6)];
   }
}
