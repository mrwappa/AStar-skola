using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AStar.GameObjects.Tools___Accessories;

namespace AStar.GameObjects 
{
    class GameObject
    {
        public static List<GameObject> GameObjects = new List<GameObject>();
        public static GraphicsDeviceManager GraphicsDevice;
        public static ContentManager Content;

        public static Camera Camera;

        public static Astar AStarGrid = new Astar(new List<List<Node>>());

        public static MouseState Mouse;
        public static MouseState PreviousMouseState;
        public static KeyboardState Keyboard;
        public static KeyboardState PreviousKeyboardState;

        public static Texture2D Box;
        public static SpriteFont Font;

        public static Vector2 GridSnap
        {
            get
            {
                return new Vector2((float)Math.Floor(Camera.MouseX / Node.NODE_SIZE) * Node.NODE_SIZE + 16,
                                    (float)Math.Floor(Camera.MouseY / Node.NODE_SIZE) * Node.NODE_SIZE + 16);
            }
        }

        public static void InitGame(GraphicsDeviceManager graphicsDevice, Camera camera, ContentManager content, SpriteFont font)
        {
            GraphicsDevice = graphicsDevice;
            Camera = camera;
            Content = content;
            Font = font;
            Box = Content.Load<Texture2D>("spr_box");
        }

        public Texture2D Texture { get; protected set; }
        public float X { get; set; }
        public float Y { get; set; }
        public Color Color { get; set; }

        public GameObject(float x, float y)
        {
            X = x;
            Y = y;
            Color = Color.White;
            GameObjects.Add(this);
        }

        public bool GetKeyPressed(Keys key)
        {
            if (Keyboard.IsKeyDown(key) && PreviousKeyboardState.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }

        public bool GetMousePressed(ButtonState state)
        {
            //detta är dumt, men det funkar och jag orkar inte fixa en optimisering
            if (state == Mouse.RightButton && Mouse.RightButton == ButtonState.Pressed && PreviousMouseState.RightButton == ButtonState.Released)
            {
                return true;
            }
            else if (state == Mouse.LeftButton && Mouse.LeftButton == ButtonState.Pressed && PreviousMouseState.LeftButton == ButtonState.Released)
            {
                return true;
            }
            else if (state == Mouse.MiddleButton && Mouse.MiddleButton == ButtonState.Pressed && PreviousMouseState.MiddleButton == ButtonState.Released)
            {
                return true;
            }
            return false;
        }

        public virtual void DestroyInstance()
        {
            GameObjects.Remove(this);
        }

        public virtual void Update()
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Vector2(X, Y), new Rectangle(0, 0, Texture.Width, Texture.Height), Color, 0,
               new Vector2((Texture.Width / 2), (Texture.Height / 2)), new Vector2(1, 1), SpriteEffects.None, 1);


        }
        public virtual void DrawGUI(SpriteBatch spriteBatch)
        {

        }

    }
}
