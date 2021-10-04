using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DTerrain
{
    public abstract class PlaceableObject : MonoBehaviour
    {
        public abstract void Place();

        private float lastSpawnTime = -10;
        private const float cooldown = 0.75f;
        private SpriteRenderer sprite;

        public void Start()
        {
            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.SetPositionAndRotation(new Vector3(point.x, point.y, 0), Quaternion.identity);
        }

        public void Update()
        {
            if (Input.GetMouseButton(1))
            {
                Destroy(this);
                return;
            }

            sprite ??= GetComponent<SpriteRenderer>();

            var isReady = Time.time - lastSpawnTime > cooldown;
            sprite.color = new Color(1, 1, 1, isReady ? 1 : 0.5f);

            if (isReady && Input.GetMouseButton(0))
            {
                lastSpawnTime = Time.time;
                Place();
            }

            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.SetPositionAndRotation(new Vector3(point.x, point.y, 0), Quaternion.identity);
        }
    }
}