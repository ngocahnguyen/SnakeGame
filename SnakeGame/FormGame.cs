using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class FormGame : Form
    {
        private Timer gameTimer;//đối tượng Timer điều khiển tốc độ của rắn
        private List<Point> snake; //Danh sách các điểm tạo thành rắn
        private Point food; //Điểm chứa vị trí mồi (thức ăn)
        private Direction currentDirection; //Hướng di chuyển hiện tại
        private int score; //Điểm số người chơi
        private int gridSize = 20; //Kích thước của một ô trên màn hình
        private Random rand; //Random (ngẫu nhiên) vị trí mồi và chướng ngại vật
        private List<Point> obstacles; //Danh sách các điểm tạo thành các chướng ngại vật
        private int level = 1; //Cấp độ khởi đầu của game
        private int foodEatenCount = 0; //Số lượng thức ăn ban đầu đã ăn
        private int highScore;
        private SpeedLevel currentSpeed;
        private int gameSpeedMilliseconds;
        public SpeedLevel SelectedSpeed { get; private set; }
        public FormGame(SpeedLevel speedLevel)
        {


            InitializeComponent();
            InitializeGame(); 
            this.Controls.Add(pictureBox1); //Thêm PictureBox1 vào danh sách các controls của form
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true); //Đặt style để hỗ trợ màu trong suốt
            this.Controls.Add(controlPanel); // Thêm controlPanel vào danh sách các controls của form
            currentSpeed = speedLevel;
            SetGameSpeed(currentSpeed); // Tốc độ di chuyển của rắn
            gameTimer = new Timer();
            gameTimer.Interval = gameSpeedMilliseconds;
            gameTimer.Tick += GameTick;
        }
        private void InitializeGame()
        {

            rand = new Random();
            snake = new List<Point> { new Point(5, 5), new Point(5, 6), new Point(5, 7) }; // Khởi tạo rắn ban đầu
            currentDirection = Direction.Up; // Hướng di chuyển ban đầu

            
            

            GenerateObstacles(); //Tạo chướng ngại vật ban đầu
            GenerateFood(); // Tạo thức ăn ban đầu
            
            //Khởi tạo controlPanel để chứa điểm, các nút: bắt đầu, dừng, tiếp tục
            Panel controlPanel = new Panel();
            controlPanel.Dock = DockStyle.Left;
            controlPanel.Width = 248;
            controlPanel.Height = pictureBox1.Height;
            controlPanel.BackColor = Color.White;
            this.Controls.Add(controlPanel);
            controlPanel.PreviewKeyDown -= ChangeDirection;

            pictureBox1.PreviewKeyDown += new PreviewKeyDownEventHandler(ChangeDirection);
            pictureBox1.KeyDown += new KeyEventHandler(ChangeDirection);
            pictureBox1.Size = new Size(545, 403);
            pictureBox1.Location = new Point(255,2);


            controlPanel.Controls.Add(label1);
            label2.Text = "0";
            controlPanel.Controls.Add(label2);

            controlPanel.Controls.Add(pictureBox2);
            controlPanel.Controls.Add(btnStart);
            controlPanel.Controls.Add(btnPause);
            controlPanel.Controls.Add(btnResume);
        }

        private void SetGameSpeed(SpeedLevel speed)
        {
            switch (speed)
            {
                case SpeedLevel.Slow:
                    gameSpeedMilliseconds = 150; // Tốc độ di chuyển của rắn chậm
                    break;
                case SpeedLevel.Medium:
                    gameSpeedMilliseconds = 100; // Tốc độ di chuyển của rắn trung bình
                    break;
                case SpeedLevel.Fast:
                    gameSpeedMilliseconds = 50; // Tốc độ di chuyển của rắn nhanh
                    break;
            }
        }

        //Tạo vị trí ngẫu nhiên cho thức ăn, không trùng với vị trí rắn
        private void GenerateFood()
        {
            do
            {
                food = new Point(rand.Next(pictureBox1.ClientSize.Width / gridSize), rand.Next(pictureBox1.ClientSize.Height / gridSize));
            } while (snake.Contains(food));

        }

        //Tạo vị trí ngẫu nhiên cho các chướng ngại vật, không trùng với vị trí của rắn và mồi
        private void GenerateObstacles()
        {
            obstacles = new List<Point>(); // Khởi tạo obstacles
            int numberOfObstacles = level * 3;
            for (int i = 0; i < numberOfObstacles; i++)
            {
                Point obstacle;
                do
                {
                    obstacle = new Point(rand.Next(pictureBox1.ClientSize.Width / gridSize), rand.Next(pictureBox1.ClientSize.Height / gridSize));
                } while (snake.Contains(obstacle) || food == obstacle || obstacles.Contains(obstacle));
                obstacles.Add(obstacle);
            }
        }

        //thay đổi hướng di chuyển bằng cách ấn các nút mũi tên trên bàn phím
        private void ChangeDirection(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (currentDirection != Direction.Down) currentDirection = Direction.Up;
                    break;
                case Keys.Down:
                    if (currentDirection != Direction.Up) currentDirection = Direction.Down;
                    break;
                case Keys.Left:
                    if (currentDirection != Direction.Right) currentDirection = Direction.Left;
                    break;
                case Keys.Right:
                    if (currentDirection != Direction.Left) currentDirection = Direction.Right;
                    break;
            }
        }
        private void ChangeDirection(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (currentDirection != Direction.Down) currentDirection = Direction.Up;
                    break;
                case Keys.Down:
                    if (currentDirection != Direction.Up) currentDirection = Direction.Down;
                    break;
                case Keys.Left:
                    if (currentDirection != Direction.Right) currentDirection = Direction.Left;
                    break;
                case Keys.Right:
                    if (currentDirection != Direction.Left) currentDirection = Direction.Right;
                    break;
            }

        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            snake = new List<Point> { new Point(5, 5), new Point(5, 6), new Point(5, 7) };
            currentDirection = Direction.Up;
            score = 0;
            level = 1;
            foodEatenCount = 0;

            GenerateFood(); // Tạo lại thức ăn ban đầu
            GenerateObstacles(); // Tạo lại chướng ngại vật ban đầu

            // Cập nhật lại điểm hiển thị
            label2.Text = score.ToString();
            gameTimer.Start();
            pictureBox1.Focus();
            pictureBox1.Invalidate();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            gameTimer.Stop();
            pictureBox1.Focus();
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            gameTimer.Start();
            pictureBox1.Focus();
        }

        //Di chuyển của rắn theo hướng hiện tại
        private void MoveSnake()
        {
            Point head = snake[0];
            Point newHead = head;

            switch (currentDirection)
            {
                case Direction.Up:
                    newHead.Offset(0, -1);
                    break;
                case Direction.Down:
                    newHead.Offset(0, 1);
                    break;
                case Direction.Left:
                    newHead.Offset(-1, 0);
                    break;
                case Direction.Right:
                    newHead.Offset(1, 0);
                    break;
            }

            snake.Insert(0, newHead);
            snake.RemoveAt(snake.Count - 1);
        }

        //Kiểm tra va chạm
        private bool CheckCollision()
        {
            Point head = snake[0];
            if (head.X < 0 || head.X >= pictureBox1.ClientSize.Width / gridSize || head.Y < 0 || head.Y >= pictureBox1.ClientSize.Height / gridSize)
            {
                return true; // Rắn đụng tường
            }
            for (int i = 1; i < snake.Count; i++)
            {
                if (snake[i] == head)
                {
                    return true; // Rắn tự va chạm vào cơ thể của nó
                }
            }
            foreach (var obstacle in obstacles)
            {
                if (obstacle == head)
                {
                    return true; // Rắn đụng chướng ngại vật
                }
            }
            return false;
        }

        //xử lý khi rắn ăn mồi
        private void EatFood()
        {
            score += 10;
            snake.Add(snake[snake.Count - 1]); // Thêm một phần thân mới cho rắn
            GenerateFood(); // Tạo thức ăn mới
        }
       

        private void GameTick(object sender, EventArgs e)
        {
            MoveSnake();
            if (CheckCollision())
            {
                gameTimer.Stop();
                this.Hide();
                FormGameOver GameOver = new FormGameOver(score);
                if (GameOver.ShowDialog() == DialogResult.Retry)
                {
                    RestartGame();
                    this.Show();
                }
                else
                {
                    this.Close();
                }
               
            }
            if (snake[0] == food)
            {
                EatFood();
                foodEatenCount++; // Tăng số lần ăn thức ăn
                if (foodEatenCount % 10 == 0) // Kiểm tra xem đã ăn đủ 10 lần chưa
                {
                    level++; // Tăng cấp độ
                    GenerateObstacles(); // Tạo chướng ngại vật mới
                }
                label2.Text = score.ToString();
            }
            pictureBox1.Invalidate();
        }

        private void RestartGame()
        {
            highScore = HighScoreManager.GetHighScore();
            snake = new List<Point> { new Point(5, 5), new Point(5, 6), new Point(5, 7) };
            currentDirection = Direction.Up;
            score = 0;
            foodEatenCount = 0;
            level = 1;
            GenerateFood();
            GenerateObstacles();
            gameTimer.Start();
            pictureBox1.Invalidate();
            label2.Text = score.ToString();
        }
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void gamePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            controlPanel.Visible = false;
        }

        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Vẽ rắn
            for (int i = 0; i < snake.Count; i++)
            {
                if (i == 0)
                {
                    // Vẽ đầu rắn
                    Rectangle headRect = new Rectangle(snake[i].X * gridSize, snake[i].Y * gridSize, gridSize, gridSize);
                    g.FillEllipse(Brushes.Green, headRect);
                    g.DrawEllipse(Pens.Black, headRect); // Viền cho đầu rắn
                }
                else
                {
                    // Vẽ phần thân của rắn
                    Rectangle bodyRect = new Rectangle(snake[i].X * gridSize, snake[i].Y * gridSize, gridSize, gridSize);
                    g.FillRectangle(Brushes.DarkGreen, bodyRect);
                    g.DrawRectangle(Pens.Black, bodyRect); // Viền cho phần thân của rắn
                }
            }
            Rectangle foodRect = new Rectangle(food.X * gridSize, food.Y * gridSize, gridSize, gridSize);
            g.FillEllipse(Brushes.Red, foodRect);
            g.DrawEllipse(Pens.Black, foodRect); // Viền cho thức ăn
            foreach (var obstacle in obstacles)
            {
                g.FillRectangle(Brushes.Black, obstacle.X * gridSize, obstacle.Y * gridSize, gridSize, gridSize);
            }
        }
        
        
    }
}
