using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CrowActions : MonoBehaviour
{
    public float prob = 0.02f;
    public int days = 10;
    float rand;

    public bool validation;

    GameObject crow;
    //the container from the crops
    CropsContainer crops;
    List<detector> lista;

    public TilemapCropsManager manager;

    private void Update() {

        crops = GameObject.Find("CropsTilemap").GetComponent<TilemapCropsManager>().container;
        lista = GameObject.Find("CropsTilemap").GetComponent<TilemapCropsManager>().lista;
        manager = GameObject.Find("CropsTilemap").GetComponent<TilemapCropsManager>();
        if(crops == null){Debug.Log("primer 1 if"); return;}
        if(lista == null){Debug.Log("primer 2 if"); return;}
        if(manager == null){Debug.Log("primer 3 if"); return;}

        //get the crow and check it
        crow = GameObject.Find("Crow");
        if(crow == null){Debug.Log("segundo if"); return;}

        //have been 10 days since the player start the game
        if(DayTimeController.days > days)
        {
            Debug.Log("entro a los dias");
            if(validation)
            {
                crow.SetActive(false);
                Debug.Log("entro a la validacion");
                for(int i = 0; i < crops.crops.Count; i ++)
                {
                    if(crops.crops[i].crowProtect == false)
                    {
                        Debug.Log("elimino un cultivo, indice: " + i);
                        manager.ReturnToDirt(i);
                        crops.crops.RemoveAt(i);
                        lista.RemoveAt(i);
                    }
                }
            }

            validation = false;
            days = DayTimeController.days;
            //if the prob passes the chances to get the crows
            rand = UnityEngine.Random.value;
            if(rand <= prob)
            {
                Debug.Log("se activo la validacion");
                //the crow get active in the morning
                crow.SetActive(true);
                validation = true;
            }
        }

    }

}

