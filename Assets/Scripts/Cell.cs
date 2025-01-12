using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    #region Publics

    public Item _item;

    #endregion

    
    public Item item // For reaching the cell's item.
    {
        get
        {
            return _item;
        }
        set
        {
            if (_item == value) return;

            var oldItem = _item;
            _item = value;

            if (oldItem != null && Equals(oldItem.Cell, this))
                oldItem.Cell = null;
            
            if (value != null)
                value.Cell = this;
        }
    }
}
