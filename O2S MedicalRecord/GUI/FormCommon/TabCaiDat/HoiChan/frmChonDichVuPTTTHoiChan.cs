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
    public partial class frmChonDichVuPTTTHoiChan : Form
    {
        O2S_MedicalRecord.DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        List<MrdDMHoiChanPTTTDTO> lstmrd_dmhc_pttt { get; set; }

        public frmChonDichVuPTTTHoiChan()
        {
            InitializeComponent();
        }

        private void frmChonDichVuPTTTHoiChan_Load(object sender, EventArgs e)
        {
            try
            {
                GetDataDanhMucPTTT();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void GetDataDanhMucPTTT()
        {
            SplashScreenManager.ShowForm(typeof(O2S_MedicalRecord.Utilities.ThongBao.WaitForm1));
            try
            {
                this.lstmrd_dmhc_pttt = new List<MrdDMHoiChanPTTTDTO>();
                string sqlLayDanhMuc = "select servicepricerefid, servicepricecode, servicepricegroupcode, servicegrouptype, servicepricetype, servicepricename, servicepricenamebhyt, servicepricenamenuocngoai, servicepriceunit, servicepricefee, servicepricefeenhandan, servicepricefeebhyt, servicepricefeenuocngoai, bhyt_groupcode, pttt_hangid, pttt_loaiid, (case pttt_loaiid when 1 then 'Phẫu thuật đặc biệt' when 2 then 'Phẫu thuật loại 1' when 3 then 'Phẫu thuật loại 2' when 4 then 'Phẫu thuật loại 3' when 5 then 'Thủ thuật đặc biệt' when 6 then 'Thủ thuật loại 1' when 7 then 'Thủ thuật loại 2' when 8 then 'Thủ thuật loại 3' else '' end) as pttt_loai,servicepricecodeuser, servicepricesttuser from servicepriceref where servicegrouptype = 4 and bhyt_groupcode in ('06PTTT','07KTC') and pttt_loaiid not in (5,6,7,8); ";
                DataView dv_DanhMucPTTTHC = new DataView(condb.GetDataTable_HIS(sqlLayDanhMuc));
                if (dv_DanhMucPTTTHC.Count > 0)
                {
                    for (int i = 0; i < dv_DanhMucPTTTHC.Count; i++)
                    {
                        MrdDMHoiChanPTTTDTO dmDichVu = new MrdDMHoiChanPTTTDTO();
                        dmDichVu.stt = i + 1;
                        dmDichVu.servicepricerefid = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucPTTTHC[i]["servicepricerefid"].ToString());
                        dmDichVu.servicepricegroupcode = dv_DanhMucPTTTHC[i]["servicepricegroupcode"].ToString();
                        dmDichVu.servicegrouptype = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucPTTTHC[i]["servicegrouptype"].ToString());
                        dmDichVu.servicepricetype = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucPTTTHC[i]["servicepricetype"].ToString());
                        dmDichVu.servicepricecode = dv_DanhMucPTTTHC[i]["servicepricecode"].ToString();
                        dmDichVu.servicepricename = dv_DanhMucPTTTHC[i]["servicepricename"].ToString();
                        dmDichVu.servicepricenamebhyt = dv_DanhMucPTTTHC[i]["servicepricenamebhyt"].ToString();
                        dmDichVu.servicepricenamenuocngoai = dv_DanhMucPTTTHC[i]["servicepricenamenuocngoai"].ToString();
                        dmDichVu.servicepriceunit = dv_DanhMucPTTTHC[i]["servicepriceunit"].ToString();
                        dmDichVu.servicepricefee = dv_DanhMucPTTTHC[i]["servicepricefee"].ToString();
                        dmDichVu.servicepricefeenhandan = dv_DanhMucPTTTHC[i]["servicepricefeenhandan"].ToString();
                        dmDichVu.servicepricefeebhyt = dv_DanhMucPTTTHC[i]["servicepricefeebhyt"].ToString();
                        dmDichVu.servicepricefeenuocngoai = dv_DanhMucPTTTHC[i]["servicepricefeenuocngoai"].ToString();
                        dmDichVu.bhyt_groupcode = dv_DanhMucPTTTHC[i]["bhyt_groupcode"].ToString();
                        dmDichVu.servicepricecodeuser = dv_DanhMucPTTTHC[i]["servicepricecodeuser"].ToString();
                        dmDichVu.servicepricesttuser = dv_DanhMucPTTTHC[i]["servicepricesttuser"].ToString();
                        dmDichVu.pttt_hangid = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucPTTTHC[i]["pttt_hangid"].ToString());
                        dmDichVu.pttt_loaiid = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucPTTTHC[i]["pttt_loaiid"].ToString());
                        dmDichVu.pttt_loai = dv_DanhMucPTTTHC[i]["pttt_loai"].ToString();

                        dmDichVu.servicepricecode_khongdau = Utilities.Common.String.Convert.UnSignVNese(dmDichVu.servicepricecode).ToLower();
                        dmDichVu.servicepricename_khongdau = Utilities.Common.String.Convert.UnSignVNese(dmDichVu.servicepricename).ToLower();
                        this.lstmrd_dmhc_pttt.Add(dmDichVu);
                    }
                }
                //hien thi
                if (this.lstmrd_dmhc_pttt != null && this.lstmrd_dmhc_pttt.Count > 0)
                {
                    if (txtTuKhoaTimKiem.Text.Trim() != "")
                    {
                        string tukhoa = Utilities.Common.String.Convert.UnSignVNese(txtTuKhoaTimKiem.Text.Trim().ToLower());
                        List<MrdDMHoiChanPTTTDTO> lstmrd_serviceref_timkiem = this.lstmrd_dmhc_pttt.Where(o => o.servicepricecode_khongdau.Contains(tukhoa) || o.servicepricename_khongdau.Contains(tukhoa)).ToList();
                        gridControlDMPTTT.DataSource = lstmrd_serviceref_timkiem;
                    }
                    else
                    {
                        gridControlDMPTTT.DataSource = this.lstmrd_dmhc_pttt;
                    }
                }
                else
                {
                    gridControlDMPTTT.DataSource = null;
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
                GetDataDanhMucPTTT();
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
                foreach (int i in gridViewDMPTTT.GetSelectedRows())
                {
                    string se_servicepricecode = gridViewDMPTTT.GetRowCellValue(i, "servicepricecode").ToString();
                    MrdDMHoiChanPTTTDTO hoichanPTTT = this.lstmrd_dmhc_pttt.Where(o => o.servicepricecode == se_servicepricecode).FirstOrDefault();

                    string kt_tontai = "select servicepricecode from mrd_dmhc_pttt where servicepricecode='" + se_servicepricecode + "' ;";
                    DataView dv_kttontai = new DataView(condb.GetDataTable_HSBA(kt_tontai));
                    if (dv_kttontai != null && dv_kttontai.Count > 0)
                    {
                        if (hoichanPTTT != null)
                        {
                            //update
                            string sql_update = "update mrd_dmhc_pttt set servicepricerefid='" + hoichanPTTT.servicepricerefid + "', servicepricegroupcode='" + hoichanPTTT.servicepricegroupcode + "', servicegrouptype='" + hoichanPTTT.servicegrouptype + "', servicepricetype='" + hoichanPTTT.servicepricetype + "', servicepricename='" + hoichanPTTT.servicepricename + "', servicepricenamebhyt='" + hoichanPTTT.servicepricenamebhyt + "', servicepricenamenuocngoai='" + hoichanPTTT.servicepricenamenuocngoai + "',servicepriceunit='" + hoichanPTTT.servicepriceunit + "', servicepricefee='" + hoichanPTTT.servicepricefee + "', servicepricefeenhandan='" + hoichanPTTT.servicepricefeenhandan + "', servicepricefeebhyt='" + hoichanPTTT.servicepricefeebhyt + "', servicepricefeenuocngoai='" + hoichanPTTT.servicepricefeenuocngoai + "', bhyt_groupcode='" + hoichanPTTT.bhyt_groupcode + "', pttt_hangid='" + hoichanPTTT.pttt_hangid + "', pttt_loaiid='" + hoichanPTTT.pttt_loaiid + "', servicepricecodeuser='" + hoichanPTTT.servicepricecodeuser + "', servicepricesttuser='" + hoichanPTTT.servicepricesttuser + "' where servicepricecode='" + se_servicepricecode + "'; ";
                            if (condb.ExecuteNonQuery_HSBA(sql_update))
                            {
                                dem_up += 1;
                            }
                        }
                    }
                    else//insert
                    {
                        if (hoichanPTTT != null)
                        {
                            string sql_insert = "insert into mrd_dmhc_pttt(servicepricerefid, servicepricecode, servicepricegroupcode, servicegrouptype, servicepricetype, servicepricename, servicepricenamebhyt, servicepricenamenuocngoai, servicepriceunit, servicepricefee, servicepricefeenhandan, servicepricefeebhyt, servicepricefeenuocngoai, bhyt_groupcode, pttt_hangid, pttt_loaiid, servicepricecodeuser, servicepricesttuser, mrd_dmhc_ptttnamepath) values('" + hoichanPTTT.servicepricerefid + "', '" + hoichanPTTT.servicepricecode + "', '" + hoichanPTTT.servicepricegroupcode + "', '" + hoichanPTTT.servicegrouptype + "', '" + hoichanPTTT.servicepricetype + "', '" + hoichanPTTT.servicepricename + "', '" + hoichanPTTT.servicepricenamebhyt + "', '" + hoichanPTTT.servicepricenamenuocngoai + "', '" + hoichanPTTT.servicepriceunit + "', '" + hoichanPTTT.servicepricefee + "', '" + hoichanPTTT.servicepricefeenhandan + "', '" + hoichanPTTT.servicepricefeebhyt + "', '" + hoichanPTTT.servicepricefeenuocngoai + "', '" + hoichanPTTT.bhyt_groupcode + "', '" + hoichanPTTT.pttt_hangid + "', '" + hoichanPTTT.pttt_loaiid + "', '" + hoichanPTTT.servicepricecodeuser + "', '" + hoichanPTTT.servicepricesttuser + "', '');";
                            if (condb.ExecuteNonQuery_HSBA(sql_insert))
                            {
                                dem_ins += 1;
                            }
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
                GetDataDanhMucPTTT();
            }
        }

        private void txtTuKhoaTimKiem_TextChanged(object sender, EventArgs e)
        {
            GetDataDanhMucPTTT();
        }
    }
}
