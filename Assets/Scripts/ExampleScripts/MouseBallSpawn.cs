using System;
using UnityEngine;

namespace DTerrain
{
    /// <summary>
    /// Example script: Spawining Unity gameobject with collider to show that DTerrain works with Unity Colliders 2D.
    /// Press B to spawn a ball.
    /// </summary>
    public class MouseBallSpawn : MonoBehaviour
    {
        [SerializeField]
        private GameObject ball = null;

        private bool pressed;
        private TimeSpan sinceLastSpawn = TimeSpan.Zero;

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.B))
            {
                pressed = false;
            }

            if (Input.GetKeyDown(KeyCode.B))
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
                    Instantiate(ball, mPos, new Quaternion(0, 0, 0, 0));
                    sinceLastSpawn = TimeSpan.Zero;
                }
            }
        }
    }
}