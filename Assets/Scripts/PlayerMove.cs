using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D myBody;
    SpriteRenderer myRenderer;
    AudioSource myAudio;

    [SerializeField] private Sprite[] LRSprites;
    [SerializeField] private Sprite[] JumpSprites;
    [SerializeField] private Sprite[] IdleSprites;
    [SerializeField] private Sprite[] IceSprites;

    [SerializeField] private float animationSpeed = 0.3f;
    private float timer;
    private int currentSpriteIndex = 0;

    public float speed;
    public float jumpHeight;
    public float gravityMultiplier;
    public float drag;
    public int flowerCount = 0;
    public TMP_Text deadText;
    public TMP_Text flowerText;

    public AudioClip flower;
    public AudioClip enemy;
    public AudioClip jump;

    public Sprite walkSprite;
    public Sprite jumpSprite;
    public Sprite slideSprite;
    bool onFloor;
    bool onIce;

    public static bool faceRight = true;    
        
    // Start is called before the first frame update
    void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        myAudio = gameObject.GetComponent<AudioSource>();
        onIce = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkKey();
        exitOnfloor();
        JumpPhysics();
        restart();
        flowerText.text = (flowerCount+" / 15");
    }

    void checkKey()
    {
        timer += Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("a");
            myRenderer.flipX = true;
            faceRight = false;
            if (onIce == true)
            {
                PlayerAnimation(IceSprites);
            }
            else
            { 
            PlayerAnimation(LRSprites);
            }
            LRMovement(-speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            myRenderer.flipX = false;
            faceRight = true;
            if (onIce == true)
            {
                PlayerAnimation(IceSprites);
            }
            else
            {
                PlayerAnimation(LRSprites);
            }
            LRMovement(speed);
        }
        else
        {
            if (onIce == true)
            {
                PlayerAnimation(IceSprites);
            }
            else
            {
                PlayerAnimation(IdleSprites);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && onFloor)
        {
            myRenderer.sprite = jumpSprite;
            myBody.velocity = new Vector3(myBody.velocity.x, jumpHeight);
            drag = 2f;      //Jump forward more
            PlayerAnimation(JumpSprites);
            Debug.Log("jump");
            AudioSource.PlayClipAtPoint(jump, transform.position);
        }
        else if (!onFloor)
        {
            myBody.velocity += Vector2.up * Physics2D.gravity.y * (jumpHeight - 1f) * Time.deltaTime;
            PlayerAnimation(JumpSprites);
            Debug.Log("jumping");
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

    void PlayerAnimation(Sprite[] currentSprite)
    {
        timer += Time.deltaTime;
        if(timer >= animationSpeed)
        {
            timer = 0;
            currentSpriteIndex++;
            currentSpriteIndex %= currentSprite.Length;
        }
        myRenderer.sprite = currentSprite[currentSpriteIndex];
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
            onIce = false;
            myRenderer.sprite = walkSprite;
            drag = 0.9f;
        }

        if(collision.gameObject.tag == "ice")
        {
            onFloor = true;
            myRenderer.sprite = slideSprite;
            drag = 5f;
            onIce = true; 
            //myBody.drag = 0.3f;
            //myBody.angularDrag = 0.05f;
        }

        if (collision.gameObject.tag == "flower")
        {
            Destroy(collision.gameObject);
            AudioSource.PlayClipAtPoint (flower, transform.position);
            Debug.Log("flower");
            flowerCount++;
        }

        if (collision.gameObject.tag == "enemy")
        {
            myRenderer.enabled = false;     //invisible to the player AKA dead without losing the camera
            speed = 0;
            jumpHeight = 0;
            AudioSource.PlayClipAtPoint(enemy, transform.position);
            //gameObject.SetActive(false);
            //Destroy(gameObject);
            Debug.Log("enemy");
            deadText.text = ("Press P to Restart");
        }

        if(collision.gameObject.tag == "restart")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
