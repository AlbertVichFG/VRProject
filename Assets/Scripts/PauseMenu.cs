using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private Transform playerCamera;

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
     //  Debug.Log( pauseAction.action.ReadValue<bool>());
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
        //Debug.Log("Pausa");

        paused = !paused;

        if (paused)
        {
            ShowPanel(pausePanel);

            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);

            Time.timeScale = 1f;
        }
    }

    public void Resume()
    {
        paused = false;

        pausePanel.SetActive(false);

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


    public void ShowPanel(GameObject panel)
    {
        Vector3 spawnPos =
            playerCamera.position +
            playerCamera.forward * 2f;

        panel.transform.position = spawnPos;

        panel.transform.LookAt(playerCamera);

        panel.transform.Rotate(0, 180, 0);

        panel.SetActive(true);
    }
}
