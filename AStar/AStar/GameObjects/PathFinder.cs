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
        float movementSpeed;
        float xSpeed;
        float ySpeed;
        float xTarget;
        float yTarget;
        
        bool walkToLastNode;

        public PathFinder(float x, float y) : base(x, y)
        {
            X = x;
            Y = y;
            Color = Color.Green;
            Texture = Content.Load<Texture2D>("spr_box");
            movementSpeed = 5;

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
                xTarget = Path.Peek().Parent.Center.X;
                yTarget = Path.Peek().Parent.Center.Y;
                if(Path.Count == 1)
                {
                    if(X == Path.Peek().Parent.Center.X && Y == Path.Peek().Parent.Center.Y)
                    {
                        walkToLastNode = true;
                    }
                    if(walkToLastNode)
                    {
                        xTarget = Path.Peek().Center.X;
                        yTarget = Path.Peek().Center.Y;
                    }
                    if(X == Path.Peek().Center.X && Y == Path.Peek().Center.Y)
                    {
                        walkToLastNode = false;
                        Path.Pop();
                    }
                }
                direction = G.PointDirection(X, Y, xTarget, yTarget);
                xSpeed = (float)Math.Cos(direction) * movementSpeed;
                ySpeed = (float)Math.Sin(direction) * movementSpeed;

                X += Math.Abs(X - xTarget) < xSpeed ? 0 : xSpeed;
                Y += Math.Abs(Y - yTarget) < ySpeed ? 0 : ySpeed;
                
                if (Math.Abs(X - xTarget) < xSpeed)
                {
                    X = xTarget;
                }
                if (Math.Abs(Y - yTarget) < ySpeed)
                {
                    Y = yTarget;
                }
                if(X == xTarget && Y == yTarget)
                {
                    if(Path.Count != 1 && Path.Count != 0)
                    {
                        Path.Pop();
                    }
                }

                
            }

            base.Update();
        }

        float AdjustX()
        {
            X = xTarget;
            return 0;
        }

        float AdjustY()
        {
            Y = yTarget;
            return 0;
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

        public override void DrawGUI(SpriteBatch spriteBatch)
        {
            if(Path != null)
            {
                spriteBatch.DrawString(Font, Path.Count.ToString(), new Vector2(100, 100), Color.White);
            }
            
            base.DrawGUI(spriteBatch);
        }
    }
}
