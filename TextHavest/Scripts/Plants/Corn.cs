using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corn : Plant
{
    private void Start()
    {
        id = 11;
        label = "������";
    }

    public override void Initialize()
    {
        growth = 16;
        limAge = 14;
    }
}
