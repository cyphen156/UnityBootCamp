using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;
    public int spawnCount = 5;

    private List<GameObject> spawnedZombies = new List<GameObject>();

    private void Start()
    {
        if (DaySystem.Instance != null)
        {
            DaySystem.Instance.OnDayNightChanged += OnDayNightChanged;
        }
    }

    private void OnDestroy()
    {
        if (DaySystem.Instance != null)
        {
            DaySystem.Instance.OnDayNightChanged -= OnDayNightChanged;
        }
    }

    private void OnDayNightChanged(bool isDay, float weight)
    {
        if (!isDay)
        {
            SpawnZombies();
        }
    }

    private void SpawnZombies()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
            spawnedZombies.Add(zombie);
        }
    }
}
