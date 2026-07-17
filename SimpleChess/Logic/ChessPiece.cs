using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameLibrary;
using GameLibrary.Graphics;

namespace SimpleChess.Logic;

public abstract class ChessPiece
{
    public static float Size = 150;
    public float BaseWidth => _texture.Width;
    public float BaseHeight => _texture.Height;
    public bool IsWhite {get; init;}
    protected abstract string RegionName {get;}
    private readonly TextureRegion _texture;
    private readonly Sprite _sprite;

    public ChessPiece(bool isWhite, TextureAtlas atlas)
    {
        IsWhite = isWhite;
        _texture = atlas.GetRegion(RegionName);
        _sprite = new Sprite(_texture);
    }

    public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, float scale = 0.5f)
    {
        _sprite.Scale = new Vector2(scale,scale);
        _sprite.Draw(spriteBatch, position);
    }

    // public abstract Point[] GetLegalMoves(ChessPiece[,] boardState);

}