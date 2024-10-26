using System.Collections;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Transform enemyTarget;
    
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
    void SpawnRandomRoom()
    {
        StartCoroutine(SpawnRandomRoomCouroutine());
    }
    
    [ContextMenu("Spawn Boss Room")]
    void SpawnBossRoom()
    {
        StartCoroutine(SpawnBossRoomCoroutine());
    }
    
    void DeleteCurrentRoom()
    {
        if(_currentRoom != null)
        {
            Destroy(_currentRoom.gameObject);
        }
    }

    IEnumerator SpawnBossRoomCoroutine()
    {
        DeleteCurrentRoom();

        yield return null;
        
        _currentRoom = Instantiate( bossRoom );
        
        _currentRoom.BakeNavMesh();

        _currentRoom.SetEnemyTarget(enemyTarget);
    }
    IEnumerator SpawnRandomRoomCouroutine()
    {
        DeleteCurrentRoom();

        yield return null;
        
        _currentRoom = Instantiate( roomTemplates[Random.Range(0, roomTemplates.Length)] );
        
        _currentRoom.SetMaterials(floorMaterial, wallMaterial, ceilingMaterial);
        _currentRoom.InstantiateScenery(sceneryObject);
        _currentRoom.InstantiateLights(lightObject);
        
        _currentRoom.BakeNavMesh();
        
        _currentRoom.InstantiateEnemies(enemyObject);
        _currentRoom.SetEnemyTarget(enemyTarget);
    }
}
