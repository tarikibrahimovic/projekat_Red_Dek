﻿using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projekat_Red_Dek.Models
{
    public class DB
    {
        public string connectionString;
        public MySqlConnection con;
        private MySqlCommand cmd;
        public DB()
        {
            connectionString = @"server=localhost;userid=root;password=root;database=projekat_red_dek";
            con = new MySqlConnection(connectionString);
            cmd = new MySqlCommand();
        }
        public bool Create(string naziv, List<Clan> NizObjekata)
        {
            try
            {
                int pomocna;
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = $"INSERT INTO red (nazivReda) VALUES ('{naziv}')";
                cmd.ExecuteNonQuery();
                pomocna = (int)cmd.LastInsertedId;
                foreach (Clan c in NizObjekata)
                {
                    if (c.Sledeci == null)
                    {
                        cmd.CommandText = $"INSERT INTO clan (redId, value, idClana, idSledeceg) VALUES ({pomocna}, '{c.Vrednost}', {c.ID}, '/')";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd.CommandText = $"INSERT INTO clan (redId, value, idClana, idSledeceg) VALUES ({pomocna}, '{c.Vrednost}', {c.ID}, {c.Sledeci.ID})";
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public List<string> Read(string naziv, List<string> vrednosti)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = $"SELECT value FROM clan WHERE redId = (SELECT id FROM red WHERE nazivReda = '{naziv}')";
                //vrednosti.Add(cmd.ExecuteReader().ToString());
                //MySqlDataReader reader = cmd.ExecuteReader();
                //if (reader.Read())
                //{
                //    vrednosti.Add(reader.GetString(0));
                //}
                //reader.Close();
                cmd.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.SelectCommand.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    vrednosti.Add(row[0].ToString());
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return vrednosti;
        }

        //public DataTable Read()
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        con.Open();
        //        cmd.Connection = con;
        //        cmd.CommandText = "SELECT * FROM main left join rakete on main.SerijskiBroj = rakete.SerijskiBroj";
        //cmd.ExecuteNonQuery();
        //        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        //da.SelectCommand.Connection = con;
        //        da.SelectCommand = cmd;
        //        da.Fill(dt);
        //    }
        //    catch (MySqlException e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }
        //    return dt;
        //}

        //public void Update(Let let)
        //{
        //    try
        //    {
        //        con.Open();
        //        cmd.Connection = con;
        //        cmd.CommandText = $"UPDATE main SET TipAviona = {let.TipAviona}, RegistracioniBroj = '{let.RegistracioniBr}', Vlasnik = '{let.Vlasnik}', BrojSedista = {let.BrSedista}, KapacitetRezervoara = {let.KapacitetRezervoara}, Nosivost = {let.Nosivost} WHERE SerijskiBroj = '{let.SerijskiBroj}'";
        //        cmd.ExecuteNonQuery();
        //        if (let.TipAviona == 2)
        //        {
        //            cmd.CommandText = $"UPDATE rakete SET BrojRaketa = {let.BrRaketa} WHERE SerijskiBroj = '{let.SerijskiBroj}'";
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch (MySqlException e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        //public void Delete(Let let)
        //{
        //    try
        //    {
        //        con.Open();
        //        cmd.Connection = con;
        //        if (let.TipAviona == 2)
        //        {
        //            cmd.CommandText = $"DELETE FROM rakete WHERE SerijskiBroj = '{let.SerijskiBroj}'";
        //            cmd.ExecuteNonQuery();
        //        }
        //        cmd.CommandText = $"DELETE FROM main WHERE SerijskiBroj = '{let.SerijskiBroj}'";
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (MySqlException e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

    }
}
