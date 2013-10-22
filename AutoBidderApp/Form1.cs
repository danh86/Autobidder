using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoBidder.Attributes;

namespace AutoBidderApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //if (this.Load() = )
            //{

            //}
            comboBox1.Items.Add(new Item("Prem Bid", 1));
            comboBox1.Items.Add(new Item("Fitness Bid", 2));
            comboBox1.Items.Add(new Item("Fitness Buyout", 3));
        }

        private class Item
        {
            public string Name;
            public int Value;
            public Item(string name, int value)
            {
                Name = name; Value = value;
            }
            public override string ToString()
            {
                // Generates the text shown in the combo box
                return Name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AutoBidder.Controller c = new AutoBidder.Controller();

            switch (comboBox1.SelectedText)
            {
                case "Prem Bid" :
                    c.PremBid(richTextBox1.Text);
                    break;
                case "Fitness Bid" :
                    c.FitnessBid();
                    break;
                case "Fitness Buyout" :
                    c.FitnessBuyout();
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "Process cancelled." + Environment.NewLine;
            Application.Exit();
        }
    }
}
