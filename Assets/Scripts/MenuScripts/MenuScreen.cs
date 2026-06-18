using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuScreen : MonoBehaviour
{
    void Update()
    {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            QuitGame();
    }

     public void PlayGame() 
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
