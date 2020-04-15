
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mygame
{
    class Controller
    {
        public List<Asteroid> asteroids = new List<Asteroid>();
        public double timer = 2D;
        
        public void conUpdate(GameTime gameTime){
            timer -= gameTime.ElapsedGameTime.TotalSeconds;
            if (timer <= 0){
                asteroids.Add(new Asteroid(250));
                timer = 2D;
            }
        }
    }
}