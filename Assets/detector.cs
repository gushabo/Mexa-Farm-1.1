using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detector : MonoBehaviour
{
    public int indice;
    public CropsContainer container;

    private void OnTriggerStay2D(Collider2D other) {
        container.crops[indice].crowProtect = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        container.crops[indice].crowProtect = false;
    }

}
