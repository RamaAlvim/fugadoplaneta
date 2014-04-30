using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace fugadoplaneta
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D backgroundTexture;
        Rectangle vistaRect;
        SpriteFont Fonte;
        Animacao gohan;
        Animacao FRIZA;
        Animacao bolaFogo;
        KeyboardState kbState;
        Vector2 FontPos;
        Random random = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            // Altera o vídeo para 800 pixels de largura
            graphics.PreferredBackBufferWidth = 800;
            // Altera o vídeo para 600 pixels de altura
            graphics.PreferredBackBufferHeight = 600;

            graphics.IsFullScreen = true;

            IsMouseVisible = true;

            graphics.ApplyChanges();

            Window.Title = "Fuga do Planeta";
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //metodos do fundo de tela
            backgroundTexture = Content.Load<Texture2D>(@"volcano");
            vistaRect = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);

            // TODO: use this.Content to load your game content here

            gohan = new Animacao(Content.Load<Texture2D>("gohan3"), 3, 4);
            gohan.AddAnimation("Up", 1);
            gohan.AddAnimation("Right", 2);
            gohan.AddAnimation("Down", 3);
            gohan.AddAnimation("Left", 4);
            gohan.Animation = "Right";
            gohan.Position = new Vector2(400, 490);
            gohan.IsLooping = true;
            gohan.FramesPerSecond = 15;
            //frieza
            FRIZA = new Animacao(Content.Load<Texture2D>("FRIEZASPRITE"), 3, 2);
            FRIZA.AddAnimation("Right", 1);
            FRIZA.AddAnimation("Left", 2);
            FRIZA.Animation = "Right";
            FRIZA.Position = new Vector2(200, 0);
            FRIZA.IsLooping = true;
            FRIZA.FramesPerSecond = 15;
            //bola de Fogo
            bolaFogo = new Animacao(Content.Load<Texture2D>("Bolafogo1"), 2, 1);
            bolaFogo.AddAnimation("expl", 1);
            bolaFogo.Animation = "expl";
            bolaFogo.Position = new Vector2(300, 390);
            bolaFogo.IsLooping = true;
            bolaFogo.FramesPerSecond = 15;
            //fonte
            Fonte = Content.Load<SpriteFont>("Courier New");
            FontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            kbState = Keyboard.GetState();
            if (kbState.IsKeyDown(Keys.Left))
            {
                if (gohan.Position.X > 2)
                {
                    gohan.Animation = "Left";
                    gohan.Position.X -= 2;
                }
            }
            else if (kbState.IsKeyDown(Keys.Right))
            {
                if (gohan.Position.X < 768)
                {
                    gohan.Animation = "Right";
                    gohan.Position.X += 2;
                }
            }
            else if (kbState.IsKeyDown(Keys.Up))
            {
                if (gohan.Position.Y > 476)
                {
                    gohan.Animation = "Up";
                    gohan.Position.Y -= 2;
                }
            }
            else if (kbState.IsKeyDown(Keys.Down))
            {
                if (gohan.Position.Y < 542)
                {
                    gohan.Animation = "Down";
                    gohan.Position.Y += 2;
                }
            }

            //friza fica indo pra lá e pra cá
            //friza vai pra direita
            if (FRIZA.Animation == "Right" && FRIZA.Position.X < 600)
            {
                FRIZA.Position.X += 2;
            }
            else if (FRIZA.Animation == "Right" && FRIZA.Position.X == 600)
            {
                FRIZA.Animation = "Left";
                FRIZA.Position.X -= 2;
            }
            else if (FRIZA.Animation == "Left" && FRIZA.Position.X > 50)
            {
                FRIZA.Position.X -= 2;
            }
            else if (FRIZA.Animation == "Left" && FRIZA.Position.X == 50)
            {
                FRIZA.Animation = "Right";
                FRIZA.Position.X += 2;
            }

            //bolafogo
            if (FRIZA.Animation == "Right")
            {
                bolaFogo.Position.Y += 1;
            }
            else if (FRIZA.Animation == "Left")
            {
                bolaFogo.Position.Y -= 1;
            }

            FRIZA.Update(gameTime);
            bolaFogo.Update(gameTime);

            gohan.Update(gameTime);
            // TODO: Add your update logic here


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            string output = "X: " + FRIZA.Position.X + " Y: " + FRIZA.Position.Y;
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, vistaRect, Color.White);
            gohan.Draw(spriteBatch);
            FRIZA.Draw(spriteBatch);
            bolaFogo.Draw(spriteBatch);
            Vector2 FontOrigin = Fonte.MeasureString(output) / 2;
            //spriteBatch.DrawString(Fonte, output, FontPos, Color.LightGreen, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);



            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
