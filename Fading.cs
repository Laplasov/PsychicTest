using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fading : MonoBehaviour
{
    public Animator transition;
    public static Fading Instans;
    private AsyncOperation asyncLoad;
    private bool _isLoading = false;

    private void Awake()
    {
        if (Instans != null)
        {
            Destroy(gameObject);
        } else
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

        Scene scene1 = SceneManager.GetSceneByName("Scene1");
        foreach (GameObject go in scene1.GetRootGameObjects())
        {
            go.SetActive(false);
        }

        asyncLoad = SceneManager.LoadSceneAsync(Scene, LoadSceneMode.Additive);

        while (!asyncLoad.isDone) yield return null;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(Scene));

        //SceneManager.LoadScene(Scene);
        transition.SetTrigger("End");
        _isLoading = false;
    }
    IEnumerator WorldSceneLoader(string Scene)
    {
        transition.SetTrigger("Start");
        SceneManager.UnloadSceneAsync("Scene2");
        yield return new WaitForSeconds(1f);

        while (!asyncLoad.isDone) yield return null;

        Scene scene1 = SceneManager.GetSceneByName("Scene1");
        foreach (GameObject go in scene1.GetRootGameObjects())
        {
            go.SetActive(true);
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(Scene));

        //SceneManager.LoadScene(Scene);
        transition.SetTrigger("End");
        _isLoading = false;
    }
}
