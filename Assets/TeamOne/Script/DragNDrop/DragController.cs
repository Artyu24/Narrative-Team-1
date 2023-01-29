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

        [SerializeField] private GameObject dialogue;
        [SerializeField] private GameObject blackScreen;

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
            lastDragged.GetComponent<Sword>().idle = false;
            lastDragged.transform.position = new Vector3(0, -4, 0);
            blackScreen.GetComponent<SpriteRenderer>().DOColor(new Vector4(0, 0, 0, .7f), 1);
            if (dialogue != null)
                dialogue.GetComponent<TextMeshProUGUI>().DOColor(new Vector4(255, 255, 255, .4f), 1);
            else
                Debug.LogError("Renseigner le text de dialogue dans DragController");

        }

        private void Drag()
        {
            lastDragged.transform.DOMove(new Vector2(worldPosition.x, worldPosition.y), .25f);
            lastDragged.ValidateZone();
        }

        private void Drop() 
        {
            blackScreen.GetComponent<SpriteRenderer>().DOColor(new Vector4(0, 0, 0, 0), 1);
            if (dialogue != null)
                dialogue.GetComponent<TextMeshProUGUI>().DOColor(new Vector4(255, 255, 255, 255), 1);
            else
                Debug.LogError("Renseigner le text de dialogue dans DragController");


            lastDragged.GetComponent<Sword>().idle = false;
            isDragActive = false;

            if (lastDragged.hasToReturn)
                lastDragged.transform.DOMove(lastDragged.InitPos, 1); lastDragged.transform.rotation = lastDragged.InitRot;

            if (lastDragged.ValidateRight)
            {
                GameManager.instance.SwipeChoice(true);
                lastDragged.GetComponent<Sword>().badParticle.SetActive(false);
                lastDragged.gameObject.SetActive(false);
            }
            else if (lastDragged.ValidateLeft)
            {
                GameManager.instance.SwipeChoice(false);
                lastDragged.GetComponent<Sword>().goodParticle.SetActive(false);
                lastDragged.gameObject.SetActive(false);
            }

            lastDragged = null;
        }
    }
}
