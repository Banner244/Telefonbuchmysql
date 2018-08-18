using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace mysqltelefonbuch
{
    public partial class Form1 : Form
    {
        string myConnectionString = "SERVER=127.0.0.1;" +
                    "DATABASE=db;" +
                    "UID=db;" +
                    "PASSWORD=pass;";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM daten";
            MySqlDataReader Reader;
            connection.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                string row = "";
                
                for (int i = 0; i < Reader.FieldCount; i++)
                {
              //      row = Reader.GetValue(i).ToString()/* + ", "*/;
                    if(i==Reader.FieldCount-1)
                    {
                        ListViewItem lvl = new ListViewItem(Reader.GetValue(0).ToString());
                        lvl.SubItems.Add(Reader.GetValue(1).ToString());
                        lvl.SubItems.Add(Reader.GetValue(2).ToString());
                        lvl.SubItems.Add(Reader.GetValue(3).ToString());
                        listView1.Items.Add(lvl);
                    }
                    //  listView1.Items[0].SubItems[0].Text = "";
                }
                
                //    textBox1.Text+= row;


            }
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand command = connection.CreateCommand();

            int loeschindex = int.Parse( listView1.SelectedItems[0].Text);
            command.CommandText = "DELETE FROM daten " +
                                  "WHERE ID =" + loeschindex + " " +
                                  "limit 1";
            //    "Values('dfdf')";

            command.Connection = connection;
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "INSERT INTO daten VALUES('', '"+txtNachname.Text+"', '"+txtVorname.Text+"', '"+txtTelNummer.Text+"')";
            //    "Values('dfdf')";

            command.Connection = connection;
            connection.Open();
            command.ExecuteNonQuery();
        
            connection.Close();
        }
    }
}
