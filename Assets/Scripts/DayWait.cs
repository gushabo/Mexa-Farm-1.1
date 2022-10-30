using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayWait : MonoBehaviour
{

    private void Update() {

        if(DayTimeController.days > 0){ Destroy(gameObject);}
    }
}
