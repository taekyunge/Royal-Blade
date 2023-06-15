using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DropObject")
        {
            collision.gameObject.SetActive(false);

            GameMgr.instance.attackCount++;
            GameMgr.instance.totalPoint += 100 * (GameMgr.instance.currentWave + 1);
        }
    }
}
