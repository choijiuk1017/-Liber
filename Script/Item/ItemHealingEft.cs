using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ItemEft/Conumable/Health")]
public class ItemHealingEft : ItemEffect
{
    public int healingPoint = 0;
    public override bool ExcuteRole()
    {
        Debug.Log("PlayerHp Add:" + healingPoint);
        return true;
    }
}
