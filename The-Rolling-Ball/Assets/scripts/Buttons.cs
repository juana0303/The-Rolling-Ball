using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject creditsPanel;
    public GameObject settingsPanel;

    public void PlayGame()
    {
        // Cambia "1" por el número o nombre de tu escena de juego principal
        SceneManager.LoadScene(0); 
    }

    public void OpenCredits(bool open)
    {
        if (creditsPanel != null)
            creditsPanel.SetActive(open);
    }

    public void OpenSettings(bool open)
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(open);
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}