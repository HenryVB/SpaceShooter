using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    //States of Game
    public enum GameManagerState
    {
        Start,
        GamePlay,
        GameOver
    }

    private GameManagerState gmState;
    [SerializeField]
    private GameObject btnPlay;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject enemySpawner;
    [SerializeField]
    private GameObject gameOverSprite;
    [SerializeField]
    private GameObject txtScore;
    [SerializeField]
    private GameObject txtTimer;
    [SerializeField]
    private GameObject txtTitle;

    private const float restartAfterSeconds = 8f;

    // Start is called before the first frame update
    void Start()
    {
        gmState = GameManagerState.Start;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void UpdateGMState()
    {
        switch (gmState)
        {
            case GameManagerState.Start:
                gameOverSprite.SetActive(false);
                btnPlay.SetActive(true);
                txtTitle.SetActive(true);
                break;
            case GameManagerState.GamePlay:
                //Score to 0, Get out button and init player for gameplay and init spawn enemies and start timer
                txtTitle.SetActive(false);
                txtScore.GetComponent<GameScore>().Score = 0;
                btnPlay.SetActive(false);
                player.GetComponent<PlayerControl>().Init();
                enemySpawner.GetComponent<EnemySpawn>().ScheduleEnemySpawn();
                txtTimer.GetComponent<TimeCounter>().StartTimeCounter();
                break;
            case GameManagerState.GameOver:
                //Stop enemy spawn and restart game after seconds and stop timer
                txtTimer.GetComponent<TimeCounter>().StopTimeCounter();
                enemySpawner.GetComponent<EnemySpawn>().UnscheduleEnemySpawn();
                gameOverSprite.SetActive(true);
                Invoke("RestartGame", restartAfterSeconds);
                break;
        }
    }

    public void SetGMState(GameManagerState state)
    {
        gmState=state;
        UpdateGMState();
    }

    public void StartGamePlay()
    {
        gmState = GameManagerState.GamePlay;
        UpdateGMState();
    }

    public void RestartGame()
    {
        SetGMState(GameManagerState.Start);
    }
}
