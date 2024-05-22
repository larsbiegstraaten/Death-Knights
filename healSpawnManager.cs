using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healSpawnManager : MonoBehaviour
{
    public GameObject[] healSpawnPoints;
    public GameObject healPotionPrefab; 

    void Start()
    {
        SpawnHealPotions();
    }

    void SpawnHealPotions()
    {
        foreach (GameObject spawnPoint in healSpawnPoints)
        {
            Instantiate(healPotionPrefab, spawnPoint.transform.position, Quaternion.identity, spawnPoint.transform);
        }
    }
    public void RespawnHeal(Transform p)
    {
        StartCoroutine(RespawnHealCo(p));
    }
    IEnumerator RespawnHealCo(Transform respawnPoint)
    {
        yield return new WaitForSeconds(45);
        if (respawnPoint != null)
        {
            Instantiate(healPotionPrefab, respawnPoint.position, Quaternion.identity, respawnPoint);
        }
    }
}
