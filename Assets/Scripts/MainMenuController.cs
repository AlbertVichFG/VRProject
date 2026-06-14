using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject bttnPlay, bttnExit, bttnTest;

    public void TestBttn()
    {
        StartCoroutine(LoadSceneAsync());
        bttnPlay.SetActive(false);
        bttnExit.SetActive(false);
        bttnTest.SetActive(false);
    }

    public void PlayBttn()
    {
        StartCoroutine(LoadSceneAsyncGame());
        bttnPlay.SetActive(false);
        bttnExit.SetActive(false);
        bttnTest.SetActive(false);
    }

    public void ExitBttn()
    {
            Application.Quit();
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game");
        //asyncLoad.allowSceneActivation = false; // Evita que la escena se active automaticamente
        while (!asyncLoad.isDone)
        {
            //    asyncLoad.progress  barra carga 
            yield return null;
        }
        //asyncLoad.allowSceneActivation = true; // Activa la escena una vez que esté completamente cargada
    }

    IEnumerator LoadSceneAsyncGame()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameFinal");
        //asyncLoad.allowSceneActivation = false; // Evita que la escena se active automaticamente
        while (!asyncLoad.isDone)
        {
            //    asyncLoad.progress  barra carga 
            yield return null;
        }
        //asyncLoad.allowSceneActivation = true; // Activa la escena una vez que esté completamente cargada
    }
}
