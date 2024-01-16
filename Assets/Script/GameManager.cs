using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject Hexagone, Parent, Hexa, GameOverPanel, GameStartPanel,GameWinPanel;
    int Num = 0, MidPoint;
    [SerializeField]
    List<GameObject> AllHexa, EvenHexa, OddHexa, CatPosWalk, CatMoveObj, RemoveList;
    bool Flag;
    public static GameManager Instance;
    bool Lose, Bordar;
    [SerializeField]
    Sprite cat;
    public void Awake()
    {
        //GameStart();
        Instance = this;
     
    }
    public void GameStart()
    {
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                Hexa = Instantiate(Hexagone, Parent.transform);
                AllHexa.Add(Hexa);
                //Hexagone.name = Num.ToString();
                //Num++;
                if (j == 0 || j == 10 || i == 0 || i == 10)
                {
                    Hexa.tag = "Bordar";
                  
                }
                if (!Flag)
                {
                    Hexa.transform.position = new Vector2(j * 0.7f, i * 0.8f);
                    EvenHexa.Add(Hexa);
                }
                else
                {
                    Hexa.transform.position = new Vector2(0.3f + (j * 0.7f), i * 0.8f);
                    OddHexa.Add(Hexa);
                }
                Hexa.gameObject.name = Num.ToString();
                Num++;
            }
            Flag = !Flag;
        }
        Parent.transform.position = new Vector2(-1f, -3.5f);
        MidPoint = AllHexa.Count / 2;
        MiddlePointSet();
    }
    void MiddlePointSet()
    {
        CatPosWalk.Clear();
        //MidPoint = 91;
        AllHexa[MidPoint].gameObject.GetComponent<SpriteRenderer>().sprite= cat;
        AllHexa[MidPoint].GetComponent<PolygonCollider2D>().enabled = false;

        //Debug.Log(MidPoint);

        if (OddHexa.Contains(AllHexa[MidPoint]))
        {
            //AllHexa[MidPoint + 1].GetComponent<SpriteRenderer>().material.color = Color.black;
            //AllHexa[MidPoint - 1].GetComponent<SpriteRenderer>().material.color = Color.black;
            //AllHexa[MidPoint - 11].GetComponent<SpriteRenderer>().material.color = Color.black;
            //AllHexa[MidPoint + 12].GetComponent<SpriteRenderer>().material.color = Color.black;
            //AllHexa[MidPoint - 10].GetComponent<SpriteRenderer>().material.color = Color.black;
            //AllHexa[MidPoint + 11].GetComponent<SpriteRenderer>().material.color = Color.black;

            CatPosWalk.Add(AllHexa[MidPoint + 1]);
            CatPosWalk.Add(AllHexa[MidPoint - 1]);
            CatPosWalk.Add(AllHexa[MidPoint - 11]);
            CatPosWalk.Add(AllHexa[MidPoint + 12]);
            CatPosWalk.Add(AllHexa[MidPoint - 10]);
            CatPosWalk.Add(AllHexa[MidPoint + 11]);
        }
        else
        {
            //AllHexa[MidPoint + 1].GetComponent<SpriteRenderer>().material.color = Color.black;
            //AllHexa[MidPoint - 1].GetComponent<SpriteRenderer>().material.color = Color.black;
            //AllHexa[MidPoint - 11].GetComponent<SpriteRenderer>().material.color = Color.black;
            //AllHexa[MidPoint - 12].GetComponent<SpriteRenderer>().material.color = Color.black;
            //AllHexa[MidPoint + 10].GetComponent<SpriteRenderer>().material.color = Color.black;
            //AllHexa[MidPoint + 11].GetComponent<SpriteRenderer>().material.color = Color.black;

            CatPosWalk.Add(AllHexa[MidPoint + 1]);
            CatPosWalk.Add(AllHexa[MidPoint - 1]);
            CatPosWalk.Add(AllHexa[MidPoint - 11]);
            CatPosWalk.Add(AllHexa[MidPoint - 12]);
            CatPosWalk.Add(AllHexa[MidPoint + 10]);
            CatPosWalk.Add(AllHexa[MidPoint + 11]);
        }
    }

    public void CatMoveAction(GameObject gameObj)
    {
        gameObj.GetComponent<SpriteRenderer>().color = Color.yellow;
        gameObj.GetComponent<PolygonCollider2D>().enabled = false;
        CatMoveObj.Add(gameObj);
        CheakCatMove();
        CatMove();
    }
    public void GameRestart()
    {
        GameOverPanel.SetActive(false);
        //GameStartPanel.SetActive(true);
        SceneManager.LoadScene(0);
    }
    public void Gamewin()
    {
        GameWinPanel.SetActive(false);
        //GameStartPanel.SetActive(true);
        SceneManager.LoadScene(0);
    }
    public void GameStartt()
    {
        GameStartPanel.SetActive(false);
         GameStart();
    }
    public void CheakCatMove()
    {
        for (int i = 0; i < CatPosWalk.Count; i++)
        {
            if (CatMoveObj.Contains(CatPosWalk[i]))
            {
                //CatPosWalk.Remove(CatPosWalk[i]);
                RemoveList.Add(CatPosWalk[i]);
            }
        }
        foreach (GameObject item in RemoveList)
        {
            Debug.Log("remove");
            CatPosWalk.Remove(item);
        }
        Bordar = true;
    }
    public void CatMove()
    {
        if (Lose)
        {
            AllHexa[MidPoint].GetComponent<SpriteRenderer>().color = Color.green;
            Debug.Log("ooopps cat is gone , you are loss");
            GameOverPanel.SetActive(true);
        }
        else
        {
            AllHexa[MidPoint].GetComponent<SpriteRenderer>().color = Color.green;
            AllHexa[MidPoint].GetComponent<PolygonCollider2D>().enabled = true;
            if (CatPosWalk.Count == 0)
            {
                Debug.Log("cat is catch , you are win");
                GameWinPanel.SetActive(true);
            }
            else
            {
                int Val = Random.Range(0, CatPosWalk.Count);
                MidPoint = int.Parse(CatPosWalk[Val].gameObject.name);
            }
            if (AllHexa[MidPoint].CompareTag("Bordar"))
            {
                Debug.Log("cat is here");
                AllHexa[MidPoint].GetComponent<SpriteRenderer>().color = Color.black;
                Lose = true;
            }
            else
            {
                if (Bordar)
                {
                    MiddlePointSet();
                }
                else
                {
                    GameOverPanel.SetActive(true);
                }
            }
        }
    }
}
