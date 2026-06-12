using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.Timeline.DirectorControlPlayable;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseCanvas;

    [SerializeField]
    private bool paused;

    [SerializeField] private GameObject pausePanel;

    [SerializeField]
    private InputActionReference pauseAction;

    private void OnEnable()
    {
        pauseAction.action.Enable();
        pauseAction.action.performed += PausePressed;
    }

    private void Update()
    {
       Debug.Log( pauseAction.action.ReadValue<bool>());
    }


    private void OnDisable()
    {
        pauseAction.action.performed -= PausePressed;
    }

    private void PausePressed(InputAction.CallbackContext context)
    {
        TogglePause();
    }

    public void TogglePause()
    {
        Debug.Log("Pausa");

        paused = !paused;

        pausePanel.SetActive(paused);

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
