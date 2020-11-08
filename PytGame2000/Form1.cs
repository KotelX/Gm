using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PytGame2000
{
    public partial class PytGame2000 : Form
    {
        public PytGame2000()
        {
            InitializeComponent();
            ComboBox comboBox = new ComboBox();
            MessageBox.Show("Выберите размер поля и сложность");
        }
        List<Button> buttons = new List<Button>();  //Список кнопок
        private void SetBtnSettingVisiable()
        {
            btnSetting1.Visible = false;
            btnSetting1.Enabled = false;
            btnSetting2.Visible = false;
            btnSetting2.Enabled = false;
            btnSetting3.Visible = false;
            btnSetting3.Enabled = false;
            btnSetting4.Visible = false;
            btnSetting4.Enabled = false;
        }
        private void Button_Click(object sender, EventArgs e)
        {
            if (Game.Size == 2)
            {
                Game.GameSetings(Convert.ToInt16((sender as Button).Text));
                SetBtnSettingVisiable();
                StartGame();
                return;
            }
            if (Game.Shift(CovertNumberToPosition(Convert.ToInt16((sender as Button).Tag))))    //проверка нажатия на коректность по координатам
                Refrash();              //обновление занчений на кнопках
            if (CheckWining(Game.Map))  //проверка на конец игры
            {         
                DialogResult result = MessageBox.Show("Желаите сыграть снова?", "Вы победили!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Application.Restart();
                    return;
                }
                Application.Exit();
            }
        }   //обработчик нажатий на кнопки с числами
        private int ConverPositionToNamber(Position position)
        {
            return position.X + position.Y * Game.Size;
        }   // Конвертирует позицию(Х,У) в номер(1,2,3,4...)
        /// <summary>
        /// конвертирует номер(1,2,3...) в позицию(Х,У)
        /// </summary>
        /// <param name="number">номер кнопки(от левого верхнего угла начиная с 0)</param>
        /// <returns>Position координаты кнопки</returns>
        private Position CovertNumberToPosition(int number)
        {
            return new Position { X = number / Game.Size, Y = number % Game.Size };
        }   
        /// <summary>
        /// Основной метод
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartGame()
        {
            CreateTable();              //создание таблицы размером size на size
            Game.GenerateReandomMap((int)numericUpDown.Value);    //генерация рандомных занчений для кнопок( 20 - количество искуственных ходов для генерации значений)
            numericUpDown.Visible = false;
            label1.Visible = false;
            Refrash();              //обновление значений на кнопках
            if (DateTime.Now.Date == new DateTime(2020, 10, 20) || DateTime.Now.Date == new DateTime(2020, 11, 08))
            {
                var form2 = new SpecialForAnya();
                form2.Show();
            }
        }
        /// <summary>
        /// метод для обновления значений на кнопках
        /// </summary>
        /// <param name="map">Map - двумерный масив с занчениями кнопок</param>
        private void Refrash()
        {
            for (int i = 0; i < Game.Size * Game.Size; i++)
            {
                Position position = CovertNumberToPosition(i);
                var value = Game.Map[position.X,position.Y];
                var but = buttons[i];
                but.Text = value.ToString();
                but.Visible = value > 0;
            }
        }       
        /// метод для проверки победной комбинации
        /// </summary>
        /// <param name="map"> Map - двумерный масив с занчениями кнопок</param>
        /// <returns>возвращает bool в случаи победной комбинации(1,2,3...14,15)</returns>
        private bool CheckWining(int[,] map)
        {
            for (int i = 1; i < Game.Size * Game.Size; i++)     // 1 т.к цифры на кнопках идут с 1 и до 15
            {
                Position pos = CovertNumberToPosition(i - 1);   //-1 т.к индексы идут от 0, а цифры на кнопках с 1
                if (!(map[pos.X, pos.Y] == i))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// метод для герерации таблицы
        /// </summary>
        private void CreateTable()
        {
            tableLayoutPanel.ColumnCount = Game.Size;
            tableLayoutPanel.RowCount = Game.Size;
            tableLayoutPanel.ColumnStyles.Clear();
            tableLayoutPanel.RowStyles.Clear();
            for (int i = 0; i < Game.Size; i++)
            {
                tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F / Game.Size));
                tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F / Game.Size));

            }
            for (int i = 0; i < Game.Size * Game.Size; i++)
            {
                buttons.Add(new Button());
                buttons[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
                buttons[i].Dock = System.Windows.Forms.DockStyle.Fill;
                buttons[i].Enabled = true;
                buttons[i].FlatAppearance.BorderSize = 0;
                buttons[i].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                buttons[i].Font = new System.Drawing.Font("WildWordsRus", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                buttons[i].ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
                buttons[i].Location = new System.Drawing.Point(11, 202);
                buttons[i].Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
                buttons[i].Name = "button" + i.ToString();
                buttons[i].Size = new System.Drawing.Size(303, 180);
                buttons[i].TabIndex = i;
                buttons[i].Tag = i.ToString();
                buttons[i].UseVisualStyleBackColor = false;
                buttons[i].Click += new System.EventHandler(Button_Click);
                Position position = CovertNumberToPosition(i);
                tableLayoutPanel.Controls.Add(buttons[i], position.Y, position.X);
                if(i != Game.Size * Game.Size - 1) Game.Map[position.X, position.Y] = buttons[i].TabIndex + 1;
            }
        }
    }
}



