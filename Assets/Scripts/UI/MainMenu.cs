using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance => _instance;
    private static MainMenu _instance;
    public UnityEvent GameStartEvent;
    public Animator mask;

    // Start is called before the first frame update
    public GameObject mainPanel;
    public bool gameStart = false;

    public GameObject continueButton;
    public GameObject currentSecondPanel;
    
    void Awake()
    {
        if(_instance) Destroy(gameObject);
        _instance = this;
        DontDestroyOnLoad(gameObject);
        int lastScene = PlayerPrefs.GetInt("scene", -1);
        if (lastScene > 1) SceneManager.LoadScene(lastScene);
        else
        {
            SceneManager.LoadScene(1);
            continueButton.SetActive(false);
        }

        mask.Play("CG Mask Fade");
    }

    public void NewGame()
    {
        if(SceneManager.GetActiveScene().buildIndex != 1) SceneManager.LoadScene(1);
        ContinueGame();
    }

    public void ContinueGame()
    {
        gameStart = true;
        GameStartEvent.Invoke();
        mainPanel.SetActive(false);
        continueButton.SetActive(false);
    }

    public void OpenSecondPanel(GameObject panel)
    {
       if(currentSecondPanel) currentSecondPanel.SetActive(false);
       currentSecondPanel = panel;
       panel.SetActive(true);
    }

    public void CloseMenu(InputAction.CallbackContext cbc)
    {
        if (cbc.started && gameStart)
        {
            if (currentSecondPanel&&currentSecondPanel.activeSelf)
                currentSecondPanel.SetActive(false);
            else 
                mainPanel.SetActive(!mainPanel.activeSelf);
        }
    }

    public void Exit()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

}
