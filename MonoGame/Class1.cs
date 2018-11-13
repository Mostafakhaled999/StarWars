using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MonoGame
{
    public class Input
    {
        public Keys Left { get; set; }
        public Keys Right { get; set; }
        public Keys Up { get; set; }
        public Keys Down { get; set; }
        public Keys Shoot { get; set; }

        public Input(Keys l, Keys r, Keys u, Keys d, Keys s)
        {
            Left = l;
            Right = r;
            Up = u;
            Down = d;
            Shoot = s;
        }
    }

    public class Background
    {
        public Vector2[] Background_Size = new Vector2[5];
        public Vector2[] Background_Position = new Vector2[5];
        public float[] Backround_Speed = new float[2];
        public Texture2D[] Background_Texture = new Texture2D[5];
        public bool[] Background_Move = new bool[5];

        public Background(Texture2D []background_texture)
        {
            Background_Texture[0] = background_texture[0];
            Background_Texture[1] = background_texture[0];
            Background_Texture[2] = background_texture[1];
            Background_Texture[3] = background_texture[2];
            Background_Texture[4] = background_texture[3];
            Background_Size[0].X = Background_Texture[0].Width;
            Background_Size[0].Y = Background_Texture[0].Height;
            Background_Size[2].X = Background_Texture[2].Width;
            Background_Size[2].Y = Background_Texture[2].Height;
            Background_Size[3].X = Background_Texture[3].Width;
            Background_Size[3].Y = Background_Texture[3].Height;
            Background_Size[4].X = Background_Texture[4].Width;
            Background_Size[4].Y = Background_Texture[4].Height;
            Background_Position[0].X = (1860 / 2) - (Background_Size[0].X / 2);//418
            Background_Position[0].Y = 0;
            Background_Position[1].X = (1860 / 2) - (Background_Size[0].X / 2);//418
            Background_Position[1].Y = 1000;
            Background_Position[2].X = (1860 / 2) - (Background_Size[2].X / 2);
            Background_Position[2].Y = 0;
            Background_Position[3].X = (1860 / 2) - (Background_Size[3].X / 2);
            Background_Position[3].Y =  1000;
            Background_Position[4].X = (1860 / 2) - (Background_Size[4].X / 2);
            Background_Position[4].Y = 1000;
            Background_Move[0] = true;
            Background_Move[1] = true;
            Background_Move[2] = true;
            Background_Move[3] = false;
            Background_Move[4] = false;
            Backround_Speed[0] = 1;
            Backround_Speed[1] = 2;
        }

        public void Move()
        {

            if (Background_Position[0].Y <= -1000)
            {
                Background_Move[1] = true;
                Background_Move[0] = false;
            }

            if (Background_Position[1].Y <= -1000)
            {
                Background_Move[1] = false;
                Background_Move[0] = true;
            }

            if (Background_Move[0])
                Background_Position[0].Y -= Backround_Speed[0];
            else
            {
                Background_Position[0].Y = 1000;
                Background_Move[0] = true;
            }

            if (Background_Move[1])
                Background_Position[1].Y -= Backround_Speed[0];
            else {
                Background_Position[1].Y = 1000;
                Background_Move[1] = true;
            }


            if (Background_Position[2].Y <= -1000)
            {
                Background_Move[2] = false;
            }

            if (Background_Position[3].Y <= -1000)
            {
                Background_Move[3] = false;
            }

            if (Background_Position[4].Y <= -1000)
            {
                Background_Move[4] = false;
            }

            if (Background_Move[2])
                Background_Position[2].Y -= Backround_Speed[1];
            else
            {
                Background_Position[2].Y = 1000;
                Background_Move[3] = true;
            }

            if (Background_Move[3])
                Background_Position[3].Y -= Backround_Speed[1];
            else
            {
                Background_Position[3].Y = 1000;
                Background_Move[4] = true;
            }

            if (Background_Move[4])
                Background_Position[4].Y -= Backround_Speed[1];
            else
            {
                Background_Position[4].Y = 1000;
                Background_Move[2] = true;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {            
            spriteBatch.Draw(Background_Texture[0], Background_Position[0], null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Background_Texture[1], Background_Position[1], null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Background_Texture[2], Background_Position[2], null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Background_Texture[3], Background_Position[3], null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Background_Texture[4], Background_Position[4], null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }

    public class Spaceship
    {
        public Input Spaceship_Input;
        public Texture2D[] Spaceship_Texture = new Texture2D[4];
        public Vector2 Spaceship_Position;
        public Vector2 Spaceship_Size;
        public Vector2 Spaceship_Origin;
        public float Spaceship_Speed;
        public Rectangle Spaceship_BoundingBox
        {
            get
            {
                return new Rectangle((int)Spaceship_Position.X, (int)Spaceship_Position.Y, (int)Spaceship_Size.X, (int)Spaceship_Size.Y);
            }
        }
        public List<Bullet> spaceship_bullets = new List<Bullet>();
        KeyboardState pastkey;
        public int Score = 0;
        public SpriteFont Font;
        bool right, left, up, down;

        public Spaceship(Texture2D[] spaceship_texture, Input input, SpriteFont font)
        {
            Spaceship_Input = input;
            Spaceship_Texture[0] = spaceship_texture[0];
            Spaceship_Texture[1] = spaceship_texture[1];
            Spaceship_Texture[2] = spaceship_texture[2];
            Spaceship_Texture[3] = spaceship_texture[3];
            Spaceship_Speed = 7;
            Spaceship_Size.X = (float)(Spaceship_Texture[0].Width * .2);
            Spaceship_Size.Y = (float)(Spaceship_Texture[0].Height * .2);
            Spaceship_Position.X = (1860 / 2) - Spaceship_Size.X;
            Spaceship_Position.Y = (1000 / 2) - Spaceship_Size.Y;
            Spaceship_Origin = new Vector2(Spaceship_Texture[0].Width / 2, Spaceship_Texture[0].Height / 2);
            Font = font;
            right = true;
        }
     
        public void Move(List<Enemy> enemies, Texture2D bullet_texture)
        {
          
            if (Spaceship_Input == null)
                return;

            //if (Keyboard.GetState().IsKeyDown(Spaceship_Input.Up) && Spaceship_Position.Y >= 0)
            //{
            //    Spaceship_Position.Y -= Spaceship_Speed;
            //}
            //if (Keyboard.GetState().IsKeyDown(Spaceship_Input.Down) && Spaceship_Position.Y + Spaceship_Size.Y <= 1000)
            //{
            //    Spaceship_Position.Y += Spaceship_Speed;
            //}
            //if (Keyboard.GetState().IsKeyDown(Spaceship_Input.Left) && Spaceship_Position.X >= 418)
            //{
            //    Spaceship_Position.X -= Spaceship_Speed;
            //}
            //if (Keyboard.GetState().IsKeyDown(Spaceship_Input.Right) && Spaceship_Position.X + Spaceship_Size.X <= 1442)
            //{
            //    Spaceship_Position.X += Spaceship_Speed;
            //}
            //if (Keyboard.GetState().IsKeyDown(Keys.Space) && pastkey.IsKeyUp(Keys.Space))
            //{
            //    //Shoot_Spaceship_Bullets(bullet_texture);
            //}

            if (Keyboard.GetState().IsKeyDown(Spaceship_Input.Right))
            {
                right = true;
                left = false;
                up = false;
                down = false;
            }

            if (Keyboard.GetState().IsKeyDown(Spaceship_Input.Left))
            {
                right = false;
                left = true;
                up = false;
                down = false;
            }

            if (Keyboard.GetState().IsKeyDown(Spaceship_Input.Up))
            {
                right = false;
                left = false;
                up = true;
                down = false;
            }

            if (Keyboard.GetState().IsKeyDown(Spaceship_Input.Down))
            {
                right = false;
                left = false;
                up = false;
                down = true;
            }

            if (right)
            {
                Spaceship_Position.X += Spaceship_Speed;
            }

            if (left)
            {
                Spaceship_Position.X -= Spaceship_Speed;
            }

            if (up)
            {
                Spaceship_Position.Y -= Spaceship_Speed;
            }

            if (down)
            {
                Spaceship_Position.Y += Spaceship_Speed;
            }

            if(Spaceship_Position.Y <= 0 + enemies[0].Enemy_Size.X)
            {
                up = false;
                down = true;
            }

            if (Spaceship_Position.X + Spaceship_Size.X >= 1442 - enemies[1].Enemy_Size.X)
            {
                right = false;
                left = true;
            }

            if (Spaceship_Position.X <= 418 + enemies[2].Enemy_Size.X)
            {
                left = false;
                right = true;
            }

            if(Spaceship_Position.Y + Spaceship_Size.Y >= 1000 - enemies[3].Enemy_Size.Y)
            {
                down = false;
                up = true;
            }

            pastkey = Keyboard.GetState();

            foreach (Enemy e in enemies)
            {
                foreach (Bullet b in e.Enemy_bullets)
                {
                    if (Spaceship_BoundingBox.Intersects(b.Bullet_BoundingBox))
                    {
                        Score -= 100;
                        b.Bullet_IsVisable = false;
                    }
                }
            }

            foreach (Enemy e in enemies)
            {
                if (Spaceship_BoundingBox.Intersects(e.Enemy_BoundingBox))
                {
                    Score -= 100;
                    if (e.Enemy_Pattern == 0)
                    {
                        up = false;
                        down = true;
                    }

                    if (e.Enemy_Pattern == 1)
                    {
                        right = false;
                        left = true;
                    }

                    if (e.Enemy_Pattern == 2)
                    {
                        left = false;
                        right = true;
                    }

                    if (e.Enemy_Pattern == 3)
                    {
                        down = false;
                       up = true;
                    }
                }
            }

            Score++;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (right)
            {
                spriteBatch.Draw(Spaceship_Texture[0], Spaceship_Position, null, Color.Gold, 0, Vector2.Zero, .2f, SpriteEffects.None, 0f);
            }
            if (left)
            {
                spriteBatch.Draw(Spaceship_Texture[1], Spaceship_Position, null, Color.Gold, 0, Vector2.Zero, .2f, SpriteEffects.None, 0f);
            }
            if (up)
            {
                spriteBatch.Draw(Spaceship_Texture[2], Spaceship_Position, null, Color.Gold, 0, Vector2.Zero, .2f, SpriteEffects.None, 0f);
            }
            if (down)
            {
                spriteBatch.Draw(Spaceship_Texture[3], Spaceship_Position, null, Color.Gold, 0, Vector2.Zero, .2f, SpriteEffects.None, 0f);
            }
          
            foreach (Bullet bullet in spaceship_bullets)
            {
                bullet.Draw(spriteBatch);
            }

            spriteBatch.DrawString(Font, "Score " + Score, new Vector2(100, 100), Color.White);

        }

        public void Update_Spaceship_Bullets()
        {
            foreach (Bullet bullet in spaceship_bullets)
            {
                bullet.Bullet_Position -= bullet.Bullet_Velocity;
                if (bullet.Bullet_Position.Y <= 0)
                {
                    bullet.Bullet_IsVisable = false;
                }
            }
            for (int i = 0; i < spaceship_bullets.Count; i++)
            {
                if (!spaceship_bullets[i].Bullet_IsVisable)
                {
                    spaceship_bullets.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Shoot_Spaceship_Bullets(Texture2D bullet_texture)
        {
            Bullet bullet = new Bullet(bullet_texture);
            bullet.Bullet_Position = Spaceship_Position;
            bullet.Bullet_Position.X += Spaceship_Size.X / 2 - bullet.Bullet_Size.X / 2;
            bullet.Bullet_Velocity *= Spaceship_Speed; ;
            bullet.Bullet_IsVisable = true;

            if (spaceship_bullets.Count < 20)
            {
                spaceship_bullets.Add(bullet);
            }
        }
    }

    public class Bullet
    {
        public Texture2D Bullet_Texture;
        public Vector2 Bullet_Position;
        public Vector2 Bullet_Size;
        public Vector2 Bullet_Velocity;
        public bool Bullet_IsVisable;

        public Rectangle Bullet_BoundingBox
        {
            get
            {
                return new Rectangle((int)Bullet_Position.X, (int)Bullet_Position.Y, (int)Bullet_Size.X, (int)Bullet_Size.Y);
            }
        }

        public Bullet(Texture2D bullet_texture)
        {
            Bullet_Texture = bullet_texture;
            Bullet_IsVisable = false;
            Bullet_Size.X = (float)(Bullet_Texture.Width * 2.5);
            Bullet_Size.Y = (float)(Bullet_Texture.Height * 2.5);
            Bullet_Velocity.X = 3;
            Bullet_Velocity.Y = 3;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Bullet_Texture, Bullet_Position, null, Color.Gold, 0, Vector2.Zero, 2.5f, SpriteEffects.None, 0f);
        }
    }

    public class Enemy 
    {
        public Texture2D Enemy_Texture;
        public Vector2 Enemy_Position;
        public Vector2 Enemy_Size;
        public Vector2 Enemy_Origin;
        public float Enemy_Speed;
        public int Enemy_Pattern;
        public bool Enemy_IsLeft;
        public bool Enemy_IsRight;
        public bool Enemy_IsHit;
        public Rectangle Enemy_BoundingBox
	    {      
         get
	      {
           return new Rectangle((int)Enemy_Position.X, (int)Enemy_Position.Y, (int)Enemy_Size.X, (int)Enemy_Size.Y);
          }
        }
        public List<Bullet> Enemy_bullets = new List<Bullet>();
        int timer = 0;
        int shoot;

        public Enemy(Texture2D enemy_texture, int enemy_pattern)
        {
            Enemy_Texture = enemy_texture;
            Enemy_Size.X = (float)(Enemy_Texture.Width * .5);
            Enemy_Size.Y = (float)(Enemy_Texture.Height * .5);
            Enemy_Origin.X = Enemy_Size.X / 2; 
            Enemy_Origin.Y = Enemy_Size.Y / 2;
            Enemy_IsHit = false;
            Enemy_Pattern = enemy_pattern;
            if(Enemy_Pattern == 0)
            {
                Enemy_IsLeft = true;
                Enemy_IsRight = false;
                Enemy_Position.X = (1860 / 2) - Enemy_Origin.X;
                Enemy_Position.Y = 0;
                Enemy_Speed = 3;
                shoot = 250;
            }

            if (Enemy_Pattern == 1)
            {
                Enemy_IsLeft = false;
                Enemy_IsRight = true;
                Enemy_Position.X = 1442 - Enemy_Origin.X; 
                Enemy_Position.Y = (1000 / 2) - Enemy_Origin.X; 
                Enemy_Speed = 3;
                shoot = 200;
            }

            if (Enemy_Pattern == 2)
            {
                Enemy_IsLeft = true;
                Enemy_IsRight = false;
                Enemy_Position.X = 418;
                Enemy_Position.Y = (1000 / 2) - Enemy_Origin.X;
                Enemy_Speed = 3;
                shoot = 150;
            }

            if (Enemy_Pattern == 3)
            {
                Enemy_IsLeft = false;
                Enemy_IsRight = true;
                Enemy_Position.X = (1860 / 2) - Enemy_Origin.X;
                Enemy_Position.Y = 1000 - Enemy_Size.Y - 10;
                Enemy_Speed = 3;
                shoot = 100;
            }
        }

        public void Move(List<Bullet> bullets, Texture2D bullet_texture)
        {           
            if (Enemy_Pattern == 0)
            {                              
                if (Enemy_IsLeft)
                {
                    Enemy_Position.X -= Enemy_Speed;
                }

                if (Enemy_IsRight)
                {
                    Enemy_Position.X += Enemy_Speed;
                }    
                
                if (Enemy_Position.X <= 418)
                {
                    Enemy_IsRight = true;
                    Enemy_IsLeft = false;
                    Enemy_Speed += .05F;
                }

                if (Enemy_Position.X +Enemy_Size.X >= 1442)
                {
                    Enemy_IsLeft = true;
                    Enemy_IsRight = false;
                    Enemy_Speed += .05F;
                }               
            }

            if (Enemy_Pattern == 1)
            {
                if (Enemy_IsLeft)
                {
                    Enemy_Position.Y -= Enemy_Speed;
                }

                if (Enemy_IsRight)
                {
                    Enemy_Position.Y += Enemy_Speed;
                }

                if (Enemy_Position.Y <= 0)
                {
                    Enemy_IsRight = true;
                    Enemy_IsLeft = false;
                    Enemy_Speed += .1F;
                }

                if (Enemy_Position.Y + Enemy_Size.Y >= 1000)
                {
                    Enemy_IsLeft = true;
                    Enemy_IsRight = false;
                    Enemy_Speed += .1F;
                }             
            }

            if (Enemy_Pattern == 2)
            {
                if (Enemy_IsLeft)
                {
                    Enemy_Position.Y -= Enemy_Speed;
                }

                if (Enemy_IsRight)
                {
                    Enemy_Position.Y += Enemy_Speed;
                }

                if (Enemy_Position.Y <= 0)
                {
                    Enemy_IsRight = true;
                    Enemy_IsLeft = false;
                    Enemy_Speed += .15F;
                }

                if (Enemy_Position.Y + Enemy_Size.Y >= 1000)
                {
                    Enemy_IsLeft = true;
                    Enemy_IsRight = false;
                    Enemy_Speed += .15F;
                }
            }

            if (Enemy_Pattern == 3)
            {
                if (Enemy_IsLeft)
                {
                    Enemy_Position.X -= Enemy_Speed;
                }

                if (Enemy_IsRight)
                {
                    Enemy_Position.X += Enemy_Speed;
                }

                if (Enemy_Position.X <= 418)
                {
                    Enemy_IsRight = true;
                    Enemy_IsLeft = false;
                    Enemy_Speed += .2F;
                }

                if (Enemy_Position.X + Enemy_Size.X >= 1442)
                {
                    Enemy_IsLeft = true;
                    Enemy_IsRight = false;
                    Enemy_Speed += .2F;
                }
            }

            timer++;
            if (timer % shoot == 0)
            {
                if (shoot > 0)
                {
                    shoot -= 5;
                    if (shoot == 0)
                    {
                        shoot = 250;
                    }
                }
                Shoot_Enemy_Bullets(bullet_texture);
            }


            Update_Enemy_Bullets();

            foreach (Bullet bullet in bullets)
            {
                if (Enemy_BoundingBox.Intersects(bullet.Bullet_BoundingBox))
                {
                    Enemy_IsHit = true;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Enemy_Texture, Enemy_Position, null, Color.White, (float)0, Vector2.Zero, .5f, SpriteEffects.None, 0f);

            foreach (Bullet bullet in Enemy_bullets)
            {
                bullet.Draw(spriteBatch);
            }
        }

        public void Update_Enemy_Bullets()
        {
            foreach (Bullet bullet in Enemy_bullets)
            {
                if (Enemy_Pattern == 0)
                {
                    bullet.Bullet_Position.Y += bullet.Bullet_Velocity.Y;

                    if (bullet.Bullet_Position.Y >= 1000)
                    {
                        bullet.Bullet_IsVisable = false;
                    }
                }

                if (Enemy_Pattern == 1)
                {
                    bullet.Bullet_Position.X -= bullet.Bullet_Velocity.X;

                    if (bullet.Bullet_Position.X + bullet.Bullet_Size.X <= 418)
                    {
                        bullet.Bullet_IsVisable = false;
                    }
                }

                if (Enemy_Pattern == 2)
                {
                    bullet.Bullet_Position.X += bullet.Bullet_Velocity.X;

                    if (bullet.Bullet_Position.X >= 1442)
                    {
                        bullet.Bullet_IsVisable = false;
                    }
                }

                if (Enemy_Pattern == 3)
                {
                    bullet.Bullet_Position.Y -= bullet.Bullet_Velocity.Y;

                    if (bullet.Bullet_Position.Y + bullet.Bullet_Size.Y <= 0)
                    {
                        bullet.Bullet_IsVisable = false;
                    }
                }
            }

            for (int i = 0; i < Enemy_bullets.Count; i++)
            {
                if (!Enemy_bullets[i].Bullet_IsVisable)
                {
                    Enemy_bullets.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Shoot_Enemy_Bullets(Texture2D bullet_texture)
        {
            Bullet bullet = new Bullet(bullet_texture);
            bullet.Bullet_Position = Enemy_Position;
            bullet.Bullet_Position.X += Enemy_Size.X / 2 - bullet.Bullet_Size.X / 2;
            bullet.Bullet_Position.Y += Enemy_Size.Y;
            bullet.Bullet_Velocity *= Enemy_Speed;
            bullet.Bullet_IsVisable = true;

            if (Enemy_bullets.Count < 20)
            {
                Enemy_bullets.Add(bullet);
            }
        }
    }

    public class Jweel
    {
        public Vector2 Jweel_Size;
        public Vector2 Jweel_Position;
        public Vector2[] Jweel_Spawn_Position1 = new Vector2[5];
        public Texture2D[] Jweel_Texture=new Texture2D[3];
        public bool Jweel_IsVisable;
        public int n = 0;
        public Rectangle Jweel_BoundingBox
        {
            get
            {
                return new Rectangle((int)Jweel_Position.X, (int)Jweel_Position.Y, (int)Jweel_Size.X, (int)Jweel_Size.Y);
            }
        }
        public int timer = 0;
        public int counter1 = 0;
        public SpriteFont Font;

        public Jweel(Texture2D[] jweels_textures, SpriteFont font)
        {

            Jweel_Texture[0] = jweels_textures[0];
            Jweel_Texture[1] = jweels_textures[1];           
            Jweel_Texture[2] = jweels_textures[2];          
            Jweel_Size.X = Jweel_Texture[0].Width*1.5F;
            Jweel_Size.Y = Jweel_Texture[0].Height*1.5F;
            Jweel_IsVisable = true;
            Font = font;

            Jweel_Spawn_Position1[0].X = 1860 / 2;
            Jweel_Spawn_Position1[0].Y = 1000 / 2;
            Jweel_Spawn_Position1[1].X = 1860 / 2 + Jweel_Size.X;
            Jweel_Spawn_Position1[1].Y = 1000 / 2 + Jweel_Size.Y;
            Jweel_Spawn_Position1[2].X = 1860 / 2 + 175;
            Jweel_Spawn_Position1[2].Y = 1000 / 2 + 175;
            Jweel_Spawn_Position1[3].X = 1860 / 2 - 100;
            Jweel_Spawn_Position1[3].Y = 1000 / 2 - 100;
            Jweel_Spawn_Position1[4].X = 1860 / 2 + 150; 
            Jweel_Spawn_Position1[4].Y = 1000 / 2;
        }

        public void Update(Spaceship spaceship)
        {
            if (spaceship.Spaceship_BoundingBox.Intersects(Jweel_BoundingBox))
            {
                n++;
                Jweel_IsVisable = false;
                Jweel_Position.X = 0;
                Jweel_Position.Y = 0;
            }
            
            timer++;
            if (timer % 250 == 0)
            {
                Jweel_IsVisable = true;
                Jweel_Position = Jweel_Spawn_Position1[counter1];
                counter1++;
                if (counter1 == 4)
                {
                    counter1 = 0;
                }
            }        
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Jweel_IsVisable)
            {
                spriteBatch.Draw(Jweel_Texture[0], Jweel_Position, null, Color.White, (float)0, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            }

            spriteBatch.DrawString(Font, "Number of Jweels " + n, new Vector2(100, 200), Color.White);

        }
    }
}