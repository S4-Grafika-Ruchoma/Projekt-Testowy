using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ProjektTestowy
{
    // TEST BY JOHN v1
    // TEST BY 191
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont DefaultFont;
        int maxinterval = 16;
        int interval;
        Vector2 textPos;
        Vector2 textSpeed;
        float rotation;
        Color Bkg;
        Random rnd;
        bool RotationDir;
        bool rBlock;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            this.IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            DefaultFont = Content.Load<SpriteFont>("SpriteFontPL");
            textPos = new Vector2((GraphicsDevice.Viewport.Width - DefaultFont.MeasureString("Sladu to pedał").X) / 2,
                                  (GraphicsDevice.Viewport.Height - DefaultFont.MeasureString("Sladu to pedał").Y) / 2);

            textSpeed = new Vector2(2.5f, 2.5f);
            rnd = new Random();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            interval += gameTime.ElapsedGameTime.Milliseconds;
            if (gameTime.TotalGameTime.Seconds % 5 != 0)
                rBlock = false;

            if (gameTime.TotalGameTime.Seconds % 5 == 0 && !rBlock)
            {
                RotationDir = !RotationDir;
                rBlock = true;
            }

            if (interval > maxinterval)
            {
                textPos += textSpeed;
                if (RotationDir)
                    rotation += MathHelper.Pi / 60;
                else
                    rotation -= MathHelper.Pi / 60;

                Bkg = new Color((float)rnd.NextDouble(),
                    (float)rnd.NextDouble(),
                    (float)rnd.NextDouble());
            }


            if (textPos.X >= GraphicsDevice.Viewport.Width)
                textSpeed.X = -2.5f;
            if (textPos.X <= 0)
                textSpeed.X = 2.5f;
            if (textPos.Y >= GraphicsDevice.Viewport.Height)
                textSpeed.Y = -2.5f;
            if (textPos.Y <= 0)
                textSpeed.Y = 2.5f;


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Bkg);

            // TODO: Add your drawing code here
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(DefaultFont, "Sladu to pedał", textPos, Color.Black, rotation, new Vector2(DefaultFont.MeasureString("Sladu to pedał").X / 2, DefaultFont.MeasureString("Sladu to pedał").Y / 2), 1, new SpriteEffects(), 1);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
