using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scanner
{
    public partial class Form1 : Form
    {
        ParserTree pt;
        Parser parse;
        public Form1()
        {
            InitializeComponent();
           
        }
        
        public static List<string> scannerResults;
        Input getInputCode;
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        public void PrintOnGrid(string s1, string s2)
        {
            DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row.Cells[0].Value = s1;
            row.Cells[1].Value = " : "+s2;
            if (s2 == "Number")
                scannerResults.Add("Number" + s1);
            else if (s2 == "ID")
                scannerResults.Add("ID" + s1);
            else
                scannerResults.Add(s2);
            dataGridView1.Rows.Add(row);
        }
        
        

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op= new OpenFileDialog();
            try
            {
                DialogResult res = op.ShowDialog();
                string name = op.FileName;
                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    textBox1.Text = System.IO.File.ReadAllText(name);
                    getInputCode = new Input(name);
                }
            }
            catch {
                MessageBox.Show("error");
              
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            initialization initialize = new initialization();
            scanner scannInputCode = new scanner();
            dataGridView1.Rows.Clear();
            scannerResults = new List<string>();
            dataGridView1.Refresh();
            scannInputCode.scan(getInputCode.getInput(), initialize.getReservedWords(), initialize.getSpecialSymbols(),this);

            
            button4.Enabled = !scanner.scannerError;


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

           
            int index = 0;
            parse = new Parser();
            parse.statementSequence(ref index);
            if (Parser.parserError == true)
                MessageBox.Show("There is a parser Error");
            pt = new ParserTree();
            pt.BringToFront();
            //this.Hide();
            pt.Show();
        }
    }
}
