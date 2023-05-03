using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGroup : MonoBehaviour
{
    public List<GameObject> panels;

    public void Show(int idPanel)
    {
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].SetActive(i == idPanel);
        }
    }
}
