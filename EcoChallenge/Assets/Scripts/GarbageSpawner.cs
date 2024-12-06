using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageSpawner : MonoBehaviour
{
    public static GarbageSpawner Instance;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] garbagePrefabs;
    
    public List<GameObject> GarbageList;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        GarbageList = new List<GameObject>();
    }

    private void Start()
    {
        SpawnGarbages();    
    }

    public void SpawnGarbages()
    {
        // Shuffle garbagePrefabs array
        List<GameObject> garbageList = new List<GameObject>(garbagePrefabs);
        ShuffleList(garbageList);

        // Spawn garbages at each spawn point without repeating
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int garbageIndex = i % garbageList.Count;  // Loop back to start if more spawn points than garbage types
            GameObject garbage = Instantiate(garbageList[garbageIndex], spawnPoints[i].position, Quaternion.identity);
            garbage.GetComponent<SpriteRenderer>().sortingOrder = 5;
            GarbageList.Add(garbage);
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public bool IsNoGarbageLeft()
    {
        return GarbageList.Count == 0;
    }

    public void DestroyGarbage(GameObject garbage)
    {
        GarbageList.Remove(garbage);
        Destroy(garbage);
    }
}
