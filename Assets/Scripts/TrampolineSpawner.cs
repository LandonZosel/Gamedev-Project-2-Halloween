using NUnit.Framework;
using UnityEngine;

public class TrampolineSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform pumpkin;
    [SerializeField]
    private GameObject trampoline;
    private float nextSpawnThreshold = 100f;
    private float spawnInterval = 10f;
    private float lastSpawnZ;
    private int trampolineNumber = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastSpawnZ = pumpkin.position.z;
    }

    // Update is called once per frame
    // void Update()
    // {

    // }

    void Update()
    {
        // if (pumpkin.position.z > lastSpawnZ + spawnInterval)
        // {
        //     Vector3 spawnPosition = new Vector3(pumpkin.position.x + Random.Range(-50f, 50f), pumpkin.position.y + Random.Range(-50f, 50f), pumpkin.position.z + Random.Range(30f, 100f));
        //     if (spawnPosition.y <= 0)
        //     {
        //         spawnPosition.y = 1;
        //     }
            
        //     trampolineNumber++;
        //     Debug.Log("Spawned trampoline #" + trampolineNumber);
        //     Instantiate(trampoline, spawnPosition, Quaternion.identity);

        //     lastSpawnZ = pumpkin.position.z;
        // }
    }
}
