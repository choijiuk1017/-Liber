using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance; //다른 클래스에서 접근 허용

    public void Awake()
    {
        instance = this;
    }

    public List<Item> itemDB = new List<Item>(); //아이템 리스트

    public GameObject fieldItemPrefab; //필드 아이템 프리펩 복제
    public Vector3[] pos; //아이템 생성 위치
    private void Start()
    {
        for(int i = 0; i< 5;i++)
        {
           GameObject go = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity); //생성된 fieldItem의 Item을 itemDB 중 한 개로 초기화
            go.GetComponent<FieldItems>().SetItem(itemDB[Random.Range(0,3)]);
        }     
    }
}
