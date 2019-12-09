using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum GameState
{
    Starting,
    Won,
    Lost,
    InProgress,
    Finished
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public HumanManager hMan;

    public GameState GameState { get; private set; }
    public int TimeForDisguise = 1;
    float initializationTime;
    public int MaxInnocentDeads = 1;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    // Use this for initialization
    void Start()
    {
        GameState = GameState.Starting;
        initializationTime = Time.timeSinceLevelLoad;
        hMan.Humans.ForEach(h =>
        {
            h.OnDie += OnHumanDies;
            int app = 1;
            app += 2;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState == GameState.Starting) CheckForBeginGame();
        CheckGame();
    }

    void CheckForBeginGame()
    {
        if (Time.timeSinceLevelLoad - initializationTime > TimeForDisguise)
        {
            hMan.SetDisguised(true);
            GameState = GameState.InProgress;
        }
    }


    void CheckGame()
    {
        if (GameState != GameState.Won && GameState != GameState.Lost) return;
        if (GameState == GameState.Won) OnWin();
        if (GameState == GameState.Lost) OnLose();
        hMan.SetDisguised(false);
        GameState = GameState.Finished;
        hMan.GetHumans();
    }

    void OnWin()
    {
        Debug.Log("You won!");
    }

    void OnLose()
    {
        Debug.Log("You lost...");
    }

    void OnHumanDies(Human h)
    {
        if (h.role == Role.Spy) OnSpyDies(h);
        else OnInnocentDies(h);
    }

    void OnSpyDies(Human human)
    {
        var livingSpies = hMan.GetHumans(Role.Spy).Where(h => !h.isDead);
        if (livingSpies.Count() <= 0)
        {
            GameState = GameState.Won;
        }
    }

    void OnInnocentDies(Human human)
    {
        if (human.role != Role.Spy) Debug.Log("Don't kill the innocents!");
        var deadInnocent = hMan.GetHumans(Role.Innocent).Where(h => h.isDead);
        if (deadInnocent.Count() >= MaxInnocentDeads)
        {
            GameState = GameState.Lost;
            return;
        }
    }
}
