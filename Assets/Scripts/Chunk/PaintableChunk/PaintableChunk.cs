using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DTerrain
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PaintableChunk : MonoBehaviour, ITexturedChunk
    {
        public SpriteRenderer SpriteRenderer { get; private set; }

        public ITextureSource TextureSource { get; set; }
        public int SortingLayerID { get; set; }

        protected bool painted=true;


        public virtual void Init()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            SpriteRenderer.sprite = Sprite.Create(
                TextureSource.Texture, 
                new Rect(0, 0, TextureSource.Texture.width, TextureSource.Texture.height), 
                new Vector2(0.0f, 0.0f), 
                TextureSource.PPU,
                0,
                SpriteMeshType.FullRect);
            TextureSource.SetUpToRenderer(SpriteRenderer);

            SpriteRenderer.sortingLayerID = SortingLayerID;
        }

        public virtual bool Paint(RectInt r, PaintingParameters pp, int column, int columnMin)
        {
            if (pp.PaintingMode == PaintingMode.NONE) return false;

            //Find common rect that will be applied on this texture rect
            RectInt common;
            r.Intersects(new RectInt(0, 0, TextureSource.Texture.width, TextureSource.Texture.height), out common);

            //Generate color array...
            int len = common.width * common.height;

            if (len == 0) return false;

            Color32[] cs = new Color32[len];

            //...using paiting method
            if (pp.PaintingMode == PaintingMode.REPLACE_COLOR)
                for (int i = 0; i < len; i++)
                {
                    var px = column;
                    var py = i+columnMin-(r.y-common.y);
                    var ss = py * pp.Shape.Texture.width + px;
                    
                    var color = pp.Shape.Pixels[ss];
                    cs[i] = color;//Color32.Lerp(currentColor, color, color.a / 255f);
                    cs[i].a = 255;
                   // var currentColor = TextureSource.Texture.GetPixel(common.x, common.y - i);
                    // if (currentColor.a == 0)
                    // {
                    //     cs[i] = color;
                    // }
                    // else
                    // {
                    //     cs[i] = color;//Color32.Lerp(currentColor, color, color.a / 255f);
                    //     cs[i].a = 255;
                    // }
                }
            else if (pp.PaintingMode == PaintingMode.REMOVE_COLOR)
                for (int i = 0; i < len; i++)
                    cs[i] = Color.clear;
            else if (pp.PaintingMode == PaintingMode.ADD_COLOR)
            {
                // for (int i = 0; i < common.width; i++)
                // {
                //     for (int j = 0; j < common.height; j++)
                //     {
                //         cs[i*common.height + j] = TextureSource.Texture.GetPixel(common.x + i, common.y + j) + pp.Color;
                //     }
                // }
            }

            //Apply color
            TextureSource.Texture.SetPixels32(common.x, common.y, common.width, common.height, cs);

            //Set up this chunk as ready to be updated on next Update()
            painted = true;
            return true;
            
        }

        public virtual void Update()
        {
            if(painted)
            {
                TextureSource.Texture.Apply();
                painted = false;
            }
            
        }

        public virtual bool IsOccupied(Vector2Int at)
        {
            if (at.x >= 0 && at.x < TextureSource.Texture.width && at.y >= 0 && at.y < TextureSource.Texture.height)
                return TextureSource.Texture.GetPixel(at.x, at.y).a == 0.0f;
            else return false;
        }
    }
}

