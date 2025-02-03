using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Raylib_cs;
using static System.Formats.Asn1.AsnWriter;
using static Raylib_cs.Raylib;

namespace SPACEWAR
{

    public class Game
    {
        // GAME VARIABLES
        public static List<Enemy> enemies = new List<Enemy>();
        public static Spaceship player = new Spaceship();
        CollisionDetector collision = new CollisionDetector();
        Bullet bullet = new Bullet();
        public  static bool isGameOver = false;
        public static bool isYouWon = false;
        public static bool isBossActive = false;
        Random rand = new Random();
        public Texture2D shiPlayer = LoadTexture("spaceshiplayer.png");
        public Texture2D shiPlayeReverse = LoadTexture("spaceshiplayereverse.png");

        //STARTGAME METHOD
        public void StartGame() 
        {
            if(player.direction == 1)
            DrawTexture(shiPlayer, player.posX, player.posY, Color.White);
            if(player.direction == 0)
            DrawTexture(shiPlayeReverse, player.posX, player.posY, Color.White);
            CreateEnemies();
        }

        //UPDATEGAME METHOD
        public void UpdateGame()
        {
            player.Move();
            player.Shoot();
            Program.score = Program.totalPoint;
            DrawText(Program.score.ToString(), 600, 13, 30, Color.Orange);

            player.LevelUp();

            DrawText("Health: " + "\t\t\t\t" + player.health, 30, 13, 30, Color.Orange);
            if (Program.totalPoint == 500)
                DrawText("----", 30, 50, 30, Color.Orange);
            else if (Program.totalPoint == 1000)
                DrawText("--------", 30, 50, 30, Color.Orange);
            else if (Program.totalPoint == 1500)
                DrawText("------------", 30, 50, 30, Color.Orange);
            else
                DrawText("", 30, 50, 30, Color.Orange);

            DrawRectangle(320, 10, 30, 30, Color.DarkBlue);
            DrawText(player.level.ToString(), 327, 13, 30, Color.Orange);
            
            for (int i = 0; i < enemies.Count; i++)
            {
                 enemies[i].Move();
                 enemies[i].Attack();
            }

            CheckCollisions();
        }

        //CREATEENEMIES FUNCTION
        double lastBasicEnemySpawnTime = 0;
        double lastFastEnemySpawnTime = 0;
        double lastStrongEnemySpawnTime = 0;
        double lastBossEnemySpawnTime = 0;
        public void CreateEnemies()
        {
            if(isBossActive)
                return;

            //BASIC ENEMY
            double currentTimeBE = Program.time;
            if (currentTimeBE - lastBasicEnemySpawnTime > 5.0)
            {
                int spawnSide = rand.Next(0, 2);
                int spawnX = (spawnSide == 0) ? 0 : Program.SCREEN_WIDTH - 40;
                int spawnY = rand.Next(60, Program.SCREEN_HEIGHT - 40);
                enemies.Add(new BasicEnemy(60, 10, 1, 600) { spawnX = spawnX, spawnY = spawnY, spawnSide = spawnSide });
                lastBasicEnemySpawnTime = currentTimeBE;
            }

            //FAST ENEMY
            double currentTimeFE = Program.time;
            if (currentTimeFE - lastFastEnemySpawnTime > 10.0)
            {
                int spawnSide = rand.Next(0, 2);
                int spawnX = (spawnSide == 0) ? 0 : Program.SCREEN_WIDTH - 40;
                int spawnY = rand.Next(60, Program.SCREEN_HEIGHT - 40);
                enemies.Add(new FastEnemy(40, 20, 3, 400) { spawnX = spawnX, spawnY = spawnY, spawnSide = spawnSide });
                lastFastEnemySpawnTime = currentTimeFE;
            }

            //STRONG ENEMY
            double currentTimeSE = Program.time;
            if (currentTimeSE - lastStrongEnemySpawnTime > 15.0)
            {
                int spawnSide = rand.Next(0, 2);
                int spawnX = (spawnSide == 0) ? 0 : Program.SCREEN_WIDTH - 40;
                int spawnY = rand.Next(60, Program.SCREEN_HEIGHT - 40);
                enemies.Add(new StrongEnemy(100, 30, 1, 1500) { spawnX = spawnX, spawnY = spawnY, spawnSide = spawnSide });
                lastStrongEnemySpawnTime = currentTimeSE;
            }

            //BOSS ENEMY
            double currentTimeBOSS = Program.time;
            if (currentTimeBOSS - lastBossEnemySpawnTime > 60.0)
            {        
                int spawnX = Program.SCREEN_WIDTH - 40;
                int spawnY = Program.SCREEN_HEIGHT / 2;
                enemies.Add(new BossEnemy(1000, 1000, 0.5f, 3000) { spawnX = spawnX, spawnY = spawnY, spawnSide = 1 });
                lastBossEnemySpawnTime = currentTimeBOSS;
                isBossActive = true;
            }
        }

        //CHECKCOLLISIONS METHOD
        public void CheckCollisions()
        {            
            collision.CheckCollision(player, enemies);
            collision.CheckBulletCollisionE(player.playerbBullets, enemies);
            collision.CheckBulletCollisionP(StrongEnemy.enemyBullets, player);
            collision.CheckBulletCollisionP(BossEnemy.bossBullets, player);
        }

        //ENDGAME METHOD
        public void EndGame()
        {
            if (player.health <= 0) 
            isGameOver = true;
            
        }

        public int playerCount = 0;
        public bool isScoreSaved = false;
        // SAVESCORES METHOD
        public void SaveScores(int score)
        {
            string file = "spacewarscores.txt";
            List<KeyValuePair<string, int>> scores = new List<KeyValuePair<string, int>>();

            if (File.Exists(file))
            {
                string[] lines = File.ReadAllLines(file);
                foreach (string line in lines)
                {
                    if (line.StartsWith("Player"))
                    {
                        string[] parts = line.Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 2 && int.TryParse(parts[1], out int existingScore))
                        {
                            scores.Add(new KeyValuePair<string, int>(parts[0], existingScore));
                        }
                    }
                }
            }

            playerCount = scores.Count + 1;
            string playerScore = "Player" + playerCount;
            scores.Add(new KeyValuePair<string, int>(playerScore, score));

            scores = scores.OrderByDescending(s => s.Value).ToList();

            using (StreamWriter writer = new StreamWriter(file, false))
            {
                writer.WriteLine("Scoreboard:");
                foreach (var entry in scores)
                {
                    writer.WriteLine($"{entry.Key}: {entry.Value}");
                }
            }

            isScoreSaved = true;
        }

        // SHOWSCOREBOARD METHOD
        public void ShowScoreboard()
        {
            string file = "spacewarscores.txt";

            if (File.Exists(file))
            {
                string[] lines = File.ReadAllLines(file);
                int y = 200;

                foreach (string line in lines)
                {
                    DrawText(line, Program.SCREEN_WIDTH / 2 - 150, y, 30, Color.Orange);
                    y += 40;
                }
            }
            else
            {
                DrawText("No scores yet", Program.SCREEN_WIDTH / 2 - 200, 200, 30, Color.Orange);
            }
        }
    }
}
