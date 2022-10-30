using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    [SerializeField] public float height = 1f;
    [SerializeField] public float width = 1f;

    [SerializeField] GameObject[] spawn;
    [SerializeField] float probability = 0.3f;
    [SerializeField] int spawnCount = 1;
    [SerializeField] public bool oneTime = false;
    public int daysPass = 0;

    List<SpawnedObject> spawnedObjects;
    [SerializeField] JSONStringList targetSaveJSONList;
    [SerializeField] int idInList = -1;

    [SerializeField] int objectSpawnLimit = -1;
    int Lenght;


    private void Start()
    {
        Lenght = spawn.Length;
        if (oneTime == false)
        {
            //we need the time agent to make it appear in seconds
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

    public void SpawnedObjectDestroyed(SpawnedObject spawnedObject)
    {
        spawnedObjects.Remove(spawnedObject);
    }

    private void Update()
    {
        //this is to check if a days has pass or not
        if (DayTimeController.time > 85500f)
        {
            daysPass++;
        }
    }

    public void Spawn()
    {

        //if (daysPass == 3)
        //{
            //this checks if it goings to appear and object or not

            if (Random.value <= probability) { return; }
            if(objectSpawnLimit <= spawnedObjects.Count && objectSpawnLimit != -1){return;} 

            for (int i = 0; i < spawnCount; i++)
            {
                int id = UnityEngine.Random.Range(0, Lenght);
                //create the game object in the scene
                GameObject go = Instantiate(spawn[id]);
                Transform t = go.transform;

                if (oneTime == false)
                {
                    //this gets the component to save it to then loaded and give them an id
                    t.SetParent(transform);
                    //here we attached another component to can spawned and save it
                    SpawnedObject spawnedObject = go.AddComponent<SpawnedObject>();
                    spawnedObjects.Add(spawnedObject);
                    spawnedObject.objId = id;
                }

                Vector3 pos = transform.position;
                //this are the new positions of the objects that spawn randomly
                pos.x += UnityEngine.Random.Range(-width, width);
                pos.y += UnityEngine.Random.Range(-height, height);

                t.position = pos;
                daysPass = 0;
            }
        //}
    }

    public class ToSave
    {
        //create a list to save objects
        public List<SpawnedObject.SaveSpawnedObjectData> spawnedObjectDatas;

        //constructor from the class
        public ToSave()
        {
            //charge the list again
            spawnedObjectDatas = new List<SpawnedObject.SaveSpawnedObjectData>();
        }
    }

    string Read()
    {
        //initialize the list
        ToSave toSave = new ToSave();
        //populate the list
        //moves through all the list to adding all the objects with their id
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            //adds the objects to the list
            toSave.spawnedObjectDatas.Add(
                new SpawnedObject.SaveSpawnedObjectData(
                    spawnedObjects[i].objId,
                    spawnedObjects[i].transform.position
                )
            );
        }

        //returns a string of Json to save the items
        return  JsonUtility.ToJson(toSave);
    }

    public void Load(string json)
    {
        //checks if the string is not empty
        if(json == "" || json =="{}" || json == null) {return;}

        //change the string to a new ToSave class
        ToSave toLoad = JsonUtility.FromJson<ToSave>(json);

        for(int i = 0; i < toLoad.spawnedObjectDatas.Count; i++)
        {
            //gets the spawn object with their data of the load objects
            SpawnedObject.SaveSpawnedObjectData data = toLoad.spawnedObjectDatas[i];
            //creates an object
            GameObject go = Instantiate(spawn[data.objectId]);
            go.transform.position = data.worldPosition;
            //give them the parent transform
            go.transform.SetParent(transform);
            //Add the component to a new object to add it to the list
            SpawnedObject so = go.AddComponent<SpawnedObject>();
            //add the id to the new obj
            so.objId = data.objectId;
            //add it to the list
            spawnedObjects.Add(so);
            
        }

    }

    private void OnDestroy()
    {
        SaveData();
    }

    private void SaveData()
    {
        if(CheckJSON() == false){return;}

        string jsonString = Read();
        targetSaveJSONList.SetString(jsonString, idInList);
    }

    private void LoadData()
    {
        if(CheckJSON() == false){return;}
        Load(targetSaveJSONList.GetString(idInList));
    }

    private bool CheckJSON()
    {
        //checks all the errors that can happen on the JSON list
        if (oneTime == true) { return false; }
        if (targetSaveJSONList == null) { Debug.LogError("The target save JSON List is null"); return false; }
        if (idInList == -1) { Debug.LogError("The ID List is empty"); return false; }

        return true;

    }

    private void OnDrawGizmos()
    {

        //this is only the first area
        //here we only draw a blue square for the editor
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(width * 2, height * 2));
    }

}
