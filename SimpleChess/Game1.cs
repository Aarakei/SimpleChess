using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameLibrary;
using GameLibrary.Graphics;
using System;
using SimpleChess.Logic;

namespace SimpleChess;

public class Game1 : Core
{
    // private TextureRegion _board;
    // private TextureRegion _pawnWhite;
    // private TextureRegion _rookWhite;
    // private TextureRegion _knightWhite;
    // private TextureRegion _bishopWhite;
    // private TextureRegion _queenWhite;
    // private TextureRegion _kingWhite;
    // private TextureRegion _pawnBlack;
    // private TextureRegion _rookBlack;
    // private TextureRegion _knightBlack;
    // private TextureRegion _bishopBlack;
    // private TextureRegion _queenBlack;
    // private TextureRegion _kingBlack;
    private ChessBoard _chessBoard;
    
    public Game1() : base("SimpleChess", 1280, 720, false)
    {
        
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        

        base.Initialize(); // LoadContent is called at the end of this function call

        // Initialization that depends on content being loaded goes here
    }

    protected override void LoadContent()
    {

        // Load the chess spritesheet (atlas)
        Texture2D atlasTexture = Content.Load<Texture2D>("images/chess_wood");
        // TextureAtlas atlas = new TextureAtlas(atlasTexture);

        _chessBoard = new ChessBoard(atlasTexture);

        // Add board and pawns/pieces to the atlass
        // atlas.AddRegion("board",1,5,1200,1200);
        // atlas.AddRegion("bishopBlack",1,1211,150,150);
        // atlas.AddRegion("kingBlack",152,1211,150,150);
        // atlas.AddRegion("knightBlack",303,1211,150,150);
        // atlas.AddRegion("pawnBlack",454,1211,150,150);
        // atlas.AddRegion("queenBlack",605,1211,150,150);
        // atlas.AddRegion("rookBlack",756,1211,150,150);
        // atlas.AddRegion("bishopWhite",1,1367,150,150);
        // atlas.AddRegion("kingWhite",152,1367,150,150);
        // atlas.AddRegion("knightWhite",303,1367,150,150);
        // atlas.AddRegion("pawnWhite",454,1367,150,150);
        // atlas.AddRegion("queenWhite",605,1367,150,150);
        // atlas.AddRegion("rookWhite",756,1367,150,150);

        // _board = atlas.GetRegion("board");
        // _bishopBlack = atlas.GetRegion("bishopBlack");
        // _bishopWhite = atlas.GetRegion("bishopWhite");
        // _kingBlack = atlas.GetRegion("kingBlack");
        // _kingWhite = atlas.GetRegion("kingWhite");
        // _knightBlack = atlas.GetRegion("knightBlack");
        // _knightWhite = atlas.GetRegion("knightWhite");
        // _pawnBlack = atlas.GetRegion("pawnBlack");
        // _pawnWhite = atlas.GetRegion("pawnWhite");
        // _queenBlack = atlas.GetRegion("queenBlack");
        // _queenWhite = atlas.GetRegion("queenWhite");
        // _rookBlack = atlas.GetRegion("rookBlack");
        // _rookWhite = atlas.GetRegion("rookWhite");

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.MonoGameOrange);

        float scale = 0.5f;

        // TODO: Add your drawing code here
        SpriteBatch.Begin();//samplerState: SamplerState.PointClamp);
        
        _chessBoard.Draw(SpriteBatch, scale);

        // _board.Draw(SpriteBatch, Vector2.Zero, Color.White, 0.0f, Vector2.One, scale, SpriteEffects.None, 0.0f);

        // _rookBlack.Draw(SpriteBatch, Vector2.Zero, Color.White, 0.0f, Vector2.One, scale, SpriteEffects.None, 0.0f);
        // _knightBlack.Draw(SpriteBatch, new Vector2(150,0)*scale, Color.White, 0.0f, Vector2.One, scale, SpriteEffects.None, 0.0f);
        // _bishopBlack.Draw(SpriteBatch, new Vector2(300,0)*scale, Color.White, 0.0f, Vector2.One, scale, SpriteEffects.None, 0.0f);
        // _queenBlack.Draw(SpriteBatch, new Vector2(450,0)*scale, Color.White, 0.0f, Vector2.One, scale, SpriteEffects.None, 0.0f);
        // _kingBlack.Draw(SpriteBatch, new Vector2(600,0)*scale, Color.White, 0.0f, Vector2.One, scale, SpriteEffects.None, 0.0f);
        // _bishopBlack.Draw(SpriteBatch, new Vector2(750,0)*scale, Color.White, 0.0f, Vector2.One, scale, SpriteEffects.None, 0.0f);
        // _knightBlack.Draw(SpriteBatch, new Vector2(900,0)*scale, Color.White, 0.0f, Vector2.One, scale, SpriteEffects.None, 0.0f);
        // _rookBlack.Draw(SpriteBatch, new Vector2(1050,0)*scale, Color.White, 0.0f, Vector2.One, scale, SpriteEffects.None, 0.0f);
        // for (int i = 0; i < 8; i++)
        // {
        //     _pawnBlack.Draw(SpriteBatch, new Vector2(150*i, 150)*scale, Color.White, 0.0f, Vector2.One, scale, SpriteEffects.None, 0.0f);
        // }

        // _rookWhite.Draw(SpriteBatch, new Vector2(0,1050)*scale, Color.White, 0.0f, Vector2.One, scale, SpriteEffects.None, 0.0f);
        // _bishopWhite.Draw(SpriteBatch, new Vector2(300,1050)*scale, Color.White, 0.0f, Vector2.One, scale, SpriteEffects.None, 0.0f);
        // _knightWhite.Draw(SpriteBatch, new Vector2(150,1050)*scale, Color.White, 0.0f, Vector2.One, scale, SpriteEffects.None, 0.0f);
        // _queenWhite.Draw(SpriteBatch, new Vector2(450,1050)*scale, Color.White, 0.0f, Vector2.One, scale, SpriteEffects.None, 0.0f);
        // _kingWhite.Draw(SpriteBatch, new Vector2(600,1050)*scale, Color.White, 0.0f, Vector2.One, scale, SpriteEffects.None, 0.0f);
        // _bishopWhite.Draw(SpriteBatch, new Vector2(750,1050)*scale, Color.White, 0.0f, Vector2.One, scale, SpriteEffects.None, 0.0f);
        // _knightWhite.Draw(SpriteBatch, new Vector2(900,1050)*scale, Color.White, 0.0f, Vector2.One, scale, SpriteEffects.None, 0.0f);
        // _rookWhite.Draw(SpriteBatch, new Vector2(1050,1050)*scale, Color.White, 0.0f, Vector2.One, scale, SpriteEffects.None, 0.0f);
        // for (int i = 0; i < 8; i++)
        // {
        //     _pawnWhite.Draw(SpriteBatch, new Vector2(150*i, 900)*scale, Color.White, 0.0f, Vector2.One, scale, SpriteEffects.None, 0.0f);
        // }

        // _pawnWhite.Draw(SpriteBatch,new Vector2(1200/2-150/2,1200/2-150/2)*.5f,Color.White);

        SpriteBatch.End();

        base.Draw(gameTime);
    }
}
