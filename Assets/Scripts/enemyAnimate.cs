using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAnimate : MonoBehaviour
{
    SpriteRenderer myRenderer;
    [SerializeField] Sprite[] enemyAnimation;
    float timer = 0;
    float animationSpeed = 0.3f;
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
            currentSpriteIndex %= enemyAnimation.Length;
        }
        myRenderer.sprite = enemyAnimation[currentSpriteIndex];
    }
}
