using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
    private MoveController moveController;
    private AttackController attackController;
    private GameObject background;

    // Start is called before the first frame update
    void Start()
    {
        background = GameObject.Find("Background");
        moveController = GetComponent<MoveController>();
        attackController = GetComponent<AttackController>();

        moveController.MoveToPoint(GetRandomBackgroundPoint());
    }

    // Update is called once per frame
    void Update()
    {
        if (attackController.GetIsAttacking())
        {
            moveController.StopMoving();
        }
        else if (!attackController.GetIsAttacking() && !moveController.GetIsMoving())
        {
            moveController.MoveToPoint(GetRandomBackgroundPoint());
        }
    }

    private Vector2 GetRandomBackgroundPoint()
    {
        float maxX = background.GetComponent<SpriteRenderer>().size.x;
        float maxY = background.GetComponent<SpriteRenderer>().size.y;
        return new Vector2(Random.Range(0f, maxX), Random.Range(0f, maxY));
    }
}
