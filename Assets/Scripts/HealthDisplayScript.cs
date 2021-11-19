using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplayScript : MonoBehaviour
{
    private UnitProperties unitProperties;
    private SpriteRenderer spriteRenderer;
    private Transform hpBar;

    private float maxHp;
    private float initHpBarXScale;

    // Start is called before the first frame update
    void Start()
    {
        unitProperties = transform.parent.GetComponentInParent<UnitProperties>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        hpBar = transform.GetChild(0);
        maxHp = unitProperties.health;
        initHpBarXScale = hpBar.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        float hpPercentage = unitProperties.health * 100 / maxHp;
        float currentHpBarXScale = hpPercentage * initHpBarXScale / 100;
        if (currentHpBarXScale >= 0) // if less than zero then hpBar will grow up
        {
            hpBar.localScale = new Vector2(currentHpBarXScale, hpBar.localScale.y);
        }
        
        if (hpPercentage < 20f)
        {
            spriteRenderer.color = Color.red;
        }
        else if (hpPercentage < 60f)
        {
            spriteRenderer.color = Color.yellow;
        }
        else
        {
            spriteRenderer.color = Color.green;
        }
    }
}
