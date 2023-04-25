using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    #region Singleton

    private static GameManger _instance;

    public static GameManger Instance => _instance;

    #endregion

    public PlayerAgent Player;
    public UnityEvent OnGameRestart;
    public GameObject MainMenu;

    private void Awake()
    {
        _instance = this;
    }

    public void RestartGame()
    {
        OnGameRestart.Invoke();
    }

    public void NextScene()
    {
        int sceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(++sceneIdx);
    }

    /// <summary>
    /// lazy dog action
    /// </summary>
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
