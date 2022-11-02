using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGamePart1 : MonoBehaviour
{
    [Range(0f, 1f)][SerializeField] float prob = 0.5f;
    public bool b = false;
    public int day = 0;
    float random;

    bool childAppear = false;

    public GameObject go;
    public GameObject child;

    // Start is called before the first frame update
    void Start()
    {
        go = transform.GetChild(0).gameObject;
        go.SetActive(false);
        child = transform.GetChild(1).gameObject;
        child.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (day < DayTimeController.days)
        {
            child.SetActive(false);
            random = UnityEngine.Random.value;
            if (random <= prob)
            {
                go.SetActive(true);
                b = true;
                if(childAppear == false)
                {
                    child.SetActive(true);
                }
                childAppear = true;

            }
            else
            {
                go.SetActive(false);
            }
            day = DayTimeController.days;
        }

    }

}
