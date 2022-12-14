using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerRegado : MonoBehaviour
{
    [SerializeField] TilemapCropsManager manager;

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.transform.CompareTag("Player"))
        {
            for (int i = 0; i < manager.lista.Count; i++)
            {
                if (manager.container.crops[i].crop != null)
                {
                    manager.lista[i].text.text = manager.container.crops[i].CurrWater.ToString();
                    manager.lista[i].UI.SetActive(true);
                }
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            for (int i = 0; i < manager.lista.Count; i++)
            {
                manager.lista[i].UI.SetActive(false);
            }
        }

    }

}
