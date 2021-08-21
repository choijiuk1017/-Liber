using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Item item; //� ����������
    public SpriteRenderer image; //������ �̹����� �ٲٱ� ����

    public void SetItem(Item _item) //�ʵ忡 �������� ������ �� SetItem�� ���� ���޹��� Item���� ���� Ŭ������ Item�ʱ�ȭ
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemType = _item.itemType;
        item.efts = _item.efts;

        image.sprite = _item.itemImage; //�����ۿ� �°� Sprite �ٲ�
    }

    public Item GetItem() //������ ȹ��� ������ ���
    {
        return item;
    }

    public void DestroyItem()//������ ȹ�� �� �ʵ��� ������ ����
    {
        Destroy(gameObject);
    }
}
