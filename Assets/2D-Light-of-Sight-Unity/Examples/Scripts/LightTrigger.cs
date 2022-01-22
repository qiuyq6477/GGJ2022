using System;
using UnityEngine;

public class LightTrigger:MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        ChangeVisibleOnLOSEvent losEvent = other.GetComponent<ChangeVisibleOnLOSEvent>();
        if (losEvent)
        {
            losEvent.OnVisible();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        ChangeVisibleOnLOSEvent losEvent = other.GetComponent<ChangeVisibleOnLOSEvent>();
        if (losEvent)
        {
            losEvent.OnNotVisible();
        }
    }
}
