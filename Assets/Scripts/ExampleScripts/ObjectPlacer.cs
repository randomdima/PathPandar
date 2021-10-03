using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DTerrain
{
    /// <summary>
    /// Simple script that paints with Clear color the primary layer and paints using Black the secondary layer.
    /// Additionally, on right click paints primary layer with black.
    /// Use with SampleScene1.
    /// </summary>
    public class ObjectPlacer : PlaceableObject
    {
        [field:SerializeField]
        public GameObject Object { get; set; }

        public override bool Place()
        {
            Instantiate(Object, transform.position, Quaternion.identity, GameObject.Find("BlockController").transform);
            return true;
        }
    }
}