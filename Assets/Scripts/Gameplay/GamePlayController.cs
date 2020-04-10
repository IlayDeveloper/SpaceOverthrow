using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GamePlayController : MonoBehaviour
{
    public enum State
    {
        Play,
        GameOver,
        Pause,
        PlayDelay
    }
    public State state { get; private set; }
    public static GamePlayController Instance { get; private set; }
    public SpaceShip spaceShip;
    public GameObject menu;
    [Space] 
    public int delayToStartSeconds;
    public TextMeshProUGUI startCounter;
    public TextMeshProUGUI timer;
    private float counterTextSize;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        counterTextSize = startCounter.fontSize;
        menu.SetActive(false);
        spaceShip.ShipDestroyedEvent += GameOver;
        
        Play();
    }

    public void Play()
    {
        state = State.PlayDelay;
        startCounter.gameObject.SetActive(true);
        StartCoroutine(DelayToStart());
    }

    public void Pause()
    {
        
    }

    public void GameOver()
    {
        menu.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void CheckTime()
    {
        
    }

    private float time;
    private IEnumerator Timer()
    {
        yield return null;
    }

    private IEnumerator DelayToStart()
    {
        float timer = delayToStartSeconds;
        float rate = 0;
        startCounter.SetText(timer.ToString());
        float timeUpdate = Time.time;
        
        while (timer > 0)
        {
            startCounter.fontSize = counterTextSize * (1 - (Time.time - timeUpdate));
            if ((Time.time - timeUpdate) >= 1)
            {
                timer -= 1;
                timeUpdate = Time.time;
                startCounter.SetText(timer.ToString());
            }
            
            yield return null;
        }
        
        state = State.Play;
    }
}
