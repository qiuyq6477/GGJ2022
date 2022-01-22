using System;
using LOS.Event;
using UnityEngine;

public class ChangeVisibleOnLOSEvent : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        LOSEventTrigger trigger = GetComponent<LOSEventTrigger>();
        trigger.OnNotTriggered += OnNotVisible;
        trigger.OnTriggered += OnVisible;
        
        OnNotVisible();
    }

    void OnVisible()
    {
        _spriteRenderer.enabled = true;
        _boxCollider2D.isTrigger = false;
    }

    void OnNotVisible()
    {
        _spriteRenderer.enabled = false;
        _boxCollider2D.isTrigger = true;
    }
}
