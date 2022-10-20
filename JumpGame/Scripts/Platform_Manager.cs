using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class Platform_Manager : MonoBehaviour
{
    // ������ ���� prefab
    public GameObject prefab_platform;

    // ���� �÷��� ��ġ ������
    private Vector3 nextPos;

    // �÷��� �¿� ����
    private float minDistance;
    private float maxDistance;

    // �÷��� ���� �ʱⰪ, ����
    private float stdHeight;
    private float minHeight;
    private float maxHeight;

    // �÷��� �߰�
    private int count;
    private bool flag;
    private float endTime;
    private float nowTime;

    // Start is called before the first frame update
    void Awake()
    {
        // ���� �÷��� ��ġ ����
        nextPos = prefab_platform.transform.position;

        minDistance = 1.0f;
        maxDistance = 3.0f;
        minHeight = -1.0f;
        maxHeight = 1.0f;

        stdHeight = nextPos.y;

        count = 100;
        flag = false;
        endTime = 1.0f;
        nowTime = 0.0f;
    }

    private void FixedUpdate()
    {
        nowTime += Time.deltaTime;

        if (nowTime >= endTime & (flag & count > 0))
        {
            AddNewPlatform();
            nowTime = 0.0f;
            count--;
        }
    }

    public void SetFlag()
    {
        flag = true;
    }

    private void AddNewPlatform()
    {
        // Instantiate �Լ��� �־��� prefab�� ���� GameObject�� ����
        GameObject added_Platform = Instantiate(prefab_platform);

        // ������ �÷��� ��ġ�� ���� ��ġ�� ����
        added_Platform.transform.position = new Vector3(nextPos.x + prefab_platform.GetComponent<BoxCollider2D>().size.x + RandomRange(minDistance, maxDistance), stdHeight + RandomRange(minHeight, maxHeight), 0);

        //�ֱ� �÷��� ��ġ�� ������ �÷��� ��ġ ����
        nextPos = added_Platform.transform.position;
    }

    private float RandomRange(float a, float b)
    {
        return Random.Range(a, b);
    }
}
