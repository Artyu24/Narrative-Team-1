using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TeamOne;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace TeamOne
{    public class DragController : MonoBehaviour
    {
        [SerializeField] private bool isDragActive = false;
        private Vector2 screenPosition;
        private Vector3 worldPosition;
        [SerializeField] private Draggable lastDragged;

        private void Awake()
        {
            DragController[] controller = FindObjectsOfType<DragController>();
            if (controller.Length > 1)
                Destroy(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            if (isDragActive && (Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended))) //Drop 
            {
                Drop();
                return;
            }

            //input + Pos
            if(Input.GetMouseButtonDown(0))
            {
                Vector3 _mousePos = Input.mousePosition;
                screenPosition = new Vector2(_mousePos.x, _mousePos.y);
            }
            else if(Input.touchCount > 0)
            {
                screenPosition = Input.GetTouch(0).position;
            }

            worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);


            if (isDragActive)
                Drag();

            else
            {
                RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
                if(hit.collider != null && (Input.touchCount > 0) || Input.GetMouseButtonDown(0))
                {
                    if (hit.collider != null)
                    {
                        Draggable _draggable = hit.transform.GetComponent<Draggable>();
                        if (_draggable != null)
                        {
                            lastDragged = _draggable;
                            InitDrag();
                        }
                    }
                    else
                        return;
                    
                }
            }
        }

        private void InitDrag()
        {
            isDragActive = true;
        }

        private void Drag()
        {
            lastDragged.transform.DOMove(new Vector2(worldPosition.x, worldPosition.y), .25f);
            lastDragged.ValidateZone();
        }

        private void Drop() 
        {
            isDragActive = false;

            if (lastDragged.hasToReturn)
                lastDragged.transform.DOMove(lastDragged.InitPos, 1); lastDragged.transform.rotation = lastDragged.InitRot;

            if (lastDragged.ValidateRight)
                GameManager.instance.SwipeChoice(true);
            else if (lastDragged.ValidateLeft)
                GameManager.instance.SwipeChoice(false);

            lastDragged = null;
        }
    }
}
