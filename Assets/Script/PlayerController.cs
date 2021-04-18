using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    Vector3 worldPosition;
    public Transform objeto;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            else
            {
                RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
                if (!rayHit.collider.CompareTag("CannotPlace"))
                {
                    if (GameManager.main.current)
                    {
                        if ((GameManager.main.gold - GameManager.main.current.GetComponent<Monkey>().cost) >= 0)
                        {
                            GameManager.main.gold -= GameManager.main.current.GetComponent<Monkey>().cost;
                            GameManager.main.SetGoldOnScreen();
                            GameObject @object = Instantiate(GameManager.main.current);
                            @object.transform.position = rayHit.point;
                        }
                        else
                        {
                            GameManager.main.MessageERROR();
                        }
                    }
                }
            }
        }
    }
}
