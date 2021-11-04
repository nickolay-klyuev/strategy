using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayFireScript : MonoBehaviour
{
    public float rayPower = 40f;

    [SerializeField] private Texture[] rayTextures;
    [SerializeField] private float animationFps = 30f;
    [SerializeField] private AudioClip rayFiresSound;
    
    private int animationStep;
    private float fpsCounter;

    private UnitProperties unitProperties;
    private AttackController attackController;
    private CapsuleCollider2D thisCollider;
    private LineRenderer lineRenderer;
    private bool isRayActive = false;

    private Transform rayPosGameObject;
    private AudioSource audioSource;

    void Start()
    {
        unitProperties = GetComponentInParent<UnitProperties>();
        attackController = GetComponent<AttackController>();

        thisCollider = GetComponent<CapsuleCollider2D>();
        thisCollider.enabled = false;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        //lineRenderer.useWorldSpace = true;
        lineRenderer.enabled = false;

        rayPosGameObject = transform.Find("Ray Position");
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isRayActive)
        {
            SetLineRenPos();

            // ray animation
            fpsCounter += Time.deltaTime;
            if (fpsCounter >= 1f / animationFps)
            {
                animationStep++;
                if (animationStep >= rayTextures.Length)
                {
                    animationStep = 0;
                }

                lineRenderer.material.SetTexture("_MainTex", rayTextures[animationStep]);

                fpsCounter = 0f;
            }
        }
    }

    private void SetLineRenPos()
    {
        lineRenderer.SetPosition(0, rayPosGameObject.position);
        lineRenderer.SetPosition(1, StaticMethods.GetMaxAttackRangePosition(transform.position, attackController.GetTargetGameobject().transform.position, 
                                    unitProperties.attackRange));
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        UnitProperties hitProperties = collider.GetComponentInChildren<UnitProperties>();
        if (hitProperties != null && ((hitProperties.unitType == "enemy" && unitProperties.unitType == "friendly") || 
            (hitProperties.unitType == "friendly" && unitProperties.unitType == "enemy")))
        {
            hitProperties.health -= rayPower;
        }
    }

    public void FireByRay()
    {
        audioSource.PlayOneShot(rayFiresSound);

        thisCollider.enabled = true;
        thisCollider.size = new Vector2(thisCollider.size.x, unitProperties.attackRange);
        thisCollider.offset = new Vector2(thisCollider.offset.x, unitProperties.attackRange / 2);

        SetLineRenPos();
        lineRenderer.enabled = true;
        isRayActive = true;
    }

    public void FireIsOver()
    {
        if (thisCollider != null)
        {
            thisCollider.enabled = false;
        }

        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
        isRayActive = false;
    }
}