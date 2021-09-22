using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    #region SingleTon
    public static Inventory instance; //인벤토리 스크립트를 다른 오브젝트의 스크립트에서도 불러오기 위해 싱글톤 패턴 이용
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

    public delegate void OnChangeItem(); //아이템이 추가되면 슬롯에도 추가
    public OnChangeItem onChangeItem;

    public List<Item> items = new List<Item>();//획득 아이템을 닫을 List 생성

    private int slotCnt;

   

    public int SlotCnt //슬롯 칸 개수 설정
    {
        get => slotCnt; //처음 슬롯 칸의 개수인 3을 받음
        set
        {
            slotCnt = value; //슬롯 칸의 개수를 변경할 수 있도록 함
           // onSlotCountChage.Invoke(slotCnt);
        }
    }  

    // Start is called before the first frame update
    void Start()
    {
        SlotCnt = 3; //플레이어가 P키를 입력했을 때 슬롯칸의 개수가 3개로 뜨도록 함
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
        if (items.Count < SlotCnt)//인벤토리 슬롯의 개수가 아이템 개수보다 많을때
        {
            items.Add(_item);
            if(onChangeItem != null)
            onChangeItem.Invoke(); //아이템 추가 성공하면 OnchangeItem호출
            return true; //아이템 추가 성공
        }
        return false; //아이템 추가 실패

    }

    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);
        onChangeItem.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag == "FieldItem") //플레이어와 필드 아이템이 충돌하면
        {
            FieldItems fieldItems = collision.GetComponent<FieldItems>();
            if (AddItem(fieldItems.GetItem())) //AddItem호출
                fieldItems.DestroyItem(); //아이템이 추가되면 true를 반환하니 아이템 획득시 필드위의 아이템 파괴
        }
    }
}
