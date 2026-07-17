using GameLibrary.Graphics;

namespace SimpleChess.Logic;

public class Knight : ChessPiece
{

    protected override string RegionName => IsWhite ? "whiteKnight" : "blackKnight";

    public Knight(bool isWhite, TextureAtlas atlas) : base(isWhite, atlas)
    {
        
    }

}