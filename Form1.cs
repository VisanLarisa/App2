using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.UI.WindowManagement;


namespace App2
{
  
    public partial class Form1 : Form
    {
        //private AppWindow _app;
        public string connectionString = "server=localhost; user=root; database=world2; port=3306; password=Oracle!Password19";

        public List<Continent> continents;
        public List<Country> countries;
        public List<City> cities;
        public string cursor = "continent";

        public Continent continent;
        public Country country;
        public City city;
        public string toDo = "";

        public Form1()
        {
            continents = new List<Continent>();
            countries = new List<Country>();
            cities = new List<City>();
            continent = new Continent();
            country = new Country();
            city = new City();
            InitializeComponent();
            //_app = GetAppWindowForCurrentWindow();
        }

/*        private AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(wndId);
        }*/

        public List<Continent> selectAllContinents()
        {
            List<Continent> continents = new List<Continent>();
            using (MySqlConnection con = new MySqlConnection(this.connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from continent", con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //Extract / fetch my data
                    Continent continent = new Continent();
                    continent.Code = reader["Code"].ToString();
                    continent.Name = reader["Name"].ToString();
                    continents.Add(continent);
                }
                reader.Close();
            }
            return continents;
        }

        public List<Country> selectAllCountries()
        {
            List<Country> countries = new List<Country>();

            using (MySqlConnection con = new MySqlConnection(this.connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from country", con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //Extract / fetch my data
                    Country country = new Country();
                    country.Code = reader["Code"].ToString();
                    country.Name = reader["Name"].ToString();
                    country.Continent = reader["Continent"].ToString();
                    country.Region = reader["Region"].ToString();
                    country.SurfaceArea = (float)Convert.ToDouble(reader["SurfaceArea"]);
                    try { country.IndepYear = Convert.ToInt16(reader["IndepYear"]); }
                    catch { country.IndepYear = 0; }

                    country.Population = Convert.ToInt32(reader["Population"]);
                    try { country.LifeExpectency = (float)Convert.ToDouble(reader["LifeExpectancy"]); }
                    catch { country.LifeExpectency = 0; }
                    try { country.GNP = (float)Convert.ToDouble(reader["GNP"]); }
                    catch { country.GNP = 0; }
                    try { country.GNPOId = (float)Convert.ToDouble(reader["GNPOId"]); }
                    catch { country.GNPOId = 0; }
                    country.LocalName = reader["LocalName"].ToString();
                    country.GovernmentForm = reader["GovernmentForm"].ToString();
                    country.HeadOfState = reader["HeadOfState"].ToString();
                    try { country.Capital = Convert.ToInt32(reader["Capital"]); }
                    catch { country.Capital = 0; }
                    country.Code2 = reader["Code2"].ToString();
                    country.ContinentCode = reader["ContinentCode"].ToString();

                    //also extract the cities
                    //country.cities=

                    countries.Add(country);
                }
                reader.Close();
            }
            return countries;
        }

        public List<City> selectAllCities()
        {
            List<City> cities = new List<City>();

            //Connect to MySQL
            using (MySqlConnection con = new MySqlConnection(this.connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from city", con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //Extract / fetch my data
                    City city = new City();
                    city.Id = Convert.ToInt32(reader["ID"]);
                    city.Name = reader["Name"].ToString();
                    city.CountryCode = reader["CountryCode"].ToString();
                    city.District = reader["District"].ToString();
                    city.Population = Convert.ToInt32(reader["Population"]);

                    cities.Add(city);
                }
                reader.Close();
            }
            return cities;
        }

