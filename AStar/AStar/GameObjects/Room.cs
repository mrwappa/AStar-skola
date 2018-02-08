using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar.GameObjects
{
    class Room : GameObject
    {
        public Room(float x, float y) : base(x, y)
        {

        }

        public override void Update()
        {
            if(GetMousePressed(Mouse.LeftButton) && Keyboard.IsKeyUp(Keys.LeftShift) && Keyboard.IsKeyUp(Keys.Space))
            {
                if (GridSnapMouse.X < 960 && GridSnapMouse.Y < 540 && GridSnapMouse.X > 0 && GridSnapMouse.Y > 0)
                {
                    bool canCreate = true;
                    foreach (Solid obj in Solid.Solids.ToList())
                    {
                        if (obj.X == GridSnapMouse.X && obj.Y == GridSnapMouse.Y)
                        {
                            canCreate = false;
                        }
                    }
                    if (canCreate)
                    {
                        new Solid(GridSnapMouse.X, GridSnapMouse.Y);
                    }

                }

            }
            if (GetMousePressed(Mouse.RightButton))
            {
                foreach (Solid obj in Solid.Solids.ToList())
                {
                    if(obj.X == GridSnapMouse.X && obj.Y == GridSnapMouse.Y)
                    {
                        obj.DestroyInstance();
                    }
                }
            }
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //draw box on a position that is snapped to the grid
            spriteBatch.Draw(Box, GridSnapMouse, new Rectangle(0, 0, Box.Width, Box.Height), Color.Black, 0,
            new Vector2((Box.Width / 2), (Box.Height / 2)), new Vector2(1, 1), SpriteEffects.None, 1);
        }

        public override void DrawGUI(SpriteBatch spriteBatch)
        {
            //draw the GridSnapMouse position
            spriteBatch.DrawString(Font, GridSnapMouse.ToString(), new Vector2(20, 80), Color.White);
            
        }
    }
}
