// Created by team AdaptEvolve for the 
// GamesForLove Game Jam 1 April 2020
// Primary contributers: Jeremy Pernesz

using UnityEngine;

public class BallSpawn : MonoBehaviour
{
    public GameManager gameManager;
    
    [Header("Spawn Information")]
    public Transform spawnTransform;
    public GameObject ballPrefab;

    public void SpawnBall()
    {
        //gameManager.gameStarted = true;
        BallsManager.Instance.SpawnBall(spawnTransform.position);
    }
}
