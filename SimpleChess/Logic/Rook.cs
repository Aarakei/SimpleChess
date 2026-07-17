using GameLibrary.Graphics;

namespace SimpleChess.Logic;

public class Rook : ChessPiece
{

    protected override string RegionName => IsWhite ? "whiteRook" : "blackRook";

    public Rook(bool isWhite, TextureAtlas atlas) : base(isWhite, atlas)
    {
        
    }

}