using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemySlimePrefab;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ;

    public int Count;

    private void Start()
    {
        Count++;
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        Vector3 slimeSpawnPos = new Vector3(-15.3594f, 0.2645f, -16.604f);
        if (Count == 1)
        {
            Debug.Log("Instantiated Slime Enemy!");
            PhotonNetwork.Instantiate(enemySlimePrefab.name, slimeSpawnPos, Quaternion.identity);
        }
    }

    
}
