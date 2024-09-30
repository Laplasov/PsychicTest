using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private Light _light;
    void Awake()
    {
        Transform[] children = GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
        {
            if (child == transform)
                continue;
            //if (child.childCount > 0)
            //{
            //    Transform grandchild = child.GetChild(0);
            //    Destroy(grandchild.gameObject);
            //}
            Instantiate(_prefab, child.position, child.rotation, child);
            Instantiate(_light, child.position, child.rotation, child);
        }

    }

}
