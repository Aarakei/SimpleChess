using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameLibrary;
using GameLibrary.Graphics;
using System.Collections.Generic;

namespace SimpleChess.Logic;

public abstract class ChessPiece
{
    public static readonly float Size = 150;
    public float BaseWidth => _texture.Width;
    public float BaseHeight => _texture.Height;
    protected abstract string RegionName {get;}
    private readonly TextureRegion _texture;
    private readonly Sprite _sprite;
    public bool IsWhite {get; init;}
    protected bool HasMoved {get; set;}
    protected abstract Point[] MoveDirections {get;}

    public ChessPiece(bool isWhite, TextureAtlas atlas)
    {
        IsWhite = isWhite;
        HasMoved = false;
        _texture = atlas.GetRegion(RegionName);
        _sprite = new Sprite(_texture);
    }

    public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, float scale = 0.5f)
    {
        _sprite.Scale = new Vector2(scale,scale);
        _sprite.Draw(spriteBatch, position);
    }

    public virtual Point? CheckSpace(ChessPiece[,] boardState, int col, int row)
    {
        // Space is valid if it is empty or contains a piece of an opposite color
        if (boardState[col, row] == null || boardState[col,row].IsWhite != IsWhite)
            return new Point(col,row);
        
        return null;
    }

    public virtual List<Point> GetLegalMoves(ChessPiece[,] boardState, Point position)
    {
        List<Point> legalMoves = new List<Point>();

        // Check movement in each direction the piece can move
        foreach (Point dir in MoveDirections)
        {
            int col = position.X;
            int row = position.Y;

            while (true)
            {
                col += dir.X;
                row += dir.Y;

                // Out-of-bounds check
                if (col < 0 || col > 7 || row < 0 || row > 7)
                    break;

                // Add to list if the space is empty or contains the opposite color
                Point? potentialMove = CheckSpace(boardState,col,row);
                if (potentialMove != null)
                    legalMoves.Add(potentialMove.Value);
                
                // Stop when a piece is reached (no jumping)
                if (boardState[col,row] != null)
                    break;
            }
        }

        return legalMoves;
    }

    protected virtual void Move()
    {
        HasMoved = true;
    }

}