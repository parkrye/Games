using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peach : Plant
{
    private void Start()
    {
        id = 10;
        label = "������";
    }

    public override void Initialize()
    {
        growth = 18;
        limAge = 14;
    }
}
