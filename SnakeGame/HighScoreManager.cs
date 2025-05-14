using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SnakeGame
{
    public static class HighScoreManager
    {
        private static string filePath = "highscore.txt";

        public static int GetHighScore()
        {
            if (File.Exists(filePath))
            {
                var scoreText = File.ReadAllText(filePath);
                if (int.TryParse(scoreText, out int highScore))
                {
                    return highScore;
                }
            }
            return 0;
        }

        public static void SaveHighScore(int score)
        {
            File.WriteAllText(filePath, score.ToString());
        }

        public static void ResetHighScore()
        {
            File.WriteAllText(filePath, "0");
        }
    }
}
