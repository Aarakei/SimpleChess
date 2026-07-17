using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SimpleChess.Logic;

public class Knight : ChessPiece
{

    public Knight(bool isWhite, Texture2D textureAtlas) : base(isWhite, textureAtlas, GetSourceRectangle(isWhite))
    {
        
    }

    public static Rectangle GetSourceRectangle(bool isWhite)
    {
        return new Rectangle(303, isWhite ? 1367 : 1211, 150, 150);
    }
}