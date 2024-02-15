using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestInv : MonoBehaviour
{
    private void Update()
    {
        int num = -1;
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyUp(i.ToString()))
            {
                num = i - 1;
                break;
            }
        }
        if (num == -1) return;


        string inv = "";
        foreach (var item in PlayerDoorInventory.Instance.GetDoorsInPlayerInventory())
        {
            inv += item.GetDoorColor() + " ";
        }
        Debug.Log("Inventory: " + inv);

        var door = PlayerDoorInventory.Instance[num];
        if (door != null)
        {
            EventManager.Instance.OnSelectedDoor(door);
            return;
        }
    }
}
