using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolsCharacterController : MonoBehaviour
{
    CharacterControler2D character;
    ToolBarController toolBarController;
    Rigidbody2D rgbd2d;
    Animator animator;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float delayUse = 0.1f;
    private float delay = 0f;
    //[SerializeField] float sizeOfInteractableArea = 1.2f;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TileMapReadController tileMapReadController;
    [SerializeField] float maxDistance = 4f;
    [SerializeField] ToolAction onTilePickUp;
    [SerializeField] IconHighlight iconHighlight;

    Vector3Int selectedTile;
    bool selectable;

    private void Awake()
    {
        character = GetComponent<CharacterControler2D>();
        rgbd2d = GetComponent<Rigidbody2D>();
        toolBarController = GetComponent<ToolBarController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SelectTile();
        CanSelectCheck();
        Marker();

        if (delay > 0)
        {
            delay -= Time.deltaTime;
            return;
        }

        if (Input.GetMouseButton(0))
        {            
            if (UseToolWorld())                           
                return;            
            UseToolGrid();
        }
        delay = delayUse;
    }
    private void SelectTile()
    {
        selectedTile = tileMapReadController.GetGridPosition(Input.mousePosition, true);
    }

    private void Marker()
    {
        markerManager.markedCellPosition = selectedTile;
        iconHighlight.cellPosition = selectedTile;
    }

    void CanSelectCheck()
    {
        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;
        markerManager.Show(selectable);
        iconHighlight.CanSelect = selectable;
    }

    private bool UseToolWorld()
    {
        Vector2 position = rgbd2d.position + character.lastMotionVector * offsetDistance;

        Item item = toolBarController.GetItem;
        if (item == null)
            return false;
        if(item.onAction == null)
            return false;

        animator.SetTrigger("act");
        bool complete = item.onAction.OnApply(position);

        return complete;
    }

    private void UseToolGrid()
    {
        if (selectable)
        {
            Item item = toolBarController.GetItem;
            if (item == null)
            {
                PickUpTile();
                return;
            }

            if (item.onTileMapAction == null)
                return;

            animator.SetTrigger("act");
            bool complete = item.onTileMapAction.OnApplyToTileMap(selectedTile, tileMapReadController, item);

            if (complete)
            {
                if(item.onItemUsed != null)
                    item.onItemUsed.OnItemUsed(item, GameManager.instance.inventoryContainer);
            }
        }
    }

    private void PickUpTile()
    {
        if (onTilePickUp == null)
            return;

        onTilePickUp.OnApplyToTileMap(selectedTile, tileMapReadController, null);
    }
}
