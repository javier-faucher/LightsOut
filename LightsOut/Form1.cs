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
            // label1
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Location = new System.Drawing.Point(762, 42);
            this.scoreLabel.Name = "label1";
            this.scoreLabel.Size = new System.Drawing.Size(68, 20);
            this.scoreLabel.TabIndex = 1;
            this.scoreLabel.Text = "Score: 0";
            // 
            // label2
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(762, 102);
            this.infoLabel.Name = "label2";
            this.infoLabel.Size = new System.Drawing.Size(51, 20);
            this.infoLabel.TabIndex = 2;
            this.infoLabel.Text = "";
            // 
            // button1
            // 
            this.restartButton.Location = new System.Drawing.Point(766, 172);
            this.restartButton.Name = "button1";
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
        private void OnClick(object sender, EventArgs e)
        {
            increaseScore();
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
        private void increaseScore()
        {
            scoreLabel.Text = "Score: " + logic.clicks;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            logic.generateGrid();
            setState();
            infoLabel.Text = "";
            restartButton.Visible = false;
        }
    }
}
