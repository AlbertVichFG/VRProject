using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject bttnPlay, bttnExit;

    public void PlayBttn()
    {
        StartCoroutine(LoadSceneAsync());
        bttnPlay.SetActive(false);
        bttnExit.SetActive(false);
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
}
