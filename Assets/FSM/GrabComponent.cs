using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabComponent : MonoBehaviour
{
    private bool isGrab;

    private GameObject GrabObj;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GrabObj)
        {
            GrabObj.transform.position = transform.position;
        }
    }


    public void TryGrab()
    {
        if (isGrab)
        {
            isGrab = false;
            GrabObj = null;
        }
        else
        {
            RaycastHit2D[] hits =
                Physics2D.CircleCastAll(transform.position, 360, transform.forward, 1f);
            if (hits.Length > 0)
            {
                foreach (var hit in hits)
                {
                    Debug.Log("???" + hit.collider.name);
                    Iobj obj = hit.collider.transform.GetComponent<Iobj>();
                    if (obj)
                    {
                        if (obj.type == Iobj.ObjType.GrabAble)
                        {
                            GrabObj = obj.gameObject;
                            isGrab = true;
                        }
                    }
                }
            }
        }
    }
    
}