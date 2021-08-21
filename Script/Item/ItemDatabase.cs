using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance; //�ٸ� Ŭ�������� ���� ���

    public void Awake()
    {
        instance = this;
    }

    public List<Item> itemDB = new List<Item>(); //������ ����Ʈ

    public GameObject fieldItemPrefab; //�ʵ� ������ ������ ����
    public Vector3[] pos; //������ ���� ��ġ
    private void Start()
    {
        for(int i = 0; i< 5;i++)
        {
           GameObject go = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity); //������ fieldItem�� Item�� itemDB �� �� ���� �ʱ�ȭ
            go.GetComponent<FieldItems>().SetItem(itemDB[Random.Range(0,3)]);
        }     
    }
}
