using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using SPACEWAR;
using static Raylib_cs.Raylib;

namespace SPACEWAR
{
    public class CollisionDetector
    {
        
        //CHECKCOLLISION METHOD
        public void CheckCollision(Spaceship player, List<Enemy> enemies) 
        {
            Game game = new Game();
            for (int j = enemies.Count - 1; j >= 0; j--)
            {
                Enemy enemy = enemies[j];
                
                Rectangle playerCol = new Rectangle(player.posX, player.posY + 20, player.width, player.height);
                Rectangle basicenemyCol = new Rectangle(enemy.spawnX, enemy.spawnY, 73, 70);
                Rectangle fastenemyCol = new Rectangle(enemy.spawnX, enemy.spawnY, 55, 71);
                Rectangle strongenemyCol = new Rectangle(enemy.spawnX, enemy.spawnY, 101, 86);
                Rectangle bossenemyCol = new Rectangle(enemy.spawnX, enemy.spawnY, 124, 126);
                Rectangle enemyCol = new Rectangle();

                if (enemy.type == "basicenemy") { enemyCol = basicenemyCol; }
                if (enemy.type == "fastenemy") { enemyCol = fastenemyCol; }
                if (enemy.type == "strongenemy") { enemyCol = strongenemyCol; }
                if (enemy.type == "bossenemy") { enemyCol = bossenemyCol; }

                if (CheckCollisionRecs(playerCol, enemyCol))
                {
                   if (player.direction == 1)
                       DrawTexture(game.shiPlayer, player.posX, player.posY, Color.Red);
                   if (player.direction == 0)
                       DrawTexture(game.shiPlayeReverse, player.posX, player.posY, Color.Red);

                   player.TakeDamage(enemy.damage);

                   enemy.Destroy();

                   break;
                }
            }

        }

        //CHECKBULLETCOLLISION WITH ENEMIES METHOD
        public void CheckBulletCollisionE(List<Bullet> bullets, List<Enemy> enemies)
        {
            for (int i = bullets.Count - 1; i >= 0; i--) 
            {
                Bullet bullet = bullets[i];
                bool bulletDestroyed = false;

                for (int j = enemies.Count - 1; j >= 0; j--) 
                {
                    Enemy enemy = enemies[j];
                    
                    Rectangle bulletCol = new Rectangle(bullet.posX, bullet.posY, 30, 5);
                    Rectangle basicenemyCol = new Rectangle(enemy.spawnX, enemy.spawnY, 73, 70);
                    Rectangle fastenemyCol = new Rectangle(enemy.spawnX, enemy.spawnY, 55, 71);
                    Rectangle strongenemyCol = new Rectangle(enemy.spawnX, enemy.spawnY, 101, 86);
                    Rectangle bossenemyCol = new Rectangle(enemy.spawnX, enemy.spawnY, 124, 126);
                    Rectangle enemyCol = new Rectangle();

                    if (enemy.type == "basicenemy") { enemyCol = basicenemyCol; enemy.width = 73; enemy.height = 70; }
                    if (enemy.type == "fastenemy") { enemyCol = fastenemyCol; enemy.width = 55; enemy.height = 71; }
                    if (enemy.type == "strongenemy") { enemyCol = strongenemyCol; enemy.width = 101; enemy.height = 86; }
                    if (enemy.type == "bossenemy") { enemyCol = bossenemyCol; enemy.width = 124; enemy.height = 126; }

                    if (CheckCollisionRecs(bulletCol, enemyCol))
                    {
                       bullet.OnHit(enemy);
                       bullets.RemoveAt(i);
                       bulletDestroyed = true;
                       break;
                    }

                }

                if (!bulletDestroyed && (bullet.posX < 0 || bullet.posX > Program.SCREEN_WIDTH)) 
                {
                    bullets.RemoveAt(i);
                }
            }
        }

        //CHECKBULLETCOLLISION WITH PLAYER METHOD
        public void CheckBulletCollisionP(List<Bullet> bullets, Spaceship player)
        {
            Game game = new Game();
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = bullets[i];
                bool bulletDestroyed = false;

                Rectangle bulletCol = new Rectangle(bullet.posX, bullet.posY, 30, 5);
                Rectangle playerCol = new Rectangle(player.posX, player.posY + 20, player.width, player.height);

                if (bullet.type == "strongenemy")
                {
                    if (CheckCollisionRecs(bulletCol, playerCol))
                    {
                        if(player.direction == 1)
                           DrawTexture(game.shiPlayer, player.posX, player.posY, Color.Red);
                        if (player.direction == 0)
                            DrawTexture(game.shiPlayeReverse, player.posX, player.posY, Color.Red);

                        player.TakeDamage(10);
                        bullets.RemoveAt(i);
                        bulletDestroyed = true;
                        break;
                    }

                }

                Rectangle bossBulletCol = new Rectangle(bullet.posX, bullet.posY, 20, 15);
                if (bullet.type == "boss")
                {
                    if (CheckCollisionRecs(bossBulletCol, playerCol))
                    {
                        if (player.direction == 1)
                            DrawTexture(game.shiPlayer, player.posX, player.posY, Color.Red);
                        if (player.direction == 0)
                            DrawTexture(game.shiPlayeReverse, player.posX, player.posY, Color.Red);

                        player.TakeDamage(30);
                        bullets.RemoveAt(i);
                        bulletDestroyed = true;
                        break;
                    }

                }

                if (!bulletDestroyed && (bullet.posX < 0 || bullet.posX > Program.SCREEN_WIDTH))
                {
                    bullets.RemoveAt(i);
                }
            }
        }
    }
}


