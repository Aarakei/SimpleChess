using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SimpleChess.Logic;

public class Pawn : ChessPiece
{

    public Pawn(bool isWhite, Texture2D textureAtlas) : base(isWhite, textureAtlas, GetSourceRectangle(isWhite))
    {
        
    }

    public static Rectangle GetSourceRectangle(bool isWhite)
    {
        return new Rectangle(454, isWhite ? 1367 : 1211, 150, 150);
    }
}