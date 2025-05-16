using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace SPACEWAR
{
    public class Spaceship
    {
        //SPACESHIP VARIABLES
        public int health = 100;
        private int damage = 30;
        private int speed = 5;
        public int posX = 550;
        public int posY = 300;
        public int width = 122;
        public int height = 90;
        public int direction = 1;
        public double interval = 1.0;
        public int level = 1;
        public List<Bullet> playerbBullets = new List<Bullet>();
        public Bullet bullet = new Bullet();

        //MOVE METHOD
        public void Move()
        {
            if (IsKeyDown(KeyboardKey.Right)) 
            {
                if (posX + width > Program.SCREEN_WIDTH - 10)
                    posX = Program.SCREEN_WIDTH - width - 10;
                posX += speed;
                direction = 1;
                bullet.direction = direction;
            }
            if (IsKeyDown(KeyboardKey.Left))
            {
                if (posX < 0)
                    posX = 0;

                posX -= speed;
                direction = 0;
                bullet.direction = direction;
            }
            if (IsKeyDown(KeyboardKey.Up))
            {
                if (posY < 35)
                    posY = 35;

                posY -= speed;
            }
            if (IsKeyDown(KeyboardKey.Down))
            {
                if (posY - height > Program.SCREEN_HEIGHT - 200)
                    posY = Program.SCREEN_HEIGHT - height;

                posY += speed;
            }
            

        }

        //SHOOT METHOD
        double lastShotTime = 0;
        public void Shoot()
        {
            double currentTime = Program.time;

            if (IsKeyDown(KeyboardKey.Z) && currentTime - lastShotTime >= interval)
            {
                Bullet newBullet = new Bullet
                {                  
                    posX = posX + (direction == 1 ? 100 : -10),
                    posY = posY + 55,
                    direction = direction,
                    type = "player"
                };

                playerbBullets.Add(newBullet);
                lastShotTime = currentTime;
            }

            for (int i = 0; i < playerbBullets.Count; i++)
            {
                playerbBullets[i].Move();
                playerbBullets[i].Draw();


                if (playerbBullets[i].posX > Program.SCREEN_WIDTH || playerbBullets[i].posX < 0)
                {
                    playerbBullets.RemoveAt(i);
                    i--;
                }
            }
        }

        //TAKEDAMAGE METHOD
        public void TakeDamage(int amount)
        {
            health -= amount;
        }

        //LEVELUP METHOD
        public void LevelUp()
        {
            
            switch (level)
            {
                case 1:
                    if (Program.totalPoint >= 2000)
                    {
                        health += 10;
                        speed += 1;
                        interval -= 0.2;
                        
                        level++;

                    }
                    break;
   
                case 2:
                    if (Program.totalPoint >= 4000)
                    {
                        health += 10;
                        speed += 1;
                        interval -= 0.2;
                        
                        level++;
                    }
                    break;

                case 3:
                    if (Program.totalPoint >= 7000)
                    {
                        health += 20;
                        speed += 2;
                        interval -= 0.2;
                        
                        level++;
                    }
                    break;  

                case 4:
                    if (Program.totalPoint >= 10000)
                    {
                        health += 20;
                        speed += 2;
                        interval -= 0.2;
                        
                        level++;
                    }
                    break;

            }
        }
    }
}
