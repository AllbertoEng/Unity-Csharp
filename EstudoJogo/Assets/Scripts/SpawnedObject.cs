using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedObject : MonoBehaviour
{
    [Serializable]
    public class SaveSpawnedObjectData
    {
        public int objectId;
        public Vector3 worldPosition;

        public SaveSpawnedObjectData(int id, Vector3 vector3)
        {
            this.objectId = id;
            this.worldPosition = vector3;
        }
    }

    public int objId;

    public void SpawnedObjectDestroyed()
    {
        transform.parent.GetComponent<ObjectSpawner>().SpawnedObjectDestroyed(this);
    }
}
