using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawn : MonoBehaviour
{
    //all the prefabs of the fruit we are going to instantiate
    [SerializeField] GameObject[] fruitPrefabs;
    //time to spawn each fruit
    [SerializeField] float secondSpawn = 1f;
    //position of the spawn
    [SerializeField] float min;
    [SerializeField] float max;

    //the fruit gameObject
    GameObject go;
    
    //llamacion al otro script
    [SerializeField] pusoPared pared;

    void Start()
    {
        StartCoroutine(fruit());    
    }

    IEnumerator fruit()
    {
        while(pared.lives > 0){
            var wanted = Random.Range(min, max);
            var position = new Vector3(wanted,transform.position.y);
            go = Instantiate(fruitPrefabs[Random.Range(0, fruitPrefabs.Length)],position,Quaternion.identity);
            yield return new WaitForSeconds(secondSpawn);
        }
    }

}
