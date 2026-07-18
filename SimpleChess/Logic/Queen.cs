using System.Collections.Generic;
using GameLibrary.Graphics;
using Microsoft.Xna.Framework;

namespace SimpleChess.Logic;

public class Queen : ChessPiece
{
    private static readonly Point[] _queenVectors = 
    [
        new Point(-1,-1), new Point(0,-1), new Point(1,-1),
        new Point(-1,0),                   new Point(1,0),
        new Point(-1,1),  new Point(0,1),  new Point(1,1)
    ];
    protected override Point[] MoveDirections => _queenVectors;
    protected override string RegionName => IsWhite ? "whiteQueen" : "blackQueen";

    public Queen(bool isWhite, TextureAtlas atlas) : base(isWhite, atlas)
    {
        
    }

}