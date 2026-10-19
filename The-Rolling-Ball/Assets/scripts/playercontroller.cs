using UnityEngine;
using TMPro;




public class playercontroller : MonoBehaviour
{
    public float speed = 5f;
    public int score = 0;
    public bool hasKey = false;
    public bool hasWater = false;
    public TextMeshProUGUI textScore;
	public TextMeshProUGUI notificationText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         UpdateTextScore();
    }

    // Update is called once per frame
    void Update()
    {
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
		}






		if (other.CompareTag("Water"))
		{
			hasWater = true;
			Debug.Log("You has touched the water and cannot win");
            ShowNotification("You has touched the water and cannot win");
			
		}
        // WIN condition
		if (score >= 15 && hasKey == true && !hasWater)
		{
			Debug.Log("You Won!");
            ShowNotification("You Won!");
		}
    }

void UpdateTextScore ()
    {
        textScore.text = "Score: " + score;
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
 


}

