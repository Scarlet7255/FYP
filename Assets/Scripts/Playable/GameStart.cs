using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class GameStart : MonoBehaviour
{
    public PlayableDirector director;
    public UnityAction GameStartAction;
    private void Awake()
    {
        PlayerPrefs.SetInt("scene", SceneManager.GetActiveScene().buildIndex);
        if (!MainMenu.Instance.gameStart)
        {
            GameStartAction = () =>
            {
                director.Play();
                MainMenu.Instance.GameStartEvent.RemoveListener(GameStartAction);
            };
            MainMenu.Instance.GameStartEvent.AddListener(GameStartAction);
        }

    }

    private void Start()
    {
        if(MainMenu.Instance.gameStart) director.Play();
    }
}
