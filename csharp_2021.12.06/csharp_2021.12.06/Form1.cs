using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csharp_2021._12._06
{
    public partial class Form1 : Form
    {
        public string ConnectionString { get; set; }


        public Form1()
        {
            ConnectionString =
               @"Server   = (localdb)\MSSQLLocalDB;" +
                "Database = palyazatok;";
            InitializeComponent();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvFill();
        }

        private void dgvFill()
        {
            using (var c = new SqlConnection(ConnectionString))
            {
                dgv.Rows.Clear();
                c.Open();
                var r = new SqlConnection(
                 "SELECT palyazat.id, sum(palyazat.tervezetA + palyazat.tervezetC), count(szamla.szamlaszam), sum(szamla.ertek) " +
                 "FROM koltsegtipus, palyazat.szamla" +
                 "WHERE palyazat.id = szamla.id" +
                 "AND szamla.koltsegtipusid = koltsegtipus.id" +
                 "GROUP BY palyazat.id" +
                 "ORDER BY palyazat.id ASC", c).ExecuteReader();
                while (r.Read())
                {
                    dgv.Rows.Add(r[0], r[1] + " Ft", r[2] + " db", r[3] + " Ft");
                }
                c.Close();
            }
               
            
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void uj_Click(object sender, EventArgs e)
        {

        }
    }
}
