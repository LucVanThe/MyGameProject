using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
  
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameoverui;
    [SerializeField] private GameObject GameWinUI;
    [SerializeField] private GameObject Tamdung;
    private bool isGameWin = false;
    private bool isGameover = false;
  
    public int score = 0;
  
    

    void Start()
    {
        uploadscore();
        gameoverui.SetActive(false);
        GameWinUI.SetActive(false);
        Tamdung.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TamDung();
         
    }
 
  
    public void addScore(int point)
    {
        if (!isGameover && !isGameWin)
        {
            score += point;
             uploadscore();
        }
       
    }
    
   
    private void uploadscore()
    {
        scoreText.text = score.ToString();
       
    }
    public void gameover()
    {
        isGameover = true;
        score = 0;
        Time.timeScale = 0;
        gameoverui.SetActive(true);
    }
    public void TamDung()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Tam dung");
            isGameover = true;
            Time.timeScale = 0;
            Tamdung.SetActive(true);
        }
    }
    public void ResumeGame()
    {
        Tamdung.SetActive(false); 
        Time.timeScale = 1f; 
        isGameover = false;
    }
    public void GameWin()
    {
        isGameWin = true;
        Time.timeScale = 0;
        GameWinUI.SetActive(true);
    }
    public void restartgame()
    {
        isGameover = false;
        score = 0;
        uploadscore();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene("Game3");
    }
    public bool IsGameOver()
    {
        return isGameover;
    }
    public bool isGamewin()
    {
        return isGameWin;
    }
    public void gotoMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
    public void gotoNextMap( int map)
    {
        
        SceneManager.LoadScene(map);
    }
}
