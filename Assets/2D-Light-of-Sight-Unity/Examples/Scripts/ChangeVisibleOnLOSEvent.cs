using System;
using LOS.Event;
using UnityEngine;

public class ChangeVisibleOnLOSEvent : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        LOSEventTrigger trigger = GetComponent<LOSEventTrigger>();
        trigger.OnNotTriggered += OnNotVisible;
        trigger.OnTriggered += OnVisible;
        
        OnNotVisible();
    }

    public void OnVisible()
    {
        if (_rigidbody2D)
        {
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }

        if (_spriteRenderer)
        {
            _spriteRenderer.material = Resources.Load<Material>("Sprite-Unlit-Default");
            _spriteRenderer.enabled = true;
        }

        if (_boxCollider2D)
        {
            _boxCollider2D.enabled = true;
        }
    }

    public void OnNotVisible()
    {
        if (_rigidbody2D)
        {
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
        }

        if (_spriteRenderer)
        {
            _spriteRenderer.material = Resources.Load<Material>("Sprite-Lit-Default");
            _spriteRenderer.enabled = false;
        }

        if (_boxCollider2D)
        {
            _boxCollider2D.enabled = false;
        }
    }
}
