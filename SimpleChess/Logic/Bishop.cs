using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SimpleChess.Logic;

public class Bishop : ChessPiece
{

    public Bishop(bool isWhite, Texture2D textureAtlas) : base(isWhite, textureAtlas, GetSourceRectangle(isWhite))
    {
        
    }

    public static Rectangle GetSourceRectangle(bool isWhite)
    {
        return new Rectangle(1, isWhite ? 1367 : 1211, 150, 150);
    }
}