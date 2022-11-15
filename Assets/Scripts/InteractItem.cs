using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InteractItem : Interact
{
    [SerializeField] public int storedSouls = 0;

    private void Start()
    {
        storedSouls = Random.Range(3, 11);
    }

    public override void InteractAction()
    {
        GameManager.Instance.AddSouls(storedSouls);
        storedSouls = 0;
    }
}
