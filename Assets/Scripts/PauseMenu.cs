using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseCanvas;

    [SerializeField]
    private bool paused;

    public void TogglePause()
    {
        paused = !pauseCanvas.activeSelf;

        pauseCanvas.SetActive(paused);

        Debug.Log("PAUSA");

        if (paused)
        {
            Transform cam = Camera.main.transform;

            pauseCanvas.transform.position = cam.position + cam.forward * 1.5f;

            pauseCanvas.transform.rotation =
                Quaternion.LookRotation(pauseCanvas.transform.position - cam.position);
        }

        Time.timeScale = paused ? 0f : 1f;
    }

    public void Resume()
    {
        paused = false;

        pauseCanvas.SetActive(false);

        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   
    }
}
