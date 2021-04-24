using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBloonController : MonoBehaviour
{
    public enum TypeController { Spawnner, Destroyer}
    public TypeController typeController;
    public GameObject prefabBloon;
    public int round;
    public float timer, startTimer;
    public int countBloons, maxBloons;

    void Start()
    {
        countBloons = maxBloons;
    }
    public IEnumerator SpawnBloons()
    {
        bool needRestart = false;
        yield return new WaitForEndOfFrame();
        while (GameManager.main.hasStard)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = startTimer;
                if (round < 18)
                {
                    if (needRestart)
                    {
                        countBloons = maxBloons + (int)(maxBloons * 0.2f);
                        yield return new WaitForSeconds(3f);
                        needRestart = false;
                    }
                    if (countBloons < 0)
                    {
                        round++;
                        needRestart = true;
                    }
                    else
                    {
                        countBloons--;
                        GameObject @object = Instantiate(prefabBloon, transform.position, Quaternion.identity);
                        @object.GetComponent<AIMove>().damage = 1;
                        @object.GetComponent<SpriteRenderer>().color = Color.red;
                        @object.GetComponent<AIMove>().balaoType = AIMove.TipoDeBalao.Normal;
                        @object.GetComponent<AIMove>().life = 1;
                        @object.GetComponent<AIMove>().amountGold = 5;
                    }
                }
                else if (round >= 18)
                {
                    if (needRestart)
                    {
                        countBloons += maxBloons + (int)(maxBloons * 0.35f * 100f);
                        yield return new WaitForSeconds(3f);
                        needRestart = false;
                    }
                    if (countBloons < 0)
                    {
                        round++;
                        needRestart = true;
                        GameObject @object = Instantiate(prefabBloon, transform.position, Quaternion.identity);
                        @object.GetComponent<SpriteRenderer>().color = Color.yellow;
                        @object.GetComponent<AIMove>().damage = 3;
                        @object.GetComponent<AIMove>().balaoType = AIMove.TipoDeBalao.Camuflado;
                        @object.GetComponent<AIMove>().life = 10;
                        @object.GetComponent<AIMove>().amountGold = 20;
                    }
                }
                else if (round >= 36)
                {
                    if (needRestart)
                    {
                        countBloons += maxBloons + (int)(maxBloons * 0.4f);
                        yield return new WaitForSeconds(3f);
                        needRestart = false;
                    }
                    if (countBloons < 0)
                    {
                        round++;
                        needRestart = true;
                    }
                    else
                    {
                        countBloons--;
                        GameObject @object = Instantiate(prefabBloon, transform.position, Quaternion.identity);
                        @object.GetComponent<SpriteRenderer>().color = Color.green;
                        @object.GetComponent<AIMove>().balaoType = AIMove.TipoDeBalao.Encouracado;
                        @object.GetComponent<AIMove>().damage = 5;
                        @object.GetComponent<AIMove>().life = 15;
                        @object.GetComponent<AIMove>().amountGold = 40;
                    }
                }
                else if (round >= 54)
                {
                    if (needRestart)
                    {
                        countBloons += maxBloons + (int)(maxBloons * 0.45f);
                        yield return new WaitForSeconds(3f);
                        needRestart = false;
                    }
                    if (countBloons < 0)
                    {
                        round++;
                        needRestart = true;
                    }
                    else
                    {
                        countBloons--;
                        GameObject @object = Instantiate(prefabBloon, transform.position, Quaternion.identity);
                        @object.GetComponent<SpriteRenderer>().color = Color.blue;
                        @object.GetComponent<AIMove>().balaoType = AIMove.TipoDeBalao.MOAB;
                        @object.GetComponent<AIMove>().damage = 25;
                        @object.GetComponent<AIMove>().life = 50;
                        @object.GetComponent<AIMove>().amountGold = 40;
                    }
                }
                else
                {
                    if (needRestart)
                    {
                        countBloons += maxBloons + (int)(maxBloons * 0.5f);
                        yield return new WaitForSeconds(3f);
                        needRestart = false;
                    }
                    if (countBloons < 0)
                    {
                        round++;
                        needRestart = true;
                    }
                    else
                    {
                        countBloons--;
                        GameObject @object = Instantiate(prefabBloon, transform.position, Quaternion.identity);
                        @object.GetComponent<SpriteRenderer>().color = Color.black;
                        @object.GetComponent<AIMove>().balaoType = AIMove.TipoDeBalao.MOAB;
                        @object.GetComponent<AIMove>().damage = 50;
                        @object.GetComponent<AIMove>().life = 100;
                        @object.GetComponent<AIMove>().amountGold = 500;
                    }
                }
            }
            GameManager.main.round = round;
            GameManager.main.SetRoundOnScreen();
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (typeController == TypeController.Destroyer && collision.CompareTag("Enemy"))
        {
            if ((GameManager.main.life - collision.GetComponent<AIMove>().damage) > 0)
            {
                GameManager.main.life -= collision.GetComponent<AIMove>().damage;
            }
            else
            {
                GameManager.main.life = 0;
            }
            GameManager.main.SetLifeOnScreen();
            Destroy(collision.gameObject);
        }
    }
}
