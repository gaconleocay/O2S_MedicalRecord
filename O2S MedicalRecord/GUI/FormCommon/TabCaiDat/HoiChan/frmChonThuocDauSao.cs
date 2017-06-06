using DevExpress.XtraSplashScreen;
using O2S_MedicalRecord.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace O2S_MedicalRecord.GUI.FormCommon.TabCaiDat
{
    public partial class frmChonThuocDauSao : Form
    {
        O2S_MedicalRecord.DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        List<MrdDMHoiChanThuocDTO> mrd_dmhc_thuoc { get; set; }

        public frmChonThuocDauSao()
        {
            InitializeComponent();
        }

        private void frmChonThuocDauSao_Load(object sender, EventArgs e)
        {
            try
            {
                GetDataDanhMucThuoc();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void GetDataDanhMucThuoc()
        {
            SplashScreenManager.ShowForm(typeof(O2S_MedicalRecord.Utilities.ThongBao.WaitForm1));
            try
            {
                this.mrd_dmhc_thuoc = new List<MrdDMHoiChanThuocDTO>();
                string sqlLayDanhMuc = "select ROW_NUMBER () OVER (ORDER BY medicinegroupcode, medicinename) as stt, medicinerefid, medicinegroupcode, medicinecode, medicinereftype, medicinetype, medicinename, tenkhoahoc, donvitinh, nongdo, lieuluong, hoatchat, gianhap, giaban, vatnhap, vatban, bhyt_groupcode, servicepricefee, servicepricefeenhandan, servicepricefeebhyt, servicepricefeenuocngoai, mahoatchat, medicinecodeuser, stt_dauthau, medicinename_byt, stt_thuoc_tt40, stt_thuoc_tt40text from medicine_ref where datatype=0 and medicinecode<>'' and isremove=0 and medicinerefid in (select medicinerefid_org from medicine_ref where datatype=0 and medicinecode<>'' and isremove=0 group by medicinerefid_org); ";
                DataView dv_DanhMucThuocHC = new DataView(condb.GetDataTable_HIS(sqlLayDanhMuc));
                if (dv_DanhMucThuocHC.Count > 0)
                {
                    for (int i = 0; i < dv_DanhMucThuocHC.Count; i++)
                    {
                        MrdDMHoiChanThuocDTO dmDichVu = new MrdDMHoiChanThuocDTO();
                        dmDichVu.stt = i + 1;
                        dmDichVu.medicinerefid = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucThuocHC[i]["medicinerefid"].ToString());
                        dmDichVu.medicinegroupcode = dv_DanhMucThuocHC[i]["medicinegroupcode"].ToString();
                        dmDichVu.medicinecode = dv_DanhMucThuocHC[i]["medicinecode"].ToString();
                        dmDichVu.medicinename = dv_DanhMucThuocHC[i]["medicinename"].ToString();
                        dmDichVu.medicinename_byt = dv_DanhMucThuocHC[i]["medicinename_byt"].ToString();
                        dmDichVu.donvitinh = dv_DanhMucThuocHC[i]["donvitinh"].ToString();
                        dmDichVu.hoatchat = dv_DanhMucThuocHC[i]["hoatchat"].ToString();
                        dmDichVu.mahoatchat = dv_DanhMucThuocHC[i]["mahoatchat"].ToString();
                        dmDichVu.bhyt_groupcode = dv_DanhMucThuocHC[i]["bhyt_groupcode"].ToString();
                        dmDichVu.medicinecodeuser = dv_DanhMucThuocHC[i]["medicinecodeuser"].ToString();
                        dmDichVu.stt_dauthau = dv_DanhMucThuocHC[i]["stt_dauthau"].ToString();
                        dmDichVu.stt_thuoc_tt40text = dv_DanhMucThuocHC[i]["stt_thuoc_tt40text"].ToString();
                        //dmDichVu.medicinecode = dv_DanhMucThuocHC[i]["medicinecode"].ToString();
                        //dmDichVu.medicinecode = dv_DanhMucThuocHC[i]["medicinecode"].ToString();
                        //dmDichVu.mrd_dmhc_thuocnamepath = dv_DanhMucThuocHC[i]["mrd_dmhc_thuocnamepath"].ToString();
                        dmDichVu.medicinecode_khongdau = Utilities.Common.String.Convert.UnSignVNese(dmDichVu.medicinecode).ToLower();
                        dmDichVu.medicinename_khongdau = Utilities.Common.String.Convert.UnSignVNese(dmDichVu.medicinename).ToLower();
                        this.mrd_dmhc_thuoc.Add(dmDichVu);
                    }
                }
                //hien thi
                if (this.mrd_dmhc_thuoc != null && this.mrd_dmhc_thuoc.Count > 0)
                {
                    if (txtTuKhoaTimKiem.Text.Trim() != "")
                    {
                        string tukhoa = Utilities.Common.String.Convert.UnSignVNese(txtTuKhoaTimKiem.Text.Trim().ToLower());
                        List<MrdDMHoiChanThuocDTO> lstmrd_serviceref_timkiem = this.mrd_dmhc_thuoc.Where(o => o.medicinecode_khongdau.Contains(tukhoa) || o.medicinename_khongdau.Contains(tukhoa)).ToList();
                        gridControlDMThuoc.DataSource = lstmrd_serviceref_timkiem;
                    }
                    else
                    {
                        gridControlDMThuoc.DataSource = this.mrd_dmhc_thuoc;
                    }
                }
                else
                {
                    gridControlDMThuoc.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                GetDataDanhMucThuoc();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                int dem_ins = 0;
                int dem_up = 0;
                foreach (int i in gridViewDMThuoc.GetSelectedRows())
                {
                    string se_medicinecode = gridViewDMThuoc.GetRowCellValue(i, "medicinecode").ToString();
                    string kt_tontai = "select medicinecode from mrd_dmhc_thuoc where medicinecode='" + se_medicinecode + "' ;";
                    DataView dv_kttontai = new DataView(condb.GetDataTable_HSBA(kt_tontai));
                    if (dv_kttontai != null && dv_kttontai.Count > 0)
                    {
                        //update
                        string sql_update = "update mrd_dmhc_thuoc set  medicinename='" + gridViewDMThuoc.GetRowCellValue(i, "medicinename").ToString() + "', donvitinh='" + gridViewDMThuoc.GetRowCellValue(i, "donvitinh").ToString() + "', mahoatchat='" + gridViewDMThuoc.GetRowCellValue(i, "mahoatchat").ToString() + "', hoatchat='" + gridViewDMThuoc.GetRowCellValue(i, "hoatchat").ToString() + "', bhyt_groupcode='" + gridViewDMThuoc.GetRowCellValue(i, "bhyt_groupcode").ToString() + "', medicinename_byt='" + gridViewDMThuoc.GetRowCellValue(i, "medicinename_byt").ToString() + "', medicinecodeuser='" + gridViewDMThuoc.GetRowCellValue(i, "medicinecodeuser").ToString() + "', stt_dauthau='" + gridViewDMThuoc.GetRowCellValue(i, "stt_dauthau").ToString() + "', stt_thuoc_tt40text='" + gridViewDMThuoc.GetRowCellValue(i, "stt_thuoc_tt40text").ToString() + "' where medicinecode='" + se_medicinecode + "'; ";
                        if (condb.ExecuteNonQuery_HSBA(sql_update))
                        {
                            dem_up += 1;
                        }
                    }
                    else//insert
                    {
                        string sql_insert = "insert into mrd_dmhc_thuoc(medicinerefid, medicinecode, medicinename, donvitinh, mahoatchat, hoatchat, bhyt_groupcode, medicinename_byt, medicinecodeuser, stt_dauthau, stt_thuoc_tt40text) values ('" + gridViewDMThuoc.GetRowCellValue(i, "medicinerefid").ToString() + "', '" + gridViewDMThuoc.GetRowCellValue(i, "medicinecode").ToString() + "', '" + gridViewDMThuoc.GetRowCellValue(i, "medicinename").ToString() + "', '" + gridViewDMThuoc.GetRowCellValue(i, "mahoatchat").ToString() + "', '" + gridViewDMThuoc.GetRowCellValue(i, "donvitinh").ToString() + "', '" + gridViewDMThuoc.GetRowCellValue(i, "hoatchat").ToString() + "', '" + gridViewDMThuoc.GetRowCellValue(i, "bhyt_groupcode").ToString() + "', '" + gridViewDMThuoc.GetRowCellValue(i, "medicinename_byt").ToString() + "', '" + gridViewDMThuoc.GetRowCellValue(i, "medicinecodeuser").ToString() + "', '" + gridViewDMThuoc.GetRowCellValue(i, "stt_dauthau").ToString() + "', '" + gridViewDMThuoc.GetRowCellValue(i, "stt_thuoc_tt40text").ToString() + "');";
                        if (condb.ExecuteNonQuery_HSBA(sql_insert))
                        {
                            dem_ins += 1;
                        }
                    }
                }
                MessageBox.Show("Thêm thành công SL=[" + dem_ins + "] và cập nhật thành công SL=[" + dem_up + "] !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void txtTuKhoaTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetDataDanhMucThuoc();
            }
        }

        private void txtTuKhoaTimKiem_TextChanged(object sender, EventArgs e)
        {
            GetDataDanhMucThuoc();
        }
    }
}
