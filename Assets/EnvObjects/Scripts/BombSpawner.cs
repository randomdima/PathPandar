using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DTerrain;

public class BombSpawner : MonoBehaviour
{
    [SerializeField]
    protected BasicPaintableLayer primaryLayer;
    [SerializeField]
    protected BasicPaintableLayer secondaryLayer;

    public GameObject bombPrefab;
    private bool pressed = false;
    private TimeSpan sinceLastSpawn = TimeSpan.Zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.V))
        {
            pressed = false;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            pressed = transform;
        }

        if (pressed)
        {
            sinceLastSpawn += TimeSpan.FromSeconds(Time.deltaTime);
            if (sinceLastSpawn > TimeSpan.FromMilliseconds(50))
            {
                Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mPos.z = 0;
                GameObject b = Instantiate(bombPrefab, mPos, Quaternion.identity);
                b.GetComponent<Bomb>().Action(primaryLayer, secondaryLayer);
                sinceLastSpawn = TimeSpan.Zero;
            }
        }
    }
}
