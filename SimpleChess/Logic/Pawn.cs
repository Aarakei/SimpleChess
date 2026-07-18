using GameLibrary.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SimpleChess.Logic;

public class Pawn : ChessPiece
{
    protected override Point[] MoveDirections => null;
    protected override string RegionName => IsWhite ? "whitePawn" : "blackPawn";

    public Pawn(bool isWhite, TextureAtlas atlas) : base(isWhite, atlas)
    {
        
    }

    public override List<Point> GetLegalMoves(ChessPiece[,] boardState, Point position)
    {
        List<Point> legalMoves = new List<Point>();
        
        int directionY = IsWhite ? -1 : 1;

        int col = position.X;
        int row = position.Y;

        int newRow = position.Y + directionY;
        
        // If moving forward puts you off the board, then there's no legal moves
        if (newRow < 0 || newRow > 7)
            return legalMoves;
        
        if (boardState[col,newRow] == null)
        {
            legalMoves.Add(new Point(col, newRow));

            int baseRank = IsWhite ? 6 : 1;
            if (row == baseRank && boardState[col,row+directionY*2] == null)
            {
                legalMoves.Add(new Point(col, row+directionY*2));
            }
        }

        int[] diagonalOffsets = [-1,1];
        foreach (int offsetX in diagonalOffsets)
        {
            int newCol = col + offsetX;
            // newRow was previously defined as row + directionY

            // Out-of-bounds checking for newRow has already been evaluated
            if (newCol < 0 || newCol > 7)
                continue;
            
            // If there's a piece and it's of the opposite color:
            if (boardState[newCol,newRow] != null && boardState[newCol,newRow].IsWhite != IsWhite)
                legalMoves.Add(new Point(newCol,newRow));
        }
        
        return legalMoves;
    }

}