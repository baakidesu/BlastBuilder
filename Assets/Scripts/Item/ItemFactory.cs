using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : Singleton<ItemFactory>
{
    public ItemBase itemBasePrefab;
    
    private Dictionary<ItemType, Func<ItemBase, Item>> itemCreators = new Dictionary<ItemType, Func<ItemBase, Item>>
    {
        { ItemType.GreenCube, (itemBase) => CreateNormalItem(itemBase, MatchType.Green) },
        { ItemType.BlueCube, (itemBase) => CreateNormalItem(itemBase, MatchType.Blue) },
        { ItemType.RedCube, (itemBase) => CreateNormalItem(itemBase, MatchType.Red) },
        { ItemType.YellowCube, (itemBase) => CreateNormalItem(itemBase, MatchType.Yellow) },
        { ItemType.PurpleCube, (itemBase) => CreateNormalItem(itemBase, MatchType.Purple)},
        { ItemType.PinkCube, (itemBase) => CreateNormalItem(itemBase, MatchType.Pink)},
    };

    public Item CreateItem(ItemType itemType, Transform parent)
    {
        if (itemType == ItemType.None) return null;
        
        var itemBase = Instantiate(itemBasePrefab, Vector3.zero, Quaternion.identity, parent);
        itemBase.itemType = itemType;

        if (!itemCreators.TryGetValue(itemType, out var createItem))
        {
            Debug.LogError($"Item creator for {itemType} not found");
            return null;
        }
        
        return createItem(itemBase);
    }

    private static Item CreateNormalItem(ItemBase itemBase, MatchType matchType)
    {
        var normalItem = itemBase.gameObject.AddComponent<NormalCubeItem>();
        normalItem.PrepareNormalCubeItem(itemBase, matchType);
        return normalItem;
    }
}
