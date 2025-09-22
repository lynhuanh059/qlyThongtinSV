using System;
using System.Windows.Forms;

namespace SinhVien
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
            dgvSinhVien.CellClick += dgvSinhVien_CellClick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvSinhVien.AllowUserToAddRows = false;
            ResetFormToDefaults();
            txtTongNam.Text = "0";
            txtTongNu.Text = "0";
        }

        private void btnThemSua_Click(object sender, EventArgs e)
        {
            string mssv = txtMaSV.Text.Trim();
            string hoten = txtHoTen.Text.Trim();
            string dtb = txtDiemTB.Text.Trim();
            string gioiTinh = radNam.Checked ? "Nam" : "Nữ";
            string chuyenNganh = cboChuyenNganh.SelectedItem != null ? cboChuyenNganh.SelectedItem.ToString() : "";

            if (string.IsNullOrWhiteSpace(mssv) || string.IsNullOrWhiteSpace(hoten) || string.IsNullOrWhiteSpace(dtb))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            int rowIndex = TimDongTheoMSSV(mssv);

            if (rowIndex == -1)
            {
                dgvSinhVien.Rows.Add(mssv, hoten, gioiTinh, dtb, chuyenNganh);
                MessageBox.Show("Thêm mới dữ liệu thành công!");
            }
            else
            {
                var row = dgvSinhVien.Rows[rowIndex];
                row.Cells[0].Value = mssv;
                row.Cells[1].Value = hoten;
                row.Cells[2].Value = gioiTinh;
                row.Cells[3].Value = dtb;
                row.Cells[4].Value = chuyenNganh;
                MessageBox.Show("Cập nhật dữ liệu thành công!");
            }

            CapNhatTongNamNu();
            ResetFormToDefaults();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string mssv = txtMaSV.Text.Trim();
            if (string.IsNullOrWhiteSpace(mssv))
            {
                MessageBox.Show("Không tìm thấy MSSV cần xóa!");
                return;
            }

            int rowIndex = TimDongTheoMSSV(mssv);
            if (rowIndex == -1)
            {
                MessageBox.Show("Không tìm thấy MSSV cần xóa!");
                return;
            }

            var ans = MessageBox.Show("Bạn có chắc muốn xóa sinh viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ans == DialogResult.Yes)
            {
                if (!dgvSinhVien.Rows[rowIndex].IsNewRow)
                {
                    dgvSinhVien.Rows.RemoveAt(rowIndex);
                    MessageBox.Show("Xóa sinh viên thành công!");
                    CapNhatTongNamNu();
                    ResetFormToDefaults();
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvSinhVien.Rows[e.RowIndex];

            txtMaSV.Text = row.Cells[0].Value?.ToString() ?? "";
            txtHoTen.Text = row.Cells[1].Value?.ToString() ?? "";
            string gt = row.Cells[2].Value?.ToString();
            if (gt == "Nam") radNam.Checked = true; else radNu.Checked = true;
            txtDiemTB.Text = row.Cells[3].Value?.ToString() ?? "";
            string cn = row.Cells[4].Value?.ToString() ?? "";
            if (!string.IsNullOrEmpty(cn) && cboChuyenNganh.Items.Contains(cn))
                cboChuyenNganh.SelectedItem = cn;
            else
                cboChuyenNganh.SelectedIndex = -1;
        }

        private void dgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvSinhVien_CellClick(sender, e);
        }

        private int TimDongTheoMSSV(string mssv)
        {
            foreach (DataGridViewRow r in dgvSinhVien.Rows)
            {
                if (r.IsNewRow) continue;
                if (string.Equals(r.Cells[0].Value?.ToString(), mssv, StringComparison.OrdinalIgnoreCase))
                    return r.Index;
            }
            return -1;
        }

        private void CapNhatTongNamNu()
        {
            int nam = 0, nu = 0;
            foreach (DataGridViewRow r in dgvSinhVien.Rows)
            {
                if (r.IsNewRow) continue;
                string gt = r.Cells[2].Value?.ToString();
                if (gt == "Nam") nam++;
                else if (gt == "Nữ") nu++;
            }
            txtTongNam.Text = nam.ToString();
            txtTongNu.Text = nu.ToString();
        }

        private void ResetFormToDefaults()
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            txtDiemTB.Clear();
            radNu.Checked = true;
            if (cboChuyenNganh.Items.Contains("QTKD"))
                cboChuyenNganh.SelectedItem = "QTKD";
            else if (cboChuyenNganh.Items.Count > 0)
                cboChuyenNganh.SelectedIndex = 0;
        }

        private void lblChuyenNganh_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void txtTongNam_TextChanged(object sender, EventArgs e) { }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