        public List<Country> selectCountriesByContinent(string continentCode)
        {
            List<Country> countries = new List<Country>();

            using (MySqlConnection con = new MySqlConnection(this.connectionString))
            {
                con.Open();
                string command = "select * from country where ContinentCode = '" + continentCode + "'";
                MySqlCommand cmd = new MySqlCommand(command, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //Extract / fetch my data
                    Country country = new Country();
                    country.Code = reader["Code"].ToString();
                    country.Name = reader["Name"].ToString();
                    country.Continent = reader["Continent"].ToString();
                    country.Region = reader["Region"].ToString();
                    country.SurfaceArea = (float)Convert.ToDouble(reader["SurfaceArea"]);
                    try { country.IndepYear = Convert.ToInt16(reader["IndepYear"]); }
                    catch { country.IndepYear = 0; }

                    country.Population = Convert.ToInt32(reader["Population"]);
                    try { country.LifeExpectency = (float)Convert.ToDouble(reader["LifeExpectancy"]); }
                    catch { country.LifeExpectency = 0; }
                    try { country.GNP = (float)Convert.ToDouble(reader["GNP"]); }
                    catch { country.GNP = 0; }
                    try { country.GNPOId = (float)Convert.ToDouble(reader["GNPOId"]); }
                    catch { country.GNPOId = 0; }
                    country.LocalName = reader["LocalName"].ToString();
                    country.GovernmentForm = reader["GovernmentForm"].ToString();
                    country.HeadOfState = reader["HeadOfState"].ToString();
                    try { country.Capital = Convert.ToInt32(reader["Capital"]); }
                    catch { country.Capital = 0; }
                    country.Code2 = reader["Code2"].ToString();
                    country.ContinentCode = reader["ContinentCode"].ToString();


                    countries.Add(country);
                }
                reader.Close();
            }
            return countries;
        }

