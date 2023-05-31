using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnScreenMessage
{
    public GameObject go;
    public float timeToLeave;

    public OnScreenMessage(GameObject go)
    {
        this.go = go;
    }
}

public class OnScreenMessageSystem : MonoBehaviour
{
    [SerializeField] GameObject textPrefab;

    List<OnScreenMessage> onScreenMessagesList;
    List<OnScreenMessage> openList;
    [SerializeField] float timeToLeaveDefault = 1f;

    [SerializeField] float horizontalScatter = 0.25f;
    [SerializeField] float verticalScatter = 0.5f;

    private void Awake()
    {
        onScreenMessagesList = new List<OnScreenMessage>();
        openList = new List<OnScreenMessage>();
    }

    private void Update()
    {
        for (int i = onScreenMessagesList.Count - 1; i >= 0; i--)
        {
            onScreenMessagesList[i].timeToLeave -= Time.deltaTime;
            if (onScreenMessagesList[i].timeToLeave < 0)
            {
                onScreenMessagesList[i].go.SetActive(false);

                openList.Add(onScreenMessagesList[i]);

                onScreenMessagesList.RemoveAt(i);
            }
        }
    }

    public void PostMessage(Vector3 worldPosition, string message)
    {
        worldPosition.x += Random.Range(-horizontalScatter, horizontalScatter);
        worldPosition.y += Random.Range(-verticalScatter, verticalScatter);

        if (openList.Count > 0)
        {
            ReuseObjectFromOpenList(worldPosition, message);
        }
        else
        {
            CreateNewOnScreenMessageObject(worldPosition, message);
        }
    }

    private void ReuseObjectFromOpenList(Vector3 worldPosition, string message)
    {
        OnScreenMessage osm = openList[0];
        osm.go.SetActive(true);
        osm.timeToLeave = timeToLeaveDefault;
        osm.go.GetComponent<TextMeshPro>().text = message;
        osm.go.transform.position = worldPosition;
        openList.RemoveAt(0);
        onScreenMessagesList.Add(osm);
    }

    private void CreateNewOnScreenMessageObject(Vector3 worldPosition, string message)
    {
        GameObject textGo = Instantiate(textPrefab, transform);
        textGo.transform.position = worldPosition;

        TextMeshPro tmp = textGo.GetComponent<TextMeshPro>();
        tmp.text = message;

        OnScreenMessage onScreenMessage = new OnScreenMessage(textGo);
        onScreenMessage.timeToLeave = timeToLeaveDefault;
        onScreenMessagesList.Add(onScreenMessage);
    }
}
