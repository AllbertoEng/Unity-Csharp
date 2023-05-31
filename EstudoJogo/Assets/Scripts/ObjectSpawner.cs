using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] float spawnArea_height = 1f;
    [SerializeField] float spawnArea_width = 1f;

    [SerializeField] GameObject[] spawn;
    int lenght;
    [SerializeField] float probability = 0.1f;
    [SerializeField] int spawnCount = 1;

    [SerializeField] bool oneTime = false;

    List<SpawnedObject> spawnedObjects;
    [SerializeField] JSONStringList targetSaveJSONList;
    [SerializeField] int idInList = -1;

    [SerializeField] int objectSpawnLimit = -1;

    private void Start()
    {
        lenght = spawn.Length;

        if (!oneTime)
        {            
            TimeAgent timeAgent = GetComponent<TimeAgent>();
            timeAgent.onTimeTick += Spawn;
            spawnedObjects = new List<SpawnedObject>();

            LoadData();
        }
        else
        {
            Spawn();
            Destroy(gameObject);
        }
        
    }

    internal void SpawnedObjectDestroyed(SpawnedObject spawnedObject)
    {
        spawnedObjects.Remove(spawnedObject);
    }

    public void Spawn()
    {
        if (UnityEngine.Random.value > probability)
            return;

        if (objectSpawnLimit <= spawnedObjects.Count && objectSpawnLimit != -1)
            return;

        for (int i = 0; i < spawnCount; i++)
        {
            int id = UnityEngine.Random.Range(0, lenght);
            GameObject go = Instantiate(spawn[id]);
            go.transform.parent = transform.parent.transform;
            Transform t = go.transform;

            if (!oneTime)
            {
                SpawnedObject spawnedObject = go.AddComponent<SpawnedObject>();
                spawnedObject.objId = id;
                spawnedObjects.Add(spawnedObject);
                
            }

            Vector3 position = transform.position;
            position.x += UnityEngine.Random.Range(-spawnArea_width, spawnArea_width);
            position.y += UnityEngine.Random.Range(-spawnArea_height, spawnArea_height);

            t.position = position;
        }        
    }

    [Serializable]
    public class ToSave
    {
        public List<SpawnedObject.SaveSpawnedObjectData> spawnedObjectDatas;

        public ToSave()
        {
            spawnedObjectDatas = new List<SpawnedObject.SaveSpawnedObjectData>();
        }
    }

    public string Read()
    {
        ToSave toSave = new ToSave();

        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            if (spawnedObjects[i] == null)
                continue;

            toSave.spawnedObjectDatas.Add
                (new SpawnedObject.SaveSpawnedObjectData(spawnedObjects[i].objId
                ,spawnedObjects[i].transform.position));
        }

        return JsonUtility.ToJson(toSave);
    }

    public void Load(string jsonString)
    {
        if (string.IsNullOrEmpty(jsonString) || jsonString == "{}")
            return;

        ToSave toLoad = JsonUtility.FromJson<ToSave>(jsonString);

        for (int i = 0; i < toLoad.spawnedObjectDatas.Count; i++)
        {
            SpawnedObject.SaveSpawnedObjectData data = toLoad.spawnedObjectDatas[i];
            GameObject go = Instantiate(spawn[data.objectId]);
            go.transform.position = data.worldPosition;
            go.transform.parent = transform.parent.transform;
            SpawnedObject so = go.AddComponent<SpawnedObject>();
            so.objId = data.objectId;
            spawnedObjects.Add(so);
        }
    }
    private void LoadData()
    {
        if (!CheckJSON())
            return;

        Load(targetSaveJSONList.GetString(idInList));
    }

    private bool CheckJSON()
    {
        if (oneTime || targetSaveJSONList == null || idInList == -1)
            return false;

        return true;
    }

    private void OnDestroy()
    {
        SaveData();
    }

    private void SaveData()
    {
        if (!CheckJSON())
            return;

        string jsonString = Read();
        targetSaveJSONList.SetString(jsonString, idInList);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnArea_width * 2, spawnArea_height * 2));
    }

}
