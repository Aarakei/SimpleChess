using GameLibrary.Graphics;

namespace SimpleChess.Logic;

public class Pawn : ChessPiece
{

    protected override string RegionName => IsWhite ? "whitePawn" : "blackPawn";

    public Pawn(bool isWhite, TextureAtlas atlas) : base(isWhite, atlas)
    {
        
    }

}