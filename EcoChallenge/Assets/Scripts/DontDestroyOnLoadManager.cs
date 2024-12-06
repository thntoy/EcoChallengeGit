using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadManager : MonoBehaviour
{
    public List<GameObject> DontDestroyOnLoadObjects;

    public static DontDestroyOnLoadManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoadObjects = new List<GameObject>();
            DontDestroyOnLoadObjects.Add(gameObject);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DestroyAllDontDestroyOnLoadObjects()
    {
        foreach (GameObject obj in DontDestroyOnLoadObjects)
        {
            Destroy(obj);
        }
    }

}
