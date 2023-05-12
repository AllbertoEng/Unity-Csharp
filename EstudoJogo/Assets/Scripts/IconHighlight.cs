using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IconHighlight : MonoBehaviour
{
    public Vector3Int cellPosition;
    Vector3 targetPosistion;
    [SerializeField] Tilemap targetTilemap;
    SpriteRenderer spriteRenderer;

    bool canSelect;
    bool show;

    public bool CanSelect
    { 
        set { 
            canSelect = value;
            gameObject.SetActive(canSelect && show);
        } 
    }

    public bool Show
    {
        set
        {
            show = value;
            gameObject.SetActive(canSelect && show);
        }
    }


    private void Update()
    {
        targetPosistion = targetTilemap.CellToWorld(cellPosition);
        transform.position = targetPosistion + targetTilemap.cellSize/2 ;
    }

    internal void Set(Sprite icon)
    {
        if (spriteRenderer == null)        
            spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = icon;
        
    }
}
