using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingTextMesh : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer.sortingOrder = 3000;
    }

}
