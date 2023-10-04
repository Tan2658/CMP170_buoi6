using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using buoi6.Models;

namespace buoi6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        StaffContextDB context = new StaffContextDB();

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Ban co muon thoat?", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (rs == DialogResult.Yes)
                this.Close();
        }

        private void FillFacultyBox(List<Phongban> phongbans)
        {
            this.cmbFaculty.DataSource = phongbans;
            this.cmbFaculty.DisplayMember = "TenPB";
            this.cmbFaculty.ValueMember = "MaPB";
        }

        private void BindGrid()
        {
            List<Nhanvien> nhanviens = context.Nhanvien.ToList();
            dgvStaff.Rows.Clear();

            foreach(var item in nhanviens)
            {
                int index = dgvStaff.Rows.Add();
                dgvStaff.Rows[index].Cells[0].Value = item.MaNV;
                dgvStaff.Rows[index].Cells[1].Value = item.TenNV;
                dgvStaff.Rows[index].Cells[2].Value = item.Ngaysinh;
                dgvStaff.Rows[index].Cells[3].Value = item.Phongban.TenPB;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                List<Phongban> phongbans = context.Phongban.ToList();
                FillFacultyBox(phongbans);
                BindGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvStaff_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStaff.SelectedRows.Count == 1)
                if(dgvStaff.SelectedRows[0].Cells[0].Value != null)
                {
                    txtID.Text = dgvStaff.SelectedRows[0].Cells[0].Value.ToString();
                    txtName.Text = dgvStaff.SelectedRows[0].Cells[1].Value.ToString();
                    dteBirth.Value = Convert.ToDateTime(dgvStaff.SelectedRows[0].Cells[2].Value);
                    cmbFaculty.Text = dgvStaff.SelectedRows[0].Cells[3].Value.ToString();
                }
        }

        private int checkIDExists(string ID)
        {
            if (dgvStaff.RowCount > 1)
                for (int i = 0; i < dgvStaff.RowCount - 1; i++)
                    if (dgvStaff.Rows[i].Cells[0].Value.ToString() == ID)
                        return 1;
            return -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtID.Text == "" || txtName.Text == "")
                    throw new Exception("Vui long nhap day du thong tin");
                if (checkIDExists(txtID.Text) == 1)
                    throw new Exception("Ma so nhan vien bi trung");

                Nhanvien n = new Nhanvien()
                {
                    MaNV = txtID.Text,
                    TenNV = txtName.Text,
                    Ngaysinh = dteBirth.Value,
                    MaPB = cmbFaculty.SelectedValue.ToString()
                };

                context.Nhanvien.Add(n);
                context.SaveChanges();
                List<Nhanvien> nhanviens = context.Nhanvien.ToList();
                BindGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Nhanvien findID(string ID)
        {
            return context.Nhanvien.FirstOrDefault(p => p.MaNV == ID);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Nhanvien db = findID(txtID.Text);

            if (db != null)
            {
                db.TenNV = txtName.Text;
                db.Ngaysinh = dteBirth.Value;
                db.MaPB = cmbFaculty.SelectedValue.ToString();
                context.SaveChanges();
                BindGrid();
                MessageBox.Show("Sua thong tin thanh cong", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Khong tim thay nhan vien voi ma", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            Nhanvien db = findID(txtID.Text);

            if (db != null)
            {
                DialogResult rs = MessageBox.Show("Ban co muon xoa?", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rs == DialogResult.Yes)
                {
                    context.Nhanvien.Remove(db);
                    context.SaveChanges();
                    BindGrid();
                    MessageBox.Show("Xoa thong tin thanh cong", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
                MessageBox.Show("Khong tim thay nhan vien voi ma", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
