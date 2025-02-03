using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace SPACEWAR
{
    public class BossEnemy : Enemy
    {
        //CONSTRUCTOR
        public BossEnemy(int health, int damage, float speed, int point) : base(health, speed, damage, point, 0, 0, 0) { }

        //TEXTURES
        public Texture2D shipbossenemy = LoadTexture("bossenemy.png");

        //OVERRIDE MOVE METHOD
        public override void Move()
        {
            if (spawnSide == 0)
            {
                spawnX += speed;
            }

            if (spawnSide == 1)
            {
                spawnX -= speed;
            }

            DrawTexture(shipbossenemy, (int)spawnX, (int)spawnY, Color.White);
        }

        double lastShotTime = 0;
        double interval = 4;
        public static List<Bullet> bossBullets = new List<Bullet>();
        //OVERRIDE ATTACK METHOD
        public override void Attack()
        {
            double currentTime = Program.time;

            if (currentTime - lastShotTime >= interval)
            {

                Bullet newBullet = new Bullet
                {
                    posX = spawnX - 20,
                    posY = spawnY + 50,
                    speed = 5,
                    direction = 0,
                    type = "boss"
                };

                bossBullets.Add(newBullet);
                lastShotTime = currentTime; 
            }

            
            for (int i = 0; i < bossBullets.Count; i++)
            {
                bossBullets[i].Move();
                bossBullets[i].Draw();

                
                if (bossBullets[i].posY > Program.SCREEN_HEIGHT || bossBullets[i].posY < 0)
                {
                    bossBullets.RemoveAt(i);
                    i--;
                }
            }

            type = "bossenemy";
        }

        //OVERRIDE DESTROY METHOD
        public override void Destroy()
        {
            if(Game.player.health <= 0)
            base.Destroy();
            else
            Game.isYouWon = true;
        }
    }
}
