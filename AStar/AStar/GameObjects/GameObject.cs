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

        public static void InitGame(GraphicsDeviceManager graphicsDevice, Camera camera, ContentManager content)
        {
            GraphicsDevice = graphicsDevice;
            Camera = camera;
            Content = content;
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

        public void DestroyInstance(GameObject gameObject)
        {
            GameObjects.Remove(gameObject);
            //jag vill inte
            if(gameObject.GetType() == typeof(Solid))
            {
                Solid.Solids.Remove(gameObject as Solid);
            }
        }

        public virtual void Update()
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Vector2(X, Y), new Rectangle(0, 0, Texture.Width, Texture.Height), Color, 0,
               new Vector2((Texture.Width / 2), (Texture.Height / 2)), new Vector2(1, 1), SpriteEffects.None, 1);
        }
    }
}
