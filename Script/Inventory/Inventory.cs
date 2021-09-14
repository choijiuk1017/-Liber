using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    #region SingleTon
    public static Inventory instance; //�κ��丮 ��ũ��Ʈ�� �ٸ� ������Ʈ�� ��ũ��Ʈ������ �ҷ����� ���� �̱��� ���� �̿�
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

    }
    #endregion  

    public GameObject Panel;

    public delegate void OnSlotCountChage(int val);
    public OnSlotCountChage onSlotCountChage;

    public delegate void OnChangeItem(); //�������� �߰��Ǹ� ���Կ��� �߰�
    public OnChangeItem onChangeItem;

    public List<Item> items = new List<Item>();//ȹ�� �������� ���� List ����

    private int slotCnt;

   

    public int SlotCnt //���� ĭ ���� ����
    {
        get => slotCnt; //ó�� ���� ĭ�� ������ 3�� ����
        set
        {
            slotCnt = value; //���� ĭ�� ������ ������ �� �ֵ��� ��
            onSlotCountChage.Invoke(slotCnt);
        }
    }  

    // Start is called before the first frame update
    void Start()
    {
        SlotCnt = 3; //�÷��̾ PŰ�� �Է����� �� ����ĭ�� ������ 3���� �ߵ��� ��
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Panel.SetActive(true);
        }

    }

   public bool AddItem(Item _item)
    {
        if (items.Count < SlotCnt)//�κ��丮 ������ ������ ������ �������� ������
        {
            items.Add(_item);
            if(onChangeItem != null)
            onChangeItem.Invoke(); //������ �߰� �����ϸ� OnchangeItemȣ��
            return true; //������ �߰� ����
        }
        return false; //������ �߰� ����

    }

    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);
        onChangeItem.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag == "FieldItem") //�÷��̾�� �ʵ� �������� �浹�ϸ�
        {
            FieldItems fieldItems = collision.GetComponent<FieldItems>();
            if (AddItem(fieldItems.GetItem())) //AddItemȣ��
                fieldItems.DestroyItem(); //�������� �߰��Ǹ� true�� ��ȯ�ϴ� ������ ȹ��� �ʵ����� ������ �ı�
        }
    }
}
