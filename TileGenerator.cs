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
    }
    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Collider>().enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!_isTriggered)
        {
            _isTriggered = true;
            gameObject.transform.GetComponent<Collider>().enabled = false;
            Destroy(gameObject);
            Fading.Instans.LoadNextTile(Tile, gameObject.transform.name);
        }
        else { return; }
    }
}
