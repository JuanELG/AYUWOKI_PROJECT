using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class uiManager : MonoBehaviour
{
    //this is canvas to pause the game
    public Canvas pauseGame;
    //these are panels in to canvas_init
    public GameObject panel_Principal;
    public GameObject panel_History;
    //this is a Text in panel_History
    public Text tx_History;
    //variable static that indicate pause game
    public static bool gameIsPaused = false; 
    //instanciate class TextWriter, to animate the text
    [SerializeField] private TextWriter textWriter;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    //In This method write the name of your Scene to Change
    public void changeScene(string scene){
        SceneManager.LoadScene(scene);
    }

    //Method that init the History of the game
    public void hide_PanelPrincipal(){
        panel_Principal.SetActive(false);
        panel_History.SetActive(true);
        textWriter.addWriter(tx_History,"help...\nI can  hear my dear friends scream, but the night is very dark and I sense that something is hiding and stalking us",0.1f);
        StartCoroutine(ChangeToGame());
    }

    //Methods that pauses the game
    void Update ()
    {
        if (pauseGame == null && SceneManager.GetActiveScene().name == SceneController.singleton.GAME)
        {
            Cursor.visible = false;
            pauseGame = GameObject.Find("Pause").GetComponent<Canvas>();
            pauseGame.enabled = false;
        }

        if (SceneManager.GetActiveScene().name == SceneController.singleton.GAME && Input.GetKeyDown(KeyCode.Escape)){
            if (gameIsPaused){
                Resume();
            }else{
                Pause();
            }
        }
        
    }

    public void Resume(){
        pauseGame.enabled = false;
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause(){
        pauseGame.enabled = true;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    IEnumerator ChangeToGame()
    {
        yield return new WaitForSeconds(20f);
        changeScene(SceneController.singleton.GAME);        
    }
}
