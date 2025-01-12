using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    #region Publics

    public Cell cell;

    #endregion
    
    #region Privates

    private float spriteSize = 0.7f;

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
}
