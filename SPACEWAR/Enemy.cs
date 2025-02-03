using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Raylib_cs.Raylib;

namespace SPACEWAR
{
    public abstract class Enemy
    {

        //ENEMY VARIABLES
        public int health;
        public int damage;
        public float speed;
        public float spawnX;
        public float spawnY;
        public int width;
        public int height;
        public string type;
        public int spawnSide;
        public int direction;
        public  int point;

        //CONSTRUCTOR
        public Enemy(int health, float speed, int damage, int point, float spawnX, float spawnY, int spawnSide)
        {
            this.health = health;
            this.speed = speed;
            this.damage = damage;
            this.point = point;
            this.spawnX = spawnX;
            this.spawnY = spawnY;
            this.spawnSide = spawnSide;
        }

        //ABSTRACT METHODS
        public abstract void Move();
        public abstract void Attack();

        //TAKEDAMAGE METHOD
        public void TakeDamage(int amount)
        {
            health -= amount;

            if (health <= 0)
            {
                Destroy();
                Program.totalPoint += point;
            }
        }

        //DESTROY METHOD
        public virtual void Destroy()
        {      
            Game.enemies.Remove(this);
        }

        //CALCULATEDISTANCE METHOD
        public float CalculateDistance(float targetX, float targetY)
        {
            return (float)Math.Sqrt(Math.Pow(targetX - spawnX, 2) + Math.Pow(targetY - spawnY, 2));
        }
    }
}
