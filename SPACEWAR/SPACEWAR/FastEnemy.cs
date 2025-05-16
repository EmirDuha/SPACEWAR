using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace SPACEWAR
{
    public class FastEnemy : Enemy
    {
        //CONSTRUCTOR
        public FastEnemy(int health, int damage, float speed, int point) : base(health, (int)speed, damage, point, 0, 0, 0) { }

        //TEXTURES
        public Texture2D shipfastenemy = LoadTexture("fastenemy.png");
        public Texture2D shipfastenemyreverse = LoadTexture("fastenemyreverse.png");

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

            if(direction == 1)
            DrawTexture(shipfastenemy, (int)spawnX, (int)spawnY, Color.White);
            if (direction == 0)
            DrawTexture(shipfastenemyreverse, (int)spawnX, (int)spawnY, Color.White);
        }

        //OVERRIDE ATTACK METHOD
        public override void Attack() { type = "fastenemy"; }
    }
}
