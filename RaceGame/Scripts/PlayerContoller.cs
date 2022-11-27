using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    public WheelCollider[] wheels = new WheelCollider[4];   // ���ݶ��̴�
    public float power = 100f;  // ���� ȸ�� ��
    public float rot = 45f;     // ���� ȸ�� ����
    Rigidbody rb;
    InputManager IM;
    public float downForceValue;    // ��ü�� ������ ��
    public float radius = 6;        // ��Ŀ�� ������ ���� ������
    public GameObject startPoint;   // ��ġ �ʱ�ȭ�� ���� ��������

    // �޹��� Ÿ�̾� ����
    WheelFrictionCurve fFrictionL;
    WheelFrictionCurve sFrictionL;
    WheelFrictionCurve fFrictionR;
    WheelFrictionCurve sFrictionR;

    float slipRate = 1.0f;          // Ÿ�̾� �⺻ ���� ���
    float handBrakeSlipRate = 0.4f; // Ÿ�̾� �극��ũ�� ���� ���
    

    // Start is called before the first frame update
    void Start()
    {
        // ���� �߽��� -y������ �����
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1, 0);

        IM = GetComponent<InputManager>();

        SetLocation();

        fFrictionL = wheels[2].forwardFriction;
        sFrictionL = wheels[2].sidewaysFriction;
        fFrictionR = wheels[3].forwardFriction;
        sFrictionR = wheels[3].sidewaysFriction;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = startPoint.transform.position;
            transform.rotation = startPoint.transform.rotation;
        }
    }

    private void FixedUpdate()
    {
        // �� ������ ���� ��ȯ, �� ������ ���� �̵�
        for (int i = 0; i < 4; i++)
        {
            if(i < 2)
            {
                wheels[i].steerAngle = IM.horizontal * rot;
            }

            wheels[i].motorTorque = IM.vertical * power;

            if (IM.handbrake)
            {
                fFrictionL.stiffness = handBrakeSlipRate;
                wheels[2].forwardFriction = fFrictionL;
                sFrictionL.stiffness = handBrakeSlipRate;
                wheels[2].sidewaysFriction = sFrictionL;

                fFrictionR.stiffness = handBrakeSlipRate;
                wheels[3].forwardFriction = fFrictionR;
                sFrictionR.stiffness = handBrakeSlipRate;
                wheels[3].sidewaysFriction = sFrictionR;
            }
            else
            {
                fFrictionL.stiffness = slipRate;
                wheels[2].forwardFriction = fFrictionL;
                sFrictionL.stiffness = slipRate;
                wheels[2].sidewaysFriction = sFrictionL;

                fFrictionR.stiffness = slipRate;
                wheels[3].forwardFriction = fFrictionR;
                sFrictionR.stiffness = slipRate;
                wheels[3].sidewaysFriction = sFrictionR;
            }
        }

        AddDownForce();
        SteerVehicle();
    }

    // �ӵ��� �������� ���� ��ü�� ������ ���� ����
    void AddDownForce()
    {
        rb.AddForce(-transform.up * downForceValue * rb.velocity.magnitude);
    }

    void SteerVehicle()
    {
        if (IM.horizontal > 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * IM.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * IM.horizontal;
        }
        else if(IM.horizontal < 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * IM.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * IM.horizontal;
        }
        else
        {

            wheels[0].steerAngle = 0;
            wheels[1].steerAngle = 0;
        }
    }

    public void SetCarStatus(int number)
    {
        rb = GetComponent<Rigidbody>();
        if (number == 0)
        {
            rb.mass = 600;
            power = 100;
        }
        else if (number == 1)
        {
            rb.mass = 700;
            power = 110;
        }
        else if (number == 2)
        {
            rb.mass = 800;
            power = 120;
        }
        else if (number == 3)
        {
            rb.mass = 900;
            power = 130;
        }
        else if (number == 4)
        {
            rb.mass = 1000;
            power = 140;
        }
        else if (number == 5)
        {
            rb.mass = 1100;
            power = 150;
        }
        else if (number == 6)
        {
            rb.mass = 1200;
            power = 160;
        }
        else if (number == 7)
        {
            rb.mass = 1300;
            power = 170;
        }
    }

    public void SetLocation()
    {
        transform.position = startPoint.transform.position;
        transform.rotation = startPoint.transform.rotation;
    }
}
