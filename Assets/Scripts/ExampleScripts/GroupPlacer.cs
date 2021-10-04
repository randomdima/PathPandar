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
    public class GroupPlacer : MonoBehaviour
    {
        [field: SerializeField]
        public GameObject[] Objects { get; set; }

        private GameObject SelectedObject { get; set; }

        private int offset = -1;
        private float dx = 0;

        public void Start()
        {
            SelectedObject = Instantiate(Objects.First(), transform);
            offset = 0;
        }

        public void Update()
        {
            if (Input.GetMouseButton(1))
            {
                Destroy(this);
                return;
            }

            dx += Input.mouseScrollDelta.y;
            var o = (int)(dx % Objects.Length);
            if (o < 0) o += Objects.Length;
            if (o == offset)
            {
                return;
            }

            offset = o;
            var old = SelectedObject;
            SelectedObject = Instantiate(Objects[offset], transform);
            Destroy(old);
        }

        private void OnDestroy()
        {
            Destroy(SelectedObject);
        }
    }
}