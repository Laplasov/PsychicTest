using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Fading;

public class FirstTileField : MonoBehaviour
{
    int _count;
    private void Awake()
    {
        _count = 4;
        Fading.onTileLoaded += OnTileLoaded;
    }
    private void OnTileLoaded()
    {
        if (_count == 0)
        {
                Destroy(gameObject);
        }
        --_count;
    }
    private void OnDestroy()
    {
        Fading.onTileLoaded -= OnTileLoaded;
    }
}
