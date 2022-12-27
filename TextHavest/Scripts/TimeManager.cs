using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeManager : MonoBehaviour
{
    public Text text;
    public bool alarm;

    public PricesManager pricesManager;

    private void Start()
    {
        alarm = false;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = DateTime.Now.ToString(("yyyy�� MM�� dd�� HH�� mm�� ss��"));
        if (alarm)
        {
            if (!DateTime.Now.ToString(("ss")).Equals("00"))
            {
                alarm = false;
            }
        }
        else if (DateTime.Now.ToString(("ss")).Equals("00"))
        {
            pricesManager.PriceChange();
            alarm = true;
        }
    }
}
