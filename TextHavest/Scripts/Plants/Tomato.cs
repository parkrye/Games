using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : Plant
{
    private void Start()
    {
        id = 7;
        label = "�丶��";
    }

    public override void Initialize()
    {
        growth = 26;
        limAge = 16;
    }
}