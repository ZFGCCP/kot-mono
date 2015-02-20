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

        HighPerfTimer _updateTimer = new HighPerfTimer();
        HighPerfTimer _drawTimer = new HighPerfTimer();
        System.Timers.Timer _fpsTimer = new System.Timers.Timer(1000);
        public void _fpsHandler(object sender, ElapsedEventArgs e) { updateFPS = updateFrames; updateFrames = 0; drawFPS = drawFrames; drawFrames = 0; }
        int updateFrames;
        int drawFrames;
        int updateFPS;
        int drawFPS;

        //Screen Resolution defaults
        private const int ScreenWidth = 320;
        private const int ScreenHeight = 240;

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

            _fpsTimer.Elapsed += new ElapsedEventHandler(_fpsHandler);
            _fpsTimer.Enabled = true;
            _fpsTimer.Start();

            Master.SetClearColor(Color.CornflowerBlue);

            CTextures.init(Content);

            Master.Push(new DevMenu());
            //Master.Push(new PlayableState());

            Master.GetInputManager().AddInputHandler(new CInput());
            CMasterControl.glblInput = Master.GetInputManager().GetCurrentInputHandler() as Input.CInput;
            CMasterControl.healthController = new Actors.HUD.health.CHealthController(20,78);
            CMasterControl.buttonController = new Actors.HUD.buttons.CButtonController();

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
            CMasterControl.mapManager.cacheMaps(false, "tiletester.xml");
            CMasterControl.mapManager.cacheMaps(false, "thieves-house-f1.xml");

            textTest = new Actors.HUD.Text.CTextBox();
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
            updateFrames++;

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }
            //Exit when Escape is pressed(Dunno if this iterferes with the editor?)
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            CInput input = Master.GetInputManager().GetCurrentInputHandler() as CInput;
            if (input.getInputRelease(Microsoft.Xna.Framework.Input.Keys.B))
                CActor.showHitBox = !CActor.showHitBox;
            _updateTimer.Start();
            
            Master.Update(gameTime);
            //CMasterControl.mapManager.updateMap(gameTime);
            _updateTimer.Stop();

            if (CMasterControl.glblInput.keysReleased.Contains(Microsoft.Xna.Framework.Input.Keys.X))
                textTest.displayMessageBox("The quick brown fox jumped over the fence. I am a potato. blah blah Ash rocks etc testing some mad wacky shit hello am i your god please eat me for i am delicious blah blah abcdefgh i jklmno pqrs t u vwxyz hoo hahahahaha look at me i'm a text box i go to school i wear glasses ganondorf can suck a my linky ding dong while i shoot fire arrows wearing some kinda cloak. I have over 9000 master swords and they're all up Ganondorf's butthole. ");


            textTest.update(gameTime);
            //CMasterControl.audioPlayer.Update();
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
            float updateTime = (float)System.Math.Ceiling(_updateTimer.Duration * 1000.0);
            float drawTime = (float)System.Math.Ceiling(_drawTimer.Duration * 1000.0);

            _drawTimer.Start();
            GraphicsDevice.Clear(Master.GetClearColor());

            CMasterControl.camera.update(gameTime);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, CMasterControl.camera.transformation);
            
            Master.Draw(spriteBatch);

            CEffects.drawThisShit();

            textTest.drawMe();

            if (CActor.showHitBox)
            {
                string debugString = "UpdateTime: " + updateTime + " ms\n" +
                                    "DrawTime: " + drawTime + " ms\n" +
                                    "FPS(Draw: " + drawFPS + " | Update: " + updateFPS + ") \n";
                spriteBatch.DrawString(Content.Load<SpriteFont>("Fonts/benchmarker"), debugString, Vector2.Zero, Color.White);
            }

            spriteBatch.End();

            

            base.Draw(gameTime);

            System.GC.Collect();
            _drawTimer.Stop();

        }
    }
}
