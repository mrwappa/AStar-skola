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
    class Solid : GameObject
    {
        public static List<Solid> Solids = new List<Solid>();

        public Solid(float x, float y) : base(x, y)
        {
            Solids.Add(this);
            X = x;
            Y = y;
            Color = Color.PapayaWhip;
            Texture = Content.Load<Texture2D>("spr_box");
            AStarGrid.Grid[((int)X - Node.NODE_SIZE / 2) / 32][((int)Y - Node.NODE_SIZE/2) / 32].Walkable = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void DrawGUI(SpriteBatch spriteBatch)
        {
            
        }

        public override void DestroyInstance()
        {
            Solids.Remove(this);
            AStarGrid.Grid[((int)X - Node.NODE_SIZE / 2) / 32][((int)Y - Node.NODE_SIZE / 2) / 32].Walkable = true;
            base.DestroyInstance();
        }

        
    }
}
