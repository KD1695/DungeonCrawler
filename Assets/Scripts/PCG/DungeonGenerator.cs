using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace PCG
{
    public class DungeonGenerator : MonoBehaviour
    {
        [Header("RoomTypes")]
        [SerializeField] private List<DungeonRoom> dungeonNodeSingleOutRoomTypes;
        [SerializeField] private List<DungeonRoom> dungeonNodeMultipleOutRoomTypes;
        [SerializeField] private List<DungeonRoom> dungeonEndRoomTypes;
        [SerializeField] private DungeonRoom startRoom;

        [Header("Items")] 
        [SerializeField] private List<GameObject> roomItems;

        private int generatedRooms = 0;
        private int roomLimit = 4;
        private DungeonGenerator instance;

        void Awake()
        {
            DontDestroyOnLoad(this);
            if (instance == null)
                instance = this;
            else
                DestroyImmediate(this.gameObject);
        }

        public void InitiateDungeonGeneration(int _roomLimit)
        {
            roomLimit = _roomLimit;
            generatedRooms = 0;
            GenerateDungeon(startRoom);
        }

        void GenerateDungeon(DungeonRoom room)
        {
            if(room.IsDestroyed())
                return;
            if (generatedRooms < roomLimit)
            {
                if (room.gatesOut.Count > 1)
                {
                    foreach (var gateOut in room.gatesOut)
                    {
                        GenerateDungeon(CreateDungeonRoom(dungeonNodeSingleOutRoomTypes, gateOut));
                    }

                    foreach (var gateOut in room.gatesOut)
                    {
                        gateOut.gameObject.SetActive(false);
                    }
                }
                else if(room.gatesOut.Count == 1)
                {
                    GenerateDungeon(CreateDungeonRoom(dungeonNodeMultipleOutRoomTypes, room.gatesOut.First()));
                    room.gatesOut.First().gameObject.SetActive(false);
                }
            }
            else
            {
                foreach (var gateOut in room.gatesOut)
                {
                    CreateDungeonRoom(dungeonEndRoomTypes, gateOut);
                }
                foreach (var gateOut in room.gatesOut)
                {
                    gateOut.gameObject.SetActive(false);
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
            if (newRoom.itemSpawnPoints.Count > 0)
            {
                //spawn room items
                foreach (var itemTransform in newRoom.itemSpawnPoints)
                {
                    GameObject.Instantiate(roomItems[Random.Range(0, roomItems.Count)], itemTransform);
                }
            }

            return newRoom;
        }
    }
}
