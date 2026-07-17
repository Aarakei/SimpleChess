using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameLibrary;
using GameLibrary.Graphics;

namespace SimpleChess.Logic;

public class ChessBoard
{
    private Texture2D _textureAtlas {get; init;}
    private TextureRegion _textureRegion {get; init;}
    private Sprite _sprite {get; init;}
    private ChessPiece[,] _board;

    public ChessBoard(Texture2D textureAtlas)
    {
        _textureAtlas = textureAtlas;

        Rectangle sourceRectangle = GetSourceRectangle();
        _textureRegion = new TextureRegion(textureAtlas, sourceRectangle.X, sourceRectangle.Y, sourceRectangle.Width, sourceRectangle.Height);
        _sprite = new Sprite(_textureRegion);

        _board = InitializeChessBoard(textureAtlas);
        
    }

    private static Rectangle GetSourceRectangle()
    {
        return new Rectangle(1,5,1200,1200);
    }

    private static ChessPiece[,] InitializeChessBoard(Texture2D textureAtlas)
    {
        ChessPiece[,] board = new ChessPiece[8,8];

            // Black Pieces
            board[0,0] = new Rook(false, textureAtlas);
            board[0,1] = new Knight(false, textureAtlas);
            board[0,2] = new Bishop(false, textureAtlas);
            board[0,3] = new Queen(false, textureAtlas);
            board[0,4] = new King(false, textureAtlas);
            board[0,5] = new Bishop(false, textureAtlas);
            board[0,6] = new Knight(false, textureAtlas);
            board[0,7] = new Rook(false, textureAtlas);
            for (int i = 0; i < 8; i++)
                board[1,i] = new Pawn(false, textureAtlas);

            // White Pieces
            board[7,0] = new Rook(true, textureAtlas);
            board[7,1] = new Knight(true, textureAtlas);
            board[7,2] = new Bishop(true, textureAtlas);
            board[7,3] = new Queen(true, textureAtlas);
            board[7,4] = new King(true, textureAtlas);
            board[7,5] = new Bishop(true, textureAtlas);
            board[7,6] = new Knight(true, textureAtlas);
            board[7,7] = new Rook(true, textureAtlas);
            for (int i = 0; i < 8; i++)
                board[6,i] = new Pawn(true, textureAtlas);
        
        return board;
    }

    public void Draw(SpriteBatch spriteBatch, float scale = 0.5f)
    {
        Vector2 basePosition = Vector2.Zero;
        _sprite.Scale = new Vector2(scale,scale);

        _sprite.Draw(spriteBatch, basePosition);

        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                _board[row,col]?.Draw(spriteBatch, new Vector2(col * 150, row * 150)*scale + basePosition, scale);
            }
        }
    }
}