using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Team1
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
                        Debug.Log("Nothing as been hited");
                    
                }
            }
        }

        private void InitDrag()
        {
            isDragActive = true;
        }

        private void Drag()
        {
            lastDragged.transform.position = new Vector2(worldPosition.x, worldPosition.y);
            lastDragged.transform.position = 
        }


        private void Drop() 
        {
            isDragActive = false;
            lastDragged = null;
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAHA");
        }
    }
}
