using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using King_of_Thieves.Graphics;
using King_of_Thieves.Actors;
using System.Collections.Generic;

using Gears.Cloud;
using King_of_Thieves.usr.local;
using King_of_Thieves.Input;
using System.Linq;
using Gears.Cloud.Utility;
using System.Timers;
using System.Diagnostics;

namespace King_of_Thieves
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        CSprite testSprite = null;
        CActorTest actorTest;
        Actors.Player.CPlayer player;
        CComponent compTest = new CComponent();
        Actors.Menu.CMenu testMenu;
        CComponent menuComo = new CComponent();
        Actors.HUD.Text.CTextBox textTest = null;
        Matrix scaleMatrix = new Matrix();
        bool first = true;

		Stopwatch _updateTimer = new Stopwatch();
		Stopwatch _drawTimer = new Stopwatch();

        System.Timers.Timer _fpsTimer = new System.Timers.Timer(1000);
        public void _fpsHandler(object sender, ElapsedEventArgs e) { updateFPS = updateFrames; updateFrames = 0; drawFPS = drawFrames; drawFrames = 0; }
        int updateFrames;
        int drawFrames;
        int updateFPS;
        int drawFPS;

        //Screen Resolution defaults
        private const int ScreenWidth = 720;
        private const int ScreenHeight = 480;

        public Game1()
        {
            this.IsFixedTimeStep = true;
            
            graphics = new GraphicsDeviceManager(this);
            graphics.SynchronizeWithVerticalRetrace = false;
            //GraphicsAdapter.UseReferenceDevice = false;
            //graphics.PreferredBackBufferHeight = ScreenHeight
            //graphics.PreferredBackBufferWidth = ScreenWidth;
            //graphics.SynchronizeWithVerticalRetrace = true;
            //graphics.ApplyChanges();
            
            Content.RootDirectory = @"Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content. Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Register our ContentManager
            //ContentButler.setGame(this);
            Master.Initialize(this);

            //Setup screen display/graphics device
            ViewportHandler.SetScreen(ScreenWidth, ScreenHeight);
            graphics.PreferredBackBufferWidth = ViewportHandler.GetWidth();
            graphics.PreferredBackBufferHeight = ViewportHandler.GetHeight();
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Graphics.CGraphics.acquireGraphics(ref graphics);
            CGraphics.fullScreenRenderTarget = new RenderTarget2D(CGraphics.GPU, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, true, SurfaceFormat.Color, DepthFormat.Depth24);

            _fpsTimer.Elapsed += new ElapsedEventHandler(_fpsHandler);
            _fpsTimer.Enabled = true;
            _fpsTimer.Start();

            Master.SetClearColor(Color.Black);

            CTextures.init(Content);

            //Master.Push(new DevMenu());
            //Master.Push(new PlayableState());

            Master.GetInputManager().AddInputHandler(new CInput());
            CMasterControl.glblInput = Master.GetInputManager().GetCurrentInputHandler() as Input.CInput;
            CMasterControl.healthController = new Actors.HUD.health.CHealthController(20,78);
            CMasterControl.magicMeter = new Actors.HUD.magic.CMagicMeter();

            scaleMatrix = Matrix.CreateScale(
                           3f,
                           3f,
                           1f);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            CMasterControl.glblContent = Content;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            Graphics.CGraphics.spriteBatch = spriteBatch;
            CMasterControl.audioPlayer = new Sound.CAudioPlayer();

            // TODO: use this.Content to load your game content here
            CMasterControl.audioPlayer.soundBank.Add("cursor", new Sound.CSound(Content.Load<SoundEffect>("cursor"),true));
            CMasterControl.audioPlayer.soundBank.Add("lttp_heart", new Sound.CSound(Content.Load<SoundEffect>("lttp_heart")));

            menuComo.root = testMenu;
            CMasterControl.mapManager.cacheMaps(false, "castleTown.xml");
            CMasterControl.mapManager.cacheMaps(false, "castleTownInteriors.xml");

            //textTest = new Actors.HUD.Text.CTextBox();
            CMasterControl.buttonController = new Actors.HUD.buttons.CButtonController();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            CMasterControl.audioPlayer.stop();
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            CMasterControl.gameTime = gameTime;

            if (first)
            {
                Master.Push(new usr.local.splash.CZFGCSplash());
                first = false;
            }

            updateFrames++;

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F11))
                graphics.IsFullScreen = !graphics.IsFullScreen;

            //Exit when Escape is pressed(Dunno if this iterferes with the editor?)
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            
            CInput input = Master.GetInputManager().GetCurrentInputHandler() as CInput;
            if (input.getInputRelease(Microsoft.Xna.Framework.Input.Keys.B))
                CActor.showHitBox = !CActor.showHitBox;
            _updateTimer.Start();

            Master.Update(gameTime);

            _updateTimer.Stop();

            CMasterControl.camera.update(gameTime);

            base.Update(gameTime);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            drawFrames++;

            //Store drawtime from previous frame
            _drawTimer.Start();


                GraphicsDevice.SetRenderTarget(CGraphics.fullScreenRenderTarget);


            GraphicsDevice.Clear(Master.GetClearColor());

            

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, CMasterControl.camera.transformation);
            
            Master.Draw(spriteBatch);

            _drawTimer.Stop();

            double updateTime = System.Math.Ceiling((double)_updateTimer.Elapsed.Milliseconds);
            double drawTime = System.Math.Ceiling((double)_drawTimer.Elapsed.Milliseconds);

            if (CActor.showHitBox)
            {
                string debugString = "UpdateTime: " + updateTime + " ms\n" +
                                    "DrawTime: " + drawTime + " ms\n" +
                                    "FPS(Draw: " + drawFPS + " | Update: " + updateFPS + ") \n";
                spriteBatch.DrawString(Content.Load<SpriteFont>("Fonts/benchmarker"), debugString, Vector2.Zero, Color.White);
            }

            float aspect = GraphicsDevice.DisplayMode.AspectRatio;

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate,null,SamplerState.PointClamp,null,null,null,scaleMatrix);

            

                GraphicsDevice.SetRenderTarget(null);
                spriteBatch.Draw((Texture2D)CGraphics.fullScreenRenderTarget, new Rectangle(0,0,
                                                                                           720,
                                                                                          480),
                                                                                           Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
            _drawTimer.Stop();

        }
    }
}
