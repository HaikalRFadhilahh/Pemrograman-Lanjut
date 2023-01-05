using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Data.SQLite;

namespace HaikalResponsi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadGrid();
        }

        Database conn = new Database();

        public void clearData()
        {
            name_txt.Clear();
            age_txt.Clear();
            gender_txt.Clear();
            city_txt.Clear();
            search_txt.Clear();
        }

        public void LoadGrid()
        {
            SQLiteCommand cmd = new SQLiteCommand("select * from FirstTable", conn.db);
            DataTable dt = new DataTable();
            conn.OpenConnection();
            SQLiteDataReader hasil = cmd.ExecuteReader();
            dt.Load(hasil);
            conn.CloseConnection();
            datagrid.ItemsSource = dt.DefaultView;
        }

        private void ClearDataBtn_Click(object sender, RoutedEventArgs e)
        {
            clearData();
        }

        public bool isValid()
        {
            if (name_txt.Text == string.Empty)
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (age_txt.Text == string.Empty)
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (gender_txt.Text == string.Empty)
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (city_txt.Text == string.Empty)
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void InsertBtn_Click(object Sender, RoutedEventArgs e)
        {
            conn.OpenConnection();
            try
            {
                if (isValid())
                {
                    SQLiteCommand cmd = new SQLiteCommand("INSERT INTO FirstTable(Name,Age,Gender,City) VALUES(@Name,@Age,@Gender,@City)", conn.db);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Name", name_txt.Text);
                    cmd.Parameters.AddWithValue("@Age", age_txt.Text);
                    cmd.Parameters.AddWithValue("@Gender", gender_txt.Text);
                    cmd.Parameters.AddWithValue("@City", city_txt.Text);

                    cmd.ExecuteNonQuery();
                    conn.CloseConnection();
                    LoadGrid();
                    MessageBox.Show("Successfully registered", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            conn.OpenConnection();
            SQLiteCommand cmd = new SQLiteCommand("delete from FirstTable where ID = " + search_txt.Text + " ", conn.db);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been delete", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                conn.CloseConnection();
                clearData();
                LoadGrid();
                conn.CloseConnection();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not Delete" + ex.Message);
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            conn.OpenConnection();
            SQLiteCommand cmd = new SQLiteCommand("update firstTable set Name = '" + name_txt.Text + "',Age = '" + age_txt.Text + "',Gender = '" + gender_txt.Text + "',City= '" + city_txt.Text + "' WHERE ID ='" + search_txt.Text + "' ", conn.db);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been update succesfully", "update", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.CloseConnection();
                clearData();
                LoadGrid();
            }
        }
    }
}
