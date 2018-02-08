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
        float direction;
        float movement_speed;
        float xspeed;
        float yspeed;
        float xtarget;
        float ytarget;
        
        public PathFinder(float x, float y) : base(x, y)
        {
            X = x;
            Y = y;
            Color = Color.Green;
            Texture = Content.Load<Texture2D>("spr_box");
            movement_speed = 5;

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
            if (Path != null && Path.Count > 0)
            {
                /*xtarget = Path.Count != 0 ? Path.Peek().Center.X : Path.Peek().Parent.Center.X;
                ytarget = Path.Count != 0 ? Path.Peek().Center.Y : Path.Peek().Parent.Center.Y;*/

                /* xtarget = Path.Peek().Parent.Center.X;
                 ytarget = Path.Peek().Parent.Center.Y;*/
                /*if(Path.Count == 1)
                {
                    xtarget = Path.Peek().Center.X;
                    ytarget = Path.Peek().Center.Y;
                }*/
                xtarget = Path.Peek().Parent.Center.X;
                ytarget = Path.Peek().Parent.Center.Y;
                if(Path.Count == 1)
                {
                    xtarget = Path.Peek().Center.X;
                    ytarget = Path.Peek().Center.Y;
                }
                direction = G.PointDirection(X, Y, xtarget, ytarget);
                xspeed = (float)Math.Cos(direction) * movement_speed;
                yspeed = (float)Math.Sin(direction) * movement_speed;
                
                X += Math.Abs(X - xtarget) < xspeed ? 0 : xspeed;
                Y += Math.Abs(Y - ytarget) < yspeed ? 0 : yspeed;
                if (Math.Abs(X - xtarget) < xspeed)
                {
                    X = xtarget;
                }
                if (Math.Abs(Y - ytarget) < yspeed)
                {
                    Y = ytarget;
                }
                if(X == xtarget && Y == ytarget)
                {
                    if(Path.Count > 1)
                    {
                        Path.Pop();
                    }
                    
                }
            }

            base.Update();
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Path != null && Path.Count > 0)
            {
                //DrawLine(Path.Peek().Parent.Center, Path.Peek().Parent.Center + new Vector2(1, 1), Color.Black,1, spriteBatch);
                foreach (Node obj in Path)
                {
                    DrawLine(obj.Center, obj.Parent.Center, Color.White,0, spriteBatch);
                }
                
            }
            base.Draw(spriteBatch);
        }
    }
}
