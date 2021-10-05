using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaSpawnPlace : MonoBehaviour
{
    public GameObject pandaPrefab;
    public int pandaCount = 10;
    public float spawnPeriod = 5f;

    private float time = 0.0f;
    private int currentCount = 0;
    public Animator animator;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time < spawnPeriod - 1f || currentCount >= pandaCount)
        {
            animator.SetBool("is_spawning", false);
        } else
        {
            animator.SetBool("is_spawning", true);
        }

        if (time >= spawnPeriod && currentCount <= pandaCount)
        {
            time = 0.0f;
            Spawn();

        }
        
    }

    private void Spawn()
    {
        Vector2 v = transform.position;
        v.x += 1;
        Instantiate(pandaPrefab, v, Quaternion.identity);
        currentCount++;

        if (currentCount >= pandaCount)
        {
            Debug.Log("all pandas spawned publishing 1");
            UILogic.Bus.Publish(new AllPandasSpawnedEvent());
            Debug.Log("all pandas spawned publishing 2");
        }
    }

    
}
