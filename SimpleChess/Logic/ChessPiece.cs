using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameLibrary;
using GameLibrary.Graphics;
using System.Dynamic;
using System.Reflection.Metadata.Ecma335;

namespace SimpleChess.Logic;

public abstract class ChessPiece
{
    public bool _isWhite {get; init;}
    public TextureRegion _texture {get; init;}
    public Sprite _sprite {get; init;}

    public ChessPiece(bool isWhite, Texture2D textureAtlas, Rectangle sourceRectangle)
    {
        _isWhite = isWhite;
        _texture = new TextureRegion(textureAtlas, sourceRectangle.X, sourceRectangle.Y, sourceRectangle.Width, sourceRectangle.Height);
        _sprite = new Sprite(_texture);
    }

    public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, float scale = 0.5f)
    {
        _sprite.Scale = new Vector2(scale,scale);
        _sprite.Draw(spriteBatch, position);
    }

    // public abstract Point[] GetLegalMoves(ChessPiece[,] boardState);

}