using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShieldButtonControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Action<bool> OnShieldStateChanged;
    public bool isShieldActive { get; private set; }
    
    private Button shieldButton;
    private bool canPress = true;
    private float shieldActiveTime = 2;
    private float maxActiveTime = 2;

    private void Awake()
    {
        shieldButton = GetComponent<Button>();
    }

    private void Update()
    {
        if (isShieldActive && shieldActiveTime > 0)
            shieldActiveTime -= Time.deltaTime;

        if (shieldActiveTime <= 0)
            OnShieldTimeOut();
    }

    private void OnShieldTimeOut()
    {
        shieldActiveTime = maxActiveTime;
        canPress = false;
        SetShieldState(false);
        OnShieldStateChanged?.Invoke(false);
        shieldButton.interactable = false;
        StartCoroutine(WaitTime());
    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(1);
        canPress = true;
        shieldButton.interactable = true;
    }

    private void SetShieldState(bool isActive)
    {
        isShieldActive = isActive;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (canPress)
        {
            SetShieldState(true);
            OnShieldStateChanged?.Invoke(true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        shieldActiveTime = maxActiveTime;
        SetShieldState(false);
        OnShieldStateChanged?.Invoke(false);
    }
}