        public List<City> selectCitiesByCountry(string countryCode)
        {
            List<City> cities = new List<City>();

            using (MySqlConnection con = new MySqlConnection(this.connectionString))
            {
                con.Open();
                string command = "select * from city where CountryCode = '" + countryCode + "'";
                MySqlCommand cmd = new MySqlCommand(command, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //Extract / fetch my data
                    City city = new City();
                    city.Id = Convert.ToInt32(reader["ID"]);
                    city.Name = reader["Name"].ToString();
                    city.CountryCode = reader["CountryCode"].ToString();
                    city.District = reader["District"].ToString();
                    city.Population = Convert.ToInt32(reader["Population"]);

                    cities.Add(city);
                }
                reader.Close();
            }
            return cities;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buttonSee.Show();
            dgv1.Show();
            dgv2.Hide();
            dgv3.Hide();

            dgv1.DataSource = this.continents=selectAllContinents();
            dgv2.DataSource = this.countries = selectAllCountries(); 
            dgv3.DataSource = this.cities = selectAllCities();        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel4.Height = button1.Height;
            panel4.Top = button1.Top;
            dgv1.Show();
            dgv2.Hide();
            dgv3.Hide();
            cursor = "continent";
            buttonSee.Show();
            label2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshDgv2();
            panel4.Height = button2.Height;
            panel4.Top = button2.Top;
            dgv2.Show();
            dgv1.Hide();
            dgv3.Hide();
            cursor = "country";
            buttonSee.Show();
            label2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshDgv3();
            panel4.Height = button3.Height;
            panel4.Top = button3.Top;
            dgv3.Show();
            dgv2.Hide();
            dgv1.Hide();
            cursor = "city";
            buttonSee.Hide();
            label2.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (cursor == "continent")
            {
                this.toDo = "toDelete";
                if (dgv1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Choose a continent!");
                    return;
                }
                if (MessageBox.Show("Are you sure?", "Delete Continent",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    this.continent = (Continent)dgv1.CurrentRow.DataBoundItem;
                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        con.Open();

                        string command = "delete from continent where Code = '" + this.continent.Code + "'";
                        MySqlCommand cmd = new MySqlCommand(command, con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Continent " + continent.Code + " Deleted Successfully!");
                        con.Close();
                        RefreshDgv1();
                        RefreshDgv2();
                    }
                }
            }
            else if (cursor=="country")
            {
                this.toDo = "toDelete";
                if (dgv2.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Choose a country!");
                    return;
                }
                if (MessageBox.Show("Are you sure?", "Delete Country",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    this.country = (Country)dgv2.CurrentRow.DataBoundItem;
                    try
                    {
                        using (MySqlConnection con = new MySqlConnection(connectionString))
                        {
                            con.Open();
                            string command = "delete from country where Code = '" + this.country.Code + "'";
                            MySqlCommand cmd = new MySqlCommand(command, con);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show(" Country " + this.country.Code + " deleted! ");
                            con.Close();
                            RefreshDgv2();
                            RefreshDgv3();
                        }
                    }
                    catch
                    {
                        MessageBox.Show(" An error occured and stopped the deletion.");
                    }
                }
            }

            else if (cursor == "city") {
                this.toDo = "toDelete";
                if (dgv3.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Choose a City!");
                    return;
                }
                if (MessageBox.Show("Are you sure?", "Delete city",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    this.city = (City)dgv3.CurrentRow.DataBoundItem;
                    using (MySqlConnection con = new MySqlConnection(this.connectionString))
                    {
                        try
                        {
                            con.Open();
                            string command = "delete from city where ID = " + city.Id;
                            MySqlCommand cmd = new MySqlCommand(command, con);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show(" City " + this.city.Name + " deleted! ");
                            con.Close();
                            RefreshDgv3();
                        }
                        catch
                        {
                            MessageBox.Show(" An error occured and stopped the deletion.");
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.toDo = "toAdd";
            if (cursor == "continent")
            {
                    _Continent_ form = new _Continent_(this);
                    form.Show();                   
            }

            else if (cursor == "country")
            {
                    _Country_ form = new _Country_(this);
                    form.Show();
            }

            else if (cursor == "city")
            {             
                    _City_ form = new _City_(this);
                    form.Show();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.toDo = "toUpdate";
            if (cursor == "continent")
            {
                if (dgv1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Choose a continent");
                    return;
                }
                this.continent=(Continent)dgv1.CurrentRow.DataBoundItem;
                if (continent != null)
                {
                    _Continent_ form = new _Continent_(this);
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Continent null!");
                }
            }

            else if (cursor == "country")
            {
                if (dgv2.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Choose a country");
                    return;
                }
                this.country = (Country)dgv2.CurrentRow.DataBoundItem;

                if (country != null)
                {
                    _Country_ form = new _Country_(this);
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Country null!");
                }

            }
            else if (cursor == "city")
            {
                if (dgv3.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Choose a city");
                    return;
                }

                this.city = (City)dgv3.CurrentRow.DataBoundItem;

                if (city != null)
                {
                    _City_ form = new _City_(this);
                    form.Show();
                }
                else
                {
                    MessageBox.Show("City null!");
                }
            }
        }

        public void RefreshDgv1()
        {
            dgv1.DataSource = selectAllContinents();
        }
        public void RefreshDgv2()
        {
            dgv2.DataSource = selectAllCountries();
        }
        public void RefreshDgv3()
        {
            dgv3.DataSource = selectAllCities();
        }

        private void buttonSee_Click(object sender, EventArgs e)
        {
            if(cursor=="continent")
            {
                if (dgv1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Choose a continent");
                    return;
                }
                this.continent = (Continent)dgv1.CurrentRow.DataBoundItem;
                dgv2.DataSource = selectCountriesByContinent(this.continent.Code);

                dgv1.Hide();        
                dgv2.Show();
                label2.Text = "Countries in " + this.continent.Name + " :";
                cursor = "country";
            }
            else if (cursor == "country")
            {
                if (dgv2.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Choose a country");
                    return;
                }
                this.country = (Country)dgv2.CurrentRow.DataBoundItem;
                dgv3.DataSource = selectCitiesByCountry(this.country.Code);

                dgv2.Hide();
                dgv3.Show();
                buttonSee.Hide();
                label2.Text = "Cities in " + this.country.Name + " :";
                cursor = "city";
            }
        }
    }
}
