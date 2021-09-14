using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject Panel;
    public Slot[] slots;
    public Transform slotHolder;
    Inventory inven;

    //bool activeInventory = false; //인벤토리의 실행 여부


    // Start is called before the first frame update
    void Start()
    {
        inven = Inventory.instance; //인벤토리 스크립트의 변수 받음
        slots = slotHolder.GetComponentsInChildren<Slot>(); //슬롯은 슬롯 홀더로 지정해둔 오브젝트의 자식 컴포넌트들을 받음
        //슬롯들을 프리펩으로 지정했기 때문에 모든 슬롯들이 Slot 스크립트를 가지고 있음
        inven.onSlotCountChage += SlotChange;
        inven.onChangeItem += RedrawSlotUI;
      

    }

    private void SlotChange(int val)
    {
       for(int i = 0; i < slots.Length; i++) 
        {
            slots[i].slotnum = i;
            if(i < inven.SlotCnt)
                slots[i].GetComponent<Button>().interactable = true; //늘릴 수 있는 슬롯 칸 수가 남아 있다면 슬롯 확장 버튼을 클릭하여 슬롯 확장이 가능하게 함 
            else
                slots[i].GetComponent<Button>().interactable = false; //칸 수를 다 늘렸다면 칸 수 확장 버튼을 클릭해도 효과가 없음
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void AddSlot()
    {
        inven.SlotCnt = inven.SlotCnt+3; //인벤토리의 슬롯 칸의 수가 늘어나도록 하는 함수
    }

    void RedrawSlotUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for (int i = 0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }
}
