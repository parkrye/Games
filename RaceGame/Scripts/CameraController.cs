using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 5.0f;
    public GameObject cameraPos;
    public GameObject playerPos;

    // Update is called once per frame
    void LateUpdate()
    {
        // ī�޶� ��ġ�� ������ ��ġ�� �̵�
        gameObject.transform.position = Vector3.Lerp(transform.position, cameraPos.transform.position, Time.deltaTime * speed);
        // ī�޶� �÷��̾ �ٶ󺸵���
        gameObject.transform.LookAt(playerPos.transform);
    }
}
