using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace PCG
{
    public class DungeonRoom : MonoBehaviour
    {
        [SerializeField] public Transform gateIn;
        [SerializeField] public List<Transform> gatesOut;
        [SerializeField] public List<Transform> itemSpawnPoints;

        void Start()
        {
        
        }

        void Update()
        {
        
        }
    }
}
