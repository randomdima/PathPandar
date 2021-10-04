using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DTerrain
{
    public class EarthquakeController : MonoBehaviour
    {
        [field:SerializeField]
        public GameObject[] Stones { get; set; }
        
        [field:SerializeField]
        public Texture2D Boom { get; set; }

        [SerializeField] 
        public BasicPaintableLayer CollisionLayer;
        [SerializeField] 
        public BasicPaintableLayer VisibleLayer;
        
        private GameObject SelectedBlock { get; set; }
        
        private ComplexShape Shape { get; set; }

        public void Start()
        {
            Shape = new ComplexShape(Boom);
            PlayerStore.Init();
            StartCoroutine("Quake");
        }

        private IEnumerator Quake()
        {
            while (true)
            {
                yield return new WaitForSeconds(5f);
                var start = CollisionLayer.transform.position;
                for (var q = 0; q < (int)(Random.value*5); q++)
                {
                    var x = start.x + Random.value * CollisionLayer.OriginalTexture.width / 100f;
                    var stoneId = (int)Math.Floor(Random.value * Stones.Length);
                    Instantiate(Stones[stoneId], new Vector3(x, 10f, 0), Quaternion.identity);
                }
                
                for (var q = 0; q < (int)(Random.value*10); q++)
                {
                    var position = new Vector2Int(
                        (int)(CollisionLayer.OriginalTexture.width*Random.value),
                        (int)(CollisionLayer.OriginalTexture.height*Random.value));
            
                    CollisionLayer.Paint(
                        new PaintingParameters()
                        {
                            Position = position,
                            Shape = Shape,
                            PaintingMode = PaintingMode.REMOVE_COLOR,
                            DestructionMode = DestructionMode.DESTROY
                        });

                    VisibleLayer.Paint(
                        new PaintingParameters()
                        {
                            Position = position,
                            Shape = Shape,
                            PaintingMode = PaintingMode.REMOVE_COLOR,
                            DestructionMode = DestructionMode.NONE
                        });
                }
            }
        }
    }
}