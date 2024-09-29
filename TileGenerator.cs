using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject Tile;
    Vector3 _tileCoordinats;
    bool _isTriggered;
    private void Awake()
    {
        _tileCoordinats = Tile.transform.position;
        _isTriggered = false;
        gameObject.GetComponent<Collider>().enabled = false;
        StartCoroutine(Waiting());

        if (Tile.TryGetComponent(out TileField tileField))
        {
            tileField.BlockWalls += () => BlockWall();
        } 
        else if (Tile.TryGetComponent(out FirstTileField FirstTileField))
        {
            FirstTileField.BlockWalls += () => BlockWall();
        }
    }
    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Collider>().enabled = true;
    }
    void BlockWall() => gameObject.GetComponent<Collider>().isTrigger = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!_isTriggered)
        {
            _isTriggered = true;
            //gameObject.transform.GetComponent<Collider>().enabled = false;
            SceneManagerSinglton.Instans.LoadNextTile(Tile, gameObject.transform.name);
            gameObject.SetActive(false);
        }
        else { return; }
    }
}
