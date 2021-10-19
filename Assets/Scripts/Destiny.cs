using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destiny : Card
{
    int number;
    //public Suit suit

    public override void Start()
    {
        base.Start();
        number = Random.Range(1, 11);
    }

    public override void OnDraw()
    {
        Debug.Log(number);
    }
}
