using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SceneManagerSinglton;
using System;

public class TileField : MonoBehaviour
{
    int _count;
    Transform childTransform;
    public Action BlockWalls;
    private void Awake()
    {
        _count = 2;
        SceneManagerSinglton.onTileLoaded += OnTileLoaded;
        
        string Wall = SceneManagerSinglton.Instans.WallSide;
        childTransform = gameObject.transform.Find(Wall);
        if (childTransform != null)
        {
            childTransform.gameObject.SetActive(false);
        }
        
    }
    private void OnTileLoaded()
    {
        if (_count == 1) 
        { 
            childTransform.gameObject.SetActive(true);
            childTransform.GetComponent<Collider>().enabled = true;
            BlockWalls?.Invoke();
        }

        if (_count == 0)
        {
            //SceneManager.UnloadSceneAsync(gameObject.scene); 
            Destroy(gameObject);
        }
        --_count;
    }
    private void OnDestroy()
    {
        SceneManagerSinglton.onTileLoaded -= OnTileLoaded;
    }
}
