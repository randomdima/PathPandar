using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DTerrain
{
    public class EarthquakeController : MonoBehaviour
    {
        [field:SerializeField]
        public GameObject[] Stones { get; set; }
        
        [field:SerializeField]
        public Texture2D[] Cracks { get; set; }

        [SerializeField] 
        public BasicPaintableLayer CollisionLayer;
        [SerializeField] 
        public BasicPaintableLayer VisibleLayer;
        
        private ComplexShape[] CrackShapes { get; set; }

        public int StoneCount;

        public int CrackCount;

        public void Start()
        {
            CrackShapes = Cracks.Select(q=> new ComplexShape(q)).ToArray();
            PlayerStore.Init();
            StartCoroutine("Quake");
        }

        private IEnumerator Quake()
        {
            while (true)
            {
                yield return new WaitForSeconds(20f);
              //  GetComponent<LevelShake>().shakeDuration = 0.7f;
                var start = CollisionLayer.transform.position;
                for (var q = 0; q < (int)(Random.value*StoneCount); q++)
                {
                    var x = start.x + Random.value * CollisionLayer.OriginalTexture.width/80f;
                    var stoneId = (int)Math.Floor(Random.value * Stones.Length);
                    var obj = Instantiate(Stones[stoneId], new Vector3(x, 25f, 0), new Quaternion(1,1,Random.value*360,1));
                    obj.transform.localScale = obj.transform.localScale*(Random.value+0.2f);
                }
                
                for (var q = 0; q < (int)(Random.value*CrackCount); q++)
                {
                    var position = new Vector2Int(
                        (int)(CollisionLayer.OriginalTexture.width*Random.value),
                        (int)(CollisionLayer.OriginalTexture.height*(Random.value-0.2f)));
                    var shapeId = (int)Math.Floor(Random.value * CrackShapes.Length);
                    var shape = CrackShapes[shapeId];
            
                    CollisionLayer.Paint(
                        new PaintingParameters()
                        {
                            Position = position,
                            Shape = shape,
                            PaintingMode = PaintingMode.REMOVE_COLOR,
                            DestructionMode = DestructionMode.DESTROY
                        });

                    VisibleLayer.Paint(
                        new PaintingParameters()
                        {
                            Position = position,
                            Shape = shape,
                            PaintingMode = PaintingMode.REMOVE_COLOR,
                            DestructionMode = DestructionMode.NONE
                        });
                }
            }
        }
    }
}