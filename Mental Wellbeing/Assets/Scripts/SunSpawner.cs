using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSpawner : MonoBehaviour
{
    //
    // Spawns suns pickups that drift across the screen.
    //

    [Header("Spawner Settings")]
    [Tooltip("Prefab of the sun collectible to spawn.")]
    public GameObject sunCollectible;
    [Tooltip("Suns will be spawned with a random offset between the x and y (in units).")]
    public Vector2 randomHeightOffsetRange = new Vector2(-4f, 4f);
    [Tooltip("Speed suns will move across the screen chosen randomly between x and y (in units per second).")]
    public Vector2 randomHorizontalVelocity = new Vector2(0.5f, 1.2f);
    [Tooltip("How long in seconds between each spawn picked randomly between x and y.")]
    public Vector2 randomTimeBetweenSpawns = new Vector2(3f, 10f);

    private float timer = 0f;

    private float sideSpawn;

    void Start()
    {
        //ResetTimer();

        Camera camera = Camera.main;
        sideSpawn = (camera.aspect * camera.orthographicSize) + 1f;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ResetTimer();
            float spawnSide = Random.Range(0, 2) == 0 ? -1f : 1f;
            float spawnX = spawnSide * sideSpawn;
            float spawnY = transform.position.y + Random.Range(randomHeightOffsetRange.x, randomHeightOffsetRange.y);
            Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);
            GameObject sun = Instantiate(sunCollectible, spawnPos, Quaternion.identity);
            SunCollectible collectible = sun.GetComponent<SunCollectible>();
            collectible.velocity.x = Random.Range(randomHorizontalVelocity.x, randomHorizontalVelocity.y) * -spawnSide;
        }
    }

    public void ResetTimer()
    {
        timer = Random.Range(randomTimeBetweenSpawns.x, randomTimeBetweenSpawns.y);
    }
}
