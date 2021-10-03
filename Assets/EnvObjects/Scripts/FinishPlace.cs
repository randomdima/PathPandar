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
        FakePandaBehavior p = collision.gameObject.GetComponent<FakePandaBehavior>();
        if (p != null && !p.Handled)
        {
            p.SetHandled();
            UILogic.Bus.Publish<CountPandaEvent>(new CountPandaEvent());
        } else if(p == null)
        {
            Debug.Log("NULL");
        } else
        {
            Debug.Log(p);
        }
        
    }
}
