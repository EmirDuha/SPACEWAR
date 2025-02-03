using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace SPACEWAR
{
    public class StrongEnemy : Enemy
    {
        //CONSTRUCTOR
        public StrongEnemy(int health, int damage, float speed, int point) : base(health, (int)speed, damage, point, 0, 0, 0) { }

        //TEXTURES
        public Texture2D shipstrongenemy = LoadTexture("strongenemy.png");
        public Texture2D shipstrongenemyreverse = LoadTexture("strongenemyreverse.png");

        //OVERRIDE MOVE METHOD
        public override void Move()
        {
            if (spawnSide == 0)
            {
                float deltaX = Game.player.posX - spawnX;
                float deltaY = Game.player.posY - spawnY;
                float distance = CalculateDistance(Game.player.posX, Game.player.posY);

                if (distance > 0)
                {
                    spawnX += speed * (deltaX / distance);
                    spawnY += speed * (deltaY / distance);
                }

                if (spawnX > Program.SCREEN_WIDTH)
                {
                    Destroy();
                }

                direction = 1;
            }

            if (spawnSide == 1)
            {

                float deltaX = Game.player.posX - spawnX;
                float deltaY = Game.player.posY - spawnY;
                float distance = CalculateDistance(Game.player.posX, Game.player.posY);

                if (distance > 0)
                {
                    spawnX += speed * (deltaX / distance);
                    spawnY += speed * (deltaY / distance);
                }

                if (spawnX < 0)
                {
                    Destroy();
                }

                direction = 0;
            }


            if (direction == 1)
                DrawTexture(shipstrongenemy, (int)spawnX, (int)spawnY, Color.White);
            if (direction == 0)
                DrawTexture(shipstrongenemyreverse, (int)spawnX, (int)spawnY, Color.White);
        }

        double lastShotTime = 0;
        double interval = 2;
        public static List<Bullet> enemyBullets = new List<Bullet>();

        //OVERRIDE ATTACK METHOD
        public override void Attack()
        {
            double currentTime = Program.time;

            if (currentTime - lastShotTime >= interval)
            {
                
                    Bullet newBullet = new Bullet
                    {
                        posX = spawnX + (spawnSide == 1 ? -20 : 100),
                        posY = spawnY + (spawnSide == 1 ? 38 : 40),
                        speed = 5,
                        direction = (spawnSide == 1 ? 0 : 1),
                        type = "strongenemy"
                    };

                enemyBullets.Add(newBullet);
                lastShotTime = currentTime; 
            }

            for (int i = 0; i < enemyBullets.Count; i++)
            {
                enemyBullets[i].Move();
                enemyBullets[i].Draw();

                if (enemyBullets[i].posY > Program.SCREEN_HEIGHT || enemyBullets[i].posY < 0)
                {
                    enemyBullets.RemoveAt(i);
                    i--;
                }
            }

            type = "strongenemy";
        }
    }
}
