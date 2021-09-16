using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjectScaleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer parentSprite = GetComponentInParent<SpriteRenderer>();
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        float pSpriteWidth = parentSprite.bounds.size.x;
        float pSpriteHeight = parentSprite.bounds.size.y;
        float spriteWidth = sprite.bounds.size.x;
        float spriteHeight = sprite.bounds.size.y;

        transform.localScale = new Vector2(pSpriteWidth / spriteWidth, pSpriteHeight / spriteHeight);
    }
}
