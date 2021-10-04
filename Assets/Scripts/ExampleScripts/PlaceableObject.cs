﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DTerrain
{
    public abstract class PlaceableObject : MonoBehaviour
    {
        public abstract bool Place();

        private float time = 0.0f;
        private float spawnPeriod = 1f;

        public void Update()
        {
            time += Time.deltaTime;
            if (Input.GetMouseButton(0) && time > spawnPeriod)
            {
                time = 0f;
                Place();
            }
            
            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.SetPositionAndRotation(new Vector3(point.x, point.y, 0), Quaternion.identity);
        }
    }
}