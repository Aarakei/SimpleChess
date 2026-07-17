using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameLibrary;
using GameLibrary.Graphics;
using System;
using SimpleChess.Logic;
using Microsoft.Xna.Framework.Content;

namespace SimpleChess;

public class Game1 : Core
{
    private float _scale;
    private Vector2 _boardPosition;
    private bool _boardFlipped;
    private ChessBoard _chessBoard;
    private MouseState _mouseState;
    
    public Game1() : base("SimpleChess", 1280, 720, false)
    {
        
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize(); // LoadContent is called at the end of this function call

        // Initialization that depends on content being loaded goes here
        PositionBoard();
        _boardFlipped = false;
    }

    protected override void LoadContent()
    {

        // Load the chess spritesheet (atlas)
        // Texture2D atlasTexture = Content.Load<Texture2D>("images/chess_wood");
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "images/chess_wood.xml");

        _chessBoard = new ChessBoard(atlas);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        _mouseState = Mouse.GetState();
        _boardFlipped = _mouseState.LeftButton == ButtonState.Pressed;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        
        SpriteBatch.Begin();
        
        _chessBoard.Draw(
                        SpriteBatch,
                        _boardPosition,
                        _scale,
                        _boardFlipped
                        );

        SpriteBatch.End();

        base.Draw(gameTime);
    }

    protected override void OnWindowResize(object sender, EventArgs e)
    {
        base.OnWindowResize(sender, e);

        PositionBoard();
    }

    private void PositionBoard()
    {
        // Scale the board to the maximum allowable size within the window
        if (Window.ClientBounds.Height < Window.ClientBounds.Width)
            _scale = Window.ClientBounds.Height / _chessBoard.BaseHeight;
        else
            _scale = Window.ClientBounds.Width / _chessBoard.BaseWidth;

        // Calculate the board's standard position
        _boardPosition = new Vector2((Window.ClientBounds.Width - _chessBoard.BaseWidth*_scale)/2,
                                     (Window.ClientBounds.Height - _chessBoard.BaseHeight*_scale)/2);
    }

}
