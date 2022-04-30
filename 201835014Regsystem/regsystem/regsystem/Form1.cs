using System.Data.SqlClient;

namespace regsystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load();
        }

        SqlConnection con = new SqlConnection("Server=.;Database=i201835014;Integrated Security = True");
        
        SqlCommand cmd;
        SqlDataReader read;
        SqlDataAdapter drr;
        string id;
        bool Mode = true;
        string sql;

        public void Load()
        {
            try
            {
                sql = "select * from Regtable";
                cmd = new SqlCommand(sql, con);
                con.Open();
                read = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();

                while (read.Read())
                {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        

        private void getID(string id)
        {
            sql = "select * from Regtable where id = '" + id + "' ";

            cmd = new SqlCommand(sql, con);
            con.Open();
            read = cmd.ExecuteReader();

            while (read.Read())
            {
                txtName.Text = read[1].ToString();
                txtLast.Text = read[2].ToString();
                txtPrice.Text = read[3].ToString();




            }
            con.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtLast.Clear();
            txtPrice.Clear();
            txtName.Focus();
            button1.Text = "Save";
            Mode = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string lastname = txtLast.Text;
            string Price = txtPrice.Text;

            if(Mode == true)
            {
                sql = "insert into Regtable(name,lastname,Price) values(@name,@lastname,@Price)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@lastname", lastname);
                cmd.Parameters.AddWithValue("@Price", Price);
                MessageBox.Show("Record Added");
                cmd.ExecuteNonQuery(); 
                
                txtName.Clear();
                txtLast.Clear();
                txtPrice.Clear();
                txtName.Focus();
            }
            else
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "update Regtable set name = @name, lastname = @lastname, Price = @Price where id = @id ";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@lastname", lastname);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@id", id);

                MessageBox.Show("Record Updated");
                cmd.ExecuteNonQuery();

                txtName.Clear();
                txtLast.Clear();
                txtPrice.Clear();
                txtName.Focus();
                button1.Text = "Save";
                Mode = true;











            }
            con.Close();


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getID(id);
                button1.Text = "Edit";

            }
            else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from Regtable where id  = @id ";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id ", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleteeeee");
                con.Close();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Load();
        }
    }
}