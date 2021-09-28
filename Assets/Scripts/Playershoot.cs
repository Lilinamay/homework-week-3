using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playershoot : MonoBehaviour
{

    public GameObject beam;
    public float shootSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) //only once
        {
            GameObject newBeam = Instantiate(beam, transform.position, transform.rotation); //default to player's position/rotation
            newBeam.transform.SetParent(gameObject.transform);
            newBeam.transform.localPosition = new Vector3(1f, -0.1f); ///local position relative to player
            
            float dir = 0f;
            if (PlayerMove.faceRight)
            {
                dir = 1f;
            }
            else
            {
                newBeam.GetComponent<SpriteRenderer>().flipX = true;
                dir = -1f;
            }

            newBeam.GetComponent<Rigidbody2D>().velocity = new Vector3(gameObject.GetComponent<Rigidbody2D>().velocity.x+dir * shootSpeed, 0f);  //beam move
        }
    }
}
