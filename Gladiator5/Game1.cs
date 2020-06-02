using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;


namespace Gladiator5.Desktop
{

    public class Game1 : Game
    {
        List<Particle> particles = new List<Particle>();
        List<Texture2D> stars = new List<Texture2D>();
        List<Texture2D> planets = new List<Texture2D>();
        SpriteFont arial;
        Color[] starColors = { Color.SeaGreen, Color.MonoGameOrange, Color.Orange, Color.Azure,Color.Green,Color.Cyan };
        Color[] blackHolesColors = { Color.SeaGreen,Color.LightBlue,Color.Yellow};
        public int maxPlayerSize = 150;
        public float playerSize = 100;
        public float eatenParticles = 0;
        public int enemyEatenParticles = 0;
        public int maxParticles = 2500;
        public float entropy = 0.000001f;
        public float galacticWind = 0;
        public Song ambient;
        public float[] paralax = { 0, 0, 0, 0 };
        public float paralaxCount = 50;
        Random random = new Random();

        int numberOfParticles = 1500;
        public static int windowWidth, windowHeight;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        double t0;

        Particle player;

        Particle enemy;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.IsFullScreen = true;

            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            this.graphics.ApplyChanges();
            windowWidth = graphics.PreferredBackBufferWidth;
            windowHeight = graphics.PreferredBackBufferHeight;
            paralax[2] = windowWidth;
            paralax[3] = windowHeight;
        }

        public void createParticle()
        {
            int r = random.Next(8000);
            Particle newP;
            if (r > 5)
            {
                int size = random.Next(10) + 5;
                newP = new Particle((int)(random.NextDouble() * windowWidth),
                                       (int)(random.NextDouble() * windowHeight),
                                       size,
                                       size,
                                       (float)random.NextDouble() * 50 + 50,
                                        stars[random.Next(stars.Count)],
                                        starColors[random.Next(starColors.Length)],
                                        100);
                double angle = galacticWind;
                newP.direction = new Vector2((float)Math.Cos(angle) * newP.speed, (float)Math.Sin(angle) * newP.speed);
                newP.value = random.Next(size) + 2;
            }
            else
            {
                int size = random.Next(50) + 25;
                newP = new Particle((int)(random.NextDouble() * windowWidth),
                                       (int)(random.NextDouble() * windowHeight),
                                       size,
                                       size,
                                       (float)random.NextDouble() * 10,
                                        planets[random.Next(planets.Count)],
                                        starColors[random.Next(starColors.Length)],
                                        100);
                double angle = galacticWind;
                newP.direction = new Vector2((float)Math.Cos(angle) * newP.speed, (float)Math.Sin(angle) * newP.speed);
                newP.value = random.Next(size) + 25;
            }
            if (particles.Count < maxParticles && newP != null)
                particles.Add(newP);
        }

        protected override void Initialize()
        {
            base.Initialize();

            MediaPlayer.Play(ambient);
            MediaPlayer.IsRepeating = true;
            Window.Title = "Entropy";
            this.IsMouseVisible = true;

            for (int i = 0; i < numberOfParticles; i++)
            {
                createParticle();
            }


            player = new Particle(0, 0, 100, 100f, 50f, SpriteAtlas.twirl2, Color.Cyan,1000);

            enemy = new Particle(100, 100, 100, 100f, 50f, SpriteAtlas.twirl2, Color.Green, 1000);
            enemy.speed = (float)random.NextDouble() * 250 + 100;
            enemy.direction = new Vector2(0.2f * enemy.speed, 0.2f * enemy.speed);

            //particles.Add(new Particle(0, 125, .15f, .15f, 50, 0, SpriteAtlas.star1, Color.Red));
            //particles.Add(new Particle(0, 180, .25f, .25f, 50, 0, SpriteAtlas.spark4, Color.White));

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            SpriteAtlas.ball = Content.Load<Texture2D>("ball");

            SpriteAtlas.star1 = SpriteAtlas.FilterGrayscale(Content.Load<Texture2D>("star_01"));
            SpriteAtlas.spark5 = SpriteAtlas.FilterGrayscale(Content.Load<Texture2D>("spark_05"));
            SpriteAtlas.spark4 = SpriteAtlas.FilterGrayscale(Content.Load<Texture2D>("spark_04"));
            SpriteAtlas.smoke1 = SpriteAtlas.FilterGrayscale(Content.Load<Texture2D>("smoke_01"));
            SpriteAtlas.twirl2 = SpriteAtlas.FilterGrayscale(Content.Load<Texture2D>("twirl_02"));
            SpriteAtlas.star7 = SpriteAtlas.FilterGrayscale(Content.Load<Texture2D>("star_07"));

            SpriteAtlas.circle2 = SpriteAtlas.FilterGrayscale(Content.Load<Texture2D>("circle_02"));
            SpriteAtlas.circle5 = SpriteAtlas.FilterGrayscale(Content.Load<Texture2D>("circle_05"));
            SpriteAtlas.light3 = SpriteAtlas.FilterGrayscale(Content.Load<Texture2D>("light_03"));

            SpriteAtlas.scorch2 = SpriteAtlas.FilterGrayscale(Content.Load<Texture2D>("scorch_02"));
            SpriteAtlas.scorch3 = SpriteAtlas.FilterGrayscale(Content.Load<Texture2D>("scorch_03"));
            SpriteAtlas.flame4 = SpriteAtlas.FilterGrayscale(Content.Load<Texture2D>("flame_04"));

            SpriteAtlas.space = Content.Load<Texture2D>("space");
            SpriteAtlas.metal = Content.Load<Texture2D>("metal");

            arial = Content.Load<SpriteFont>("File");
            SpriteAtlas.whiteSquare =  new Texture2D(GraphicsDevice, 1, 1);
            SpriteAtlas.whiteSquare.SetData(new Color[] { Color.White });
            ambient = Content.Load<Song>("ambient");


            stars.Add(SpriteAtlas.star1);
            stars.Add(SpriteAtlas.star7);
            stars.Add(SpriteAtlas.spark4);

            planets.Add(SpriteAtlas.circle2);
            planets.Add(SpriteAtlas.circle5);
            planets.Add(SpriteAtlas.light3);
            planets.Add(SpriteAtlas.flame4);
            planets.Add(SpriteAtlas.scorch2);
            planets.Add(SpriteAtlas.scorch3);

        }


