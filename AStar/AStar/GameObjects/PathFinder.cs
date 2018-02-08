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
    class PathFinder : GameObject
    {
        Stack<Node> Path;
        public PathFinder(float x, float y) : base(x, y)
        {
            X = x;
            Y = y;
            Color = Color.Green;
            Texture = Content.Load<Texture2D>("spr_box");
        }

        public override void Update()
        {
            if(Keyboard.IsKeyDown(Keys.Space) && GetMousePressed(Mouse.LeftButton))
            {
                Path = AStarGrid.FindPath(SnapToGrid(X,Y),GridSnapMouse);
            }
            if (Keyboard.IsKeyDown(Keys.LeftShift) && GetMousePressed(Mouse.LeftButton))
            {
                bool canMove = true;
                foreach (Solid obj in Solid.Solids.ToList())
                {
                    if (obj.X == GridSnapMouse.X && obj.Y == GridSnapMouse.Y)
                    {
                        canMove = false;
                    }
                }
                if(canMove)
                {
                    X = GridSnapMouse.X;
                    Y = GridSnapMouse.Y;
                }
                
            }
            base.Update();
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Path != null && Path.Count > 0)
            {
                foreach (Node obj in Path)
                {
                    DrawLine(obj.Center, obj.Parent.Center, Color.White, spriteBatch);
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
