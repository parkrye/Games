using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Data
{
    public string farmName;     // ���� �̸�
    public bool[] open;         // �� �ر�
    public int[] seeds;         // �۹� ���� ����(���, )
    public string[] fields;     // �� ����
    public int money;           // ��ȭ
    public int[] prices;        // �ü�
    public string[] plantNames; // �۹� �̸���
    public int fieldPrice;      // ���� �� �ر� ����

    public void Initialize()
    {
        farmName = "�ູ";
        open = new bool[25];
        open[0] = true;
        seeds = new int[16];
        seeds[0] = 1;
        fields = new string[25];
        for (int i = 0; i < 25; i++)
            fields[i] = "false/-1/-1/-1";
        money = 10;
        prices = new int[16] { 1, 6, 9, 12, 14, 18, 21, 24, 25, 27, 32, 34, 36, 40, 45, 49 };
        plantNames = new string[16] { "���", "�����", "����", "ȣ��", "��", "����", "���", "�丶��", "��", "�ٳ���", "������", "������", "����", "����", "����", "����" };
        fieldPrice = 10;
    }

    public void SetFarmName(string n)
    {
        farmName = n;
    }

    public string GetFarmName()
    {
        return farmName;
    }

    public void SetOpen(bool[] o)
    {
        open = o;
    }

    public bool[] GetOpen()
    {
        return open;
    }

    public void AddSeed(int i, int s)
    {
        seeds[i] += s;
    }

    public int[] GetSeeds()
    {
        return seeds;
    }

    public void SetMoney(int m)
    {
        money = m;
    }

    public int GetMoney()
    {
        return money;
    }

    public void SetPrices(int[] c)
    {
        prices = c;
    }

    public int[] GetPrices()
    {
        return prices;
    }

    public string GetPlantName(int i)
    {
        return plantNames[i];
    }

    public void SetField(int i, string f)
    {
        fields[i] = f;
    }

    public string GetField(int i)
    {
        return fields[i];
    }

    public void SetFieldPrice(int p)
    {
        fieldPrice = p;
    }

    public int GetFieldPrice()
    {
        return fieldPrice;
    }
}
