using System;
using System.Windows.Forms;

namespace Сalculator
{
    public partial class Form1 : Form
    {
        /// Объъект класса Контроллер
        TCtrl ctrl = new TCtrl();

        /// Режим для работы с числами
        //private string mode = "real";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Устанавливаем начальное значение в поле ввода
            textBox1.Text = ctrl.pNumber.StrNumber;
            // Устанавливаем начальное положение для трекбара
            trackBar1.Value = ctrl.pNumber.IntP;
            // Устанавливаем начальное значение для поля основания системы счисления
            textBox2.Text = ctrl.pNumber.StrP;
            // Обновляем состояние кнопок
            UpdateButtons();
        }

        // Обработка нажатия кнопок
        private void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int operation = Convert.ToInt16(button.Tag.ToString());
            DoCommand(operation);
        }

        /// Выполнить определенную команду 
        private void DoCommand(int command)
        {
            try
            {
                ctrl.DoCommandCalculator(command);
                string number = ctrl.pNumber.StrNumber;

                //if (mode == "int")
                //{
                //    int indexDelim = number.IndexOf(',');

                //    if (indexDelim != -1)
                //    {
                //        number = number.Remove(number.IndexOf(','));
                //    }
                //}

                textBox1.Text = number;

                // Обработка команды для кнопки C
                if (command == 20 || command == 31)
                {
                    trackBar1.Value = ctrl.pNumber.IntP;
                    textBox2.Text = ctrl.pNumber.StrP;
                    UpdateButtons();
                }
                // Обработка операций с памятью
                if (command >= 21 && command <= 24)
                {
                    UpdateButtons();
                }
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Произошло переполнение даннных", "Ошибка");
                DoCommand(19);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                DoCommand(19);
            }
        }

        /// Обновление состояния кнопок
        private void UpdateButtons()
        {
            foreach (Control item in Controls)
            {
                if (item is Button)
                {
                    int tag = Convert.ToInt16(item.Tag.ToString());

                    if (tag < trackBar1.Value)
                    {
                        item.Enabled = true;
                    }
                    if (tag >= trackBar1.Value && tag <= 15)
                    {
                        item.Enabled = false;
                    }
                    if ((tag == 21 || tag == 22) && ctrl.GetMemoryState() == "On")
                    {
                        item.Enabled = true;
                    }
                    if ((tag == 21 || tag == 22) && ctrl.GetMemoryState() == "Off")
                    {
                        item.Enabled = false;
                    }
                    //if (tag == 17 && IntModeToolStripMenuItem.CheckState == CheckState.Checked)
                    //{
                    //    item.Enabled = false;
                    //}
                    //if (tag == 17 && RealModeToolStripMenuItem.CheckState == CheckState.Checked)
                    //{
                    //    item.Enabled = true;
                    //}
                }
            }
        }

        // Обратотка изменения положения ползунка трекбара
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            ctrl.pNumber.IntP = trackBar1.Value;
            textBox2.Text = ctrl.pNumber.StrP;
            UpdateButtons();
            DoCommand(19);
        }

        // Обработка изменения режима на целые числа
        //private void IntModeToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    IntModeToolStripMenuItem.CheckState = CheckState.Checked;
        //    RealModeToolStripMenuItem.CheckState = CheckState.Unchecked;
        //    UpdateButtons();
        //    DoCommand(20);
        //    mode = "int";
        //}

        //Обработка изменения режима на вещественные числа
        //private void RealModeToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    IntModeToolStripMenuItem.CheckState = CheckState.Unchecked;
        //    RealModeToolStripMenuItem.CheckState = CheckState.Checked;
        //    UpdateButtons();
        //    DoCommand(20);
        //    mode = "real";
        //}

        // Обработка открытия Справки
        private void InfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Info_Form info = new Info_Form();
            //info.ShowDialog();
        }

        // Обработка нажатия клавиш
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int operation = -1;
            if (e.KeyChar >= 'A' && e.KeyChar <= 'F')
            {
                operation = (int)e.KeyChar - 'A' + 10;
            }
            else if (e.KeyChar >= 'a' && e.KeyChar <= 'f')
            {
                operation = (int)e.KeyChar - 'a' + 10;
            }
            else if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                operation = (int)e.KeyChar - '0';
            }
            else if ((e.KeyChar == '.' || e.KeyChar == ',') ) //&& RealModeToolStripMenuItem.CheckState == CheckState.Checked
            {
                operation = 17;
            }
            else if ((int)e.KeyChar == 8)
            {
                operation = 18;
            }
            else if (e.KeyChar == '=')
            {
                operation = 31;
            }
            else if (e.KeyChar == '/')
            {
                operation = 28;
            }
            else if (e.KeyChar == '*')
            {
                operation = 27;
            }
            else if (e.KeyChar == '+')
            {
                operation = 25;
            }
            else if (e.KeyChar == '-')
            {
                operation = 26;
            }
            if (((operation < trackBar1.Value) || (operation >= 16)) && (operation != -1))
            {
                DoCommand(operation);
            }

            textBox1.SelectionStart = ctrl.pNumber.StrNumber.Length;
            e.Handled = true;
        }

        // Обработка нажатия управляющих клавиш
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DoCommand(20);
            }
            if (e.KeyCode == Keys.Enter)
            {
                DoCommand(31);
            }
            textBox1.SelectionStart = ctrl.pNumber.StrNumber.Length;
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.Show();
        }

        private void историяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 history=new Form2();
            history.Show();
            if(ctrl.history.Count()==0)
            {
                history.textBox1.AppendText("История пуста");
            }
            history.textBox1.AppendText(ctrl.history.GetRecord());
        }
    }
}
