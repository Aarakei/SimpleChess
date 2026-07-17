using GameLibrary.Graphics;

namespace SimpleChess.Logic;

public class Queen : ChessPiece
{

    protected override string RegionName => IsWhite ? "whiteQueen" : "blackQueen";

    public Queen(bool isWhite, TextureAtlas atlas) : base(isWhite, atlas)
    {
        
    }

}