
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mygame
{
    class Ship
    {
        public Vector2 position = new Vector2(100, 100);
        public int speed = 180;

        public void shipUpdate(GameTime gameTime){
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kState.IsKeyDown(Keys.Right)){
                position.X += speed * dt;
            }
            if (kState.IsKeyDown(Keys.Left))
            {
                position.X -= speed * dt;
            }
            if (kState.IsKeyDown(Keys.Down))
            {
                position.Y += speed * dt;
            }
            if (kState.IsKeyDown(Keys.Up))
            {
                position.Y -= speed * dt;
            }
        }
    }
}