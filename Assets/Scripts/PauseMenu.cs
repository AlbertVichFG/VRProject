using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseCanvas;

    [SerializeField]
    private InputActionReference pauseAction;

    private bool paused;

    private void Update()
    {
        if (pauseAction.action.triggered)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        Debug.Log("Pause Toggled");)
        paused = !paused;

        pauseCanvas.SetActive(paused);

        Time.timeScale = paused ? 0f : 1f;

        Transform cam = Camera.main.transform;

        pauseCanvas.transform.position = cam.position + cam.forward * 1.5f;

        pauseCanvas.transform.rotation = Quaternion.LookRotation(pauseCanvas.transform.position - cam.position);
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
}
