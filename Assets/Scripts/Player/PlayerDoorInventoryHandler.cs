using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoorInventoryHandler : MonoBehaviour
{
    void Update()
    {
        int i = getPressedIndex();
        if (i >= 0)
        {
            DoorsController door = PlayerDoorInventory.Instance[i];
            if (door != null)
            {
                EventManager.Instance.OnSelectDoor(door);
            }
        }
    }

    private int getPressedIndex()
    {
        int index = -1;
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyUp(i.ToString()))
            {
                index = i - 1;
                break;
            }
        }
        return index;
    }
}
