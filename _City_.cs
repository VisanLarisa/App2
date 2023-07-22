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
    public partial class _City_ : Form
    {
        public City city;
        public string toDo = "";
        public Form1 form1;
        public int ID;

        public _City_(Form1 form)
        {
            InitializeComponent();
            
            this.city = form.city;
            this.form1 = form;
            this.toDo = form.toDo;
            if (this.toDo == "toUpdate")
            {
                textBoxName.Text = this.city.Name.ToString();
                textBoxDistrict.Text = this.city.District;
                textBoxPop.Text = this.city.Population.ToString();
                textBoxCC.Text = this.city.CountryCode;
            }
            currentId();
        }

        public void currentId()
        {
            using (MySqlConnection con = new MySqlConnection(this.form1.connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select MAX(ID) AS id from City", con);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    this.ID = (Convert.ToInt32(reader["id"])) + 1;
                reader.Close();
                
            }
        }

        public bool fkValidation(string countryCode)
        {
            bool isFound = false;
            List<Country> countries = form1.selectAllCountries();
            foreach (Country c in countries)
            {
                if (c.Code == countryCode)
                {
                    isFound = true;
                    break;
                }
            }
            return isFound;
        }

        public int cityValidation(string name, string district)
        {
            int cityValid = 1;
            if (name.Length > 0)
            {
                List<City> cities = form1.selectAllCities();
                foreach (City c in cities)
                {
                    if (c.Name == name)
                    {
                        if (c.District == district)
                        {
                            cityValid = 0;
                            break;
                        }
                    }
                }
            }
            else cityValid = 0;
            return cityValid;
        }

        public int cityUpdateValidation(string name, string district, int id)
        {
            int cityValid = 1;
            if (name.Length > 0)
            {
                List<City> cities = form1.selectAllCities();
                foreach (City c in cities)
                {
                    if (c.Name == name)
                    {
                        if (c.District == district)
                        {
                            if (c.Id != id)
                            {
                                cityValid = 0;
                                break;
                            }
                        }
                    }
                }
            }
            else cityValid = 0;
            return cityValid;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if(toDo=="toAdd")
            {
                try
                {
                    string Name = textBoxName.Text.Trim();
                    string District = textBoxDistrict.Text.Trim();
                    string Population = textBoxPop.Text.Trim();
                    string CountryCode = textBoxCC.Text.Trim();

                    if (cityValidation(Name, District) != 0)
                    {
                        if (fkValidation(CountryCode) && District != "" && Name != "" && Population != "")
                            using (MySqlConnection con = new MySqlConnection(form1.connectionString))
                            {
                                con.Open();
                                string insertCommand = "insert into city (Id, Name, District, Population, CountryCode) values (" + this.ID + ", '" + Name + "', '" + District + "', '" + Population + "', '" + CountryCode + "')";
                                MySqlCommand cmdInsert = new MySqlCommand(insertCommand, con);
                                cmdInsert.ExecuteNonQuery();
                                MessageBox.Show("City " + Name + " successfuly added!");
                                this.Close();
                                form1.RefreshDgv3();
                            }
                        else
                        {
                            MessageBox.Show(" Please complete all the fields!");
                        }
                    }
                    else
                    {
                        MessageBox.Show(" City " + Name + " already inserted!");
                    }
                }
                catch
                {
                    MessageBox.Show(" Please complete all the mandatory fields correctly! ");
                }
            }
            else if(toDo=="toUpdate")
            {
                try
                {
                    string Name = textBoxName.Text.Trim();
                    string District = textBoxDistrict.Text.Trim();
                    string Population = textBoxPop.Text.Trim();
                    string CountryCode = textBoxCC.Text.Trim();
                    if (cityUpdateValidation(Name, District, Convert.ToInt32(this.city.Id)) != 0)
                    {
                        if (fkValidation(CountryCode) && District != "" && Name != "" && Population != "")
                            using (MySqlConnection con = new MySqlConnection(form1.connectionString))
                            {
                                con.Open();
                                string insertCommand = "update city set Name= '" + Name + "', District= '" + District + "', Population = '" + Population + "', CountryCode = '" + CountryCode + "' where ID = " + this.city.Id;
                                MySqlCommand cmdInsert = new MySqlCommand(insertCommand, con);
                                cmdInsert.ExecuteNonQuery();
                                MessageBox.Show("City " + Name + " successfully updated!");
                                this.Close();
                                form1.RefreshDgv3();
                            }
                        else
                        {
                           MessageBox.Show(" Please complete all the fields correctly, and make sure that the country is valid!");
                        }
                    }
                    else
                    {
                       MessageBox.Show(" City " + Name + " already exisits in " + District + " district!");
                    }
                }
                catch
                {
                    MessageBox.Show(" Please complete the fields correctly! ");
                }
            }
        }
    }
}
