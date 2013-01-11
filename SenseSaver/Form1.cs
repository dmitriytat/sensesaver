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

namespace SenseSaver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void GoBtn_Click(object sender, EventArgs e)
        {
            System.IO.StreamReader myStream = null;
            String line;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                if ((myStream = new System.IO.StreamReader(openFileDialog1.FileName)) != null)
                {
                    while ((line = myStream.ReadLine()) != null)
                    {
                        String[] words = line.Split(null);
                        int main = Word(words[0]);
                        for (int i = 1; i < words.Length; i++)
                        {
                            int synonym = Word(words[i]);
                            Synonym(main, synonym);
                        }
                    }
                }

            }
        }

        private int Word(String word)
        {
            int id;
            MySqlConnection myConnection = new MySqlConnection(SenseSaver.Properties.Resources.ConnectStr);
            myConnection.Open();

            MySqlCommand myCommand = new MySqlCommand("SELECT id FROM words WHERE word=?word", myConnection);
            myCommand.Parameters.Add("?word", word);
            myCommand.Prepare();
            MySqlDataReader MyDataReader;
            MyDataReader = myCommand.ExecuteReader();
            if (MyDataReader.HasRows)
            {
                MyDataReader.Read();
                id = MyDataReader.GetInt32(0);
                MyDataReader.Close();
            }
            else
            {
                MyDataReader.Close();
                myCommand = new MySqlCommand("INSERT INTO words (`word`) VALUES (?word); select  last_insert_id();", myConnection);
                myCommand.Parameters.Add("?word", word);
                id = Convert.ToInt32(myCommand.ExecuteScalar());
            }
            myConnection.Close();
            return id;
        }


        private int GetWord(String word)
        {
            int id;
            MySqlConnection myConnection = new MySqlConnection(SenseSaver.Properties.Resources.ConnectStr);
            myConnection.Open();

            MySqlCommand myCommand = new MySqlCommand("SELECT id FROM words WHERE word=?word", myConnection);
            myCommand.Parameters.Add("?word", word);
            myCommand.Prepare();
            MySqlDataReader MyDataReader;
            MyDataReader = myCommand.ExecuteReader();
            if (MyDataReader.HasRows)
            {
                MyDataReader.Read();
                id = MyDataReader.GetInt32(0);
                MyDataReader.Close();
            }
            else
            {
                id = -1;
            }
            myConnection.Close();
            return id;
        }

        private void Synonym(int word, int synonym)
        {
            MySqlConnection myConnection = new MySqlConnection(SenseSaver.Properties.Resources.ConnectStr);
            myConnection.Open();

            MySqlCommand myCommand = new MySqlCommand("SELECT * FROM synonyms WHERE word_id=?word AND synonym_id=?synonym", myConnection);
            myCommand.Parameters.Add("?word", word);
            myCommand.Parameters.Add("?synonym", synonym);
            myCommand.Prepare();
            MySqlDataReader MyDataReader;
            MyDataReader = myCommand.ExecuteReader();
            if (MyDataReader.HasRows)
            {
                MyDataReader.Close();
            }
            else
            {
                MyDataReader.Close();
                myCommand = new MySqlCommand("INSERT INTO synonyms (`word_id`,`synonym_id`) VALUES (?word, ?synonym)", myConnection);
                myCommand.Parameters.Add("?word", word);
                myCommand.Parameters.Add("?synonym", synonym);
                myCommand.ExecuteNonQuery();
            }
            myConnection.Close();
        }

        private void Suitability(int word, int suit)
        {
            MySqlConnection myConnection = new MySqlConnection(SenseSaver.Properties.Resources.ConnectStr);
            myConnection.Open();

            MySqlCommand myCommand = new MySqlCommand("SELECT count FROM suitability WHERE word_id=?word AND suit_id=?suit", myConnection);
            myCommand.Parameters.Add("?word", word);
            myCommand.Parameters.Add("?suit", suit);
            myCommand.Prepare();
            MySqlDataReader MyDataReader;
            MyDataReader = myCommand.ExecuteReader();
            if (MyDataReader.HasRows)
            {
                MyDataReader.Read();
                int count = MyDataReader.GetInt32(0);
                MyDataReader.Close();
                count++;
                myCommand = new MySqlCommand("UPDATE suitability SET `count`=?count WHERE word_id=?word AND suit_id=?suit", myConnection);
                myCommand.Parameters.Add("?word", word);
                myCommand.Parameters.Add("?suit", suit);
                myCommand.Parameters.Add("?count", count);
                myCommand.ExecuteNonQuery();
            }
            else
            {
                MyDataReader.Close();
                myCommand = new MySqlCommand("INSERT INTO suitability (`word_id`,`suit_id`,`count`) VALUES (?word, ?suit, 0)", myConnection);
                myCommand.Parameters.Add("?word", word);
                myCommand.Parameters.Add("?suit", suit);
                myCommand.ExecuteNonQuery();
            }
            myConnection.Close();
        }

        private void BookBtn_Click(object sender, EventArgs e)
        {
            System.IO.StreamReader myStream = null;
            String line;
            char[] delimiter = { '.', ',', '!', '?', '-', ':', ';', '(', ')', '[', ']', '<', '>', '#', '*', '&', '№', '_', '«', '»', '=' };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                if ((myStream = new System.IO.StreamReader(openFileDialog1.FileName)) != null)
                {
                    while ((line = myStream.ReadLine()) != null)
                    {
                        String[] phrase = line.Split(delimiter);
                        for (int i = 0; i < phrase.Length; i++)
                            if (phrase[i].Length > 4)
                            {
                                String[] words = phrase[i].Split(null);
                                for (int j = 1; j < words.Length; j++)
                                {
                                    if (words[j - 1].Length > 3)
                                    {
                                        int main = GetWord(words[j - 1].ToLower());
                                        int suit = GetWord(words[j].ToLower());
                                        if (main != -1 && suit != -1)
                                            Suitability(main, suit);
                                    }
                                }
                            }
                    }
                }

            }
        }
    }
}
