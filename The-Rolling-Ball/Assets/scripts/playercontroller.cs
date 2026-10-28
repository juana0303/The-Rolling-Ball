using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;




public class playercontroller : MonoBehaviour
{
    public float speed = 5f;
    public int score = 0;
    public bool hasKey = false;
    public bool hasWater = false;
    public TextMeshProUGUI textScore;
	public TextMeshProUGUI notificationText;
    public GameObject storyPanel;
    private bool isStoryActive = true;
    public GameObject pausePanel;
    public GameObject settingsPanelInGame;
    public GameObject GameoverPanel;
    public GameObject winPanel;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (storyPanel != null)
        {
            storyPanel.SetActive(true);
        }

         UpdateTextScore();
    }

    // Update is called once per frame
    void Update()
    {

        if (isStoryActive)
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                if (storyPanel != null)
                {
                    storyPanel.SetActive(false); 
                }
                isStoryActive = false; 
            }
            return; 
        }
        // Leemos la entrada del teclado WASD o Flechas
       float moveHorizontal = Input.GetAxis("Horizontal");
       float moveVertical = Input.GetAxis("Vertical");

       Vector3 direction = new Vector3(moveHorizontal, moveVertical, 0);
       transform.Translate(direction * speed * Time.deltaTime); 
    }
       private void OnTriggerEnter2D(Collider2D other)
    {
        
         int poinsGained = CalculatePointsByTag(other.tag);

        if (poinsGained > 0)
        {
            score += poinsGained;
            UpdateTextScore();
            ShowNotification("+" + poinsGained + " points!");
            Destroy(other.gameObject);
        }

        if (other.CompareTag("key"))
        {
            hasKey = true;
			ShowNotification("You got the Key!");
			Destroy(other.gameObject);

            UpdateTextScore();
		}



		if (other.CompareTag("Water"))
		{
			hasWater = true;
			Debug.Log("You has touched the water and cannot win");
            //ShowNotification("You has touched the water and cannot win");

            //Invoke("RestartLevel", 1.5f);
            GameoverPanel.SetActive(true);
			
		}
        // WIN condition
		if (score >= 15 && hasKey == true && !hasWater)
		{
			Debug.Log("You Won!");
            //ShowNotification("You Won!");
            winPanel.SetActive(true);

    
		}
    }
public void RestartLevel()
    {
        Time.timeScale = 1f; // Restaura el tiempo normal antes de reiniciar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

void UpdateTextScore ()
    {
      string keyStatus = hasKey ? "1/1" : "0/1";
    textScore.text = "Score: " + score + "/15   key "  + keyStatus;
    }

    void ShowNotification(string message)
    {
        notificationText.text = message;
    }

    int CalculatePointsByTag (string objectTag)
    {
        if (objectTag == "collectable")
        {
           return 1; 
        }
        else if (objectTag == "collectable2")
        {
           return 2; 
        }
        else if (objectTag == "collectable3")
        {
           return 3; 
        }
        else
        {
            return 0;
        }
    }
    

    // Pausa o reanuda el juego 
    public void TogglePause()
    {
        if (pausePanel != null)
        {
            bool isPaused = pausePanel.activeSelf;
            
            if (isPaused)
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1f; // Reanuda el tiempo
            }
            else
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0f; // Detiene el tiempo
            }
        }
    }

    // Botón: Volver al menú principal
    public void GoToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Menu"); 
    }
public void OpenSettingsInGame(bool open)
    {
        if (settingsPanelInGame != null)
        {
            settingsPanelInGame.SetActive(open);
        }
    }

}

