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
    public class BlockEraser : PlaceableObject
    {
        [field:SerializeField]
        public Texture2D Texture { get; set; }
        
        public ComplexShape Shape { get; set; }

        private BlockController controller;

        public void Start()
        {
            controller = GetComponentInParent<BlockController>();
            Shape = new ComplexShape(Texture);
        }

        public override void Place()
        {
            Vector3 p = transform.position - controller.CollisionLayer.transform.position;
            var position = new Vector2Int(
                (int)(p.x * controller.CollisionLayer.PPU) - Shape.Texture.width / 2,
                (int)(p.y * controller.CollisionLayer.PPU) - Shape.Texture.height / 2);
            
            controller.CollisionLayer.Paint(
                new PaintingParameters()
                {
                    Position = position,
                    Shape = Shape,
                    PaintingMode = PaintingMode.REMOVE_COLOR,
                    DestructionMode = DestructionMode.DESTROY
                });

            controller.VisibleLayer.Paint(
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