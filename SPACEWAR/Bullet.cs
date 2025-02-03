using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace SPACEWAR
{
    public class Bullet
    {
        //BULLET VARIABLES
        public int damage = 20;
        public float speed = 10;
        public int direction;
        public float posX;
        public float posY;
        public string type;

        //MOVE METHOD
        public void Move()
        {
            if(type == "strongenemy" || type == "player")
            {
                if (direction == 1)
                    posX += speed;

                if (direction == 0)
                    posX -= speed;
            }

            if (type == "boss")
            {
                float deltaX = Game.player.posX - posX;
                float deltaY = Game.player.posY - posY;
                float distance = CalculateDistance(Game.player.posX, Game.player.posY);

                if (distance > 0)
                {
                    posX += speed / 2 * (deltaX / distance);
                    posY += speed / 2 * (deltaY / distance);
                }
            }
        }

        //DRAW METHOD
        public void Draw()
        {
            if (type == "strongenemy" || type == "player")
                DrawRectangle((int)posX, (int)posY, 30, 5, Color.White);
            if (type == "boss")
                DrawRectangle((int)posX, (int)posY, 20, 15, Color.Yellow);
        }

        //CALCULATEDISTANCE METHOD
        public float CalculateDistance(float targetX, float targetY)
        {
            return (float)Math.Sqrt(Math.Pow(targetX - posX, 2) + Math.Pow(targetY - posY, 2));
        }

        //ONHIT METHOD
        public void OnHit(Enemy enemy)
        {
            enemy.TakeDamage(damage);
        }
    }
}
