using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneButtonProxy : MonoBehaviour
{
    public void OnButtonClick()
    {
        if (Fading.Instans != null)
        {
            Fading.Instans.LoadNextLevel();
        }
        else
        {
            Debug.LogError("Fading instance is null!");
        }
    }
}