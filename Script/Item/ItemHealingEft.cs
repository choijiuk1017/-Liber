using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ItemEft/Conumable/Health")]
public class ItemHealingEft : ItemEffect
{
    Player player; //플레이어 스크립트 연동
    public int healingPoint = 0;
    public override bool ExcuteRole()
    {
        //healing();
        Debug.Log("Player HP Add:" + healingPoint);
        return true;
    }


    /*public void healing()
    {
        if (healingPoint == 1)
        {
            if (player.health == 2)
            {
                ++player.health;
                player.Hp3.SetActive(true);
            }
            else if (player.health == 1)
            {
                ++player.health;
                player.Hp2.SetActive(true);
            }
        }
        else if (healingPoint == 2)
        {
            if (player.health == 1)
            {
                player.health = player.health + 2;
                player.Hp2.SetActive(true);
                player.Hp3.SetActive(true);
            }
            else if(player.health == 2)
            {
                ++player.health;
                player.Hp3.SetActive(true);
            }
        }

    }*/

}
