using GameLibrary.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;

namespace SimpleChess.Logic;

public class Bishop : ChessPiece
{

    private static readonly Point[] _bishopVectors =
    [
        new Point(-1,-1), new Point(1,-1),
        new Point(-1,1),  new Point(1,1)
    ];
    public override Point[] MoveDirections => _bishopVectors;
    protected override string RegionName => IsWhite ? "whiteBishop" : "blackBishop";

    public Bishop(bool isWhite, TextureAtlas atlas) : base(isWhite, atlas)
    {
        
    }

}