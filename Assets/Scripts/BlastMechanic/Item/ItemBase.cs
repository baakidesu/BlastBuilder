using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemBase : MonoBehaviour
{
    public ItemType itemType;
    public bool canClickable = true;
    public bool canFall = true;
    public bool canExplode;
    public int health = 1;
    public FallAnimation fallAnimation;
}
