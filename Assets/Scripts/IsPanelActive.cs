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

    void OnEnable()
    {
        //Time.timeScale = 0f;
    }

    void OnDisable()
    {
        //Time.timeScale = 1f;
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
