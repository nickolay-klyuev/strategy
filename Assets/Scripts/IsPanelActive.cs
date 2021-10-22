using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IsPanelActive : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isActive = false;
    public bool GetIsActive()
    {
        return isActive;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData data)
    {
        isActive = true;
    }

    public void OnPointerExit(PointerEventData data)
    {
        isActive = false;
    }
}
