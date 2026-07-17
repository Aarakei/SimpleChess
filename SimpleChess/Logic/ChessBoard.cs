using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameLibrary;
using GameLibrary.Graphics;
using System;

namespace SimpleChess.Logic;

public class ChessBoard
{
    public int CellSize {get;} = 150;
    private TextureAtlas _textureAtlas {get; init;}
    private TextureRegion _texture {get; init;}
    private Sprite _sprite {get; init;}
    private ChessPiece[,] _board;

    // Provide original pixel width, regardless of current scale of sprite
    public float BaseWidth => _texture.Width;
    public float BaseHeight => _texture.Height;

    public ChessBoard(TextureAtlas atlas)
    {
        _textureAtlas = atlas;

        _texture = atlas.GetRegion("board");
        _sprite = new Sprite(_texture);

        

        _board = InitializeChessBoard(atlas);
        
    }

    private static ChessPiece[,] InitializeChessBoard(TextureAtlas atlas)
    {
        ChessPiece[,] board = new ChessPiece[8,8];

            // Black Pieces
            board[0,0] = new Rook(false, atlas);
            board[0,1] = new Knight(false, atlas);
            board[0,2] = new Bishop(false, atlas);
            board[0,3] = new Queen(false, atlas);
            board[0,4] = new King(false, atlas);
            board[0,5] = new Bishop(false, atlas);
            board[0,6] = new Knight(false, atlas);
            board[0,7] = new Rook(false, atlas);
            for (int i = 0; i < 8; i++)
                board[1,i] = new Pawn(false, atlas);

            // White Pieces
            board[7,0] = new Rook(true, atlas);
            board[7,1] = new Knight(true, atlas);
            board[7,2] = new Bishop(true, atlas);
            board[7,3] = new Queen(true, atlas);
            board[7,4] = new King(true, atlas);
            board[7,5] = new Bishop(true, atlas);
            board[7,6] = new Knight(true, atlas);
            board[7,7] = new Rook(true, atlas);
            for (int i = 0; i < 8; i++)
                board[6,i] = new Pawn(true, atlas);
        
        return board;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2? position = null, float scale = 0.5f, bool flipped = false)
    {
        Vector2 drawPosition = position ?? Vector2.Zero;
        _sprite.Scale = new Vector2(scale,scale);

        if (flipped)
        {
            _sprite.Effects = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
            _sprite.Draw(spriteBatch, drawPosition);

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    _board[row,col]?.Draw(spriteBatch, new Vector2(7-col,7-row)*CellSize*scale + drawPosition, scale);
                }
            }
        } else
        {
            _sprite.Effects = SpriteEffects.None;
            _sprite.Draw(spriteBatch, drawPosition);

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    _board[row,col]?.Draw(spriteBatch, new Vector2(col * CellSize, row * CellSize)*scale + drawPosition, scale);
                }
            }
        }
        
    }
}