using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public enum RoomType
{
    Normal,
    Shop,
    Bonus,
    Boss
}

[Serializable]
public class RoomLayer
{
    public List<RoomType> roomTypes;
}
public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public List<RoomLayer> roomLayers;
    
    public Transform enemyTarget;
    public Transform roomParent;
    
    public Material floorMaterial;
    public Material wallMaterial;
    public Material ceilingMaterial;

    public GameObject sceneryObject;
    public GameObject lightObject;
    public GameObject enemyObject;

    [Header("Rooms")]
    public Room bossRoom;
    public Room[] roomTemplates;

    private Room _currentRoom;

    public void SpawnRoomByType(RoomType type)
    {
        switch (type)
        {
            case RoomType.Normal:
                SpawnRandomRoom();
                break;
            case RoomType.Shop:
                break;
            case RoomType.Bonus:
                break;
            case RoomType.Boss:
                SpawnBossRoom();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public Transform GetPlayerSpawnPoint()
    {
        return _currentRoom.GetPlayerSpawnPoint();
    }
    
    [ContextMenu("Spawn Room")]
    public void SpawnRandomRoom()
    {
        DeleteCurrentRoom();
        _currentRoom = Instantiate( roomTemplates[Random.Range(0, roomTemplates.Length)], roomParent );
        
        StartCoroutine(SpawnRandomRoomCouroutine());
    }
    
    [ContextMenu("Spawn Boss Room")]
    public void SpawnBossRoom()
    {
        DeleteCurrentRoom();
        _currentRoom = Instantiate( bossRoom, roomParent );

        StartCoroutine(SpawnBossRoomCoroutine());
    }
    
    public void DeleteCurrentRoom()
    {
        if(_currentRoom != null)
        {
            Destroy(_currentRoom.gameObject);
        }
    }
    
    IEnumerator SpawnBossRoomCoroutine()
    {
        yield return null;
        
        _currentRoom.BakeNavMesh();
        
        _currentRoom.InstantiateEnemies(enemyObject);
        _currentRoom.SetEnemyTarget(enemyTarget);
    }
    IEnumerator SpawnRandomRoomCouroutine()
    {
        yield return null;
        
        _currentRoom.SetMaterials(floorMaterial, wallMaterial, ceilingMaterial);
        _currentRoom.InstantiateScenery(sceneryObject);
        _currentRoom.InstantiateLights(lightObject);
        
        _currentRoom.BakeNavMesh();
        
        _currentRoom.InstantiateEnemies(enemyObject);
        _currentRoom.SetEnemyTarget(enemyTarget);
    }
}
