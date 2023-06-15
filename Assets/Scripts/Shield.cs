using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private float force = 10;
    private bool useShield = false;

    private void OnEnable()
    {
        useShield = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DropObject")
        {
            var rigidbody = collision.transform.parent.GetComponent<Rigidbody2D>();

            if(rigidbody != null && !useShield)
            {
                rigidbody.AddForce(new Vector2(0, 200), ForceMode2D.Impulse);
                useShield = true;
            }
        }
    }
}
