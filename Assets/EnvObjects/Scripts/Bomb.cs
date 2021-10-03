using DTerrain;
using System.Collections;
using UnityEngine;


public class Bomb : MonoBehaviour
{
    [SerializeField]
    public int circleSize = 1000;
    [SerializeField]
    public int outlineSize = 50;

    protected Shape destroyCircle;
    protected Shape outlineCircle;

    private BasicPaintableLayer primaryLayer;
    private BasicPaintableLayer secondaryLayer;

    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Action(BasicPaintableLayer primaryLayer, BasicPaintableLayer secondaryLayer)
    {
        this.primaryLayer = primaryLayer;
        this.secondaryLayer = secondaryLayer;
        StartCoroutine("DestroyTerrain");
    }

    private IEnumerator DestroyTerrain()
    {
        yield return new WaitForSeconds(4f);
        destroyCircle = Shape.GenerateShapeCircle(circleSize);
        outlineCircle = Shape.GenerateShapeCircle(circleSize + outlineSize);
        Vector3 p = this.transform.position - primaryLayer.transform.position;
        
        primaryLayer?.Paint(new PaintingParameters()
        {
            Color = Color.clear,
            Position = new Vector2Int((int)(p.x * primaryLayer.PPU) - circleSize, (int)(p.y * primaryLayer.PPU) - circleSize),
            Shape = destroyCircle,
            PaintingMode = PaintingMode.REPLACE_COLOR,
            DestructionMode = DestructionMode.DESTROY
        });

        secondaryLayer?.Paint(new PaintingParameters()
        {
            Color = Color.clear,
            Position = new Vector2Int((int)(p.x * secondaryLayer.PPU) - circleSize, (int)(p.y * secondaryLayer.PPU) - circleSize),
            Shape = destroyCircle,
            PaintingMode = PaintingMode.REPLACE_COLOR,
            DestructionMode = DestructionMode.NONE
        });
       

    }
}
