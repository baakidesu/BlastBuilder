using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class LevelData
{
   public ItemType[,] GridData { get; protected set; }
   
   
   private static readonly Random _random = new Random(); 
   public static int GetRandomNumber(int min, int max) //Faster than unity's random.
   {
      lock (_random) //sync
      {
         return _random.Next(min, max);
      }
   }

   public LevelData(LevelInfo levelInfo)
   {
      GridData = new ItemType[levelInfo.gridHeight,levelInfo.gridWidth];

      for (int i = levelInfo.gridHeight-1; i>=0; --i)
         for (int j = 0; j < levelInfo.gridWidth; ++j)
         {
            GridData[i, j] = ((ItemType[])Enum.GetValues(typeof(ItemType)))[GetRandomNumber(1,7)];
         }
      
   }

   public static ItemType GetRandomCubeItemType()
   {
      return ((ItemType[]) Enum.GetValues(typeof(ItemType)))[GetRandomNumber(1,7)];
   }
}
