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

    bool activeInventory = false; //�κ��丮�� ���� ����


    // Start is called before the first frame update
    void Start()
    {
        inven = Inventory.instance; //�κ��丮 ��ũ��Ʈ�� ���� ����
        slots = slotHolder.GetComponentsInChildren<Slot>(); //������ ���� Ȧ���� �����ص� ������Ʈ�� �ڽ� ������Ʈ���� ����
        //���Ե��� ���������� �����߱� ������ ��� ���Ե��� Slot ��ũ��Ʈ�� ������ ����
        inven.onSlotCountChage += SlotChange;
        inven.onChangeItem += RedrawSlotUI;
      

    }

    private void SlotChange(int val)
    {
       for(int i = 0; i < slots.Length; i++) 
        {
            slots[i].slotnum = i;
            if(i < inven.SlotCnt)
                slots[i].GetComponent<Button>().interactable = true; //�ø� �� �ִ� ���� ĭ ���� ���� �ִٸ� ���� Ȯ�� ��ư�� Ŭ���Ͽ� ���� Ȯ���� �����ϰ� �� 
            else
                slots[i].GetComponent<Button>().interactable = false; //ĭ ���� �� �÷ȴٸ� ĭ �� Ȯ�� ��ư�� Ŭ���ص� ȿ���� ����
        }
    }

    // Update is called once per frame
    void Update()
    {
        
           
    }

    public void AddSlot()
    {
        inven.SlotCnt = inven.SlotCnt+4; //�κ��丮�� ���� ĭ�� ���� �þ���� �ϴ� �Լ�
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
