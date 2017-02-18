using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fonts_black_borders
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public const string TEST_TEXT = "The quick brown fox jumped over the lazy dog.";

        private readonly string[] fonts = { "folks" , "TitleMenuNormal", "TitleMenuNormal1"};
        private Dictionary<string, SpriteFont> Fonts { get; } = new Dictionary<string, SpriteFont>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 968;
            Content.RootDirectory = "Content";
        }
        
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (var f in fonts)
            {
                Fonts.Add(f, Content.Load<SpriteFont>(f));
            }
        }
        
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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

            Vector2 p = new Vector2(10, 10);
            p = DrawText(SamplerState.AnisotropicClamp, p);
            p = DrawText(SamplerState.LinearClamp, p);
            p = DrawText(SamplerState.PointClamp, p);

            base.Draw(gameTime);
        }

        private Vector2 DrawText(SamplerState samplerState, Vector2 p)
        {
            p = DrawHeading(samplerState.ToString(), p, .6f);
            p = DrawText(samplerState, p, new Vector2(.2f, .2f));
            p = DrawText(samplerState, p, Vector2.One);
            return DrawText(samplerState, p, new Vector2(1.1f, 1.1f));
        }

        private Vector2 DrawHeading(string t, Vector2 p, float size)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp,
                DepthStencilState.None, RasterizerState.CullNone);
            Vector2 s = Fonts["folks"].MeasureString(t);
            spriteBatch.DrawString(Fonts["folks"], t, p, Color.MonoGameOrange, 0f, Vector2.Zero, new Vector2(size, size),
                SpriteEffects.None, 1);
            spriteBatch.End();
            p += new Vector2(0, 5 + s.Y * size);
            return p;
        }

        private Vector2 DrawText(SamplerState samplerState, Vector2 p, Vector2 z)
        {
            p = DrawHeading($"size: {z}", p, .4f);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, samplerState,
                DepthStencilState.None, RasterizerState.CullNone);
            foreach (var f in Fonts)
            {
                SpriteFont font = f.Value;
                string t = $"{f.Key}: {TEST_TEXT}";
                Vector2 s = font.MeasureString(t);

                spriteBatch.DrawString(font, t, p, Color.White, 0f, Vector2.Zero, z,
                    SpriteEffects.None, 1);
                p += new Vector2(0, 5 + s.Y * z.Y);
            }
            spriteBatch.End();
            return p;
        }
    }
}
