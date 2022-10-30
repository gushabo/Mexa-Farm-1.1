using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarController : MonoBehaviour
{
    [SerializeField] int toolbarSize = 5;
    int selectedTool;

    [SerializeField] IconHighlight iconHighlight;

    //friking delegate it break my mind trying to understand it (╯°□°）╯︵ ┻━┻
    public Action<int> onChange;

    public Item GetItem
    {
        get{
            return GameManager.instance.InventoryContainer.slots[selectedTool].item;
        }
    }

    private void Start() {
        onChange += UpdateHighlightIcon;
        UpdateHighlightIcon(selectedTool);
    }

    private void Update() {//this changes the tool you r using with the mouse and there are moving between 5 spaces so that's why we add or substract 1
        float delta = Input.mouseScrollDelta.y;
        if(delta != 0)
        {
            if(delta > 0)
            {
                selectedTool += 1;
                selectedTool = (selectedTool >= toolbarSize ? 0 : selectedTool);
            }
            else{
                selectedTool -= 1;
                selectedTool = (selectedTool < 0 ? toolbarSize -1 : selectedTool);
            }
            onChange?.Invoke(selectedTool);
        }
    }

    internal void Set(int id)
    {
        selectedTool = id;
    }
    
    public void UpdateHighlightIcon(int id = 0)
    {
        Item item = GetItem;
        if(item == null)
        {
            iconHighlight.Show = false;
            return;
        }

        iconHighlight.Show = item.iconHighlight;
        if(item.iconHighlight)
        {
            iconHighlight.Set(item.Icon);
        }
    }

}
