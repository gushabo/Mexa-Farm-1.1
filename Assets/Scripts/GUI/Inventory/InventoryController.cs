using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject toolbarPanel;
    [SerializeField] GameObject statusPanel;
    [SerializeField] GameObject storePanel;

    private void Update()
    {
        //gives the chance to open the inventory with the "I"
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(panel.activeInHierarchy == false)
            {
                Open();
            }else{
                Close();
            }
        }
    }

    public void Open()
    {
        panel.SetActive(true);
        statusPanel.SetActive(true);
        toolbarPanel.SetActive(false);
        storePanel.SetActive(false);
    }

    public void Close()
    {
        panel.SetActive(false);
        statusPanel.SetActive(false);
        toolbarPanel.SetActive(true);
        storePanel.SetActive(false);
    }
}
