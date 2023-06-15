using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObject : MonoBehaviour
{
    // 떨어지는 오브젝트 체력
    public int hp;

    private new Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var character = collision.gameObject.GetComponent<Character>();

            if (character != null && character.isGround)
            {
                character.hp--;

                if(rigidbody != null)
                    rigidbody.AddForce(new Vector2(0, 150), ForceMode2D.Impulse);
            }
        }
    }
}
