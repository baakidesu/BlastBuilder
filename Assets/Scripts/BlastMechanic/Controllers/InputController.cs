using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class InputController : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private GameGrid board;
    private MapController _mapController;

    [Inject]
    void Construct(MapController mapController)
    {
        _mapController = mapController;
    } 
        private void Update()
        {
        #if UNITY_EDITOR
            GetTouchEditor();
        #else
            GetTouchMobile();
        #endif
        } 
        private void GetTouchEditor()
        {
            if (Input.GetMouseButtonUp(0))
            {
                ExecuteTouch(Input.mousePosition);
            }
        } 
        private void GetTouchMobile()
        {
            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    ExecuteTouch(touch.position);
                    break;
            }
        } 
        private void ExecuteTouch(Vector3 pos)
        {
            var hit = Physics2D.OverlapPoint(camera.ScreenToWorldPoint(pos)) as BoxCollider2D;
            if (hit != null && hit.CompareTag("Cell"))
            {
                hit.GetComponent<Cell>().CellTapped();
            }
        } 
        private void DisableTouch()
        {
            this.enabled = false;
        }
        
    }