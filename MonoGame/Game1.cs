using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MonoGame
{
    public class Game1 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Input input;
        Background background;
        Spaceship spaceship;            
        List<Enemy> enemies = new List<Enemy>(); 
        Texture2D[] enemies_textures = new Texture2D[4];
        int Timer = 0;
        Jweel jweel;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1860;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            IsMouseVisible = true;

            input = new Input(Keys.Left, Keys.Right, Keys.Up, Keys.Down, Keys.W);

            Texture2D[] background_texture = new Texture2D[4];

            background_texture[0] = Content.Load<Texture2D>("Stars");
            background_texture[1] = Content.Load<Texture2D>("Nebula1");
            background_texture[2] = Content.Load<Texture2D>("Nebula2");
            background_texture[3] = Content.Load<Texture2D>("Nebula3");
            background = new Background(background_texture);

            Texture2D[] spaceship_texture = new Texture2D[4];
            spaceship_texture[0] = Content.Load<Texture2D>("redfighter0005 - Copy (3)");
            spaceship_texture[1] = Content.Load<Texture2D>("redfighter0005 - Copy");
            spaceship_texture[2] = Content.Load<Texture2D>("redfighter0005");
            spaceship_texture[3] = Content.Load<Texture2D>("redfighter0005 - Copy (2)");
            SpriteFont font = Content.Load<SpriteFont>("File"); // Use the name of your sprite font file here instead of 'Score'.
            spaceship = new Spaceship(spaceship_texture, input, font);
          
            enemies_textures[0]= Content.Load<Texture2D>("8");
            enemies_textures[1] = Content.Load<Texture2D>("6");
            enemies_textures[2] = Content.Load<Texture2D>("1");
            enemies_textures[3] = Content.Load<Texture2D>("12");
            for (int a = 0; a != 4; a++)
            {             
               Make_Enemies(enemies_textures[a], a);              
            }

            Texture2D[] jweels_textures = new Texture2D[3];

            jweels_textures[0] = Content.Load<Texture2D>("redjweel");
            jweels_textures[1] = Content.Load<Texture2D>("bluejweel");
            jweels_textures[2] = Content.Load<Texture2D>("yellowjweel");
            jweel = new Jweel(jweels_textures, font);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Timer++;

            if (Timer <= 10000)
            {
                background.Move();

                spaceship.Move(enemies, Content.Load<Texture2D>("rocket_1_0000"));
                spaceship.Update_Spaceship_Bullets();

                foreach (Enemy e in enemies)
                {
                    e.Move(spaceship.spaceship_bullets, Content.Load<Texture2D>("Bullet"));
                }
            }

            jweel.Update(spaceship);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            if (Timer <= 10000)
            {
                background.Draw(spriteBatch);

                spaceship.Draw(spriteBatch);

                foreach (Enemy e in enemies)
                {
                    e.Draw(spriteBatch);
                }
            }
            else
            {               
                spriteBatch.DrawString(spaceship.Font, "Your Final Score Is " + spaceship.Score, new Vector2(100, 100), Color.White);
                spriteBatch.DrawString(spaceship.Font, "Numer of Jweels " + jweel.n, new Vector2(100, 200), Color.White);
            }

            jweel.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
          
        public void Make_Enemies(Texture2D enemy_texture, int a)
        {
            Enemy enemy = new Enemy(enemy_texture, a);

            if (enemies.Count < 20)
            {
                enemies.Add(enemy);
            }
        }

        public void Remove_Enemies()
        {
            for (int a = 0; a < enemies.Count; a++)
            {
                if (enemies[a].Enemy_IsHit || enemies[a].Enemy_Position.Y + enemies[a].Enemy_Size.Y >= 1442)
                {
                    enemies.RemoveAt(a);
                    a--;
                }
            }
        }
    }
}