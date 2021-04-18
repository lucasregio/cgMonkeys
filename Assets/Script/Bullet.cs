using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Monkey parent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<AIMove>().ApplyDamage(parent.damage);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if(Vector3.Distance(transform.position,parent.transform.position)>parent.range)
        {
            Destroy(gameObject);
        }
    }
}
