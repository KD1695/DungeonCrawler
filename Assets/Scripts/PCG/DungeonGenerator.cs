using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace PCG
{
    public class DungeonGenerator : MonoBehaviour
    {
        [SerializeField] private List<DungeonRoom> dungeonNodeSingleOutRoomTypes;
        [SerializeField] private List<DungeonRoom> dungeonNodeMultipleOutRoomTypes;
        [SerializeField] private List<DungeonRoom> dungeonEndRoomTypes;
        [SerializeField] private DungeonRoom startRoom;

        private int generatedRooms = 0;

        void Start()
        {
            GenerateDungeon(startRoom);
        }

        void GenerateDungeon(DungeonRoom room)
        {
            if (generatedRooms < 4)
            {
                if (room.gatesOut.Count > 1)
                {
                    foreach (var gateOut in room.gatesOut)
                    {
                        GenerateDungeon(CreateDungeonRoom(dungeonNodeSingleOutRoomTypes, gateOut));
                    }
                }
                else if(room.gatesOut.Count == 1)
                {
                    GenerateDungeon(CreateDungeonRoom(dungeonNodeMultipleOutRoomTypes, room.gatesOut.First()));
                }
            }
            else
            {
                foreach (var gateOut in room.gatesOut)
                {
                    CreateDungeonRoom(dungeonEndRoomTypes, gateOut);
                }
            }
            return;
        }

        DungeonRoom CreateDungeonRoom(List<DungeonRoom> sourceList, Transform gateOut)
        {
            var position = gateOut.transform.position;
            var newRoom = GameObject.Instantiate<DungeonRoom>(sourceList[Random.Range(0, sourceList.Count)],
                new Vector3(position.x, 0, position.z), Quaternion.Euler(0,gateOut.rotation.eulerAngles.y, 0));
            generatedRooms++;

            return newRoom;
        }
    }
}
