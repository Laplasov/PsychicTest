using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SceneManagerSinglton;

public class FirstTileField : MonoBehaviour
{
    int _count;
    Transform childTransform;
    public Action BlockWalls;
    private void Awake()
    {
        _count = 1;
        SceneManagerSinglton.onTileLoaded += OnTileLoaded;
    }
    private void OnTileLoaded()
    {
        if (_count == 1) BlockWalls?.Invoke();
        if (_count == 0)
        {
                Destroy(gameObject);
        }
        --_count;
    }
    private void OnDestroy()
    {
        SceneManagerSinglton.onTileLoaded -= OnTileLoaded;
    }
}
