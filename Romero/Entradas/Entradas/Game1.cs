using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;
using System.Data.SqlClient;

namespace Entradas
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch; //Begin(), Draw(), End().
        private Texture2D Sonic;
        private Texture2D _logo;
        private SpriteFont _font;
        Texture2D debugTexture;
        private Vector2 posicionVariable;
        private const float velocidad_movimiento= 5f;
        Rectangle playerHitbox;
        Rectangle logoHitbox;
        bool slow;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            

            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Sonic = Content.Load<Texture2D>("imagenes/sonic2");
            _logo = Content.Load<Texture2D>("imagenes/logoMonogame");
            _font = Content.Load<SpriteFont>("Archivo_Condensed-Black");

            debugTexture = new Texture2D(GraphicsDevice, 1, 1);
            debugTexture.SetData(new[] { Color.White});

            var instanciaBD = new ConexionBD;

            instanciaBD.GuardarProgreso("jugador1",2);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            ChequearTeclado();

           
            void ChequearTeclado ()
            {
                KeyboardState estadoTeclado = Keyboard.GetState();

                float velocidad = velocidad_movimiento;

                //Debug.WriteLine(velocidad);

                if (estadoTeclado.IsKeyDown(Keys.Space)) { velocidad *= 3; }
                if (slow == true)
                {
                    velocidad = 2;
                  //  Debug.WriteLine(velocidad);
                }
           
            if (estadoTeclado.IsKeyDown(Keys.W) || estadoTeclado.IsKeyDown(Keys.Up )) {
                posicionVariable.Y -= velocidad;
                    //Debug.WriteLine(posicionVariable);

                }
                
                if (estadoTeclado.IsKeyDown(Keys.S) || estadoTeclado.IsKeyDown(Keys.Down))
            {
                posicionVariable.Y +=  velocidad;
//                    Debug.WriteLine(posicionVariable);

                }
                if (estadoTeclado.IsKeyDown(Keys.D) || estadoTeclado.IsKeyDown(Keys.Right))
            {
                posicionVariable.X += velocidad;
                    Debug.WriteLine(posicionVariable);

                }
                if (estadoTeclado.IsKeyDown(Keys.A) || estadoTeclado.IsKeyDown(Keys.Left))
            {
                posicionVariable.X-=velocidad;
                Debug.WriteLine(posicionVariable);
            }
                if (estadoTeclado.IsKeyDown(Keys.P))
                {
                    posicionVariable.X = 0;
                    posicionVariable.Y = 0;
                }

                logoHitbox = new Rectangle(100,200, _logo.Bounds.Width, _logo.Bounds.Height);
                playerHitbox = new Rectangle((int)posicionVariable.X, (int)posicionVariable.Y, 140,160);

                if (playerHitbox.Intersects(logoHitbox))
                {
                
                    Debug.WriteLine("Colision");
                    slow = true;
                }
                else { slow = false; }
                
              
            }

            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.DrawString(_font, "BIENVENIDO", new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height )* 0.5f, Color.White);

            GraphicsDevice.Clear(Color.CornflowerBlue);







            _spriteBatch.Draw //LOGO
                (
                _logo, //Textura 
                new Vector2(100,200),  //posicion inicio
                null, //rectangulo fuente
                Color.White, //color
                0.0f, //rotacion
                Vector2.Zero, //origen
                0.30f, //Escala. Donde 1.0f es 100% 0.5f
                SpriteEffects.None, //efectos
                1.0f //profundida de capas (respecto a otros sprites) 
        );
            _spriteBatch.Draw //PERSONAJE
        (
        Sonic, //Textura 
        posicionVariable,  //posicion inicio
        null, //rectangulo fuente
        Color.White, //color
        0.0f, //rotacion
        new Vector2(20,0), //origen
        0.30f, //Escala. Donde 1.0f es 100% 0.5f
        SpriteEffects.None, //efectos
        0.0f //profundida de capas (respecto a otros sprites) 
);

         
            _spriteBatch.Draw(debugTexture, playerHitbox, Color.Red * 0.4f);

            _spriteBatch.Draw(debugTexture, logoHitbox, Color.Yellow* 0.4f);


            _spriteBatch.End();
            _spriteBatch.Begin();

        
                        _spriteBatch.End();







            base.Draw(gameTime);
        }
    }
}
