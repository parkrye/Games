using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public GameObject[] maps = new GameObject[4];   // �� �÷ξ�
    public GameObject[] stage1 = new GameObject[4]; // 1 �÷ξ� �ʵ�
    public GameObject[] stage2 = new GameObject[3]; // 2 �÷ξ� �ʵ�
    public GameObject[] stage3 = new GameObject[2]; // 3 �÷ξ� �ʵ�
    public GameObject[] stage4 = new GameObject[1]; // 4 �÷ξ� �ʵ�

    int map;            // ���� ����� �� ����
    int floor;          // ���� �÷��̾��� ����
    bool inGate;        // ���� �� ����Ʈ �տ� �ִ���
    string gateName;    // ���� ��ġ�� �� ����Ʈ �̸�

    // Start is called before the first frame update
    void Start()
    {
        map = DataManager.Manager.GetOpen();
        Debug.Log(map);
        floor = 0;

        maps[0].SetActive(true);
        for (int i = 0; i <= map && i < 4; i++)
        {
            stage1[i].SetActive(true);
        }

        if (map >= 4)
        {
            maps[1].SetActive(true);
            for (int i = 0; i <= map - 4 && i < 3; i++)
            {
                stage2[i].SetActive(true);
            }
            transform.position = new Vector2(-5f, transform.position.y + 2.7f);
            floor++;
        }

        if (map >= 7)
        {
            maps[2].SetActive(true);
            for (int i = 0; i <= map - 7 && i < 2; i++)
            {
                stage3[i].SetActive(true);
            }
            transform.position = new Vector2(-5f, transform.position.y + 2.7f);
            floor++;
        }

        if (map >= 9)
        {
            maps[3].SetActive(true);
            for (int i = 0; i <= map - 9; i++)
            {
                stage4[i].SetActive(true);
            }
            transform.position = new Vector2(-5f, transform.position.y + 2.7f);
            floor++;
        }

    }

    bool direction;

    void Update()
    {
        MiniMove();
        GoToStage();
        GoBack();
    }

    // �� ĳ���� �̵�
    void MiniMove()
    {
        if ((Input.GetKey("a") || Input.GetKey("left")) && !(Input.GetKey("d") || Input.GetKey("right")))
        {
            if (!direction)
            {
                transform.Rotate(0f, 180f, 0f);
                direction = true;
            }
            transform.Translate(0.015f, 0f, 0f);
        }
        else if ((Input.GetKey("d") || Input.GetKey("right")) && !(Input.GetKey("a") || Input.GetKey("left")))
        {
            if (direction)
            {
                transform.Rotate(0f, 180f, 0f);
                direction = false;
            }
            transform.Translate(0.015f, 0f, 0f);
        }
    }

    // �� �������� �̵�
    void GoToStage()
    {
        if (inGate)
        {
            if(Input.GetKeyDown("w") || Input.GetKeyDown("up") || Input.GetKeyDown("return") || Input.GetKeyDown("space"))
            {
                string stage = "02_" + gateName;
                SceneManager.LoadScene(stage);
            }
        }
    }

    // Ÿ��Ʋ ȭ������
    void GoBack()
    {
        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene("00_Home");
        }
    }

    // �÷ξ� ���� ������ �ٸ� �÷ξ�� �̵�
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "WallRight")
        {
            if(floor < 4 && floor < map)
            {
                floor++;
                transform.position = new Vector2(-5f, transform.position.y + 2.7f);
            }
            else
            {
                transform.position = new Vector2(5f, transform.position.y);
            }
        }

        if (collision.gameObject.name == "WallLeft")
        {
            if (floor > 0)
            {
                floor--;
                transform.position = new Vector2(5f, transform.position.y - 2.7f);
            }
            else
            {
                transform.position = new Vector2(-5f, transform.position.y);
            }
        }
    }

    // ����Ʈ�� �����ϸ� �̸��� Ȯ��
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Gate")
        {
            inGate = true;
            gateName = collision.gameObject.name;
        }
    }

    // ����Ʈ���� �־����� ����
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gate")
        {
            inGate = false;
            gateName = "";
        }
    }
}
