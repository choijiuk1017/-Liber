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

    public delegate void OnSlotCountChage(int val);
    public OnSlotCountChage onSlotCountChage;

    private int slotCnt;

    public int SlotCnt //���� ĭ ���� ����
    {
        get => slotCnt; //ó�� ���� ĭ�� ������ 3�� ����
        set
        {
            slotCnt = value; //���� ĭ�� ������ ������ �� �ֵ��� ��
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
            SlotCnt = 3; //�÷��̾ PŰ�� �Է����� �� ����ĭ�� ������ 3���� �ߵ��� ��
        }
    }
}
