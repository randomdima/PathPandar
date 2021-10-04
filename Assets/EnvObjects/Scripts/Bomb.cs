using DTerrain;
using System.Collections;
using UnityEngine;


public class Bomb : MonoBehaviour
{

    [field: SerializeField]
    public Texture2D Texture { get; set; }

    private ComplexShape Shape { get; set; }

    private BasicPaintableLayer primaryLayer;
    private BasicPaintableLayer secondaryLayer;

    private Rigidbody2D rb;
    private BlockController controller;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Shape = new ComplexShape(Texture);
        controller = GetComponentInParent<BlockController>();
        StartCoroutine("DestroyTerrain");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private IEnumerator DestroyTerrain()
    {
        yield return new WaitForSeconds(1.6f);
        
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
        
        yield return new WaitForSeconds(0.1f);
        Object.Destroy(gameObject);
    }
}
