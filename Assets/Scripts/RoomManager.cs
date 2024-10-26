using System.Collections;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
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

    [ContextMenu("Spawn Room")]
    public void SpawnRandomRoom()
    {
        StartCoroutine(SpawnRandomRoomCouroutine());
    }
    
    [ContextMenu("Spawn Boss Room")]
    public void SpawnBossRoom()
    {
        StartCoroutine(SpawnBossRoomCoroutine());
    }
    
    public void DeleteCurrentRoom()
    {
        if(_currentRoom != null)
        {
            Destroy(_currentRoom.gameObject);
        }
    }

    void SpawnRoom(Room room)
    {
        _currentRoom = Instantiate( room, roomParent );
        
        _currentRoom.BakeNavMesh();

        _currentRoom.SetEnemyTarget(enemyTarget);
    }
    
    IEnumerator SpawnBossRoomCoroutine()
    {
        DeleteCurrentRoom();

        yield return null;
        
        SpawnRoom(bossRoom);
    }
    IEnumerator SpawnRandomRoomCouroutine()
    {
        DeleteCurrentRoom();

        yield return null;
        
        _currentRoom = Instantiate( roomTemplates[Random.Range(0, roomTemplates.Length)], roomParent );
        
        _currentRoom.SetMaterials(floorMaterial, wallMaterial, ceilingMaterial);
        _currentRoom.InstantiateScenery(sceneryObject);
        _currentRoom.InstantiateLights(lightObject);
        
        _currentRoom.BakeNavMesh();
        
        _currentRoom.InstantiateEnemies(enemyObject);
        _currentRoom.SetEnemyTarget(enemyTarget);
    }
}
