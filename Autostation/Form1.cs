using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Autostation.Cafe;
using Autostation.Clients;

namespace Autostation
{
       
    public partial class Form1 : Form
    {
        public double daySum = 0;
        static int num = 1;
        List<Good> CurrentPurchases = new List<Good>();
        XDocument document = XDocument.Load("cl1");
        //ClientsXML clientsSavingFile = new ClientsXML("cl1");

        List<Client> clients = new List<Client>();
        List<Gas> gasList = new List<Gas> { new Gas("АИ-92", 1.40), new Gas("АИ-95", 1.49), new Gas("ДТ", 1.49), new Gas("ДТ-Арктика", 1.55) };
        CafeShop cafe = new CafeShop(new Good("Хот-дог", 5), new Good("Гамбургер", 4), new Good("Чизбургер", 6), new Good("Кола", 4.5));

        public Form1()
        {
            InitializeComponent();          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            comboBoxGas.DataSource = gasList;
            comboBoxGas.DisplayMember = "name";
            comboBoxGas.ValueMember = "price";
            labelGasPrice.Text = gasList[0].price.ToString();
            labelGasPriceToPay.Text = "0";
            labelCafeToPay.Text = "0";
            labelHotDogPrice.Text = cafe["Хот-дог"].price.ToString();
            labelHambPrice.Text = cafe["Гамбургер"].price.ToString();
            labelCheezePrice.Text = cafe["Чизбургер"].price.ToString();
            labelColaPrice.Text = cafe["Кола"].price.ToString();                  
        }

        private void comboBoxGas_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelGasPrice.Text = comboBoxGas.SelectedValue.ToString();
            if (radioButtonLitr.Checked && textBox1.Text.Length != 0)
            {
                double num = 0;
                if (double.TryParse(textBox1.Text, out num))
                {
                    ((Gas)comboBoxGas.SelectedItem).num = num;
                    textBox2.TextChanged -= textBox2_TextChanged;
                    textBox2.Text = labelGasPriceToPay.Text = ((Gas)comboBoxGas.SelectedItem).SumToPayForGas();
                }
                else
                {
                    ((Gas)comboBoxGas.SelectedItem).num = 0;
                    textBox2.Text = labelGasPriceToPay.Text = "0";
                    textBox2.TextChanged += textBox2_TextChanged;
                }
            }

            if (radioButtonSum.Checked && textBox2.Text.Length != 0)
            {
                double sum = 0;
                if (double.TryParse(textBox2.Text, out sum))
                {
                    textBox1.TextChanged -= textBox1_TextChanged;
                    ((Gas)comboBoxGas.SelectedItem).sum = sum;
                    labelGasPriceToPay.Text = textBox2.Text;
                    textBox1.Text = ((Gas)comboBoxGas.SelectedItem).NumOfLitres();
                }
                else
                {
                    ((Gas)comboBoxGas.SelectedItem).sum = 0;
                    textBox1.Text = labelGasPriceToPay.Text = "0";
                    textBox1.TextChanged += textBox1_TextChanged;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double num = 0;
            if (double.TryParse(textBox1.Text, out num))
            {
                ((Gas)comboBoxGas.SelectedItem).num = num;
                textBox2.TextChanged -= textBox2_TextChanged;
                textBox2.Text = labelGasPriceToPay.Text = ((Gas)comboBoxGas.SelectedItem).SumToPayForGas();
            }
            else
            {
                ((Gas)comboBoxGas.SelectedItem).num = 0;
                textBox2.Text = labelGasPriceToPay.Text = "0";
                textBox2.TextChanged += textBox2_TextChanged;
            }
        }

        private void radioButtonLitr_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.ReadOnly = false;
            textBox1.Focus();
            textBox2.Text = "";
            textBox2.ReadOnly = true;
        }

        private void radioButtonSum_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.ReadOnly = false;
            textBox1.Text = "";
            textBox2.Focus();
            textBox1.ReadOnly = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            double sum = 0;
            if (double.TryParse(textBox2.Text, out sum))
            {
                textBox1.TextChanged -= textBox1_TextChanged;
                ((Gas)comboBoxGas.SelectedItem).sum = sum;
                labelGasPriceToPay.Text = textBox2.Text;
                textBox1.Text = ((Gas)comboBoxGas.SelectedItem).NumOfLitres();
            }
            else
            {
                ((Gas)comboBoxGas.SelectedItem).sum = 0;
                textBox1.Text = labelGasPriceToPay.Text = "0";
                textBox1.TextChanged += textBox1_TextChanged;
            }         
        }

        private void checkBoxHotDog_CheckedChanged(object sender, EventArgs e)
        {
            CafeSelectNum(textBoxHotDogNum);
        }

        private void checkBoxHamburger_CheckedChanged(object sender, EventArgs e)
        {
            CafeSelectNum(textBoxHambNum);
        }

