using GameLibrary.Graphics;

namespace SimpleChess.Logic;

public class King : ChessPiece
{

    protected override string RegionName => IsWhite ? "whiteKing" : "blackKing";

    public King(bool isWhite, TextureAtlas atlas) : base(isWhite, atlas)
    {
        
    }

}