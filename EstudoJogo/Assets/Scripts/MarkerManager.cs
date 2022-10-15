using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MarkerManager : MonoBehaviour
{
    [SerializeField] Tilemap targetTilemap;
    [SerializeField] TileBase tile;
    public Vector3Int markedCellPosition;
    Vector3Int oldCellPosition;
    private bool show;

    private void Update()
    {
        if (show == false)
            return;
        targetTilemap.SetTile(oldCellPosition, null);
        targetTilemap.SetTile(markedCellPosition, tile);
        oldCellPosition = markedCellPosition;
    }

    internal void Show(bool selectable)
    {
        show = selectable;
        targetTilemap.gameObject.SetActive(show);
    }
}
