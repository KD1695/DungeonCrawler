using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : Interact
{
    [SerializeField] private Light roomLight;
    public override void InteractAction()
    {
        if (roomLight.enabled)
        {
            roomLight.enabled = false;
            GameManager.Instance.AddSouls(3);
        }
        else
        {
            if (GameManager.Instance.RemoveSouls(3))
            {
                roomLight.enabled = true;
            }
        }
    }
}
