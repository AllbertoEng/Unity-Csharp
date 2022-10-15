using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : MonoBehaviour
{
    [SerializeField] GameObject highlighter;
    GameObject currentTarget;

    public void Highlight(GameObject target)
    {
        if (currentTarget == target)
        {
            return;
        }
        currentTarget = target;

        Vector3 position = target.transform.position + Vector3.up * 0.5f;
        Highlight(position);
    }

    private void Highlight(Vector3 position)
    {
        highlighter.transform.position = position;
        highlighter.SetActive(true);        
    }

    public void Hide()
    {
        currentTarget = null;
        highlighter.SetActive(false);
    }
}
