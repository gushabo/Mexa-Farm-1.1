using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    //size of the spawnable area
    [SerializeField] public float spawnArea_height = 1f;
    [SerializeField] public float spawnArea_width = 1f;

    //the objects that are going to spawn
    [SerializeField] GameObject[] spawn;
    //the amount of objects in the array
    int lenght;
    //the probability to show another object
    [SerializeField] float prob = 0.1f;
    //number of maximun objects that are going to spawn
    [SerializeField] int spawnCount = 1;
    //if the spawner just want to be used only one time
    [SerializeField] bool onTime = false;
    //the list of the object on the scene to have track of them
    List<SpawnedObject> spawnedObjects;
    //the JSON string list where is going to save the items
    [SerializeField] JSONStringList targetSaveJSONList;
    //index to read the lists on the json array
    [SerializeField] int idInList = -1;

    private void Start()
    {
        //get in a variable the size of the array
        lenght = spawn.Length;

        if (onTime == false)
        {
            //get the component of the TimeAgent
            TimeAgent time = GetComponent<TimeAgent>();
            //get the spawn method every time tick on the game
            time.onTimeTick += Spawn;
            //creates a new instance of the list
            spawnedObjects = new List<SpawnedObject>();
            
            LoadData();
        }
        else
        {
            //use the method one time then destroy the gameObject
            Spawn();
            Destroy(gameObject);
        }
    }

    void Spawn()
    {

        //check if the probability is correct to appear another object
        if (Random.value > prob) { return; }
        //spawn the number of object that the variable have
        for (int i = 0; i < spawnCount; i++)
        {
            //getting an ID
            int id = Random.Range(0, lenght);
            //spawn the object
            GameObject go = Instantiate(spawn[id]);
            //get the position
            Transform t = go.transform;

            if (onTime == false)
            {
                //put the instatiated object as a child of the spawner
                t.SetParent(transform);
                //save the created object in a variable
                SpawnedObject spawnedObject = go.AddComponent<SpawnedObject>();
                //add the spawn object to the list of objects
                spawnedObjects.Add(spawnedObject);
                //get the object an ID
                spawnedObject.objId = id;
            }


            //save a new postion
            Vector3 position = transform.position;
            position.x += UnityEngine.Random.Range(-spawnArea_width, spawnArea_width);
            position.y += UnityEngine.Random.Range(-spawnArea_height, spawnArea_height);
            //give the object the new position
            t.position = position;
        }

    }

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

        //reading the data of the objects
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            //using the constructor of "SpawnedObject" to populate the array
            toSave.spawnedObjectDatas.Add(
                new SpawnedObject.SaveSpawnedObjectData(
                    spawnedObjects[i].objId,
                    spawnedObjects[i].transform.position
                )
            );
        }

        return JsonUtility.ToJson(toSave);
    }

    public void Load(string json)
    {
        //check if the json string is empty
        if(json == "" || json == "{}" || json == null){ return; }
        //creates a new class and reads the information of the json
        ToSave toLoad = JsonUtility.FromJson<ToSave>(json);
        //go through all the list and instantiate all the objects on the scene
        for(int i = 0; i < toLoad.spawnedObjectDatas.Count; i++)
        {
            //saving the object on the list on a variable
            SpawnedObject.SaveSpawnedObjectData data = toLoad.spawnedObjectDatas[i];
            //put the object in the scene
            GameObject go = Instantiate(spawn[data.objectId]);
            //change his position
            go.transform.position = data.worldPosition;
            //set the spawner as a parent to the object
            go.transform.SetParent(transform);
            //Create a new object and adding the component of the original and adding the new component
            SpawnedObject so = go.AddComponent<SpawnedObject>();
            //give him the same id
            so.objId = data.objectId;
            //adding the spawn object in the list
            spawnedObjects.Add(so);
            
        }

    }

    internal void SpawnedObjectDestroyed(SpawnedObject spawnedObject)
    {
        spawnedObjects.Remove(spawnedObject);
    }

    private void OnDestroy()
    {
        SaveData();
    }

    private void SaveData()
    {
        if(Check() == false){return;}
        //get the array of the objects in the string
        string jsonString = Read();
        //put the string in JSON form list
        targetSaveJSONList.SetString(jsonString, idInList);
    }

    private void LoadData()
    {
        if(Check() == false){return;}
        //to load the information of the object we have to establish the JSONList
        Load(targetSaveJSONList.GetString(idInList));
    }

    private bool Check()
    {
        //making sure of all the posibles errors
        if (onTime == true) { return false; }
        if (targetSaveJSONList == null) { return false; }
        if (idInList == -1) { return false; }
        return true;
    }

    private void OnDrawGizmos()
    {
        //draw a blue square that represent the spawner
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnArea_width * 2, spawnArea_height * 2));
    }

}
