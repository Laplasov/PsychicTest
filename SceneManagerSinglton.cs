using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerSinglton : MonoBehaviour
{
    public Animator transition;
    public static SceneManagerSinglton Instans;
    private AsyncOperation asyncLoad;
    private bool _isLoading = false;
    public GameObject RootTile;
    public string WallSide;
    public string[] sceneNames;
    private int currentSceneIndex;
    Scene _scene1;

    public delegate void OnTileLoaded();
    public static event OnTileLoaded onTileLoaded;

    private void Awake()
    {
        _scene1 = SceneManager.GetSceneByName("Scene1");
        sceneNames = new string[] { "Map1", "Map2", "Map3", "Map4", "Map5", "Map6", "Map7", "Map8", "Map9" };
        currentSceneIndex = 0;
        if (Instans != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instans = this;
            DontDestroyOnLoad(Instans);
        }
    }
    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().name == "Scene1" && !_isLoading)
        {
            StartCoroutine(BattleSceneLoader("Scene2"));
            _isLoading = true;
        }
        if (SceneManager.GetActiveScene().name == "Scene2" && !_isLoading)
        {
            StartCoroutine(WorldSceneLoader("Scene1"));
            _isLoading = true;
        }
    }
    IEnumerator BattleSceneLoader(string Scene)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        foreach (GameObject go in _scene1.GetRootGameObjects())
        {
            go.SetActive(false);
        }
        asyncLoad = SceneManager.LoadSceneAsync(Scene, LoadSceneMode.Additive);
        InitBattleData.BattleScene = true;
        while (!asyncLoad.isDone) yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(Scene));
        transition.SetTrigger("End");
        _isLoading = false;
    }
    IEnumerator WorldSceneLoader(string Scene)
    {
        transition.SetTrigger("Start");
        SceneManager.UnloadSceneAsync("Scene2");
        InitBattleData.BattleScene = false;
        yield return new WaitForSeconds(1f);
        while (!asyncLoad.isDone) yield return null;
        foreach (GameObject go in _scene1.GetRootGameObjects())
        {
            go.SetActive(true);
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(Scene));
        transition.SetTrigger("End");
        _isLoading = false;
    }

    public void LoadNextTile(GameObject currentTile, string wallName)
    {
        StartCoroutine(LoadTile(sceneNames[currentSceneIndex], currentTile, wallName));
        currentSceneIndex = (currentSceneIndex + 1) % sceneNames.Length;
    }

    IEnumerator LoadTile(string tileName, GameObject currentTile, string wallName)
    {
        Vector3 direction = WallSwitch(wallName, currentTile);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(tileName, LoadSceneMode.Additive);
        Scene newScene = SceneManager.GetSceneByName(tileName);
        yield return new WaitForSeconds(0.1f);
        GameObject[] newTile = newScene.GetRootGameObjects();
        RootTile = newTile[0];
        RootTile.transform.position = currentTile.transform.position + direction * 250f;
        onTileLoaded?.Invoke();
        SceneManager.MergeScenes(newScene, _scene1);
    }

    private Vector3 WallSwitch(string wallName, GameObject currentTile) 
    {
        switch (wallName)
        {
            case "North":
                WallSide = "South";
                return currentTile.transform.forward;
            case "South":
                WallSide = "North";
                return -currentTile.transform.forward;
            case "East":
                WallSide = "West";
                return -currentTile.transform.right;
            case "West":
                WallSide = "East";
                return currentTile.transform.right;
            default:
                Debug.LogError("Invalid wallName");
                return Vector3.zero;
        }
    }

}

