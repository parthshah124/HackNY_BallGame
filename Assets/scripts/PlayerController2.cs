using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController2 : MonoBehaviour {

    public float speed;
    public Text CountText;
    public Text WinText;
    public Rigidbody rb;
    public static int count;
    public Text Comments;
    public GameObject Next;
    private Vector3 move;
    //public int minDist;
    private Vector3 jump = new Vector3(0, 120.0f, 0);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        WinText.text = "";
    }

    

    void FixedUpdate()
    {
        //dist = Vector3.Distance(Object1.transform.position, Object2.transform.position);
        float moveHorizontal = 0.97f;      //Input.GetAxis("Mouse X");
        float moveVertical = 0.97f;         // Input.GetAxis("Mouse Y");
        
        //Vector3 left = new Vector3(-0.97f, 0, 0);
        //Vector3 right = new Vector3(0.97f, 0, 0);
        //Vector3 up = new Vector3(0, 0, 0.97f);
        //Vector3 down = new Vector3(0, 0, -0.97f);
        //if (Input.GetKey(KeyCode.W))
         //   rb.AddForce(up*speed);
        //if (Input.GetKey(KeyCode.S))
        //    rb.AddForce(down*speed);
        //if (Input.GetKey(KeyCode.A))
        //    rb.AddForce(left*speed);
        //if (Input.GetKey(KeyCode.D))
        //    rb.AddForce(right*speed);

        if (PlayerController.num > 0)
        {
            Next = FindNearestPoint();
        }

        move = Vector3.Normalize(Next.transform.position - transform.position);

        
        Vector3 movement = new Vector3(0.75f*move.x, 0, move.z);   //new Vector3(moveHorizontal, 0.0f, moveVertical);
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
            Comments.text = "Player 2 +1!";
            PlayerController.num = PlayerController.num - 1;
            
            if (PlayerController.num == 0)
            {
                
                if (count > PlayerController.count)
                {
                    WinText.text = "Game Over! Player 2 Wins!";
                }
                if (count < PlayerController.count)
                {
                    WinText.text = "Game Over! Player 1 Wins!";
                }
                if (count == PlayerController.count)
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
            PlayerController.num = PlayerController.num - 1;
            if (PlayerController.num == 0)
            {

                if (count > PlayerController.count)
                {
                    WinText.text = "Game Over! Player 2 Wins!";
                }
                if (count < PlayerController.count)
                {
                    WinText.text = "Game Over! Player 1 Wins!";
                }
                if (count == PlayerController.count)
                {
                    WinText.text = "Game Over! It's a Tie!";
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
            if (count < 0)
            {
                count = 0;
            }
            SetCountText();
            Comments.text = "Player 2 Collision with wall!";
        }
        if (other.gameObject.CompareTag("Cube"))
        {
            if (transform.position.y <= 1 && StartButton.play == true)
            {
                rb.AddForce(jump);
            }
        }
        
    }

    void SetCountText()
    {
        CountText.text = "Score: " + count.ToString();
        
    }

    GameObject FindNearestPoint()
    {
        GameObject[] Rings = GameObject.FindGameObjectsWithTag("Rings");
        GameObject[] Cubes = GameObject.FindGameObjectsWithTag("Pick Up");
        //GameObject[] points; //= Rings.Concat(Cubes).ToArray();
        GameObject[] points = new GameObject[Rings.Length + Cubes.Length];
        Rings.CopyTo(points, 0);
        Cubes.CopyTo(points, Rings.Length);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject point in points)
        {
            Vector3 diff = point.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = point;
                distance = curDistance;
            }
        }
        return closest;
    }
}
//Destroy(other.gameObject);
//if(other.gameObject.CompareTag("Player"))
//     gameObject.SetActive(false);