using GameLibrary.Graphics;

namespace SimpleChess.Logic;

public class Bishop : ChessPiece
{

    protected override string RegionName => IsWhite ? "whiteBishop" : "blackBishop";

    public Bishop(bool isWhite, TextureAtlas atlas) : base(isWhite, atlas)
    {
        
    }

}