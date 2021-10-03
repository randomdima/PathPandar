using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaSpawnPlace : MonoBehaviour
{
    public GameObject pandaPrefab;
    public int pandaCount = 10;
    public float spawnPeriod = 0.1f;

    private float time = 0.0f;
    private BoxCollider2D rb;
    private int currentCount = 0;

    void Start()
    {
        rb = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= spawnPeriod && currentCount <= pandaCount)
        {
            time = 0.0f;
            Vector2 v = rb.transform.position;
            v.x += 1;
            Instantiate(pandaPrefab, v, Quaternion.identity);
            currentCount++;
        }
        
    }

    
}
