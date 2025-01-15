using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FallAnimation : MonoBehaviour
{
    public Item item;

    [HideInInspector] public Cell target;
    [SerializeField] private const float animationDuration = 0.5f;
    private Vector3 _targetPos;

    public void Awake()
    {
        DOTween.SetTweensCapacity(500, 50);
    }

    public void Fall(Cell targetCell)
    {
        if (!IsValidTarget(targetCell)) return;

        UpdateTargetCell(targetCell);
        StartFall();
    }

    private void StartFall()
    {
        item.transform.DOMoveY(_targetPos.y, animationDuration).SetEase(Ease.InExpo).OnComplete(() => target = null);
    }

    private void UpdateTargetCell(Cell targetCell)
    {
        target = targetCell;
        item.cell = target;
        _targetPos = target.transform.position;
    }

    private bool IsValidTarget(Cell targetCell)
    {
        if (target != null && targetCell.y >= target.y) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
