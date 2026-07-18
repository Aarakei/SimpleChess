using System.Collections.Generic;
using System.Linq;
using GameLibrary.Graphics;
using Microsoft.Xna.Framework;

namespace SimpleChess.Logic;

public class Rook : ChessPiece
{
    private static readonly Point[] _rookVectors =
    [
        new Point(0, -1), // Up
        new Point(0, 1),  // Down
        new Point(-1, 0), // Left
        new Point(1, 0)   // Right
    ];
    protected override Point[] MoveDirections => _rookVectors;
    
    protected override string RegionName => IsWhite ? "whiteRook" : "blackRook";

    public Rook(bool isWhite, TextureAtlas atlas) : base(isWhite, atlas)
    {
        
    }

}