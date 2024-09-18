using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Fading;

public class TileField : MonoBehaviour
{
    int _count;
    private void Awake()
    {
        _count = 4;
        Fading.onTileLoaded += OnTileLoaded;
        
            string Wall = Fading.Instans.WallSide;
            Transform childTransform = gameObject.transform.Find(Wall);
            if (childTransform != null)
            {
                Destroy(childTransform.gameObject);
            }
        
    }
    private void OnTileLoaded()
    {
        if (_count == 0)
        {
            SceneManager.UnloadSceneAsync(gameObject.scene); 
        }
        --_count;
    }
    private void OnDestroy()
    {
        Fading.onTileLoaded -= OnTileLoaded;
    }
}
