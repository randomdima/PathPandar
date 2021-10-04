using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameEventBus;
using GameEventBus.Events;

public class FinishPlace : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Output the Collider's GameObject's name
        PandaController p = collision.gameObject.GetComponent<PandaController>();
        if (p != null)
        {
            Object.Destroy(p.gameObject);
            UILogic.Bus.Publish(new CountSavedPandaEvent());
        } else if(p == null)
        {
            Debug.Log("NULL");
        } else
        {
            Debug.Log(p);
        }
        
    }
}
