using System;
using LOS.Event;
using UnityEngine;

public class ChangeVisibleOnLOSEvent : MonoBehaviour
{
    private SpriteRenderer[] _spriteRenderers;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D[] _boxCollider2Ds;
    private void Awake()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _boxCollider2Ds = GetComponentsInChildren<BoxCollider2D>();
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

        foreach (var spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.material = Resources.Load<Material>("Sprite-Unlit-Default");
            spriteRenderer.enabled = true;
        }

        foreach (var boxCollider in _boxCollider2Ds)
        {
            boxCollider.enabled = true;
        }
    }

    public void OnNotVisible()
    {
        if (_rigidbody2D)
        {
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
        }

        foreach (var spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.material = Resources.Load<Material>("Sprite-Lit-Default");
            spriteRenderer.enabled = false;
        }
        foreach (var boxCollider in _boxCollider2Ds)
        {
            boxCollider.enabled = false;
        }
    }
}
