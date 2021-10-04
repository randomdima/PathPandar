using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DTerrain
{
    public class BlockController : MonoBehaviour
    {
        [field:SerializeField]
        public GameObject[] Objects { get; set; }
        
        [SerializeField] 
        public BasicPaintableLayer CollisionLayer;
        [SerializeField] 
        public BasicPaintableLayer VisibleLayer;
        
        private GameObject SelectedBlock { get; set; }

        public void Update()
        {
            for (var q = KeyCode.Alpha1; q < KeyCode.Alpha9; q++)
            {
                if (Input.GetKeyDown(q))
                {
                    Destroy(SelectedBlock);
                    var id = q - KeyCode.Alpha1;
                    if (id < Objects.Length)
                    {
                        SelectedBlock = Instantiate(Objects[q - KeyCode.Alpha1], transform);
                    }
                }
            }
        }
    }
}