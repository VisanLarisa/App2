using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace App2
{
    public partial class _Continent_ : Form
    {
        public Continent continent;
        public string toDo = "";
        public Form1 form1;
        public _Continent_(Form1 form)
        {
            InitializeComponent();
            this.form1 = form;
            this.continent = form.continent;
            this.toDo = form.toDo;
            if (this.toDo=="toUpdate")
            {
                textBox1.Text = continent.Code;
                textBox2.Text = continent.Name;
            }
        }

        public string getContinent(string code)
        {
            string name = "";
            List<Continent> continents = form1.selectAllContinents();
            foreach (Continent c in continents)
                if (code == c.Code)
                    name = c.Name;
            return name;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {        
            if (toDo=="toAdd")
            {
                string code = textBox1.Text.Trim();
                string name = textBox1.Text.Trim();
           
                List<Continent> continents = form1.selectAllContinents();
                int exist = 0;
                foreach (Continent c in continents)
                {
                    if (c.Code == code)
                    {
                        exist = 1; break;
                    }
                }
                if (exist == 0 && code.Length <= 3 && code.Length > 0)
                {
                    //insert data into table //try
                    using (MySqlConnection con = new MySqlConnection(form1.connectionString))
                    {
                        con.Open();
                        string insertCommand = "insert into continent (Code, Name) values ('" + code + "', '" + name + "')";
                        MySqlCommand cmdInsert = new MySqlCommand(insertCommand, con);
                        cmdInsert.ExecuteNonQuery();
                        MessageBox.Show("Continent saved!");
                        con.Close();
                        this.Close();
                        form1.RefreshDgv1();
                    }
                }
                else
                {
                    //raise pk violation error
                    MessageBox.Show("Continent with this code already exists!");
                }
            }
            else if(toDo=="toUpdate")
            {
                string oldCode = continent.Code;
                string code = textBox1.Text.Trim();
                string name = textBox2.Text.Trim();

                List<Continent> continents = form1.selectAllContinents();
                int exists = 0;
                foreach (Continent c in continents)
                {
                    if (c.Code == code)
                    {
                        exists++;
                        if (exists > 1)
                            break;
                    }
                }

                if (exists < 2)
                {
                    if (code.Length <= 3 && code.Length > 0)
                    {
                        using (MySqlConnection con = new MySqlConnection(form1.connectionString))
                        {
                            con.Open();
                            string command = "update continent set Code='" + code + "', Name='" + name + "' where Code='" + oldCode + "'";
                            MySqlCommand cmd = new MySqlCommand(command, con);
                            cmd.ExecuteNonQuery();
                            string cascadeCommand = "update country set Continent='" + getContinent(code) + "' where ContinentCode='" + code + "'";
                            MySqlCommand cmdCascade = new MySqlCommand(cascadeCommand, con);
                            cmdCascade.ExecuteNonQuery();
                            MessageBox.Show("Continent " + name + " updated Successfully!");
                            con.Close();
                            form1.RefreshDgv1();
                        }
                    }
                    else
                    {
                        //raise pk violation error
                        MessageBox.Show("The length of the code is not valid (1-3 ch)!");
                    }
                }
                else
                {
                    MessageBox.Show("Continent " + code + " already exists!");
                }
                this.Close();
            }
        }
    }
}
