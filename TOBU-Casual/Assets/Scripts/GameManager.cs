using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject gameObject = new GameObject("GameManager");
                gameObject.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    [SerializeField] private GameObject _pauseMenuPanel;
    public bool IsDead { get; set; } = false;
    private int _ringCounter = 0;
    private int _maxRings = 5;
    private float _defaultTimeScale = 1;
    public bool IsPaused { get; set; } = false;
    private bool _pauseMenuIsActive = false;

    public int RingCounter
    {
        get { return _ringCounter; }
        set
        {
            _ringCounter = value;

            if (_ringCounter < 0)
            {
                _ringCounter = 0;
            }
            else if (_ringCounter >= _maxRings)
            {
                _ringCounter = _maxRings;
            }
        }
    }

    public void PauseGame()
    {
        if(IsPaused == false)
        {
            IsPaused = true;
            Time.timeScale = 0;
            TogglePauseMenu();
        }
        else if (IsPaused == true)
        {
            IsPaused = false;
            Time.timeScale = _defaultTimeScale;
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        if (_pauseMenuIsActive)
        {
            _pauseMenuIsActive = !_pauseMenuIsActive;
            _pauseMenuPanel.SetActive(false);
        }
        else if (!_pauseMenuIsActive)
        {
            _pauseMenuIsActive = !_pauseMenuIsActive;
            _pauseMenuPanel.SetActive(true);
        }
    }

    private void Awake()
    {
        _instance = this;
    }
}
