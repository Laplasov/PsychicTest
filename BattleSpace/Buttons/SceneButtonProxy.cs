using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneButtonProxy : MonoBehaviour
{
    public static void OnButtonClick()
    {
        if (SceneManagerSinglton.Instans != null)
        {
            SceneManagerSinglton.Instans.LoadNextLevel();
        }
        else
        {
            Debug.LogError("Fading instance is null!");
        }
    }
}