        private void CafeSelectNum(TextBox textBox)
        {
            if (textBox.ReadOnly == true)
            {
                textBox.ReadOnly = false;
                textBox.Focus();
            }
            else
            {
                textBox.ReadOnly = true;
                textBox.Text = "0";
            }
        }

        private void checkBoxCheezBurg_CheckedChanged(object sender, EventArgs e)
        {
            CafeSelectNum(textBoxCheezeNum);
        }

        private void checkBoxCola_CheckedChanged(object sender, EventArgs e)
        {
            CafeSelectNum(textBoxColaNum);
        }

        private void CafeToPayMethod(TextBox textBox, string goodName )
        {
            double num = 0;
            double.TryParse(textBox.Text, out num);
            cafe[goodName].num = num;
            labelCafeToPay.Text = cafe.SetTotalShopPrice();
        }

        private void textBoxHotDogNum_TextChanged(object sender, EventArgs e)
        {
            CafeToPayMethod(textBoxHotDogNum, "Хот-дог");         
        }

        private void textBoxHambNum_TextChanged(object sender, EventArgs e)
        {
            CafeToPayMethod(textBoxHambNum, "Гамбургер");
        }

        private void textBoxCheezeNum_TextChanged(object sender, EventArgs e)
        {
            CafeToPayMethod(textBoxCheezeNum, "Чизбургер");
        }

        private void textBoxColaNum_TextChanged(object sender, EventArgs e)
        {
            CafeToPayMethod(textBoxColaNum, "Кола");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Filter1(e, textBox1);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Filter1(e, textBox2);
        }

        private void Filter1(KeyPressEventArgs e, TextBox textBox)
        {
            char number = e.KeyChar;
            if (((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44)) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
                MessageBox.Show("Ошибка ввода", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //запрет ввода разделителя первым символом.
            if (textBox.SelectionStart == 0 & e.KeyChar == ',')
            {
                e.Handled = true;
                MessageBox.Show("Ошибка ввода", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //запрет на ввод таких значений как 97.980, 0.33333. Должно быть 8.30, 5.09
            if (textBox.Text.IndexOf(',') > 0)
            {
                if (textBox.Text.Substring(textBox.Text.IndexOf(',')).Length > 2)
                {
                    if (e.KeyChar != (char)Keys.Back)
                    {
                        e.Handled = true;
                        MessageBox.Show("Ошибка ввода", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            //Ввод только 1 разделителя
            if (e.KeyChar == ',')
            {
                if (textBox.Text.IndexOf(',') != -1)
                {
                    e.Handled = true;
                    MessageBox.Show("Ошибка ввода", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Filter2(KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
                MessageBox.Show("Ошибка ввода", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxHotDogNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            Filter2(e);
        }

        private void textBoxHambNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            Filter2(e);
        }

        private void textBoxCheezeNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            Filter2(e);
        }

        private void textBoxColaNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            Filter2(e);
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            if (labelCafeToPay.Text != "0" || labelGasPriceToPay.Text != "0")
            {
                double sumMarket = 0;
                double sumGasStation = 0;

                double.TryParse(labelCafeToPay.Text, out sumMarket);
                double.TryParse(labelGasPriceToPay.Text, out sumGasStation);
                daySum += sumMarket + sumGasStation;
                labelClientSum.Text = (sumMarket + sumGasStation).ToString("0.00");
                labelDaySum.Text = daySum.ToString("0.00");
                groupBoxGas.Enabled = groupBoxCafe.Enabled = buttonCalculate.Enabled = false;
                CurrentPurchases.Add(comboBoxGas.SelectedItem as Good);
                CurrentPurchases.AddRange(cafe.SoldGoods());
                clients.Last().SetGoods(double.Parse(labelClientSum.Text), CurrentPurchases);
                clients.Last().SavingClientToXML(document, num);
                labelClientNum.Text = "";
                num++;
                CurrentPurchases.Clear();
            }
        }

         private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show($"Выручка за день: {daySum.ToString("0.00")}", "Выручка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelDateTime.Text = DateTime.Now.ToString();
        }

        private void buttonNewClient_Click(object sender, EventArgs e)
        {
            labelClientNum.Text = num.ToString();
            groupBoxGas.Enabled = groupBoxCafe.Enabled = buttonCalculate.Enabled = true;
            labelClientSum.Text = "0";
            textBox1.Text = "";
            textBox2.Text = "";
            textBoxHotDogNum.Text = "";
            textBoxColaNum.Text = "";
            textBoxHambNum.Text = "";
            textBoxCheezeNum.Text = "";
            clients.Add(new Client(DateTime.Now));            
        }

        private void Arch_Click(object sender, EventArgs e)
        {
            Archive archive = new Archive();
            archive.ShowDialog();
        }
    }
 }
