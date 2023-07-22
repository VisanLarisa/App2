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
    public partial class _Country_ : Form
    {
        public Country country;
        public string toDo = "";
        public Form1 form1;
        public _Country_(Form1 form)
        {
            InitializeComponent();
            this.form1 = form;
            this.country = form.country;
            this.toDo = form.toDo;
            if (this.toDo == "toUpdate")
            {
                textBoxCode.Text = country.Code;
                textBoxName.Text = country.Name;
                textBoxRegion.Text = country.Region;
                textBoxSurfece.Text = country.SurfaceArea.ToString();
                textBoxIndep.Text = country.IndepYear.ToString();
                textBoxPop.Text = country.Population.ToString();
                textBoxLife.Text = country.LifeExpectency.ToString();
                textBoxGnp.Text = country.GNP.ToString();
                textBoxGnpoid.Text = country.GNPOId.ToString();
                textBoxLocal.Text = country.LocalName;
                textBoxGov.Text = country.GovernmentForm;
                textBoxHead.Text = country.HeadOfState;
                textBoxCapital.Text = country.Capital.ToString();
                textBoxCode2.Text = country.Code2;
                textBoxCC.Text = country.ContinentCode;
            }
        }

        public string getCountry(string code)
        {
            string name = "";
            List<Country> countries = form1.selectAllCountries();
            foreach (Country c in countries)
                if (code == c.Code)
                    name = c.Name;
            return name;
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
            if (toDo == "toAdd")
            {
                try
                {
                    string oldCode = country.Code;
                    string code = textBoxCode.Text.Trim();
                    string name = textBoxName.Text.Trim();
                    string region = textBoxRegion.Text.Trim();
                    float surfaceArea = (float)Convert.ToDouble(textBoxSurfece.Text.Trim());
                    string IndepY = textBoxIndep.Text.Trim();
                    int indepYear=0;
                    if (IndepY != "")
                        indepYear = Convert.ToInt32(IndepY);
                    int population = Convert.ToInt32(textBoxPop.Text.Trim());
                    float lifeExpectency = (float)Convert.ToDouble(textBoxLife.Text.Trim());
                    string gnp = textBoxGnp.Text.Trim();
                    float GNP = 0;
                    if (gnp != "")
                    { GNP = (float)Convert.ToDouble(gnp); }
                    string gnpoid = this.textBoxGnpoid.Text.Trim().ToString();
                    float GNPOId = 0;
                    if (gnpoid != "")
                    { GNPOId = (float)Convert.ToDouble(gnpoid); }
                    string localName = textBoxLocal.Text.Trim().ToString();
                    string governmentForm = textBoxGov.Text.Trim().ToString();
                    string headOfState = textBoxHead.Text.Trim().ToString();
                    string Capital = textBoxCapital.Text.Trim().ToString();
                    int capital = 0;
                    if (Capital != "")
                        capital = Convert.ToInt32(Capital);
                    string code2 = textBoxCode2.Text.Trim().ToString();
                    string continentCode = textBoxCC.Text.Trim().ToString();
                    string continent = getContinent(continentCode);

                    //List<Country> countries = form1.selectAllCountries();
                    if (pkValidation(code))
                    {
                        if (fkValidation(continentCode))
                            using (MySqlConnection con = new MySqlConnection(form1.connectionString))
                            {
                                con.Open();
                                string insertCommand = "insert into country (Code, Name, Continent, Region, SurfaceArea, IndepYear, Population, LifeExpectancy, GNP, GNPOld, LocalName, GovernmentForm, HeadOfState, Capital, Code2, ContinentCode) values ('" + code + "', '" + name + "', '" + continent + "', '" + region + "', '" + surfaceArea + "', '" + indepYear + "', '" + population + "', '" + lifeExpectency + "', '" + GNP + "', '" + GNPOId + "', '" + localName + "', '" + governmentForm + "', '" + headOfState + "', '" + capital + "', '" + code2 + "', '" + continentCode + "')";
                                MySqlCommand cmdInsert = new MySqlCommand(insertCommand, con);
                                cmdInsert.ExecuteNonQuery();
                                MessageBox.Show("Country " + code + " successfuly added!");
                                this.Close();
                                form1.RefreshDgv2();
                            }
                    }
                    else
                    { //raise pk violation error
                        MessageBox.Show("Country " + code + " already exists!");
                    }
                }
                catch
                {
                    MessageBox.Show(" Please complete all the mandatory fields correctly! ");
                }
            }

            else if (toDo == "toUpdate")
            {
                try
                {
                    string oldCode = country.Code;
                    string code = textBoxCode.Text.Trim();
                    string name = textBoxName.Text.Trim();
                    string region = textBoxRegion.Text.Trim();
                    float surfaceArea = (float)Convert.ToDouble(textBoxSurfece.Text.Trim());
                    int indepYear = Convert.ToInt32(textBoxIndep.Text.Trim());
                    int population = Convert.ToInt32(textBoxPop.Text.Trim());
                    float lifeExpectency = (float)Convert.ToDouble(textBoxLife.Text.Trim());
                    string gnp = textBoxGnp.Text.Trim();
                    float GNP = 0;
                    if (gnp != "")
                    { GNP = (float)Convert.ToDouble(gnp); }
                    string gnpoid = this.textBoxGnpoid.Text.Trim().ToString();
                    float GNPOId = 0;
                    if (gnpoid != "")
                    { GNPOId = (float)Convert.ToDouble(gnpoid); }
                    string localName = textBoxLocal.Text.Trim().ToString();
                    string governmentForm = textBoxGov.Text.Trim().ToString();
                    string headOfState = textBoxHead.Text.Trim().ToString();
                    string Capital = textBoxCapital.Text.Trim().ToString();
                    int capital = 0;
                    if (Capital != "")
                        capital = Convert.ToInt32(Capital);
                    string code2 = textBoxCode2.Text.Trim().ToString();
                    string continentCode = textBoxCC.Text.Trim().ToString();  
                    string continent = getContinent(continentCode);

                    if (countryCodeUpdateValidation(code) < 2)
                    {
                        if (countryNameUpdateValidation(code) < 2)
                        {
                            if (fkValidation(continentCode))
                                using (MySqlConnection con = new MySqlConnection(form1.connectionString))
                                {
                                    con.Open();
                                    string updateCommand = "update country set Code='" + code + "',Name='" + name + "', Continent='" + continent + "', Region='" + region + "', SurfaceArea=" + surfaceArea + ", IndepYear=" + indepYear + ", Population=" + population + ", LifeExpectancy=" + lifeExpectency + ", GNP=" + gnp + ", GNPOld=" + gnpoid + ", LocalName='" + localName + "', GovernmentForm='" + governmentForm + "', HeadOfState='" + headOfState + "', Capital=" + capital + ", Code2='" + code2 + "', ContinentCode='" + continentCode + "' where Code='" + oldCode + "'";
                                    MySqlCommand cmdInsert = new MySqlCommand(updateCommand, con);
                                    cmdInsert.ExecuteNonQuery();
                                    MessageBox.Show("Country " + code + " successfuly updated!");
                                    form1.RefreshDgv2();
                                    this.Close();
                                }
                            else
                            {
                                MessageBox.Show("Not a valid continent!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Country with name " + name + " already exists!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Country with code" + code + " already exists!");
                    }
                }
                catch
                {
                    MessageBox.Show(" Please complete all the mandatory fields correctly! ");
                }
                this.Close();
            }
        }

        public int countryCodeUpdateValidation(string code)
        {
            int exists = 0;
            List<Country> countries = form1.selectAllCountries();
            foreach (Country c in countries)
            {
                if (c.Code == code)
                {
                    exists++;
                    if (exists > 1)
                        break;
                }
            }
            return exists;
        }

        public int countryNameUpdateValidation(string name)
        {
            int exists = 0;
            List<Country> countries = form1.selectAllCountries();
            foreach (Country c in countries)
            {
                if (c.Name == name)
                {
                    exists++;
                    if (exists > 1)
                        break;
                }
            }
            return exists;
        }

        public bool fkValidation(string continentCode)
        {
            bool isFound = false;
            List<Continent> continents = form1.selectAllContinents();
            foreach (Continent c in continents)
            {
                if (c.Code == continentCode)
                {
                    isFound = true;
                    break;
                }
            }
            return isFound;
        }

        public bool pkValidation(string countryCode)
        {
            bool countryValid = true;
            if (countryCode.Length <= 3 && countryCode.Length > 0)
            {
                List<Country> countries = form1.selectAllCountries();
                foreach (Country c in countries)
                {
                    if (c.Code == countryCode)
                    {
                        countryValid = false; break;
                    }
                }
            }
            else countryValid = false;
            return countryValid;
        }


        private void _Country__Load(object sender, EventArgs e)
        {

        }
    }
}
