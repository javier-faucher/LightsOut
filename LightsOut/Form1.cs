using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut
{
    public partial class Form1 : Form
    {
        LogicLayer logic;
        public Form1()
        {
            InitializeComponent();
            InitializeTableLayoutPanel();
            AssignClickEvent();
            logic = new LogicLayer();
            setState();
        }
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.infoLabel = new System.Windows.Forms.Label();
            this.restartButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(641, 407);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Location = new System.Drawing.Point(762, 42);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(68, 20);
            this.scoreLabel.TabIndex = 1;
            this.scoreLabel.Text = "Clicks: 0";
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(762, 102);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(0, 20);
            this.infoLabel.TabIndex = 2;
            // 
            // restartButton
            // 
            this.restartButton.Location = new System.Drawing.Point(766, 172);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(150, 38);
            this.restartButton.TabIndex = 3;
            this.restartButton.Text = "Play Again";
            this.restartButton.UseVisualStyleBackColor = true;
            this.restartButton.Visible = false;
            this.restartButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1163, 431);
            this.Controls.Add(this.restartButton);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        /// <summary>
        /// This method initializes and adds a button to the each of the cells in the tablePanelLayout 
        /// </summary>
        private void InitializeTableLayoutPanel()
        {
            for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
            {
                for (int j = 0; j < tableLayoutPanel1.RowCount; j++)
                {
                    Button button = new Button();
                    button.Visible = true;
                    button.Dock = DockStyle.Fill;
                  
                    tableLayoutPanel1.Controls.Add(button, i, j);
                }
            }
        }
        
        private void AssignClickEvent()
        {
            foreach (Control c in tableLayoutPanel1.Controls.OfType<Button>())
            {
                c.Click += new EventHandler(OnClick);
            }
        }

        /// <summary>
        /// This method sets the colour of each of the buttons in the tableLayoutPanel using the logicLayers grid 
        /// </summary>
        public void setState()
        {
            foreach (Button button in tableLayoutPanel1.Controls.OfType<Button>())
            {
                int row = tableLayoutPanel1.GetPositionFromControl(button).Row;
                int column = tableLayoutPanel1.GetPositionFromControl(button).Column;
                if (logic.grid[column,row])
                {
                    button.BackColor = Color.FromArgb(255, 232, 232);
                }
                else
                {
                    button.BackColor = Color.DarkGray;
                }
            }
        }
        /// <summary>
        /// This method handles a button in the tablePanelLayout being clicked.
        /// It starts by passing the position of the click in the grid to logic layer.
        /// It then updates the state of the tableLayoutPanel using the logic layers grid
        /// It finally then checks if the game has been completed, if it has then it displays a success message
        /// and a play again button
        /// If the the game is not complete it displays the number of cells left to to turn on.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClick(object sender, EventArgs e)
        {
            UpdateScore();
            Button button = sender as Button;
            int column = tableLayoutPanel1.GetPositionFromControl(button).Column;
            int row = tableLayoutPanel1.GetPositionFromControl(button).Row;
            int[] pos = new int[] { column,row };
            logic.onClick(pos);
            setState();
            if (logic.checkCompleted())
            {
                infoLabel.Text = "Congratulations you completed Lights Out in: " + logic.clicks + " clicks!";
                restartButton.Visible = true;
            }
            else
            {
                infoLabel.Text = "Boxes left to turn on : " + logic.boxesLeft;
            }
        }
        /// <summary>
        ///This method updates the score message
        /// </summary>
        private void UpdateScore()
        {
            scoreLabel.Text = "Clicks: " + logic.clicks;
        }
        /// <summary>
        /// This method handles the on click of the play again button.
        /// It generates a new random grid, updates the state of the tableLayoutPanel and disapears
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            logic.generateGrid();
            setState();
            infoLabel.Text = "";
            restartButton.Visible = false;
        }


    }
}
