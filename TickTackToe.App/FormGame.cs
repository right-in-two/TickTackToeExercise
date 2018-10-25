using Microsoft.Extensions.Logging;
using System.Linq;
using System.Windows.Forms;
using TickTackToe.Interfaces;

namespace TickTackToe.App
{
    public partial class FormGame : Form
    {
        private IBoardFactory boardFactory;
        private IBoard board;
        private ILogger logger;

        private System.Drawing.Color activeColor = System.Drawing.Color.Red;
        private System.Drawing.Color inactiveColor = System.Drawing.Color.Gray;
        
        public FormGame()
        {
            InitializeComponent();
        }

        public FormGame(IBoardFactory boardFactory, ILogger logger)
            :this()
        {
            this.boardFactory = boardFactory;
            this.logger = logger;

            board = this.boardFactory.CreateBoard();

            InitBoard();
            DrawBoardStatus();
        }

        private void InitBoard()
        {
            tableLayoutPanel.RowCount = board.LocationBoxes.Max(x => x.X) + 1;
            tableLayoutPanel.ColumnCount = board.LocationBoxes.Max(x => x.Y) + 1;

            for (int i = 0; i < tableLayoutPanel.RowCount; i++)
            {
                for (int j = 0; j < tableLayoutPanel.ColumnCount; j++)
                {
                    Label label = new Label() { Size = new System.Drawing.Size(30, 30) };

                    // local vars otherwise i, j values equal to count will be used in event handler
                    int x = i;
                    int y = j;
                    label.Click += (sender, e) => Label_Click(sender, e, x, y);
                    tableLayoutPanel.Controls.Add(label, i, j);
                }
            }
        }

        private void Label_Click(object sender, System.EventArgs e, int xPosition, int yPosition)
        {
            if (board.GameStatus == GameStatus.InProgress)
            {
                Label label = sender as Label;
                board.TakeLocationCommand(xPosition, yPosition);
                DrawBoardStatus();
            }
        }

        private void DrawBoardStatus()
        {
            labelPlayerX.ForeColor = board.ActivePlayer == PlayerId.X ? activeColor : inactiveColor;
            labelPlayerO.ForeColor = board.ActivePlayer == PlayerId.O ? activeColor : inactiveColor;
            foreach (LocationOnBoard box in board.LocationBoxes)
            {
                Label label = tableLayoutPanel.GetControlFromPosition(box.X, box.Y) as Label;
                label.Text = $"{box.TakenBy}";
                label.ForeColor = box.BelongsToSuccessRow ? activeColor : inactiveColor;
            }
            labelGameStatus.Text = $"{board.GameStatus}";
        }

        private void buttonReset_Click(object sender, System.EventArgs e)
        {
            board = boardFactory.CreateBoard();
            DrawBoardStatus();
        }
    }
}
