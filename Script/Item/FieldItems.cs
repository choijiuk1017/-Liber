using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Item item; //어떤 아이템인지
    public SpriteRenderer image; //아이템 이미지를 바꾸기 위해

    public void SetItem(Item _item) //필드에 아이템을 생성할 때 SetItem을 통해 전달받은 Item으로 현재 클래스의 Item초기화
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemType = _item.itemType;
        item.efts = _item.efts;

        image.sprite = _item.itemImage; //아이템에 맞게 Sprite 바꿈
    }

    public Item GetItem() //아이템 획득시 아이템 사용
    {
        return item;
    }

    public void DestroyItem()//아이템 획득 시 필드의 아이템 삭제
    {
        Destroy(gameObject);
    }
}
