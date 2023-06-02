using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject statusPanel;
    [SerializeField] GameObject toolbar;
    [SerializeField] GameObject additionalPanel;
    [SerializeField] GameObject storePanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!panel.activeInHierarchy)
                Open();
            else
                Close();
        }
    }

    public void Open()
    {
        panel.SetActive(true);
        statusPanel.SetActive(true);
        toolbar.SetActive(false);
        additionalPanel.SetActive(true);
        storePanel.SetActive(false);
    }

    public void Close()
    {
        panel.SetActive(false);
        statusPanel.SetActive(false);
        toolbar.SetActive(true);
        additionalPanel.SetActive(false);
        storePanel.SetActive(false);
    }
}
