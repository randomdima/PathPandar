using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DTerrain;

    public class BlockController : MonoBehaviour
    {
        [field:SerializeField]
        public GameObject[] Objects { get; set; }
        
        [SerializeField] 
        public BasicPaintableLayer CollisionLayer;
        [SerializeField] 
        public BasicPaintableLayer VisibleLayer;
        
        private GameObject SelectedBlock { get; set; }

        public void Start()
        {
            PlayerStore.Init();
        }

        public void Update()
        {
            for (var q = KeyCode.Alpha1; q < KeyCode.Alpha9; q++)
            {
                if (Input.GetKeyDown(q))
                {
                    var id = q - KeyCode.Alpha1;
                    SelectBlock(id);
                    
                }
            }
        }

        public void SelectBlock(int id)
        {
            Destroy(SelectedBlock);
            if (id < Objects.Length)
            {
                SelectedBlock = Instantiate(Objects[id], transform);
            }
        }

        public void SelectBomb()
        {
            SelectBlock(1);
        }

        public void SelectBamboo()
        {
            SelectBlock(0);
        }
    }