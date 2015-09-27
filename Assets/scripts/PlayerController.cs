using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text CountText;
    public Text WinText;
    public Text TimerText;
    public Text Comments;
    public Rigidbody rb;
    public float time = 30;
    public static int count;
    public static int num = 23;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        WinText.text = "";

    }

   

    void Update()
    {
        if (time <= 0.0f && StartButton.play == true)
        {

            StartButton.play = false;
            Comments.text = "Game Ended!";
            //WinText.text = "Time Up!!";

            if (count > PlayerController2.count)
            {
                WinText.text = "Time Up! Player 1 Wins!";
            }
            if (count < PlayerController2.count)
            {
                WinText.text = "Time Up! Player 2 Wins!";
            }
            if (count == PlayerController2.count)
            {
                WinText.text = "Time Up! It's a Tie!";
            }

            //EndGame();
        }
      if (StartButton.play == true && time >= 0)
        {
            time = time - Time.deltaTime;
            int min = (int)time / 60;
            int sec = (int)time % 60;
            if (sec >= 10)
            {
                TimerText.text = min.ToString() + ":" + sec.ToString();
            }
            else
            {
                TimerText.text = min.ToString() + ":0" + sec.ToString();
            }
            

       }
       else
       {
          int min = (int)time / 60;
          int sec = (int)time % 60;
          if (sec >= 10)
          { 
              TimerText.text = min.ToString() + ":" + sec.ToString();
          }
          else
          {
              TimerText.text = min.ToString() + ":0" + sec.ToString();
          }
         
       }
    }

    void FixedUpdate()
    {
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 jump = new Vector3(0, 30.0f, 0);
        if (Input.GetKey(KeyCode.Space) && transform.position.y<=1 && StartButton.play==true)
        {
            rb.AddForce(jump);
        }
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (StartButton.play == true)
        {
            rb.AddForce(movement * speed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up")){
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            num = num - 1;
            Comments.text = "Player 1 +1!";
            if (num == 0)
            {
                
                if (count > PlayerController2.count)
                {
                    WinText.text = "Game Over! Player 1 Wins!";
                }
                if (count < PlayerController2.count)
                {
                    WinText.text = "Game Over! Player 2 Wins!";
                }
                if (count == PlayerController2.count)
                {
                    WinText.text = "Game Over! It's a Tie!";
                }
                StartButton.play = false;
                Comments.text = "Game Ended!";
            }
        }

        if (other.gameObject.CompareTag("Rings"))
        {
            other.gameObject.SetActive(false);
            count = count + 2;
            SetCountText();
            Comments.text = "Player 1 +2!";
            num = num - 1;
            if (num == 0)
            {
                
                if (count > PlayerController2.count)
                {
                    WinText.text = "Time Up! Player 1 Wins!";
                }
                if (count < PlayerController2.count)
                {
                    WinText.text = "Time Up! Player 2 Wins!";
                }
                if (count == PlayerController2.count)
                {
                    WinText.text = "Time Up! It's a Tie!";
                }
                StartButton.play = false;
                Comments.text = "Game Ended!";
            }
        }

        if (other.gameObject.CompareTag("Ball"))
        {
            //other.gameObject.SetActive(false);
            if (other.gameObject.transform.position.y > rb.transform.position.y)
            {
                count = count - 1;
            }
            if (count < 0)
            {
                count = 0;
            }
            //count = count + 2;
            SetCountText();
            Comments.text = "Balls Collided!";
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            //other.gameObject.SetActive(false);
            count = count - 1;
            if (count <0)
            {
                count = 0;
            }
            SetCountText();
            Comments.text = "Player 1 Collision with wall!";
        }
    }

    void SetCountText()
    {
        CountText.text = "Score: " + count.ToString();
    }
}
//Destroy(other.gameObject);
//if(other.gameObject.CompareTag("Player"))
//     gameObject.SetActive(false);