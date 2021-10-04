using DTerrain;
using System.Collections;
using UnityEngine;


public static class PlayerStore
{
    private static int bombCount;
    private static int blockCount;

    public static int BlockCount
    {
        get => blockCount;
        set
        {
            blockCount = value;
            UILogic.Bus.Publish(new BambooCountEvent(value));
        }
    }

    public static int BombCount
    {
        get => bombCount;
        set
        {
            bombCount = value;
            UILogic.Bus.Publish(new BombCountEvent(value));
        }
    }

    public static void Init()
    {
        BlockCount = 10;
        BombCount = 5;
    }
}
