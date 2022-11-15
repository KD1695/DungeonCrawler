using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockGate : Interact
{
    [SerializeField] private CapsuleCollider gateCollider;
    [SerializeField] private MeshRenderer gateMesh;
    [SerializeField] private Material lockMaterial;
    [SerializeField] private Material unlockMaterial;
    public override void InteractAction()
    {
        if (gateCollider.isTrigger)
        {
            //lock
            gateCollider.isTrigger = false;
            gateMesh.material = lockMaterial;
            GameManager.Instance.AddSouls(5);
        }
        else
        {
            //unlock
            if (GameManager.Instance.RemoveSouls(5))
            {
                gateCollider.isTrigger = true;
                gateMesh.material = unlockMaterial;
            };
        }
    }
}
