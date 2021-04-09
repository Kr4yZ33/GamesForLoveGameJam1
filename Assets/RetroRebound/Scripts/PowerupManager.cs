using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [Header("Prefabs")]
    public List<GameObject> powerupPrefabs;

    [Header("Data")]
    [Range(0f, 100f)]
    public float powerupSpawnChance;

    [HideInInspector]
    public static PowerupManager Instance;

    private List<Powerup> CurrentPowerUps = new List<Powerup>();

    private void Awake()
    {
        Instance = this;
    }


    /// <summary>
    /// Called by block when it's destroyed
    /// Checks first if it can spawn a powerup with <see cref="powerupSpawnChance"/>
    /// If true, randomly spawn a powerup object from <see cref="powerupPrefabs"/>
    /// </summary>
    /// <param name="block">block destroyed</param>
    public void OnBlockDestroyed(Block block)
    {
        int spawnRoll = Random.Range(1, 101);
        if(spawnRoll <= powerupSpawnChance)
        {
            int powerupSpawnIndex = Random.Range(0, powerupPrefabs.Count);
            SpawnPowerup(powerupPrefabs[powerupSpawnIndex], block.transform.position);
        }
    }

    /// <summary>
    /// Spawns a powerup object in position
    /// </summary>
    /// <param name="powerupPrefab">powerup to spawn</param>
    /// <param name="position">where to spawn the powerup</param>
    private void SpawnPowerup(GameObject powerupPrefab, Vector2 position)
    {
        var powerUp = Instantiate(powerupPrefab, position, Quaternion.identity);
        CurrentPowerUps.Add(powerUp.GetComponent<Powerup>());
        
    }

    /// <summary>
    /// Clear particular powerup from list
    /// </summary>
    /// <param name="powerup"></param>
    public void ClearPowerup(Powerup powerup)
    {
        CurrentPowerUps.Remove(powerup);
    }
    /// <summary>
    /// Clear all powerups in scene for next level or game over
    /// </summary>
    public void ClearAllPowerups()
    {
        foreach (Powerup powerup in CurrentPowerUps)
        {
            Destroy(powerup.gameObject);
        }
            CurrentPowerUps.Clear();
    }
}
