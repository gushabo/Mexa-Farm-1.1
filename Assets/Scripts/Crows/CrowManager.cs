using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CrowManager : MonoBehaviour
{
    int days = DayTimeController.days;
    int i = 0;
    [SerializeField] GameObject crow;
    List<detector> lista;
    CropsContainer container;
    [SerializeField] TilemapCropsManager cropsManager;
    bool elimCrops;

    private void OnEnable()
    {
        lista = GameObject.Find("CropsTilemap").GetComponent<TilemapCropsManager>().lista;
        container = GameObject.Find("CropsTilemap").GetComponent<TilemapCropsManager>().container;
    }

    private void Update()
    {
        //get the validation from the manager to activate or desactivate the crow
        if (GameManager.instance.crowActions.validation)
        {
            //set the crow
            crow.SetActive(true);
            //put on true the var to eliminate the crops
            elimCrops = true;
            if (i == 0)
            {
                days = DayTimeController.days;
                i++;
            }
        }
        else
        {
            crow.SetActive(false);
            elimCrops = false;
            cropsManager.crowsCheck = false;
        }

        //checks if has been pass the elimCrops verification
        if ((DayTimeController.days > days) && (elimCrops == true))
        {
            //set the validation of the elimination on false so we do the process just one time
            elimCrops = false;
            i--;
            //set a variable to destroy all the crops
            cropsManager.crowsCheck = true;
            days = DayTimeController.days;
        }

    }

}

