using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detector : MonoBehaviour
{
    public int indice;
    public CropsContainer container;
    public Vector3Int position;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("espantapajaros"))
        {
            container.crops[indice].crowProtect = true;
        }
        if (other.CompareTag("Fence"))
        {
            container.crops[indice].rainProtect = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("espantapajaros"))
        {
            container.crops[indice].crowProtect = false;
        }
        if (other.CompareTag("Fence"))
        {
            container.crops[indice].rainProtect = false;
        }
    }

}
