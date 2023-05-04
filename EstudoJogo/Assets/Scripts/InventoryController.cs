using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject statusPanel;
    [SerializeField] GameObject toolbar;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(!panel.activeInHierarchy);
            statusPanel.SetActive(!statusPanel.activeInHierarchy);
            toolbar.SetActive(!toolbar.activeInHierarchy);
        }
    }
}
