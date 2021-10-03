using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTerrain
{
    public enum PaintingMode
    {
        REPLACE_COLOR,
        REMOVE_COLOR,
        ADD_COLOR,
        NONE
    }

    public enum DestructionMode
    {
        NONE,
        DESTROY,
        BUILD
    }

    public struct PaintingParameters
    {
        public ComplexShape Shape;
        public Vector2Int Position;
        public PaintingMode PaintingMode;
        public DestructionMode DestructionMode;
        public List<int> AffectedChildChunks; //0 means main layer

        public PaintingParameters(ComplexShape shape, Vector2Int position, PaintingMode paintingMode = PaintingMode.REPLACE_COLOR, DestructionMode destructionMode = DestructionMode.NONE, List<int> affectedChildChunks=null)
        {
            Shape = shape;
            Position = position;
            PaintingMode = paintingMode;
            DestructionMode = destructionMode;
            if (affectedChildChunks == null) AffectedChildChunks = new List<int>() { 0 };
            else AffectedChildChunks = affectedChildChunks;
        }
    }
}

