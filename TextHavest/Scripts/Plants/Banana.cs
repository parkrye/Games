using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : Plant
{
    private void Start()
    {
        id = 9;
        label = "�ٳ���";
    }

    public override void Initialize()
    {
        growth = 23;
        limAge = 15;
    }
}
