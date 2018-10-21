using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjektTestowy.CustomClasses;
using ProjektTestowy.Interfaces;

namespace ProjektTestowy
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        private Camera camera;
        private BasicEffect basicEffect;
        private Point prevMousePos;
        TestObject testObj;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            //graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            textSpeed = new Vector2(2.5f, 2.5f);
            rnd = new Random();
            base.Initialize();

            prevMousePos = new Point(0, 0);

            camera = new Camera(GraphicsDevice);
            basicEffect = new BasicEffect(GraphicsDevice)
            {
                Alpha = 1,
                VertexColorEnabled = true,
                LightingEnabled = false,
            };

            testObj = new TestObject
            {
                triangleVertices = new[]
                {
                    new VertexPositionColor(new Vector3(0, 20, 0), Color.Red),
                    new VertexPositionColor(new Vector3(-20, -20, 0), Color.Green),
                    new VertexPositionColor(new Vector3(20, -20, 0), Color.Blue),
                },
                vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly),
            };
            testObj.vertexBuffer.SetData<VertexPositionColor>(testObj.triangleVertices);
        }

        protected override void LoadContent() { }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();
            var gamepadState = GamePad.GetState(PlayerIndex.One);
            var mousePostion = new Point(mouseState.X, mouseState.Y);

            if (gamepadState.Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            #region Kamera
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                camera.MoveCamera(Vector3.Right);
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                camera.MoveCamera(Vector3.Left);
            }
            else if (keyboardState.IsKeyDown(Keys.Up))
            {
                camera.MoveCamera(Vector3.Down);
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                camera.MoveCamera(Vector3.Up);
            }
            else if (keyboardState.IsKeyDown(Keys.OemPlus))
            {
                camera.MoveCamera(Vector3.Forward);
            }
            else if (keyboardState.IsKeyDown(Keys.OemMinus))
            {
                camera.MoveCamera(Vector3.Backward);
            }
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                camera.Rotate(Vector3.Left);
            }
            #endregion

            #region Kamera Target

            prevMousePos = new Point(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            var mouseMove = mousePostion - prevMousePos;  
            
            var m = new Vector3(mouseMove.X,mouseMove.Y,0);

            if (mouseMove.X < 0)
            {
                camera.SetTarget(Vector3.Right);
            }
            else if (mouseMove.X > 0)
            {
                camera.SetTarget(Vector3.Left);
            }

            if (mouseMove.Y > 0)
            {
                camera.SetTarget(Vector3.Down);
            }
            else if (mouseMove.Y < 0)
            {
                camera.SetTarget(Vector3.Up);
            }
            #endregion

            Mouse.SetPosition(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            camera.CalculateMatrix();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            camera.UpdateView(basicEffect);

            GraphicsDevice.Clear(Color.White);

            {
                GraphicsDevice.SetVertexBuffer(testObj.vertexBuffer);

                RasterizerState rasterizerState = new RasterizerState()
                {
                    CullMode = CullMode.None
                };
                GraphicsDevice.RasterizerState = rasterizerState;

                foreach (var item in basicEffect.CurrentTechnique.Passes)
                {
                    item.Apply();
                    GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 3);
                }
            }

            base.Draw(gameTime);
        }
    }
}
