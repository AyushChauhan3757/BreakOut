using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject BallPrefab;
    public GameObject PlayerPrefab;
    public Text scoreText;
    public Text ballsText;
    public Text levelText;
    public Text highscoreText;

    public GameObject panelMenu;
    public GameObject panelPlay;
    public GameObject panelLevelCompleted;
    public GameObject panelGameOver;
    public GameObject panelOptions;
    public GameObject panelGameCompleted;

    public GameObject[] levels;

    public static GameManager Instance { get; private set; }

    public void PlayClicked()
    {
        SwitchState(State.INIT);
    }
    public void QuitClicked()
    {
        Application.Quit();
    }
    public void NextClicked()
    {
        SwitchState(State.LOADLEVEL);
        Cursor.visible = false;
    }
    public void RestartClicked()
    {
        Level--;
        SwitchState(State.LOADLEVEL);
        Cursor.visible = false;
    }
    public void Restart2Clicked()
    {
        Balls++;
        SwitchState(State.LOADLEVEL);
        Cursor.visible = false;
    }
    public void MenuClicked()
    {
        SwitchState(State.MENU);
    }
    public void OptionsClicked()
    {
        panelMenu.SetActive(false);
        panelOptions.SetActive(true);
    }
    public void BackClicked()
    {
        panelOptions.SetActive(false);
        panelMenu.SetActive(true);
    }


    private int _score;
    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            scoreText.text = "SCORE : " + _score;
        }
    }
    private int _balls;
    public int Balls
    {
        get { return _balls; }
        set 
        { 
            _balls = value;
            ballsText.text = "BALLS : " + _balls;
        }
    }
    private int _level;
    public int Level
    {
        get { return _level; }
        set 
        { 
            _level = value;
            levelText.text = "LEVEL : " + _level;
        }
    }

    public enum State { MENU, INIT, PLAY, LEVELCOMPLETED, LOADLEVEL, GAMEOVER, GAMECOMPLETED }
    State _state;
    GameObject _currentball;
    GameObject _currentlevel;
    GameObject _currentplayer;
    bool isSwitchingState;
    
    void Start()
    {
        Instance = this;
        SwitchState(State.MENU);
    }

    public void SwitchState(State newState, float delay = 0)
    {
        StartCoroutine(SwitchDelay(newState, delay));
    }

    IEnumerator SwitchDelay(State newState, float delay)
    {
        isSwitchingState = true;
        yield return new WaitForSeconds(delay);
        EndState();
        _state = newState;
        BeginState(newState);
        isSwitchingState = false;
    }

    void BeginState(State newState)
    {
        switch (newState)
        {
            case State.MENU:
                panelMenu.SetActive(true);
                break;
            case State.INIT:
                panelPlay.SetActive(true);
                Score = 0;
                Level = 0;
                Balls = 1;
                _currentplayer = Instantiate(PlayerPrefab);
                SwitchState(State.LOADLEVEL);
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                Destroy(_currentball);
                Destroy(_currentlevel);
                Level++;
                Cursor.visible = true;
                panelLevelCompleted.SetActive(true);
                break;
            case State.LOADLEVEL:
                if(Level >= levels.Length)
                {
                    SwitchState(State.GAMECOMPLETED);
                }
                else
                {
                    _currentlevel = Instantiate(levels[Level]);
                    SwitchState(State.PLAY);
                }
                break;
            case State.GAMEOVER:
                panelGameOver.SetActive(true);
                break;
            case State.GAMECOMPLETED:
                Cursor.visible = true;
                Destroy(_currentplayer);
                panelGameCompleted.SetActive(true);
                if (Score > PlayerPrefs.GetInt("highscore"))
                {
                    PlayerPrefs.SetInt("highscore", Score);
                }
                break;
        }
    }

    void Update()
    {
        switch (_state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:
                if(_currentball == null)
                {
                    if(Balls > 0)
                    {
                        _currentball = Instantiate(BallPrefab);
                    }
                    else 
                    {
                        SwitchState(State.GAMEOVER);
                    }
                }  
                if(_currentlevel != null &&_currentlevel.transform.childCount == 0 && !isSwitchingState) 
                {
                    SwitchState(State.LEVELCOMPLETED);
                }
                break;
            case State.LEVELCOMPLETED:
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                break;
            case State.GAMECOMPLETED:
                break;
        }
    }

    void EndState()
    {
        switch (_state)
        {
            case State.MENU:
                panelMenu.SetActive(false);
                break;
            case State.INIT:
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                panelLevelCompleted.SetActive(false);
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                panelPlay.SetActive(false);
                panelGameOver.SetActive(false);
                break;
            case State.GAMECOMPLETED:
                panelPlay.SetActive(false);
                panelGameCompleted.SetActive(false);
                break;
        }
    }
}