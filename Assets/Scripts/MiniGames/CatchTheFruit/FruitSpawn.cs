using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FruitSpawn : MonoBehaviour
{
    //all the prefabs of the fruit we are going to instantiate
    [SerializeField] GameObject[] fruitPrefabs;
    //time to spawn each fruit
    [SerializeField] float secondSpawn = 1f;
    //position of the spawn
    [SerializeField] float min;
    [SerializeField] float max;

    [SerializeField] private UnityEvent onWin;

    public float maxRange;
    public float minRange;

    //the fruit gameObject
    GameObject go;

    //the amount of fruits that have appear 
    int fruits = 0;

    void Start()
    {
        maxRange = gameObject.transform.position.x + max;
        minRange = gameObject.transform.position.x - min;
        StartCoroutine(fruit());
    }

    IEnumerator fruit()
    {
        while (fruits < 8)
        {
            var wanted = Random.Range(minRange, maxRange);
            Debug.Log("posicion del objeto: " + gameObject.transform.position.x);
            Debug.Log("min: " + minRange);
            Debug.Log("max: " + maxRange);
            Debug.Log("posicion x: " + wanted);
            var position = new Vector3(wanted, transform.position.y);
            go = Instantiate(fruitPrefabs[Random.Range(0, fruitPrefabs.Length)], position, Quaternion.identity);
            fruits++;
            yield return new WaitForSeconds(secondSpawn);
        }
        onWin?.Invoke();

    }



}
