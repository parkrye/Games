using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chili : Plant
{
    private void Start()
    {
        id = 12;
        label = "����";
    }

    public override void Initialize()
    {
        growth = 14;
        limAge = 13;
    }
}
