using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace SPACEWAR
{
    public class BasicEnemy : Enemy
    {
        //CONSTRUCTOR
        public BasicEnemy(int health, int damage, float speed, int point) : base(health, (int)speed, damage, point, 0, 0, 0) { }

        //TEXTURES
        public Texture2D shipbasicenemy = LoadTexture("basicenemy.png");
        public Texture2D shipbasicenemyreverse = LoadTexture("basicenemyreverse.png");

        //OVERRIDE MOVE METHOD
        public override void Move()
        {
            if(spawnSide == 0)
            {
                spawnX += speed;
                if(spawnX > Program.SCREEN_WIDTH)
                {
                    Destroy();
                }

                direction = 1;
            }

            if (spawnSide == 1)
            {
                spawnX -= speed;
                if (spawnX < 0)
                {
                    Destroy();
                }

                direction = 0;
            }


            if (direction == 1)
                DrawTexture(shipbasicenemy, (int)spawnX, (int)spawnY, Color.White);
            if (direction == 0)
                DrawTexture(shipbasicenemyreverse, (int)spawnX, (int)spawnY, Color.White);
        }

        //OVERRIDE ATTACK METHOD
        public override void Attack()
        {
            type = "basicenemy";
        }
    }
}
