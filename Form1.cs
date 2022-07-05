using System.Text.Json;
using System;
namespace Conversor_de_Moedas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public static async Task<double> ProcessRepositories(string moeda)
        {
            var wc = new HttpClient();
            var streamTask = wc.GetStringAsync($"https://economia.awesomeapi.com.br/json/last/{moeda}");
            var preco = await streamTask;

            var props = JsonSerializer.Deserialize<Dictionary<string, Propriedades>>(preco);

            return Double.Parse(props[moeda.Replace("-", "")].bid.Replace(".", ","));
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                if (comboBox1.Text != comboBox2.Text)
                {
                    var price = await ProcessRepositories(comboBox1.Text + "-" + comboBox2.Text);
                    var ValorConverter = textBox1.Text;
                    label1.Text += (price * double.Parse(ValorConverter)).ToString("F2");
                }
                else
                {
                    MessageBox.Show("Escolha moedas diferentes");
                }
            }
            else
            {
                MessageBox.Show("Digite algum valor");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Text = comboBox1.SelectedIndex.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Text = comboBox2.SelectedIndex.ToString();
        }
    }
}