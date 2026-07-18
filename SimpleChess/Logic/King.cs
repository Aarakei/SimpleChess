using GameLibrary.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SimpleChess.Logic;

public class King : ChessPiece
{
    private static readonly Point[] _kingVectors = 
    [
        new Point(-1,-1), new Point(0,-1), new Point(1,-1),
        new Point(-1,0),                   new Point(1,0),
        new Point(-1,1),  new Point(0,1),  new Point(1,1)
    ];
    protected override Point[] MoveDirections => _kingVectors;
    protected override string RegionName => IsWhite ? "whiteKing" : "blackKing";

    public King(bool isWhite, TextureAtlas atlas) : base(isWhite, atlas)
    {
        
    }

    public override List<Point> GetLegalMoves(ChessPiece[,] boardState, Point position)
    {
        List<Point> legalMoves = new List<Point>();

        // Check movement in each direction the piece can move
        foreach (Point dir in MoveDirections)
        {
            int col = position.X;
            int row = position.Y;
            
            col += dir.X;
            row += dir.Y;

            // Out-of-bounds check
            if (col < 0 || col > 7 || row < 0 || row > 7)
                continue;

            // Add to list if the space is empty or contains the opposite color
            Point? potentialMove = CheckSpace(boardState,col,row);
            if (potentialMove != null)
                legalMoves.Add(potentialMove.Value);
                
        }

        return legalMoves;
    }
}