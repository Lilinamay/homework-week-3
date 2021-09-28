using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerAnimate : MonoBehaviour
{
    SpriteRenderer myRenderer;
    [SerializeField] public Sprite[] flowerAnimation;
    float animationSpeed = 0.3f;
    float timer = 0;
    int currentSpriteIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAnimation();
    }

    void PlayerAnimation()
    {
        timer += Time.deltaTime;
        if (timer >= animationSpeed)
        {
            timer = 0;
            currentSpriteIndex++;
            currentSpriteIndex %= flowerAnimation.Length;
        }
        myRenderer.sprite = flowerAnimation[currentSpriteIndex];
    }
}
