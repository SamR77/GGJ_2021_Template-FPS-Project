using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject GamePlayUI;
    public GameObject PauseMenuUI;
    public GameObject GameOverUI;
    public GameObject CreditsUI;

    public GameObject startPosition;

    public enum GameState { MainMenu, Gameplay, Paused, GameOver, Credits }

    public GameObject player;


    //Script references
    public GameObject levelManager;
    public GameObject uIManager;

    private LevelManager _levelManager;
    //private UIManager _uIManager;


    private GameState gameState;
    private GameState LastGameState;


    // Start is called before the first frame update
    void Start()
    {
        _levelManager = levelManager.GetComponent<LevelManager>();
        // _uIManager = uIManager.GetComponent<UIManager>();

        player.GetComponent<Player_Look>().enabled = false;
        Cursor.visible = true;
        gameState = GameState.MainMenu;
        startPosition = GameObject.FindWithTag("StartPos");
    }

    // Update is called once per frame
    void Update()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        switch (gameState)
        {

            case GameState.MainMenu:                
                MainMenuUI.SetActive(true); 
                GamePlayUI.SetActive(false);
                PauseMenuUI.SetActive(false);
                GameOverUI.SetActive(false);
                CreditsUI.SetActive(false);

                player.GetComponent<Player_Look>().enabled = false;
                Cursor.visible = true;

                break;

            case GameState.Gameplay:
                MainMenuUI.SetActive(false);
                GamePlayUI.SetActive(true);
                PauseMenuUI.SetActive(false);
                GameOverUI.SetActive(false);
                CreditsUI.SetActive(false);


                player.GetComponent<Player_Look>().enabled = true;
                Cursor.visible = false;

                if (Input.GetKeyDown(KeyCode.Escape))
                {                    
                    gameState = GameState.Paused;
                }


                if (Input.GetKeyDown(KeyCode.P))
                {
                    ResetPlayerPos();
                }


                break;

            case GameState.Paused:

                Time.timeScale = 0f;

                MainMenuUI.SetActive(false);
                GamePlayUI.SetActive(false);
                PauseMenuUI.SetActive(true);
                GameOverUI.SetActive(false);
                CreditsUI.SetActive(false);

                player.GetComponent<Player_Look>().enabled = false;
                Cursor.visible = true;

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    UnPause();
                }

                break;



            case GameState.GameOver:
                MainMenuUI.SetActive(false);
                GamePlayUI.SetActive(false);
                PauseMenuUI.SetActive(false);
                GameOverUI.SetActive(true);
                CreditsUI.SetActive(false);

                player.GetComponent<Player_Look>().enabled = false;
                Cursor.visible = true;

                break;


            case GameState.Credits:
                MainMenuUI.SetActive(false);
                GamePlayUI.SetActive(false);
                PauseMenuUI.SetActive(false);
                GameOverUI.SetActive(false);
                CreditsUI.SetActive(true);

                player.GetComponent<Player_Look>().enabled = false;
                Cursor.visible = true;

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.MainMenu;
                }


                break;



        }

          
    }

    public void StartGame()
    {
        _levelManager.LoadNextlevel();
        gameState = GameState.Gameplay;
        ResetPlayerPos();
    }

    

    public void returnToMainMenu()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _levelManager.LoadMainMenu();
        gameState = GameState.MainMenu;
        //       Camera.main.transform.SetPositionAndRotation(new Vector3(-0.7f, 10f, 11f), Quaternion.Euler(new Vector3(45, -180, 0)));
    }

    public void UnPause()
    {        
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameState = GameState.Gameplay;
    }

    public void Credits()
    {
        gameState = GameState.Credits;
    }


    public void loadNextLevel()
    {
        _levelManager.LoadNextlevel();      
        
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        startPosition = GameObject.FindWithTag("StartPos");
        int LevelCount = SceneManager.GetActiveScene().buildIndex;       
    }


    public void ResetPlayerPos()
    {

        player.transform.position = startPosition.transform.position;
    }

}
