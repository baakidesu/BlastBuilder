using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class Item : MonoBehaviour
{
    #region Publics

    public Cell cell;
    
    public ItemType itemType;
    public bool canClickable = true;
    public bool canFall = true;
    public bool canExplode = false;
    public int health = 1;
    
    public SpriteRenderer spriteRenderer;


    #endregion
    
    #region Privates

    private float _spriteSize = 0.7f;
    
    private const int BaseSortingOrder = 10;
    private static int _childSpriteOrder;

    private FallAnimation fallAnimation;

    #endregion

    #region Injections

    [Inject]
    void Construct(FallAnimation _fallAnimation)
    {
        fallAnimation = _fallAnimation;
    }

    #endregion
    public Cell Cell // For reaching the item's cell.
    {
        get { return cell; }
        set
        {
            if (cell == value) return;

            var oldCell = cell;
            cell = value;

            if (oldCell != null && oldCell.item == this)
                oldCell.item = null;

            if (value != null)
            {
                value.item = this;
                gameObject.name = cell.gameObject.name + " " + GetType().Name;
            }
        }
    }
    public void Prepare(ItemBase itemBase, Sprite sprite)
    {
        spriteRenderer = CreateSprite(sprite);

        itemType = itemBase.itemType;
        canClickable = itemBase.canClickable;
        canExplode = itemBase.canExplode;
        canFall = itemBase.canFall;
        //fallAnimation = itemBase.fallAnimation; Delete this.
        health = itemBase.health;
        
        fallAnimation.item = this;
    }
    private SpriteRenderer CreateSprite(Sprite sprite)
    {
        var _spriteRenderer = new GameObject("Sprite: " + _childSpriteOrder).AddComponent<SpriteRenderer>();
        
        _spriteRenderer.transform.SetParent(transform);
        _spriteRenderer.transform.localPosition = Vector3.zero;
        _spriteRenderer.transform.localScale = new Vector2(_spriteSize, _spriteSize);
        _spriteRenderer.sprite = sprite;
        _spriteRenderer.sortingOrder = SortingLayer.NameToID("Cell");
        _spriteRenderer.sortingOrder = BaseSortingOrder + _childSpriteOrder++;
        
        return _spriteRenderer;
    }
    public virtual MatchType GetMatchType()
    {
        return MatchType.None;
    }

    public virtual void Execute()
    {
        //GoalManager.Instance.UpdateLevelGoal(ItemType);
        RemoveItem();
    }
    public void RemoveItem()
    {
        Cell.item = null;
        Cell = null;
        Destroy(gameObject);
    }
    public void UpdateSprite(Sprite sprite)
    {
        var _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _spriteRenderer.sprite = sprite;
    }
    public virtual void HintUpdateToSprite(ItemType itemType, int matchCount)
    {
        return;
    }
    public void Fall()
    {
        if(canFall) return;
        
        //FallAnimation.Fall
    }
}
