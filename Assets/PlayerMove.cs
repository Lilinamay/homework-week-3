using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D myBody;
    SpriteRenderer myRenderer;
    AudioSource myAudio;

    public float speed;
    public float jumpHeight;
    public float gravityMultiplier;
    public float drag;
    public AudioClip flower;
    public Sprite walkSprite;
    public Sprite jumpSprite;
    public Sprite slideSprite;
    bool onFloor;
        
    // Start is called before the first frame update
    void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        myAudio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        checkKey();
        exitOnfloor();
        JumpPhysics();
        restart();

    }

    void checkKey()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("a");
            myRenderer.flipX = true;
            LRMovement(-speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            myRenderer.flipX = false;
            LRMovement(speed);
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && onFloor)
        {
            myRenderer.sprite = jumpSprite;
            myBody.velocity = new Vector3(myBody.velocity.x, jumpHeight);
            drag = 2f;
        }
        else if (!onFloor)
        {
            myBody.velocity += Vector2.up * Physics2D.gravity.y * (jumpHeight - 1f) * Time.deltaTime;
        }
        
    }

    void JumpPhysics()
    {
        if (myBody.velocity.y < 0)
        {
            myBody.velocity += Vector2.up * Physics2D.gravity.y * (gravityMultiplier - 1f) * Time.deltaTime;
        }
    }

    void LRMovement(float direction)
    {
        myBody.velocity = new Vector3(direction*drag, myBody.velocity.y);       //leftright movement with drag
    }

    void exitOnfloor()
    {
        if (onFloor && myBody.velocity.y > 0.1)
        {
            onFloor = false;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            onFloor = true;
            myRenderer.sprite = walkSprite;
            drag = 0.9f;
        }

        if(collision.gameObject.tag == "ice")
        {
            onFloor = true;
            myRenderer.sprite = slideSprite;
            drag = 5f;
            //myBody.drag = 0.3f;
            //myBody.angularDrag = 0.05f;
        }

        if (collision.gameObject.tag == "flower")
        {
            Destroy(collision.gameObject);
            AudioSource.PlayClipAtPoint (flower, transform.position);
            Debug.Log("flower");
        }

        if (collision.gameObject.tag == "enemy")
        {
            myRenderer.enabled = false;     //invisible to the player AKA dead without losing the camera
            //gameObject.SetActive(false);
            //Destroy(gameObject);
            Debug.Log("enemy");
        }


    }

    void restart()
    {
        if (Input.GetKey(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    


}
