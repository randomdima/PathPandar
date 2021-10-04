using DTerrain;
using System.Collections;
using UnityEngine;


public static class PlayerStore
{
    private static int bombCount = 5;
    private static int blockCount = 10;

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
}
