using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public WheelCollider[] wheels = new WheelCollider[4];   // ���ݶ��̴�
    GameObject[] wheelMesh = new GameObject[4];             // ���� ���� ����
    GameObject bodyMesh;                                    // ���� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        SetWheelCollider();
    }

    private void FixedUpdate()
    {
        // ���� ��ġ�� �̵��� �ݶ��̴��� ������ ���ø���
        // ���߿� �� ������ ��ü�� �ٽ� ���� ��ġ(�ݶ��̴� ��ġ)�� ���ͽ�Ŵ
        WheelPosAndAni();
        BodyPosAndAni();
    }

    // ������ ���ݶ��̴� ��ġ�� �̵���Ű�� �Լ�
    void WheelPosAndAni()
    {
        Vector3 wheelPosition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;

        for (int i = 0; i < 4; i++)
        {
            wheels[i].GetWorldPose(out wheelPosition, out wheelRotation);
            wheelMesh[i].transform.position = wheelPosition;
            wheelMesh[i].transform.rotation = wheelRotation;
        }
    }

    void BodyPosAndAni()
    {
        float y = 0;
        for(int i = 0; i < 4; i++)
        {
            y -= wheels[i].transform.localPosition.y;
        }
        bodyMesh.transform.localPosition = new Vector3(0, y/4, 0);
    }

    public void SetWheelCollider()
    {
        // ���� ���� �±׸� ���� ã��
        bodyMesh = GameObject.FindGameObjectWithTag("BodyMesh");

        // ���� ���� �±׸� ���� ã��
        wheelMesh = GameObject.FindGameObjectsWithTag("WheelMesh");
        for (int i = 0; i < 4; i++)
        {
            // ���ݶ��̴� ��ġ�� ���� ��ġ�� �̵���Ų��
            wheels[i].transform.position = wheelMesh[i].transform.position;
        }
    }
}
