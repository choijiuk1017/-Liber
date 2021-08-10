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

    public delegate void OnSlotCountChage(int val);
    public OnSlotCountChage onSlotCountChage;

    private int slotCnt;

    public int SlotCnt //슬롯 칸 개수 설정
    {
        get => slotCnt; //처음 슬롯 칸의 개수인 3을 받음
        set
        {
            slotCnt = value; //슬롯 칸의 개수를 변경할 수 있도록 함
            onSlotCountChage.Invoke(SlotCnt);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SlotCnt = 3; //플레이어가 P키를 입력했을 때 슬롯칸의 개수가 3개로 뜨도록 함
        }
    }
}
