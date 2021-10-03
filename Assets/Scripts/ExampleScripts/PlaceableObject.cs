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
        public abstract bool Place();

        public void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Place();
            }
            
            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.SetPositionAndRotation(new Vector3(point.x, point.y, 0), Quaternion.identity);
        }
    }
}