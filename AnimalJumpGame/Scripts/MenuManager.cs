using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Text[] menus = new Text[4];  // ���, Ű Ȯ��, ó������, �ڷ� ���� 4�� ���
    public GameObject keyPrefab;        // Ű ��ġ UI
    int cursor;                         // Ŀ��
    bool keyCanvasOn;                   // Ű ��ġ UI�� �����Ǿ� �ִ��� ����
    public AudioSource[] audios;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        cursor = 0;
        for(int i = 0; i < 4; i++)
        {
            if(i == cursor)
            {
                menus[i].color = Color.white;
            }
            else
            {
                menus[i].color = Color.grey;
            }
        }
        keyCanvasOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!keyCanvasOn)
        {
            CursorMove();
            CursorClick();
        }
        else
        {
            KeyCanvasOut();
        }
    }

    // Ŀ�� �̵�. Ŀ���� ��ġ�� �ؽ�Ʈ�� �Ͼ��. ���� ���� ��� �� �ݴ������� �̵�
    void CursorMove()
    {
        if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
        {
            audios[0].Play();
            cursor++;
            if(cursor > 3)
            {
                cursor = 0;
            }
        }
        else if(Input.GetKeyDown("up") || Input.GetKeyDown("w"))
        {
            audios[0].Play();
            cursor--;
            if (cursor < 0)
            {
                cursor = 3;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (i == cursor)
            {
                menus[i].color = Color.white;
            }
            else
            {
                menus[i].color = Color.grey;
            }
        }
    }

    // Ŀ���� ��ġ�� �޴� Ŭ���� �̺�Ʈ ����
    void CursorClick()
    {
        if (Input.GetKeyDown("return") || Input.GetKeyDown("space"))
        {
            audios[1].Play();
            if (cursor == 0)                    // ���� ����
            {
                Time.timeScale = 1f;
                DataManager.Manager.SetMenu(false);
                Destroy(gameObject);
            }
            else if(cursor == 1)                // �� �ʱ�ȭ
            {
                Time.timeScale = 1f;
                DataManager.Manager.SetMenu(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Destroy(gameObject);
            }
            else if (cursor == 2)                // Ű Ȯ��
            {
                Instantiate(keyPrefab);
                keyCanvasOn = true;
            }
            else                                // �̴ϸ�����
            {
                Time.timeScale = 1f;
                DataManager.Manager.SetMenu(false);
                SceneManager.LoadScene("01_Map");
                Destroy(gameObject);
            }
        }
        else if (Input.GetKeyDown("escape"))    // ���� ����
        {
            audios[1].Play();
            Time.timeScale = 1f;
            DataManager.Manager.SetMenu(false);
            Destroy(gameObject);
        }
    }

    // Ű Ȯ�� UI �����
    void KeyCanvasOut()
    {
        if (Input.anyKeyDown)
        {
            keyCanvasOn = false;
        }
    }
}
