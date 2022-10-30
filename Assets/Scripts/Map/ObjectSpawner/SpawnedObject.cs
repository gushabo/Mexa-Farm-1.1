using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedObject : MonoBehaviour
{
    [Serializable]
    public class SaveSpawnedObjectData
    {
        //give the objects ID and position
        public int objectId;
        public Vector3 worldPosition;

        //the constructor of the objects
        public SaveSpawnedObjectData(int id, Vector3 worldPosition){
            this.objectId = id;
            this.worldPosition = worldPosition;
        }
    }

    public int objId;

    public void SpawnedObjectDestroyed()
    {
        //this gets the objects inside the gameObject of the spawner
        transform.parent.GetComponent<ObjectSpawner>().SpawnedObjectDestroyed(this);
    }

}
