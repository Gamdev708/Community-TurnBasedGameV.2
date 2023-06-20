using UnityEngine;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] int SceneNumber;
    public bool gamePaused = false;

    private void Start() 
    {
        if (PauseMenu!=null)
        {
            PauseMenu.SetActive(false);
        }
        
        DontDestroyOnLoad(this);
    }

    private void Update() {
        manageUI();
    }
    public void manageUI()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("done");
            if(!gamePaused)
            {
                Pause();
                Debug.Log("pause");
            }
            else
            {
                Resume();
                Debug.Log("resume");
            }
        }
    }
    public void Pause()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        gamePaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        gamePaused = false;
    }

    public void newGameLoad()
    {
        SceneManager.LoadScene(SceneNumber);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
