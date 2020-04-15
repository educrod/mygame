
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mygame
{
    class Asteroid
    {
        public Vector2 position = new Vector2(600,300);
        public int speed;
        public int radius = 59;

        public Asteroid(int newSpeed){
            speed = newSpeed;
        }
        public void asteroidUpdate(GameTime gameTime){
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X -= speed * dt;
        }
    }
}