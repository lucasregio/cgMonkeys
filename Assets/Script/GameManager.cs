using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager main;
    public bool hasStard;
    public int round, gold, life;
    public TextMeshProUGUI[] texts;
    public GameObject messageError, Canvas;
    public GameObject[] Monkeys;
    public GameObject current;
    public GenericBloonController bloonController;
    private void Start()
    {
        main = this;
        texts[0].text = (round = 1).ToString();
        texts[1].text = (gold = 900).ToString();
        texts[2].text = (life = 100).ToString();
    }
    public void SelectMonkey(int index)
    {
        current = Monkeys[index];
    }
    public void Recomecar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MessageERROR()
    {
        GameObject @object = Instantiate(messageError, Canvas.transform);
        Destroy(@object, 3f);
    }
    public void PressStart()
    {
        if (hasStard)
        {
            hasStard = false;
        }
        else
        {
            hasStard = true;
            StartCoroutine(bloonController.SpawnBloons());
        }
    }
    public void SetRoundOnScreen()
    {
        texts[0].text = round.ToString();
    }
    public void SetGoldOnScreen()
    {
        texts[1].text = gold.ToString();
    }
    public void SetLifeOnScreen()
    {
        texts[2].text = life.ToString();
    }
}
