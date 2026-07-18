using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameLibrary;
using GameLibrary.Graphics;
using System;

namespace SimpleChess.Logic;

public class ChessBoard
{
    public int CellSize {get;} = 150;
    private TextureAtlas _textureAtlas {get; init;}
    private TextureRegion _texture {get; init;}
    private Sprite _sprite {get; init;}
    private Sprite _cellOverlay {get; set;}
    private ChessPiece[,] _board;
    private Point? _selectedPiece;

    // Provides original pixel width, regardless of current scale of sprite
    public float BaseWidth => _texture.Width;
    public float BaseHeight => _texture.Height;

    public ChessBoard(TextureAtlas atlas)
    {
        _textureAtlas = atlas;
        _texture = atlas.GetRegion("board");
        _sprite = new Sprite(_texture);

        TextureRegion square = atlas.GetRegion("cellOverlay");
        _cellOverlay = new Sprite(square);
        _cellOverlay.Scale = new Vector2(CellSize,CellSize);

        _board = InitializeChessBoard(atlas);

        _selectedPiece = null;
        
    }

    private static ChessPiece[,] InitializeChessBoard(TextureAtlas atlas)
    {
        ChessPiece[,] board = new ChessPiece[8,8];

            // Black Pieces
            board[0,0] = new Rook(false, atlas);
            board[1,0] = new Knight(false, atlas);
            board[2,0] = new Bishop(false, atlas);
            board[3,0] = new Queen(false, atlas);
            board[4,0] = new King(false, atlas);
            board[5,0] = new Bishop(false, atlas);
            board[6,0] = new Knight(false, atlas);
            board[7,0] = new Rook(false, atlas);
            for (int i = 0; i < 8; i++)
                board[i,1] = new Pawn(false, atlas);

            // White Pieces
            board[0,7] = new Rook(true, atlas);
            board[1,7] = new Knight(true, atlas);
            board[2,7] = new Bishop(true, atlas);
            board[3,7] = new Queen(true, atlas);
            board[4,7] = new King(true, atlas);
            board[5,7] = new Bishop(true, atlas);
            board[6,7] = new Knight(true, atlas);
            board[7,7] = new Rook(true, atlas);
            for (int i = 0; i < 8; i++)
                board[i,6] = new Pawn(true, atlas);
        
        return board;
    }

    public void Update(MouseState mouseState, Vector2 position, float scale, bool flipped)
    {
        
    }

    public void OnClick(Vector2 mousePosition, Vector2 position, float scale, bool flipped)
    {
        Point? selectedPoint = GetSelectedPoint(mousePosition, position, scale, flipped);

        // Deselect the current piece if the click is on the same piece or out of bounds
        if (!selectedPoint.HasValue || selectedPoint == _selectedPiece)
        {
            _selectedPiece = null;
            return;
        }

        // A new square is being selected
        if (_selectedPiece == null)
        {
            Point newPoint = selectedPoint.Value;
            // Only allow the selecting of non-empty squares
            if (_board[newPoint.X,newPoint.Y] != null)
                _selectedPiece = selectedPoint;
        } else
        {
            //TODO: validate the move using the selected piece's own method
            ChessPiece piece = _board[_selectedPiece.Value.X,_selectedPiece.Value.Y];
            if (piece.GetLegalMoves(_board,_selectedPiece.Value).Contains(selectedPoint.Value))
            MovePiece(selectedPoint.Value, _selectedPiece.Value);
            _selectedPiece = null;
        }

    }

    public void Draw(SpriteBatch spriteBatch, Vector2? position = null, float scale = 0.5f, bool flipped = false)
    {
        Vector2 drawPosition = position ?? Vector2.Zero;
        _sprite.Scale = new Vector2(scale,scale);

        if (flipped)
            _sprite.Effects = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
        else
            _sprite.Effects = SpriteEffects.None;

        
        _sprite.Draw(spriteBatch, drawPosition);

        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                Vector2 cell = flipped ? new Vector2(7-col,7-row) : new Vector2(col,row);
                _board[col,row]?.Draw(spriteBatch, cell*CellSize*scale + drawPosition, scale);
            }
        }

        if (_selectedPiece != null)
        {
            Point cell = _selectedPiece.Value;
            Vector2 cellPos = flipped ? new Vector2(7-cell.X,7-cell.Y) : new Vector2(cell.X,cell.Y);
            _cellOverlay.Color = Color.White * 0.5f;
            _cellOverlay.Scale = new Vector2(scale, scale) * CellSize;
            _cellOverlay.Draw(spriteBatch, cellPos*CellSize*scale + drawPosition);
        }
    }

    private Point? GetSelectedPoint(Vector2 mousePosition, Vector2 boardPosition, float scale, bool flipped)
    {
        int row = (int)((mousePosition.Y-boardPosition.Y)/CellSize/scale);
        int col = (int)((mousePosition.X-boardPosition.X)/CellSize/scale);
        
        if (row < 0 || row > 7 || col < 0 || col > 7)
        {
            return null;
        }

        if (flipped)
        {
            row = 7 - row;
            col = 7 - col;
        }

        return new Point(col, row);
    }

    private void MovePiece(Point to, Point from)
    {
        _board[to.X,to.Y] = _board[from.X, from.Y];
        _board[from.X, from.Y] = null;
    }
}