using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pusoPared : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("frutas")){
            Destroy(other.gameObject);
        }
    }

}
