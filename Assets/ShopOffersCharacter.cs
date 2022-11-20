using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOffersCharacter : MonoBehaviour
{
    
    [SerializeField] GameObject offersPanel;
    [SerializeField] GameObject inventoryPanel;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void begin(GameObject gameObject)
    {
        offersPanel.SetActive(true);
        inventoryPanel.SetActive(true);
    }

    public void Close()
    {
        offersPanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }

}
