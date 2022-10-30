using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    //Gets the text and the image
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image highlight;

    int myIndex;

    ItemPanel itemPanel;

    public void SetIndex(int index)
    {
        myIndex = index;
    }

    //get out item panel 
    public void SetItemPanel(ItemPanel source)
    {
        itemPanel = source;
    }

    public void Set(ItemSlot slot)
    {
        //checks if we can stack slots or not and shows or not the text
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.Icon;
        
        if(slot.item.Stackable == true)
        {
            text.gameObject.SetActive(true);
            text.text = slot.count.ToString();
        }else {
            text.gameObject.SetActive(false);
        }
    }

    //Cleans the text and the icon in the inventory
    public void Clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //get the index in the panel that we clicked
        itemPanel.OnClick(myIndex);
    }

    public void Highlight(bool flag)
    {
        highlight.gameObject.SetActive(flag);
    }
}
