using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace PCG
{
    public class DungeonGenerator : MonoBehaviour
    {
        [SerializeField] private List<DungeonRoom> dungeonNodeRoomTypes;
        [SerializeField] private List<DungeonRoom> dungeonEndRoomTypes;
        [SerializeField] private DungeonRoom startRoom;
        [SerializeField] private InputActionReference generateDungeonAction;

        private int generatedRooms = 0;
        //private bool isDungeonGenerated = false;

        void Start()
        {
            GenerateDungeon(startRoom);
        }

        void GenerateDungeon(DungeonRoom room)
        {
            if (generatedRooms < 4)
            {
                foreach (var gateOut in room.gatesOut)
                {
                    var position = gateOut.transform.position;
                    var newRoom = GameObject.Instantiate<DungeonRoom>(dungeonNodeRoomTypes[Random.Range(0, dungeonNodeRoomTypes.Count)],
                        new Vector3(position.x, 0, position.z), Quaternion.Euler(0,gateOut.rotation.eulerAngles.y, 0));
                    generatedRooms++;
                    GenerateDungeon(newRoom);
                }
            }
            else
            {
                foreach (var gateOut in room.gatesOut)
                {
                    var position = gateOut.transform.position;
                    var newRoom = GameObject.Instantiate<DungeonRoom>(dungeonEndRoomTypes[Random.Range(0, dungeonEndRoomTypes.Count)],
                        new Vector3(position.x, 0, position.z), Quaternion.Euler(0,gateOut.rotation.eulerAngles.y, 0));
                    generatedRooms++;
                }
            }
            return;
        }
    }
}
