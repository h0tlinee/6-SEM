using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Converter
{
    public partial class Form1 : Form
    {
        //Объект класса Управление.
        Control_ ctl = new Control_();

        public Form1()
        {
            InitializeComponent();
            button20.Select();
            KeyPreview = true;
        }

        //Обработчик события нажатия командной кнопки.
        private void button_Click(object sender, EventArgs e)
        {
            //ссылка на компонент, на котором кликнули мышью
            System.Windows.Forms.Button but = (System.Windows.Forms.Button)sender;
            //номер выбранной команды
            int j = Convert.ToInt16(but.Tag.ToString());
            DoCmnd(j);
        }

        //Выполнить команду.
        private void DoCmnd(int j)
        {
            if (j == 19) { textBox2.Text = ctl.DoCmnd(j); }
            else
            {
                if (ctl.St == Control_.State.Преобразовано)
                {
                    textBox1.Text = ctl.DoCmnd(18);
                }
                textBox1.Text = ctl.DoCmnd(j);
                textBox2.Text = "";
            }
        }

        //Обновляет состояние командных кнопок по основанию с.сч.исходного числа.
        private void UpdateButtons()
        {
            //просмотреть все компоненты формы
            foreach (Control i in this.Controls)
            {
                if (i is System.Windows.Forms.Button)
                {
                    int j = Convert.ToInt16(i.Tag.ToString());
                    if (j < trackBar1.Value)
                    {
                        i.Enabled = true;
                    }
                    if ((j >= trackBar1.Value) && (j <= 15))
                    {
                        i.Enabled = false;
                    }
                }
            }
        }

        //Изменяет значение основания с.сч. исходного числа.
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            numericUpDown1.Value = trackBar1.Value;
            this.UpdateP1();
        }

        //Изменяет значение основания с.сч. исходного числа.
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            trackBar1.Value = Convert.ToByte(numericUpDown1.Value);
            this.UpdateP1();
        }

        //Выполняет необходимые обновления при смене ос. с.сч.р1.
        private void UpdateP1()
        {
            label3.Text = "" + trackBar1.Value;
            ctl.Pin = trackBar1.Value;
            this.UpdateButtons();
            textBox1.Text = ctl.DoCmnd(18);
            textBox2.Text = "";
        }

        //Изменяет значение основания с.сч. результата.
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            numericUpDown2.Value = trackBar2.Value;
            this.UpdateP2();
        }

        //Изменяет значение основания с.сч. результата.
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            trackBar2.Value = Convert.ToByte(numericUpDown2.Value);
            this.UpdateP2();
        }

        //Выполняет необходимые обновления при смене ос. с.сч.р2.
        private void UpdateP2()
        {
            ctl.Pout = trackBar2.Value;
            textBox2.Text = ctl.DoCmnd(19);
            label4.Text = "" + trackBar2.Value;
        }

        //Пункт меню Выход.
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Пункт меню Справка.
        //*private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        //{
           // AboutBox1 a = new AboutBox1();
           // a.Show();
        //}

        
        

       

        //Обработка клавиш управления.
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            button20.Select();
            System.Windows.Forms.Button buttonToClick = null;
            System.Console.WriteLine(e.KeyCode);
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    buttonToClick = button20; break;
                case Keys.Delete:
                    buttonToClick = button19; break;
                case Keys.Back:
                    buttonToClick = button18; break;
                case Keys.OemPeriod:
                    buttonToClick = button17; break;
                case Keys.F:
                    buttonToClick = button16; break;
                case Keys.E:
                    buttonToClick = button15; break;
                case Keys.D:
                    buttonToClick = button14; break;
                case Keys.C:
                    buttonToClick = button13; break;
                case Keys.B:
                    buttonToClick = button12; break;
                case Keys.A:
                    buttonToClick = button11; break;
                case Keys.NumPad9:
                    buttonToClick = button10; break;
                case Keys.NumPad8:
                    buttonToClick = button9; break;
                case Keys.NumPad7:
                    buttonToClick = button8; break;
                case Keys.NumPad6:
                    buttonToClick = button7; break;
                case Keys.NumPad5:
                    buttonToClick = button6; break;
                case Keys.NumPad4:
                    buttonToClick = button5; break;
                case Keys.NumPad3:
                    buttonToClick = button4; break;
                case Keys.NumPad2:
                    buttonToClick = button3; break;
                case Keys.NumPad1:
                    buttonToClick = button2; break;
                case Keys.NumPad0:
                    buttonToClick = button1; break;
                case Keys.Add:
                    {
                        if (trackBar1.Value != 16)
                        {
                            trackBar1.Value += 1;
                            numericUpDown1.Value = trackBar1.Value;
                            this.UpdateP1();
                            break;
                        }
                        break;
                    }
                case Keys.Subtract:
                    {
                        if(trackBar1.Value != 2) { 
                        trackBar1.Value -= 1;
                        numericUpDown1.Value = trackBar1.Value;
                        this.UpdateP1();
                        break;
                    }
                        break;

                    }
                case Keys.Oemplus:
                    {
                        if (trackBar2.Value != 16)
                        {
                            trackBar2.Value += 1;
                            numericUpDown2.Value = trackBar2.Value;
                            this.UpdateP2();
                            break;
                        }
                        break;
                    }
                case Keys.OemMinus:
                    {
                        if (trackBar2.Value != 2)
                        {
                            trackBar2.Value -= 1;
                            numericUpDown2.Value = trackBar2.Value;
                            this.UpdateP2();
                            break;
                        }
                        break;

                    }

            }
            if (buttonToClick != null)
            {
                //HighlightButton(buttonToClick);
                buttonToClick.PerformClick();
            }

        }

        private void историяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 history = new Form2();
            history.Show();
            if (ctl.history.Count() == 0)
            {
                history.textBox1.AppendText("История пуста");
                return;
            }
            //Ообразить историю.
           
                history.textBox1.AppendText(ctl.history.GetRecord());
            
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 about=new Form3();
            about.Show();   
        }
    }

        
    }
