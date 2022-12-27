using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    // ����� Ŭ���� ����
    public Data data;

    // ���� ������ ���� �̸�
    string gameDataFileName;

    public FarmManager farmManager;
    public FieldsManager fieldsManager;
    public PricesManager pricesManager;

    public void Initialize()
    {
        data = new Data();
        data.Initialize();
        gameDataFileName = "GameData.json";

        // ���� ���: Asset
        string filePath = Application.dataPath + "/" + gameDataFileName;

        // ���� ���� Ȯ��
        if (File.Exists(filePath))
        {
            string fromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<Data>(fromJsonData);
        }
    }

    // �����ϱ�
    public void SaveGameData()
    {
        // data ����
        data.SetFarmName(farmManager.GetFarmName());
        data.SetMoney(farmManager.GetFarmMoney());
        data.SetFieldPrice(fieldsManager.GetFieldPrice());
        data.SetOpen(fieldsManager.GetFieldOpens());
        data.SetPrices(pricesManager.GetPrices());
        for (int i = 0; i < 25; i++)
        {
            data.SetField(i, fieldsManager.fieldList[i].GetComponent<Field>().ToSaveStyle());
        }

        // Ŭ������ Json �������� ��ȯ. bool: ������ ���� �ۼ�
        string toJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.dataPath + "/" + gameDataFileName;

        // ����� ������ �ִٸ� �����, ���ٸ� ���� ����
        File.WriteAllText(filePath, toJsonData);
    }

}
