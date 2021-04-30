using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBloonController : MonoBehaviour
{
    public enum TypeController { Spawnner, Destroyer}
    public TypeController typeController;
    public GameObject prefabBloon,gameOver;
    public int round;
    public float timer, startTimer;
    public int countBloons, maxBloons;

    void Start()
    {
        countBloons = maxBloons;
    }
    public IEnumerator SpawnBloons()
    {
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
                    GameObject @object = Instantiate(prefabBloon, transform.position, Quaternion.identity);
                    @object.GetComponent<SpriteRenderer>().color = Color.red;
                    @object.GetComponent<AIMove>().damage = 1;
                    @object.GetComponent<AIMove>().balaoType = AIMove.TipoDeBalao.Normal;
                    @object.GetComponent<AIMove>().life = 1;
                    @object.GetComponent<AIMove>().amountGold = 5;
                }
                else if (round >= 18 && round < 27)
                {
                    GameObject @object = Instantiate(prefabBloon, transform.position, Quaternion.identity);
                    @object.GetComponent<SpriteRenderer>().color = Color.yellow;
                    @object.GetComponent<AIMove>().damage = 3;
                    @object.GetComponent<AIMove>().balaoType = AIMove.TipoDeBalao.Normal;
                    @object.GetComponent<AIMove>().life = 5;
                    @object.GetComponent<AIMove>().amountGold = 25;
                }
                else if (round >= 27 && round < 36)
                {
                    GameObject @object = Instantiate(prefabBloon, transform.position, Quaternion.identity);
                    @object.GetComponent<SpriteRenderer>().color = Color.green;
                    @object.GetComponent<AIMove>().damage = 6;
                    @object.GetComponent<AIMove>().balaoType = AIMove.TipoDeBalao.Camuflado;
                    @object.GetComponent<AIMove>().life = 10;
                    @object.GetComponent<AIMove>().amountGold = 100;
                }
                else if (round >= 36 && round < 54)
                {
                    GameObject @object = Instantiate(prefabBloon, transform.position, Quaternion.identity);
                    @object.GetComponent<SpriteRenderer>().color = Color.cyan;
                    @object.GetComponent<AIMove>().damage = 8;
                    @object.GetComponent<AIMove>().balaoType = AIMove.TipoDeBalao.Encouracado;
                    @object.GetComponent<AIMove>().life = 20;
                    @object.GetComponent<AIMove>().amountGold = 200;
                }
                else
                {
                    GameObject @object = Instantiate(prefabBloon, transform.position, Quaternion.identity);
                    @object.GetComponent<SpriteRenderer>().color = Color.black;
                    @object.GetComponent<AIMove>().damage = 15;
                    @object.GetComponent<AIMove>().balaoType = AIMove.TipoDeBalao.MOAB;
                    @object.GetComponent<AIMove>().life = 35;
                    @object.GetComponent<AIMove>().amountGold = 500;
                }
                changeRound();
            }
            GameManager.main.round = round;
            GameManager.main.SetRoundOnScreen();
            yield return null;
        }
    }

    public void changeRound()
    {
        if (countBloons <= 0)
        {
            round++;
            if (round < 18)
            {
                countBloons = maxBloons;
            }
            else if (round >= 18 && round < 27)
            {
                countBloons = maxBloons + (int)(maxBloons * 0.2f);
            }
            else if (round >= 27 && round < 36)
            {
                countBloons = maxBloons + (int)(maxBloons * 0.3f);
            }
            else if (round >= 36 && round < 54)
            {
                countBloons = maxBloons + (int)(maxBloons * 0.4f);
            }
            else
            {
                countBloons = maxBloons + (int)(maxBloons * 0.5f);
            }
        }
        else
        {
            countBloons--;
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
                GameManager.main.hasStard = false;
                gameOver.SetActive(true);
            }
            GameManager.main.SetLifeOnScreen();
            Destroy(collision.gameObject);
        }
    }
}
