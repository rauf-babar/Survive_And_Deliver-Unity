using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Zombie Setup")]
    public GameObject[] zombieModels;   // Assign your zombie prefabs here
    public Transform parentSlot;        // Spawn position

    [Header("Patrol Waypoints")]
    public Transform[] waypoints;       // Assign waypoints in inspector

    private void Start()
    {
        SpawnRandomZombie();
    }

    private void SpawnRandomZombie()
    {
        if (zombieModels.Length == 0 || parentSlot == null)
        {
            Debug.LogWarning("ZombieSpawner: Missing prefab or parentSlot.");
            return;
        }

        // Pick a random zombie model
        int index = Random.Range(0, zombieModels.Length);

        // Spawn the zombie
        GameObject zombieInstance = Instantiate(
            zombieModels[index],
            parentSlot.position,
            parentSlot.rotation
        );

        // Try to find ZombieAI on the spawned zombie
        ZombieAI ai = zombieInstance.GetComponent<ZombieAI>();

        if (ai != null)
        {
            ai.SetWaypoints(waypoints);
        }
        else
        {
            Debug.LogWarning("ZombieSpawner: Spawned zombie has NO ZombieAI component.");
        }
    }
}
