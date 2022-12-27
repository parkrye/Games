using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Plant : MonoBehaviour
{
    public int id;          // �۹� id
    public string label;    // �۹� �̸�
    public int growth;      // ���嵵

    public int limAge;      // �Ѱ� ����
    public int age;         // ���� ����

    public int limMo;       // �Ѱ� ����
    public int mo;          // ���� ����

    public int field;       // ��ġ�� ��
    public float time;      // ���� �ð�
    public int count;       // ���� ī��Ʈ

    public abstract void Initialize();

    public void Planting(int f)
    {
        age = 0;
        limMo = 20;
        mo = 5;
        count = 0;

        field = f;
        Initialize();
        StartCoroutine(Aging());
        StartCoroutine(Drying());
    }

    public IEnumerator Aging()
    {
        while (age < limAge)
        {
            time = Mathf.Lerp(2f, 1f, (growth / 50f)) * Mathf.Lerp(2f, 1f, (mo / 20f));
            yield return new WaitForSeconds(time);
            count++;
            if(count >= 60)
            {
                age += 1;
                count = 0;
            }
        }
    }

    public IEnumerator Drying()
    {
        while (age < limAge)
        {
            time = Mathf.Lerp(2f, 1f, (growth / 50f)) * Mathf.Lerp(2f, 1f, (mo / 20f));
            yield return new WaitForSeconds(time * 30);
            if(mo > 0)
            {
                mo -= 1;
            }
        }
    }

    public void Watering()
    {
        if(mo < 20)
            mo += 1;
    }

    public string GetName()
    {
        return label;
    }

    public int GetAge()
    {
        return age;
    }

    public int GetLimAge()
    {
        return limAge;
    }

    public int GetMo()
    {
        return mo;
    }

    public int GetId()
    {
        return id;
    }

    public void Setting(int a, int m)
    {
        age = a;
        mo = m;
    }
}
