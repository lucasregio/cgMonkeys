using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    public enum TypeOfMonkey { Normal, Snipper, Cannon, SUPER }
    public TypeOfMonkey monkey;
    public int cost, damage;
    public float range, timerProjetil;
    public Color color;
    public GameObject projetil, gun;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AttackNearestBloons", 0f, 0.5f);
    }
    public void AttackNearestBloons()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortDist = Mathf.Infinity;
        GameObject nearest = null;
        foreach (GameObject item in enemies)
        {
            float searchDistance = Vector3.Distance(transform.position, item.transform.position);
            if (searchDistance < shortDist)
            {
                shortDist = searchDistance;
                nearest = item;
            }
        }
        if (nearest != null && shortDist <= range)
        {
            target = nearest.transform;
        }
        else
        {
            nearest = null;
        }
    }
    private void Update()
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            if (timerProjetil <= 0)
            {
                timerProjetil = 0.05f;
                GameObject bullet = Instantiate(projetil, gun.transform.position,Quaternion.identity);
                bullet.GetComponent<Bullet>().parent = this;
                bullet.GetComponent<Rigidbody2D>().velocity = gun.transform.TransformDirection(Vector2.right) * 10;
                Destroy(bullet, 2f);
            }
            else
            {
                timerProjetil -= Time.deltaTime;
            }
        }
    }
}
