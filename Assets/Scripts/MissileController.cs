using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float attackPower = 10f;
    public float flySpeed = 10f;

    [SerializeField] private AudioClip explosionSound;

    private string parentUnitType;
    private Vector3 targetPosition;
    private GameObject targetGameObject;
    private bool isFlying = false;
    private bool triggeredExplosion = false;

    public void SetParentUnitType(string type)
    {
        parentUnitType = type;
    }

    public bool GetIsFlying()
    {
        return isFlying;
    }

    void FixedUpdate()
    {
        if (isFlying)
        {
            if (targetGameObject != null) // if auto aim
            {
                targetPosition = targetGameObject.transform.position;
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, flySpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                if (!triggeredExplosion)
                {
                    DoExplosion();
                }

                if (triggeredExplosion)
                {
                    isFlying = false;
                    //gameObject.SetActive(false);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<UnitProperties>() != null)
        {
            if ((collider.GetComponent<UnitProperties>().unitType == "enemy" && parentUnitType == "friendly") || 
                (collider.GetComponent<UnitProperties>().unitType == "friendly" && parentUnitType == "enemy"))
            {
                if (!triggeredExplosion)
                {
                    DoExplosion();
                }

                if (triggeredExplosion)
                {
                    collider.GetComponent<UnitProperties>().health -= attackPower;
                    isFlying = false;
                    //gameObject.SetActive(false);
                }
            }
        }
    }

    public void ExplosionIsOver()
    {
        triggeredExplosion = false;
        gameObject.SetActive(false);
    }

    public void DoExplosion()
    {
        triggeredExplosion = true;
        GetComponent<Animator>().SetTrigger("TriggerExplosion");
    }

    public void LunchMissile(GameObject target)
    {
        if (target != null)
        {
            targetGameObject = target;
            targetPosition = target.transform.position;
            isFlying = true;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void LunchMissile(Vector3 position)
    {
        targetPosition = position;
        isFlying = true;
    }

    public void PlayExplosionSound()
    {
        GetComponent<AudioSource>().PlayOneShot(explosionSound);
    }
}
