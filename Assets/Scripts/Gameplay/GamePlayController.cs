using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayController : MonoBehaviour
{
    // public static GamePlayController Instance { get; private set; }
    public SpaceShip spaceShip;
    public GameObject menu;
    
    // private void Awake()
    // {
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    private void Start()
    {
        menu.SetActive(false);
        spaceShip.ShipDestroyedEvent += GameOver;
    }

    public void Play()
    {
        
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
}
