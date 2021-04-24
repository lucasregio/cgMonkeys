using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIMove : MonoBehaviour
{
    public enum TipoDeBalao { Normal, Camuflado, Encouracado, MOAB }
    public TipoDeBalao balaoType;
    public List<Transform> path;
    public Transform pai;
    public float speed;
    public int index, life, amountGold, damage;
    private void Start()
    {
        path = GameObject.FindGameObjectWithTag("Path").GetComponentsInChildren<Transform>().ToList();
        path.Remove(path[0]);
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, path[index].position) > 0)
        {
            Vector3 dir = path[index].position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.position = Vector3.MoveTowards(transform.position, path[index].position, Time.deltaTime * speed);
        }
        else
        {
            index++;
        }
    }
    public void ApplyDamage(int damage)
    {
        if ((life - damage) <= 0)
        {
            GameManager.main.gold += amountGold;
            GameManager.main.SetGoldOnScreen();
            Destroy(gameObject);
        }
        else
        {
            life -= damage;
        }
    }
}