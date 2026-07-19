using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameLibrary;
using GameLibrary.Graphics;
using System;
using System.Collections.Generic;
using System.Data;

namespace SimpleChess.Logic;

public class ChessBoard
{
    private bool _isWhiteTurn;
    public GameState State {get; private set;}
    public const int CELLSIZE = 150;
    private TextureAtlas _textureAtlas;
    private TextureRegion _texture {get; init;}
    private Sprite _boardSprite {get; init;}
    private Sprite _cellOverlay {get; set;}
    private ChessPiece[,] _board;
    private Point? _selectedPiece;
    private List<Point> _selectedLegalMoves;

    // Provides original pixel width, regardless of current scale of sprite
    public float BaseWidth => _texture.Width;
    public float BaseHeight => _texture.Height;
    
    public ChessBoard(TextureAtlas atlas)
    {
        _textureAtlas = atlas;
        _texture = atlas.GetRegion("board");
        _boardSprite = new Sprite(_texture);

        TextureRegion square = atlas.GetRegion("cellOverlay");
        _cellOverlay = new Sprite(square);

        Reset();
    }

    public void Reset()
    {
        _board = InitializeChessBoard(_textureAtlas);
        
        _selectedPiece = null;
        _selectedLegalMoves = new List<Point>();

        _isWhiteTurn = true;
        State = GameState.Playing;
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
            _selectedLegalMoves.Clear();
            return;
        }

        // A new square is being selected
        if (_selectedPiece == null)
        {
            Point newPoint = selectedPoint.Value;
            // Only allow the selecting of non-empty squares that contain the turn player's pieces
            if (_board[newPoint.X,newPoint.Y] != null && _board[newPoint.X,newPoint.Y].IsWhite == _isWhiteTurn)
                SelectPiece(selectedPoint.Value);
        } else
        {
            //TODO: validate the move using the selected piece's own method
            ChessPiece piece = _board[_selectedPiece.Value.X,_selectedPiece.Value.Y];
            if (piece.GetLegalMoves(_board,_selectedPiece.Value).Contains(selectedPoint.Value))
                MovePiece(selectedPoint.Value, _selectedPiece.Value);

            _selectedPiece = null;
            _selectedLegalMoves.Clear();
        }

    }

    public void Draw(SpriteBatch spriteBatch, Vector2? position = null, float scale = 0.5f, bool flipped = false)
    {
        Vector2 drawPosition = position ?? Vector2.Zero;

        // Draw chess board
        _boardSprite.Scale = new Vector2(scale,scale);
        if (flipped)
            _boardSprite.Effects = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
        else
            _boardSprite.Effects = SpriteEffects.None;
        _boardSprite.Draw(spriteBatch, drawPosition);

        // Draw all pieces
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                Vector2 cell = flipped ? new Vector2(7-col,7-row) : new Vector2(col,row);
                _board[col,row]?.Draw(spriteBatch, cell*CELLSIZE*scale + drawPosition, scale);
            }
        }

        if (_selectedPiece != null)
        {
            // Draw selected piece overlay
            Point cell = _selectedPiece.Value;
            Vector2 cellPos = flipped ? new Vector2(7-cell.X,7-cell.Y) : new Vector2(cell.X,cell.Y);
            _cellOverlay.Color = Color.White * 0.25f;
            _cellOverlay.Scale = new Vector2(scale, scale) * CELLSIZE;
            _cellOverlay.Draw(spriteBatch, cellPos*CELLSIZE*scale + drawPosition);

            // Draw legal moves overlay
            foreach (Point move in _selectedLegalMoves)
            {
                Vector2 movePos = flipped ? new Vector2(7-move.X,7-move.Y) : new Vector2(move.X,move.Y);
                _cellOverlay.Color = Color.Green * 0.25f;
                _cellOverlay.Draw(spriteBatch, movePos*CELLSIZE*scale + drawPosition);
            }
        }

    }

    public void DrawWinScreen(SpriteBatch spriteBatch, Vector2 boardPosition, float scale, GameState State)
    {
        _cellOverlay.Color = Color.Gray * 0.75f;
        _cellOverlay.Scale = new Vector2(scale*CELLSIZE*8);
        _cellOverlay.Draw(spriteBatch, boardPosition);

        // If there was a winner
        if (State == GameState.WhiteWin || State == GameState.BlackWin)
        {
            string kingName = State == GameState.WhiteWin ? "whiteKing" : "blackKing";
            Sprite winKing = new Sprite(_textureAtlas.GetRegion(kingName));
            winKing.CenterOrigin();
            winKing.Scale = new Vector2(scale,scale) * 4;
            Vector2 boardCenter = boardPosition + new Vector2(_boardSprite.Width,_boardSprite.Height) / 2;
            winKing.Draw(spriteBatch, boardCenter);

            // TODO: Add Winning Text
        }

    }

    private Point? GetSelectedPoint(Vector2 mousePosition, Vector2 boardPosition, float scale, bool flipped)
    {
        int row = (int)((mousePosition.Y-boardPosition.Y)/CELLSIZE/scale);
        int col = (int)((mousePosition.X-boardPosition.X)/CELLSIZE/scale);
        
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

    private void SelectPiece(Point cell)
    {
        _selectedPiece = cell;
        ChessPiece piece = _board[cell.X,cell.Y];
        _selectedLegalMoves.Clear();
        _selectedLegalMoves = piece.GetLegalMoves(_board, cell);
    }

    private void MovePiece(Point to, Point from)
    {

        // Change the gamestate if the captured piece was a king
        if (_board[to.X,to.Y] != null)
        {
            ChessPiece capturedPiece = _board[to.X,to.Y];
            if (capturedPiece is King)
            {
                State = capturedPiece.IsWhite ? GameState.BlackWin : GameState.WhiteWin;
            }
        }

        // Check for Pawn Promotion
        if (_board[from.X,from.Y] is Pawn)
        {
            ChessPiece pawn = _board[from.X,from.Y];
            int promotionRank = pawn.IsWhite ? 0 : 7;
            if (to.Y == promotionRank)
            {
                // Auto-promote pawns to queens
                _board[from.X,from.Y] = new Queen(pawn.IsWhite, _textureAtlas);
            }
        }

        // Move the Piece
        _board[to.X,to.Y] = _board[from.X, from.Y];
        _board[from.X, from.Y] = null;

        // TODO: Calculate Check

        // Swap Turns
        _isWhiteTurn = !_isWhiteTurn;
    }
}