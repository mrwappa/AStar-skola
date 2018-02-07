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
        public PathFinder(float x, float y) : base(x, y)
        {
            X = x;
            Y = y;
            Color = Color.Green;
            Texture = Content.Load<Texture2D>("spr_box");
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
