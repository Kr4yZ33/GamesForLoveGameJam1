using UnityEngine;

public class PowerUpTest : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject ammoPowerup;
    public GameObject extendPowerup;
    public GameObject multiballPowerup;
    public float powerupDestroyTime = 10;
    Transform spawnLocation;

    void Start()
    {
        spawnLocation = gameObject.transform;    
    }

    public void SpawnAmmoPowerup()
    {
        GameObject clone = Instantiate(ammoPowerup, spawnLocation.position, spawnLocation.rotation);
        Destroy(clone, powerupDestroyTime);
    }
    public void SpawnExtendPowerup()
    {
        GameObject clone = Instantiate(extendPowerup, spawnLocation.position, spawnLocation.rotation);
        Destroy(clone, powerupDestroyTime);
    }
    public void SpawnMultiballPowerup()
    {
        GameObject clone = Instantiate(multiballPowerup, spawnLocation.position, spawnLocation.rotation);
        Destroy(clone, powerupDestroyTime);
    }
}
