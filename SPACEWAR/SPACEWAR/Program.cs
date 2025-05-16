using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace SPACEWAR;

public class Program
{

    //GAME STATES
    enum GameState
    {
        menu,
        playing,
        youwon,
        gameover,
        scoreboard
    }

    //GLOBAL VARIABLES
    public static double time = 0;
    public static int totalPoint = 0;
    public static int score = 0;
    public static int SCREEN_WIDTH = 1200;
    public static int SCREEN_HEIGHT = 800;

    public static void Main()
    {
        InitWindow(SCREEN_WIDTH, SCREEN_HEIGHT, "SPACEWAR");
        SetTargetFPS(60);

        //PROGRAM VARIABLES
        Texture2D background = LoadTexture("background.png");
        Texture2D menubackground = LoadTexture("menubackground.png");
        Texture2D lifebar = LoadTexture("lifebar.png");
        GameState gameState = GameState.menu;
        double menutime = 0;
        double leveluptime = 0;
       

        //CLASS DECLARETIONS
        Game game = new Game();
        Spaceship player = new Spaceship();


        //MAIN LOOP
        while (!WindowShouldClose())
        {          
            BeginDrawing();
            ClearBackground(Color.White);

            switch(gameState){
                
                //MENU STATE
                case GameState.menu:

                    menutime = GetTime();
                    
                    DrawTexture(menubackground, 0, 0, Color.White);        
                    DrawText("SPACEWAR", SCREEN_WIDTH / 2 - 300, SCREEN_HEIGHT / 2 - 200, 100, Color.Orange);
                    DrawText("Press ENTER to Start", SCREEN_WIDTH / 2 - 200, SCREEN_HEIGHT / 2, 30, Color.White);

                    if (IsKeyPressed(KeyboardKey.Enter))          
                        gameState = GameState.playing;
                    break;

                //PLAYING STATE
                case GameState.playing:

                    time = GetTime() - menutime;

                    DrawTexture(background, 0, 0, Color.White);
                    DrawTexture(lifebar, 0, 0, Color.White);

                    game.StartGame();
                    game.UpdateGame();
                    game.EndGame();

                    if (Game.isGameOver)
                        gameState = GameState.gameover;
                    if (Game.isYouWon)
                        gameState = GameState.youwon;

                    break;

                //GAME OVER STATE
                case GameState.gameover:

                    ClearBackground(Color.Black);
                    DrawText("GAME OVER", SCREEN_WIDTH / 2 - 300, SCREEN_HEIGHT / 2 - 200, 100, Color.Orange);
                    DrawText("Your Score: " + score, SCREEN_WIDTH / 2 - 100, SCREEN_HEIGHT / 2, 30, Color.Orange);
                    DrawText("Press space for showing scoreboard", SCREEN_WIDTH / 2 - 250, SCREEN_HEIGHT / 2 + 200, 30, Color.Orange);
                    if (!game.isScoreSaved)
                        game.SaveScores(score);
                    if (IsKeyPressed(KeyboardKey.Space))
                        gameState = GameState.scoreboard;

                    break;

                //YOUWON STATE
                case GameState.youwon:

                    ClearBackground(Color.Black);
                    DrawText("YOU WON!", SCREEN_WIDTH / 2 - 250, SCREEN_HEIGHT / 2 - 200, 100, Color.Orange);
                    DrawText("Your Score: " + score, SCREEN_WIDTH / 2 - 100, SCREEN_HEIGHT / 2, 30, Color.Orange);
                    DrawText("Press space for showing scoreboard", SCREEN_WIDTH / 2 - 250, SCREEN_HEIGHT / 2 + 200, 30, Color.Orange);
                    if (!game.isScoreSaved)
                        game.SaveScores(score);
                    if (IsKeyPressed(KeyboardKey.Space))
                        gameState = GameState.scoreboard;

                    break;

                //SCOREBOARD STATE
                case GameState.scoreboard:

                    ClearBackground(Color.Black);
                    game.ShowScoreboard();

                    break;
            }

            EndDrawing();
        }

        //UNLOAD
        UnloadTexture(game.shiPlayer);
        UnloadTexture(game.shiPlayeReverse);
        UnloadTexture(background);
        UnloadTexture(menubackground);
        CloseWindow();
    }
}