        protected override void UnloadContent()
        {
            SpriteAtlas.ball.Dispose();
        }


        protected override void Update(GameTime gameTime)
        {

                
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            galacticWind += 0.001f;
            entropy += 0.0005f;
            t0 -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if (t0 <= 0)
            {
                t0 += 5;
                eatenParticles -= entropy;

                createParticle();
            }


            MouseState mouse = Mouse.GetState();

            player.updatePositon(new Vector2(mouse.X, mouse.Y));
            if(playerSize < maxPlayerSize)
            {
                playerSize = eatenParticles / 10000f * maxPlayerSize + 50;
                player.scale = new Vector2(playerSize, playerSize);
            }
            //eatenParticles -= 2;
            player.rotation += 0.5f;

            if (player.colidesWith(enemy))
            {
                eatenParticles -= 5 * entropy;
            }
            for (int i = particles.Count-1; i >= 0; i--) {
                Particle p = particles[i];

                if (p.isOutOfBounds() || p.lifetime <= 0)
                {
                    if (p.flag == 1)
                        eatenParticles++;
                    if (p.flag == 2)
                        enemyEatenParticles++;

                    particles.Remove(p);
                    createParticle();
                    continue;
                }

                if (enemy.colidesWith(p))
                {
                    p.decay = true;
                    p.lifetime = random.Next(25);
                    float alpha = p.angleTo(enemy);
                    p.speed = (float)random.NextDouble() * 150 + 50;
                    p.direction = new Vector2((float)Math.Cos(alpha) * p.speed, (float)Math.Sin(alpha) * p.speed);
                    p.rotation = alpha;
                    if(!p.flaged)
                        p.flag = 2;
                }

                if (player.colidesWith(p))
                {
                    p.decay = true;
                    p.lifetime = random.Next(25);
                    float alpha = p.angleTo(player);
                    p.speed = (float)random.NextDouble() * 150 + 50;
                    p.direction = new Vector2((float)Math.Cos(alpha) * p.speed,(float)Math.Sin(alpha) * p.speed);
                    p.rotation = alpha;
                    if (!p.flaged)
                        p.flag = 1;

                }
                p.update(gameTime);
            }


            enemy.rotation += 0.1f;
            enemy.update(gameTime);
            if (enemy.isOutOfBounds())
            {
                int dir = random.Next(4);
                double angle = 0;
                enemy.speed = (float)random.NextDouble() * 50 + 50;
                if (dir == 0)
                {
                    angle = random.NextDouble() * 90 - 180;
                    enemy.position = new Vector2(20f, random.Next(windowHeight));
                }
                if (dir == 1)
                {
                    angle = 180 - random.NextDouble() * 180;
                    enemy.position = new Vector2(random.Next(windowWidth), 20f);

                }

                if (dir == 2)
                {
                    angle = 180 - random.NextDouble() * 90 - 180;
                    enemy.position = new Vector2(windowWidth - 20f, random.Next(windowHeight));
                }
                if (dir == 3)
                {
                    angle = random.NextDouble() * 90 - 180;
                    enemy.position = new Vector2(random.Next(windowWidth), windowHeight - 20f);

                }

                enemy.direction = new Vector2((float)Math.Cos(angle) * enemy.speed, (float)Math.Sin(angle) * enemy.speed);
                enemy.color = blackHolesColors[random.Next(blackHolesColors.Length)];
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            //Texture2D blank = new Texture2D(GraphicsDevice, 1, 1);
            //blank.SetData(new Color[] { Color.Black });
            //
            spriteBatch.Begin();
            //spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.AlphaBlend);
            paralax[0] -= (float)(paralaxCount * gameTime.ElapsedGameTime.TotalSeconds);
            paralax[1] -= (float)(paralaxCount * gameTime.ElapsedGameTime.TotalSeconds);
            paralax[2] += (float)(paralaxCount * gameTime.ElapsedGameTime.TotalSeconds);
            paralax[3] += (float)(paralaxCount * gameTime.ElapsedGameTime.TotalSeconds);
            if (paralax[0] < -400 || paralax[0] > 0)
                paralaxCount *= -1;

            spriteBatch.Draw(SpriteAtlas.space, new Rectangle((int)paralax[0], (int)paralax[1], (int)paralax[2], (int)paralax[3]), Color.White * 0.25f);

            foreach (Particle p in particles)
                p.draw(ref spriteBatch);

            enemy.draw(ref spriteBatch);
            player.draw(ref spriteBatch);


            spriteBatch.Draw(SpriteAtlas.metal,
                             new Rectangle(0,0,windowWidth,75),
                             Color.White);
            string text = "Power: " + (int)eatenParticles + " Entropy: " + entropy;
            spriteBatch.DrawString(arial, text, new Vector2(20,20) ,Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
