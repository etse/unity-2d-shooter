using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour
{
    PlayerControls inputControls;

    List<string> levels = new List<string> { "Level1Scene", "Level2Scene" };

    private void Awake()
    {
        inputControls = new PlayerControls();
        if (!inputControls.UI.enabled)
        {
            inputControls.UI.Enable();
        }

        inputControls.UI.Navigate.performed += HandleNavigate;
    }

    private void OnDestroy()
    {
        inputControls.UI.Navigate.performed -= HandleNavigate;
        inputControls.UI.Disable();
    }

    public void startNextLevel()
    {
        var levelNumber = PlayerState.Instance.currentLevel;
        var levelToPlay = levels[levelNumber % levels.Count];
        SceneManager.LoadScene(levelToPlay);
    }

    public void quit()
    {
        Application.Quit();
    }

    private void HandleNavigate(InputAction.CallbackContext context)
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
        }
    }
}
