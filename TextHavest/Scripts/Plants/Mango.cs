using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mango : Plant
{
    private void Start()
    {
        id = 13;
        label = "����";
    }

    public override void Initialize()
    {
        growth = 10;
        limAge = 13;
    }
}
