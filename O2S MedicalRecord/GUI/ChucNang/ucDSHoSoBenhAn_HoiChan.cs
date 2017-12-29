using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using O2S_MedicalRecord.DTO;
using O2S_MedicalRecord.Utilities.GUIGridView;
using DevExpress.XtraSplashScreen;
using Aspose.Words.Rendering;

namespace O2S_MedicalRecord.GUI.ChucNang
{
    public partial class ucDSHoSoBenhAn : UserControl
    {
        #region Declaration
        private List<MrdHsbaHoiChanDTO> lstHsbaHoiChan { get; set; }
        private DataTable dataHsbaHoiChan { get; set; }

        private List<MrdHsbaHoiChan_ServiceDTO> lstHsbaHoiChanService_PT { get; set; }
        private List<MrdHsbaHoiChan_ServiceDTO> lstHsbaHoiChanService_Thuoc { get; set; }
        private DataTable dataHoiChan_CV { get; set; }
        private int mrd_loaihc_id = 0;
        #endregion

        #region Load
        private void ucDSHoSoBenhAn_HoiChan_Load(MedicalrecordDTO rowMecicalrecord)
        {
            try
            {
                HoiChan_LoadDataDefault(rowMecicalrecord);
                HoiChan_LoadThongTinVeHoiChan(rowMecicalrecord);
                HoiChan_KiemTraTaoPhieuHoiChan_PT(rowMecicalrecord);
                HoiChan_KiemTraTaoPhieuHoiChan_Thuoc(rowMecicalrecord);
                HoiChan_KiemTraTaoPhieuHoiChan_CV(rowMecicalrecord);

            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void HoiChan_LoadDataDefault(MedicalrecordDTO rowMecicalrecord)
        {
            try
            {
                btnHoiChan_Thuoc.Enabled = false;
                btnHoiChan_PhauThuat.Enabled = false;
                btnHoiChan_ChuyenVien.Enabled = false;
                cboHoiChan_ChonDVThuoc.Enabled = false;
                cboHoiChan_ChonDVThuoc.Properties.DataSource = null;
                gridControlHoiChanDSHC.DataSource = null;
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void HoiChan_LoadThongTinVeHoiChan(MedicalrecordDTO rowMecicalrecord)
        {
            SplashScreenManager.ShowForm(typeof(O2S_MedicalRecord.Utilities.ThongBao.WaitForm1));
            try
            {
                this.lstHsbaHoiChan = new List<MrdHsbaHoiChanDTO>();
                string sqllstHoiChan = "select 1 as mrd_loaihc_id, 'Hội chẩn phẫu thuật' as mrd_loaihc_name, mrd_hsba_hcptttid as mrd_hsba_hcid, mrd_dmhc_ptttid as mrd_dmhc_id, 0 as his_chuyenvienid, 0 as medicinerefid_org, maubenhphamid, servicepriceid, servicepricecode, servicepricename, servicepricedate, hosobenhanid, medicalrecordid, patientid, vienphiid, departmentgroupid, departmentid, thoigianhoichan, yeucauhoichanid, yeucauhoichanname, diadiemhoichan, dbb_tomtattiensubenh, dbb_tinhtranglucvaovien, dbb_chandoantuyenduoi, dbb_tomtatdienbienbenh, yk_chandoantienluong, yk_phuongphapdieutri, yk_chamsoc, kl_ketluan, tvtg_chutoa_code, tvtg_chutoa_ten, tvtg_chutoa_cdcv, tvtg_thuky_code, tvtg_thuky_ten, tvtg_thuky_cdcv, tvtg_thanhvien1_code, tvtg_thanhvien1_ten, tvtg_thanhvien1_cdcv, tvtg_thanhvien2_code, tvtg_thanhvien2_ten, tvtg_thanhvien2_cdcv, tvtg_thanhvien3_code, tvtg_thanhvien3_ten, tvtg_thanhvien3_cdcv, tvtg_thanhvien4_code, tvtg_thanhvien4_ten, tvtg_thanhvien4_cdcv, tvtg_thanhvien5_code, tvtg_thanhvien5_ten, tvtg_thanhvien5_cdcv, tvtg_thanhvien6_code, tvtg_thanhvien6_ten, tvtg_thanhvien6_cdcv, mrd_hsba_hcptttstatus as mrd_hsba_hcstatus, create_mrduserid, create_mrdusercode, create_date from mrd_hsba_hcpttt where hosobenhanid='" + rowMecicalrecord.hosobenhanid + "' union all select 2 as mrd_loaihc_id, 'Hội chẩn thuốc dấu *' as mrd_loaihc_name, mrd_hsba_hcthuocid as mrd_hsba_hcid, mrd_dmhc_thuocid as mrd_dmhc_id, 0 as his_chuyenvienid, medicinerefid_org, maubenhphamid, servicepriceid, servicepricecode, servicepricename, servicepricedate, hosobenhanid, medicalrecordid, patientid, vienphiid, departmentgroupid, departmentid, thoigianhoichan, yeucauhoichanid, yeucauhoichanname, diadiemhoichan, dbb_tomtattiensubenh, dbb_tinhtranglucvaovien, dbb_chandoantuyenduoi, dbb_tomtatdienbienbenh, yk_chandoantienluong, yk_phuongphapdieutri, yk_chamsoc, kl_ketluan, tvtg_chutoa_code, tvtg_chutoa_ten, tvtg_chutoa_cdcv, tvtg_thuky_code, tvtg_thuky_ten, tvtg_thuky_cdcv, tvtg_thanhvien1_code, tvtg_thanhvien1_ten, tvtg_thanhvien1_cdcv, tvtg_thanhvien2_code, tvtg_thanhvien2_ten, tvtg_thanhvien2_cdcv, tvtg_thanhvien3_code, tvtg_thanhvien3_ten, tvtg_thanhvien3_cdcv, tvtg_thanhvien4_code, tvtg_thanhvien4_ten, tvtg_thanhvien4_cdcv, tvtg_thanhvien5_code, tvtg_thanhvien5_ten, tvtg_thanhvien5_cdcv, tvtg_thanhvien6_code, tvtg_thanhvien6_ten, tvtg_thanhvien6_cdcv, mrd_hsba_hcthuocstatus as mrd_hsba_hcstatus, create_mrduserid, create_mrdusercode, create_date from mrd_hsba_hcthuoc where hosobenhanid='" + rowMecicalrecord.hosobenhanid + "' union all select 3 as mrd_loaihc_id, 'Hội chẩn chuyển viện' as mrd_loaihc_name, mrd_hsba_hccvienid as mrd_hsba_hcid, mrd_dmhc_cvienid as mrd_dmhc_id, his_chuyenvienid, 0 as medicinerefid_org, 0 as maubenhphamid, 0 as servicepriceid, '' as servicepricecode, '' as servicepricename, '' as servicepricedate, hosobenhanid, medicalrecordid, patientid, vienphiid, departmentgroupid, departmentid, thoigianhoichan, yeucauhoichanid, yeucauhoichanname, diadiemhoichan, dbb_tomtattiensubenh, dbb_tinhtranglucvaovien, dbb_chandoantuyenduoi, dbb_tomtatdienbienbenh, yk_chandoantienluong, yk_phuongphapdieutri, yk_chamsoc, kl_ketluan, tvtg_chutoa_code, tvtg_chutoa_ten, tvtg_chutoa_cdcv, tvtg_thuky_code, tvtg_thuky_ten, tvtg_thuky_cdcv, tvtg_thanhvien1_code, tvtg_thanhvien1_ten, tvtg_thanhvien1_cdcv, tvtg_thanhvien2_code, tvtg_thanhvien2_ten, tvtg_thanhvien2_cdcv, tvtg_thanhvien3_code, tvtg_thanhvien3_ten, tvtg_thanhvien3_cdcv, tvtg_thanhvien4_code, tvtg_thanhvien4_ten, tvtg_thanhvien4_cdcv, tvtg_thanhvien5_code, tvtg_thanhvien5_ten, tvtg_thanhvien5_cdcv, tvtg_thanhvien6_code, tvtg_thanhvien6_ten, tvtg_thanhvien6_cdcv, mrd_hsba_hccvienstatus as mrd_hsba_hcstatus, create_mrduserid, create_mrdusercode, create_date from mrd_hsba_hccvien where hosobenhanid='" + rowMecicalrecord.hosobenhanid + "';";
                this.dataHsbaHoiChan = condb.GetDataTable_HSBA(sqllstHoiChan);
                if (this.dataHsbaHoiChan != null && this.dataHsbaHoiChan.Rows.Count > 0)
                {
                    for (int i = 0; i < this.dataHsbaHoiChan.Rows.Count; i++)
                    {
                        MrdHsbaHoiChanDTO hsbaHoiChan = new MrdHsbaHoiChanDTO();
                        hsbaHoiChan.mrd_loaihc_id = Utilities.Util_TypeConvertParse.ToInt64(this.dataHsbaHoiChan.Rows[i]["mrd_loaihc_id"].ToString());
                        hsbaHoiChan.mrd_loaihc_name = this.dataHsbaHoiChan.Rows[i]["mrd_loaihc_name"].ToString();
                        hsbaHoiChan.mrd_hsba_hcid = Utilities.Util_TypeConvertParse.ToInt64(this.dataHsbaHoiChan.Rows[i]["mrd_hsba_hcid"].ToString());
                        hsbaHoiChan.mrd_dmhc_id = Utilities.Util_TypeConvertParse.ToInt64(this.dataHsbaHoiChan.Rows[i]["mrd_dmhc_id"].ToString());
                        hsbaHoiChan.his_chuyenvienid = Utilities.Util_TypeConvertParse.ToInt64(this.dataHsbaHoiChan.Rows[i]["his_chuyenvienid"].ToString());
                        hsbaHoiChan.medicinerefid_org = Utilities.Util_TypeConvertParse.ToInt64(this.dataHsbaHoiChan.Rows[i]["medicinerefid_org"].ToString());
                        hsbaHoiChan.maubenhphamid = Utilities.Util_TypeConvertParse.ToInt64(this.dataHsbaHoiChan.Rows[i]["maubenhphamid"].ToString());
                        hsbaHoiChan.servicepriceid = Utilities.Util_TypeConvertParse.ToInt64(this.dataHsbaHoiChan.Rows[i]["servicepriceid"].ToString());
                        hsbaHoiChan.servicepricecode = this.dataHsbaHoiChan.Rows[i]["servicepricecode"].ToString();
                        hsbaHoiChan.servicepricename = this.dataHsbaHoiChan.Rows[i]["servicepricename"].ToString();
                        hsbaHoiChan.servicepricedate = Utilities.Util_TypeConvertParse.ToDateTime(this.dataHsbaHoiChan.Rows[i]["servicepricedate"].ToString());
                        hsbaHoiChan.hosobenhanid = Utilities.Util_TypeConvertParse.ToInt64(this.dataHsbaHoiChan.Rows[i]["hosobenhanid"].ToString());
                        hsbaHoiChan.medicalrecordid = Utilities.Util_TypeConvertParse.ToInt64(this.dataHsbaHoiChan.Rows[i]["medicalrecordid"].ToString());
                        hsbaHoiChan.patientid = Utilities.Util_TypeConvertParse.ToInt64(this.dataHsbaHoiChan.Rows[i]["patientid"].ToString());
                        hsbaHoiChan.vienphiid = Utilities.Util_TypeConvertParse.ToInt64(this.dataHsbaHoiChan.Rows[i]["vienphiid"].ToString());
                        hsbaHoiChan.departmentgroupid = Utilities.Util_TypeConvertParse.ToInt64(this.dataHsbaHoiChan.Rows[i]["departmentgroupid"].ToString());
                        hsbaHoiChan.departmentid = Utilities.Util_TypeConvertParse.ToInt64(this.dataHsbaHoiChan.Rows[i]["departmentid"].ToString());
                        hsbaHoiChan.thoigianhoichan = Utilities.Util_TypeConvertParse.ToDateTime(this.dataHsbaHoiChan.Rows[i]["thoigianhoichan"].ToString());
                        hsbaHoiChan.yeucauhoichanid = Utilities.Util_TypeConvertParse.ToInt64(this.dataHsbaHoiChan.Rows[i]["yeucauhoichanid"].ToString());
                        hsbaHoiChan.yeucauhoichanname = this.dataHsbaHoiChan.Rows[i]["yeucauhoichanname"].ToString();
                        hsbaHoiChan.diadiemhoichan = this.dataHsbaHoiChan.Rows[i]["diadiemhoichan"].ToString();
                        hsbaHoiChan.dbb_tomtattiensubenh = this.dataHsbaHoiChan.Rows[i]["dbb_tomtattiensubenh"].ToString();
                        hsbaHoiChan.dbb_tinhtranglucvaovien = this.dataHsbaHoiChan.Rows[i]["dbb_tinhtranglucvaovien"].ToString();
                        hsbaHoiChan.dbb_chandoantuyenduoi = this.dataHsbaHoiChan.Rows[i]["dbb_chandoantuyenduoi"].ToString();
                        hsbaHoiChan.dbb_tomtatdienbienbenh = this.dataHsbaHoiChan.Rows[i]["dbb_tomtatdienbienbenh"].ToString();
                        hsbaHoiChan.yk_chandoantienluong = this.dataHsbaHoiChan.Rows[i]["yk_chandoantienluong"].ToString();
                        hsbaHoiChan.yk_phuongphapdieutri = this.dataHsbaHoiChan.Rows[i]["yk_phuongphapdieutri"].ToString();
                        hsbaHoiChan.yk_chamsoc = this.dataHsbaHoiChan.Rows[i]["yk_chamsoc"].ToString();
                        hsbaHoiChan.kl_ketluan = this.dataHsbaHoiChan.Rows[i]["kl_ketluan"].ToString();
                        hsbaHoiChan.tvtg_chutoa_code = this.dataHsbaHoiChan.Rows[i]["tvtg_chutoa_code"].ToString();
                        hsbaHoiChan.tvtg_chutoa_ten = this.dataHsbaHoiChan.Rows[i]["tvtg_chutoa_ten"].ToString();
                        hsbaHoiChan.tvtg_chutoa_cdcv = this.dataHsbaHoiChan.Rows[i]["tvtg_chutoa_cdcv"].ToString();
                        hsbaHoiChan.tvtg_thuky_code = this.dataHsbaHoiChan.Rows[i]["tvtg_thuky_code"].ToString();
                        hsbaHoiChan.tvtg_thuky_ten = this.dataHsbaHoiChan.Rows[i]["tvtg_thuky_ten"].ToString();
                        hsbaHoiChan.tvtg_thuky_cdcv = this.dataHsbaHoiChan.Rows[i]["tvtg_thuky_cdcv"].ToString();
                        hsbaHoiChan.tvtg_thanhvien1_code = this.dataHsbaHoiChan.Rows[i]["tvtg_thanhvien1_code"].ToString();
                        hsbaHoiChan.tvtg_thanhvien1_ten = this.dataHsbaHoiChan.Rows[i]["tvtg_thanhvien1_ten"].ToString();
                        hsbaHoiChan.tvtg_thanhvien1_cdcv = this.dataHsbaHoiChan.Rows[i]["tvtg_thanhvien1_cdcv"].ToString();
                        hsbaHoiChan.tvtg_thanhvien2_code = this.dataHsbaHoiChan.Rows[i]["tvtg_thanhvien2_code"].ToString();
                        hsbaHoiChan.tvtg_thanhvien2_ten = this.dataHsbaHoiChan.Rows[i]["tvtg_thanhvien2_ten"].ToString();
                        hsbaHoiChan.tvtg_thanhvien2_cdcv = this.dataHsbaHoiChan.Rows[i]["tvtg_thanhvien2_cdcv"].ToString();
                        hsbaHoiChan.tvtg_thanhvien3_code = this.dataHsbaHoiChan.Rows[i]["tvtg_thanhvien3_code"].ToString();
                        hsbaHoiChan.tvtg_thanhvien3_ten = this.dataHsbaHoiChan.Rows[i]["tvtg_thanhvien3_ten"].ToString();
                        hsbaHoiChan.tvtg_thanhvien3_cdcv = this.dataHsbaHoiChan.Rows[i]["tvtg_thanhvien3_cdcv"].ToString();
                        hsbaHoiChan.tvtg_thanhvien4_code = this.dataHsbaHoiChan.Rows[i]["tvtg_thanhvien4_code"].ToString();
                        hsbaHoiChan.tvtg_thanhvien4_ten = this.dataHsbaHoiChan.Rows[i]["tvtg_thanhvien4_ten"].ToString();
                        hsbaHoiChan.tvtg_thanhvien4_cdcv = this.dataHsbaHoiChan.Rows[i]["tvtg_thanhvien4_cdcv"].ToString();
                        hsbaHoiChan.tvtg_thanhvien5_code = this.dataHsbaHoiChan.Rows[i]["tvtg_thanhvien5_code"].ToString();
                        hsbaHoiChan.tvtg_thanhvien5_ten = this.dataHsbaHoiChan.Rows[i]["tvtg_thanhvien5_ten"].ToString();
                        hsbaHoiChan.tvtg_thanhvien5_cdcv = this.dataHsbaHoiChan.Rows[i]["tvtg_thanhvien5_cdcv"].ToString();
                        hsbaHoiChan.tvtg_thanhvien6_code = this.dataHsbaHoiChan.Rows[i]["tvtg_thanhvien6_code"].ToString();
                        hsbaHoiChan.tvtg_thanhvien6_ten = this.dataHsbaHoiChan.Rows[i]["tvtg_thanhvien6_ten"].ToString();
                        hsbaHoiChan.tvtg_thanhvien6_cdcv = this.dataHsbaHoiChan.Rows[i]["tvtg_thanhvien6_cdcv"].ToString();
                        hsbaHoiChan.mrd_hsba_hcstatus = Utilities.Util_TypeConvertParse.ToInt64(this.dataHsbaHoiChan.Rows[i]["mrd_hsba_hcstatus"].ToString());
                        hsbaHoiChan.create_mrduserid = Utilities.Util_TypeConvertParse.ToInt64(this.dataHsbaHoiChan.Rows[i]["create_mrduserid"].ToString());
                        hsbaHoiChan.create_mrdusercode = this.dataHsbaHoiChan.Rows[i]["create_mrdusercode"].ToString();
                        hsbaHoiChan.create_date = Utilities.Util_TypeConvertParse.ToDateTime(this.dataHsbaHoiChan.Rows[i]["create_date"].ToString());
                        this.lstHsbaHoiChan.Add(hsbaHoiChan);
                    }
                    gridControlHoiChanDSHC.DataSource = this.lstHsbaHoiChan;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void HoiChan_KiemTraTaoPhieuHoiChan_PT(MedicalrecordDTO rowMecicalrecord)
        {
            SplashScreenManager.ShowForm(typeof(O2S_MedicalRecord.Utilities.ThongBao.WaitForm1));
            try
            {
                if (rowMecicalrecord.medicalrecordstatus == 2) //BN dang dieu tri thi duoc tao
                {
                    lstHsbaHoiChanService_PT = new List<MrdHsbaHoiChan_ServiceDTO>();
                    string sqlHoiChan_PT = "select ROW_NUMBER () OVER (ORDER BY ser.servicepricename) as stt, ser.servicepriceid, ser.medicalrecordid, ser.vienphiid, ser.hosobenhanid, ser.maubenhphamid, ser.departmentid, ser.departmentgroupid, ser.servicepricecode, ser.servicepricename, ser.servicepricename_bhyt, ser.servicepricename_nuocngoai, ser.servicepricedate, ser.soluong from mrd_dmhc_pttt pt inner join dblink('myconn','select servicepriceid, medicalrecordid, vienphiid, hosobenhanid, maubenhphamid, departmentid, departmentgroupid, servicepricecode, servicepricename, servicepricename_bhyt, servicepricename_nuocngoai, servicepricedate, soluong FROM serviceprice where hosobenhanid=" + rowMecicalrecord.hosobenhanid + " and bhyt_groupcode in (''06PTTT'',''07KTC'')') AS ser(servicepriceid integer, medicalrecordid integer, vienphiid integer, hosobenhanid integer, maubenhphamid integer, departmentid integer, departmentgroupid integer, servicepricecode text, servicepricename text, servicepricename_bhyt text, servicepricename_nuocngoai text, servicepricedate timestamp without time zone, soluong double precision) on pt.servicepricecode=ser.servicepricecode;";
                    DataTable dataHoiChan_PT = condb.GetDataTable_Dblink(sqlHoiChan_PT);
                    if (dataHoiChan_PT != null && dataHoiChan_PT.Rows.Count > 0)
                    {
                        for (int i = 0; i < dataHoiChan_PT.Rows.Count; i++)
                        {
                            if (this.lstHsbaHoiChan != null && this.lstHsbaHoiChan.Count > 0)
                            {
                                long servicepriceid = Utilities.Util_TypeConvertParse.ToInt64(dataHoiChan_PT.Rows[i]["servicepriceid"].ToString());
                                List<MrdHsbaHoiChanDTO> hsba_HoiChan = this.lstHsbaHoiChan.Where(o => o.servicepriceid == servicepriceid).ToList();
                                if (hsba_HoiChan.Count == 0)
                                {
                                    MrdHsbaHoiChan_ServiceDTO hsbaService = new MrdHsbaHoiChan_ServiceDTO();
                                    hsbaService.servicepriceid = Utilities.Util_TypeConvertParse.ToInt64(dataHoiChan_PT.Rows[i]["servicepriceid"].ToString());
                                    hsbaService.servicepricecode = dataHoiChan_PT.Rows[i]["servicepricecode"].ToString();
                                    hsbaService.servicepricename = dataHoiChan_PT.Rows[i]["servicepricename"].ToString();
                                    hsbaService.maubenhphamid = Utilities.Util_TypeConvertParse.ToInt64(dataHoiChan_PT.Rows[i]["maubenhphamid"].ToString());
                                    hsbaService.servicepricedate = Utilities.Util_TypeConvertParse.ToDateTime(dataHoiChan_PT.Rows[i]["servicepricedate"].ToString());
                                    lstHsbaHoiChanService_PT.Add(hsbaService);
                                }
                            }
                            else
                            {
                                MrdHsbaHoiChan_ServiceDTO hsbaService = new MrdHsbaHoiChan_ServiceDTO();
                                hsbaService.servicepriceid = Utilities.Util_TypeConvertParse.ToInt64(dataHoiChan_PT.Rows[i]["servicepriceid"].ToString());
                                hsbaService.servicepricecode = dataHoiChan_PT.Rows[i]["servicepricecode"].ToString();
                                hsbaService.servicepricename = dataHoiChan_PT.Rows[i]["servicepricename"].ToString();
                                hsbaService.maubenhphamid = Utilities.Util_TypeConvertParse.ToInt64(dataHoiChan_PT.Rows[i]["maubenhphamid"].ToString());
                                hsbaService.servicepricedate = Utilities.Util_TypeConvertParse.ToDateTime(dataHoiChan_PT.Rows[i]["servicepricedate"].ToString());
                                lstHsbaHoiChanService_PT.Add(hsbaService);
                            }
                        }
                    }
                    // kiem tra de Enable nut chuc nang
                    if (lstHsbaHoiChanService_PT.Count > 0)
                    {
                        btnHoiChan_PhauThuat.Enabled = true;
                    }
                    else
                    {
                        btnHoiChan_PhauThuat.Enabled = false;
                    }
                }
                else
                {
                    btnHoiChan_PhauThuat.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void HoiChan_KiemTraTaoPhieuHoiChan_Thuoc(MedicalrecordDTO rowMecicalrecord)
        {
            SplashScreenManager.ShowForm(typeof(O2S_MedicalRecord.Utilities.ThongBao.WaitForm1));
            try
            {
                if (rowMecicalrecord.medicalrecordstatus == 2) //BN dang dieu tri thi duoc tao
                {
                    lstHsbaHoiChanService_Thuoc = new List<MrdHsbaHoiChan_ServiceDTO>();
                    string sqlHoiChan_Thuoc = "select ROW_NUMBER () OVER (ORDER BY ser.servicepricename) as stt, pt.medicinerefid, ser.medicinerefid_org, ser.servicepriceid, ser.medicalrecordid, ser.vienphiid, ser.hosobenhanid, ser.maubenhphamid, ser.departmentid, ser.departmentgroupid, ser.servicepricecode, ser.servicepricename, ser.servicepricename_bhyt, ser.servicepricename_nuocngoai, ser.servicepricedate, ser.soluong from mrd_dmhc_thuoc pt inner join dblink('myconn','select me.medicinerefid_org, ser.servicepriceid, ser.medicalrecordid, ser.vienphiid, ser.hosobenhanid, ser.maubenhphamid, ser.departmentid, ser.departmentgroupid, ser.servicepricecode, ser.servicepricename, ser.servicepricename_bhyt, ser.servicepricename_nuocngoai, ser.servicepricedate, ser.soluong FROM serviceprice ser inner join (select medicinecode, medicinerefid_org from medicine_ref where datatype=0) me on me.medicinecode=ser.servicepricecode where bhyt_groupcode in (''09TDT'',''091TDTtrongDM'',''092TDTngoaiDM'',''093TDTUngthu'',''094TDTTyle'') and hosobenhanid=" + rowMecicalrecord.hosobenhanid + "') AS ser(medicinerefid_org integer, servicepriceid integer, medicalrecordid integer, vienphiid integer, hosobenhanid integer, maubenhphamid integer, departmentid integer, departmentgroupid integer, servicepricecode text, servicepricename text, servicepricename_bhyt text, servicepricename_nuocngoai text, servicepricedate timestamp without time zone, soluong double precision) on pt.medicinerefid=ser.medicinerefid_org;";
                    DataTable dataHoiChan_Thuoc = condb.GetDataTable_Dblink(sqlHoiChan_Thuoc);
                    if (dataHoiChan_Thuoc != null && dataHoiChan_Thuoc.Rows.Count > 0)
                    {
                        for (int i = 0; i < dataHoiChan_Thuoc.Rows.Count; i++)
                        {
                            if (this.lstHsbaHoiChan != null && this.lstHsbaHoiChan.Count > 0)
                            {
                                long medicinerefid = Utilities.Util_TypeConvertParse.ToInt64(dataHoiChan_Thuoc.Rows[i]["medicinerefid"].ToString());
                                List<MrdHsbaHoiChanDTO> hsba_HoiChan = this.lstHsbaHoiChan.Where(o => o.medicinerefid_org == medicinerefid).ToList(); //thuốc chỉ tạo hội chẩn 1 lần
                                if (hsba_HoiChan.Count == 0)
                                {
                                    MrdHsbaHoiChan_ServiceDTO hsbaService = new MrdHsbaHoiChan_ServiceDTO();
                                    hsbaService.medicinerefid_org = Utilities.Util_TypeConvertParse.ToInt64(dataHoiChan_Thuoc.Rows[i]["medicinerefid_org"].ToString());
                                    hsbaService.servicepriceid = Utilities.Util_TypeConvertParse.ToInt64(dataHoiChan_Thuoc.Rows[i]["servicepriceid"].ToString());
                                    hsbaService.servicepricecode = dataHoiChan_Thuoc.Rows[i]["servicepricecode"].ToString();
                                    hsbaService.servicepricename = dataHoiChan_Thuoc.Rows[i]["servicepricename"].ToString();
                                    hsbaService.maubenhphamid = Utilities.Util_TypeConvertParse.ToInt64(dataHoiChan_Thuoc.Rows[i]["maubenhphamid"].ToString());
                                    hsbaService.servicepricedate = Utilities.Util_TypeConvertParse.ToDateTime(dataHoiChan_Thuoc.Rows[i]["servicepricedate"].ToString());
                                    lstHsbaHoiChanService_Thuoc.Add(hsbaService);
                                }
                            }
                            else
                            {
                                MrdHsbaHoiChan_ServiceDTO hsbaService = new MrdHsbaHoiChan_ServiceDTO();
                                hsbaService.servicepriceid = Utilities.Util_TypeConvertParse.ToInt64(dataHoiChan_Thuoc.Rows[i]["servicepriceid"].ToString());
                                hsbaService.servicepricecode = dataHoiChan_Thuoc.Rows[i]["servicepricecode"].ToString();
                                hsbaService.servicepricename = dataHoiChan_Thuoc.Rows[i]["servicepricename"].ToString();
                                hsbaService.maubenhphamid = Utilities.Util_TypeConvertParse.ToInt64(dataHoiChan_Thuoc.Rows[i]["maubenhphamid"].ToString());
                                hsbaService.servicepricedate = Utilities.Util_TypeConvertParse.ToDateTime(dataHoiChan_Thuoc.Rows[i]["servicepricedate"].ToString());
                                lstHsbaHoiChanService_Thuoc.Add(hsbaService);
                            }
                        }
                    }
                    // kiem tra de Enable nut chuc nang
                    if (lstHsbaHoiChanService_Thuoc.Count > 0)
                    {
                        btnHoiChan_Thuoc.Enabled = true;
                    }
                    else
                    {
                        btnHoiChan_Thuoc.Enabled = false;
                    }
                }
                else
                {
                    btnHoiChan_Thuoc.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void HoiChan_KiemTraTaoPhieuHoiChan_CV(MedicalrecordDTO rowMecicalrecord)
        {
            try
            {
                string sqlHoiChan_CV = "select medicalrecordid, patientid, chuyenvienid, thoigianchuyenvien from chuyenvien where medicalrecordid=" + rowMecicalrecord.medicalrecordid + ";";
                this.dataHoiChan_CV = condb.GetDataTable_HIS(sqlHoiChan_CV);
                if (this.dataHoiChan_CV != null && this.dataHoiChan_CV.Rows.Count > 0)
                {
                    if (this.lstHsbaHoiChan != null && this.lstHsbaHoiChan.Count > 0)
                    {
                        for (int i = 0; i < this.dataHoiChan_CV.Rows.Count; i++)
                        {
                            List<MrdHsbaHoiChanDTO> hsba_HoiChan = this.lstHsbaHoiChan.Where(o => o.medicalrecordid == rowMecicalrecord.medicalrecordid && o.mrd_loaihc_id == 3).ToList();
                            if (hsba_HoiChan != null && hsba_HoiChan.Count > 0)
                            {
                                btnHoiChan_ChuyenVien.Enabled = false;
                            }
                            else
                            {
                                btnHoiChan_ChuyenVien.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        btnHoiChan_ChuyenVien.Enabled = true;
                    }
                }
                else
                {
                    btnHoiChan_ChuyenVien.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Custome
        private void gridViewChiTietPhieuHoiChan_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                //if (e.Column.FieldName == "serviceprice_status")
                //{
                //    string val = Convert.ToString(gridViewChiTietPhieuHoiChan.GetRowCellValue(e.RowHandle, "mrd_HoiChan_serviceid"));
                //    string mrd_HoiChan_servicestatus = Convert.ToString(gridViewChiTietPhieuHoiChan.GetRowCellValue(e.RowHandle, "mrd_HoiChan_servicestatus"));
                //    if (val != "0") //da nhap HoiChan
                //    {
                //        if (mrd_HoiChan_servicestatus == "0") //nhap
                //        {
                //            e.Handled = true;
                //            Point pos = Util_GUIGridView.CalcPosition(e, imageListstatus.Images[0]);
                //            e.Graphics.DrawImage(imageListstatus.Images[0], pos);
                //        }
                //        else if (mrd_HoiChan_servicestatus == "1")//da luu OK
                //        {
                //            e.Handled = true;
                //            Point pos = Util_GUIGridView.CalcPosition(e, imageListstatus.Images[1]);
                //            e.Graphics.DrawImage(imageListstatus.Images[1], pos);
                //        }
                //        else if (mrd_HoiChan_servicestatus == "2")//da in
                //        {
                //            e.Handled = true;
                //            Point pos = Util_GUIGridView.CalcPosition(e, imageListstatus.Images[1]);
                //            e.Graphics.DrawImage(imageListstatus.Images[1], pos);
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void gridViewHoiChanDSHC_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }
        #endregion

        #region Clink Button Tao Phieu
        private void btnHoiChan_ChuyenVien_Click(object sender, EventArgs e)
        {
            try
            {
                cboHoiChan_ChonDVThuoc.Enabled = false;
                cboHoiChan_ChonDVThuoc.Properties.DataSource = null;

                MrdHsbaHoiChanDTO rowHsbaHoiChan = new MrdHsbaHoiChanDTO();
                if (this.dataHoiChan_CV != null && this.dataHoiChan_CV.Rows.Count > 0)
                {
                    rowHsbaHoiChan.his_chuyenvienid = Utilities.Util_TypeConvertParse.ToInt64(this.dataHoiChan_CV.Rows[0]["chuyenvienid"].ToString());
                    rowHsbaHoiChan.servicepricedate = Utilities.Util_TypeConvertParse.ToDateTime(this.dataHoiChan_CV.Rows[0]["thoigianchuyenvien"].ToString());
                }
                HoiChan.frmHoiChan_NhapDuLieu frmHoiChan = new HoiChan.frmHoiChan_NhapDuLieu(3, this.SelectRowMedicalrecord, rowHsbaHoiChan);
                frmHoiChan.ShowDialog();
                ucDSHoSoBenhAn_HoiChan_Load(this.SelectRowMedicalrecord);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void btnHoiChan_PhauThuat_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstHsbaHoiChanService_PT != null && lstHsbaHoiChanService_PT.Count > 0)
                {
                    this.mrd_loaihc_id = 1;
                    cboHoiChan_ChonDVThuoc.Enabled = true;
                    cboHoiChan_ChonDVThuoc.Properties.DataSource = lstHsbaHoiChanService_PT.OrderBy(o => o.maubenhphamid);
                    cboHoiChan_ChonDVThuoc.Properties.DisplayMember = "servicepricename";
                    cboHoiChan_ChonDVThuoc.Properties.ValueMember = "servicepricecode";
                    lblTenHienThiChonDVThuoc.Text = "Chọn dịch vụ";
                }
                else
                {
                    cboHoiChan_ChonDVThuoc.Enabled = false;
                    cboHoiChan_ChonDVThuoc.Properties.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void btnHoiChan_Thuoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstHsbaHoiChanService_Thuoc != null && lstHsbaHoiChanService_Thuoc.Count > 0)
                {
                    this.mrd_loaihc_id = 2;
                    cboHoiChan_ChonDVThuoc.Enabled = true;
                    cboHoiChan_ChonDVThuoc.Properties.DataSource = lstHsbaHoiChanService_Thuoc.OrderBy(o => o.maubenhphamid);
                    cboHoiChan_ChonDVThuoc.Properties.DisplayMember = "servicepricename";
                    cboHoiChan_ChonDVThuoc.Properties.ValueMember = "servicepricecode";
                    lblTenHienThiChonDVThuoc.Text = "Chọn thuốc hoạt chất dấu *";
                }
                else
                {
                    cboHoiChan_ChonDVThuoc.Enabled = false;
                    cboHoiChan_ChonDVThuoc.Properties.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void cboHoiChan_ChonDVThuoc_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboHoiChan_ChonDVThuoc.EditValue != null && this.mrd_loaihc_id != 0)
                {
                    MrdHsbaHoiChan_ServiceDTO item_select = cboHoiChan_ChonDVThuoc.GetSelectedDataRow() as MrdHsbaHoiChan_ServiceDTO;
                    MrdHsbaHoiChanDTO rowHsbaHoiChan = new MrdHsbaHoiChanDTO();
                    rowHsbaHoiChan.medicinerefid_org = item_select.medicinerefid_org;
                    rowHsbaHoiChan.servicepriceid = item_select.servicepriceid;
                    rowHsbaHoiChan.servicepricecode = item_select.servicepricecode;
                    rowHsbaHoiChan.servicepricename = item_select.servicepricename;
                    rowHsbaHoiChan.maubenhphamid = item_select.maubenhphamid;
                    rowHsbaHoiChan.servicepricedate = item_select.servicepricedate;

                    HoiChan.frmHoiChan_NhapDuLieu frmHoiChan = new HoiChan.frmHoiChan_NhapDuLieu(this.mrd_loaihc_id, this.SelectRowMedicalrecord, rowHsbaHoiChan);
                    frmHoiChan.ShowDialog();
                    ucDSHoSoBenhAn_HoiChan_Load(this.SelectRowMedicalrecord);
                    cboHoiChan_ChonDVThuoc.EditValue = null;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Grid View Click
        private void repositoryItemButtonHoiChan_Sua_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewHoiChanDSHC.RowCount > 0)
                {
                    var rowHandle = gridViewHoiChanDSHC.FocusedRowHandle;
                    int mrd_loaihc_id = Utilities.Util_TypeConvertParse.ToInt32(gridViewHoiChanDSHC.GetRowCellValue(rowHandle, "mrd_loaihc_id").ToString());
                    long mrd_hsba_hcid = Utilities.Util_TypeConvertParse.ToInt64(gridViewHoiChanDSHC.GetRowCellValue(rowHandle, "mrd_hsba_hcid").ToString());
                    MrdHsbaHoiChanDTO rowHsbaHoiChanClick = this.lstHsbaHoiChan.Where(o => o.mrd_loaihc_id == mrd_loaihc_id && o.mrd_hsba_hcid == mrd_hsba_hcid).FirstOrDefault();
                    if (rowHsbaHoiChanClick != null)
                    {
                        HoiChan.frmHoiChan_NhapDuLieu frmHoiChan = new HoiChan.frmHoiChan_NhapDuLieu(mrd_loaihc_id, this.SelectRowMedicalrecord, rowHsbaHoiChanClick);
                        frmHoiChan.ShowDialog();
                        ucDSHoSoBenhAn_HoiChan_Load(this.SelectRowMedicalrecord);
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        //In trich bien ban hoi chan
        private void repositoryItemButtonHoiChan_InTrichBBHC_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewHoiChanDSHC.RowCount > 0)
                {
                    var rowHandle = gridViewHoiChanDSHC.FocusedRowHandle;
                    int mrd_loaihc_id = Utilities.Util_TypeConvertParse.ToInt32(gridViewHoiChanDSHC.GetRowCellValue(rowHandle, "mrd_loaihc_id").ToString());
                    long mrd_hsba_hcid = Utilities.Util_TypeConvertParse.ToInt64(gridViewHoiChanDSHC.GetRowCellValue(rowHandle, "mrd_hsba_hcid").ToString());
                    MrdHsbaHoiChanDTO rowHsbaHoiChanClick = this.lstHsbaHoiChan.Where(o => o.mrd_loaihc_id == mrd_loaihc_id && o.mrd_hsba_hcid == mrd_hsba_hcid).FirstOrDefault();
                    if (rowHsbaHoiChanClick != null)
                    {
                        PrintTrichBienBanHoiChan_Process(rowHsbaHoiChanClick);
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        //In So bien ban hoi chan
        private void repositoryItemButtonHoiChan_InSoBBHC_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewHoiChanDSHC.RowCount > 0)
                {
                    var rowHandle = gridViewHoiChanDSHC.FocusedRowHandle;
                    int mrd_loaihc_id = Utilities.Util_TypeConvertParse.ToInt32(gridViewHoiChanDSHC.GetRowCellValue(rowHandle, "mrd_loaihc_id").ToString());
                    long mrd_hsba_hcid = Utilities.Util_TypeConvertParse.ToInt64(gridViewHoiChanDSHC.GetRowCellValue(rowHandle, "mrd_hsba_hcid").ToString());
                    MrdHsbaHoiChanDTO rowHsbaHoiChanClick = this.lstHsbaHoiChan.Where(o => o.mrd_loaihc_id == mrd_loaihc_id && o.mrd_hsba_hcid == mrd_hsba_hcid).FirstOrDefault();
                    if (rowHsbaHoiChanClick != null)
                    {
                        PrintSoBienBanHoiChan_Process(rowHsbaHoiChanClick);
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void PrintTrichBienBanHoiChan_Process(MrdHsbaHoiChanDTO rowHsbaHoiChanClick)
        {
            try
            {
                string templateFullPath = Environment.CurrentDirectory + Base.KeyTrongPhanMem.BienBanHoiChan_Path + "\\40. Trich bien ban hoi chuan.DOC";

                string hoichan_fulltime2 = ".......giờ .......phút, ngày....../..../.....";
                if (rowHsbaHoiChanClick.mrd_loaihc_id == 1 || rowHsbaHoiChanClick.mrd_loaihc_id == 2)//hoi chan PTTT
                {
                    hoichan_fulltime2 = rowHsbaHoiChanClick.servicepricedate.Hour + " giờ " + rowHsbaHoiChanClick.servicepricedate.Minute + " phút, ngày " + rowHsbaHoiChanClick.servicepricedate.Day + "/" + rowHsbaHoiChanClick.servicepricedate.Month + "/" + rowHsbaHoiChanClick.servicepricedate.Year;
                }
                else //hoi chan chuyen vien
                {
                    hoichan_fulltime2 = rowHsbaHoiChanClick.thoigianhoichan.Hour + " giờ " + rowHsbaHoiChanClick.thoigianhoichan.Minute + " phút, ngày " + rowHsbaHoiChanClick.thoigianhoichan.Day + "/" + rowHsbaHoiChanClick.thoigianhoichan.Month + "/" + rowHsbaHoiChanClick.thoigianhoichan.Year;
                }
                if (this.SelectRowMedicalrecord.departmentgroupname == null || this.SelectRowMedicalrecord.departmentgroupname == "")
                {
                    string gettenkhoa = "select mrd.giuong, de.departmentname, degp.departmentgroupname from medicalrecord mrd inner join department de on de.departmentid=mrd.departmentid inner join departmentgroup degp on degp.departmentgroupid=mrd.departmentgroupid where mrd.medicalrecordid=" + this.SelectRowMedicalrecord.medicalrecordid + ";";

                    DataTable datakhoa = condb.GetDataTable_HIS(gettenkhoa);
                    if (datakhoa != null && datakhoa.Rows.Count > 0)
                    {
                        this.SelectRowMedicalrecord.giuong = datakhoa.Rows[0]["giuong"].ToString();
                        this.SelectRowMedicalrecord.departmentname = datakhoa.Rows[0]["departmentname"].ToString();
                        this.SelectRowMedicalrecord.departmentgroupname = datakhoa.Rows[0]["departmentgroupname"].ToString();
                    }
                }

                string thongtinbn = "select '" + rowHsbaHoiChanClick.servicepricename + "' as servicepricename, hsba.sovaovien as sovaovien, hsba.patientname as patientname, cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) as tuoi, hsba.gioitinhname as gioitinh, (select to_char(thoigianvaovien, 'dd/mm/yyyy') from medicalrecord where loaibenhanid=1 and hosobenhanid=hsba.hosobenhanid order by medicalrecordid limit 1) as tg_vaovien_fulltime2, (select (case when thoigianravien <> '0001-01-01 00:00:00' then to_char(thoigianravien, 'dd/mm/yyyy') end) from medicalrecord where loaibenhanid=1 and hosobenhanid=hsba.hosobenhanid order by medicalrecordid limit 1) as tg_ravien_fulltime1, '" + this.SelectRowMedicalrecord.giuong + "' as giuong, '" + this.SelectRowMedicalrecord.departmentname + "' as buong, '" + this.SelectRowMedicalrecord.departmentgroupname + "' as khoa, '" + this.SelectRowMedicalrecord.chandoanbandau + "' as chandoan, '" + hoichan_fulltime2 + "' as hoichan_fulltime2, '" + rowHsbaHoiChanClick.tvtg_chutoa_ten + "' as tvtg_chutoa_ten, '" + rowHsbaHoiChanClick.tvtg_chutoa_cdcv + "' as tvtg_chutoa_cdcv, '" + rowHsbaHoiChanClick.tvtg_thuky_ten + "' as tvtg_thuky_ten, '" + rowHsbaHoiChanClick.tvtg_thuky_cdcv + "' as tvtg_thuky_cdcv, '" + rowHsbaHoiChanClick.tvtg_thanhvien1_ten + "' as tvtg_thanhvien1_ten, '" + rowHsbaHoiChanClick.tvtg_thanhvien1_cdcv + "' as tvtg_thanhvien1_cdcv, '" + rowHsbaHoiChanClick.tvtg_thanhvien2_ten + "' as tvtg_thanhvien2_ten, '" + rowHsbaHoiChanClick.tvtg_thanhvien2_cdcv + "' as tvtg_thanhvien2_cdcv, '" + rowHsbaHoiChanClick.tvtg_thanhvien3_ten + "' as tvtg_thanhvien3_ten, '" + rowHsbaHoiChanClick.tvtg_thanhvien3_cdcv + "' as tvtg_thanhvien3_cdcv, '" + rowHsbaHoiChanClick.tvtg_thanhvien4_ten + "' as tvtg_thanhvien4_ten, '" + rowHsbaHoiChanClick.tvtg_thanhvien4_cdcv + "' as tvtg_thanhvien4_cdcv, '" + rowHsbaHoiChanClick.tvtg_thanhvien5_ten + "' as tvtg_thanhvien5_ten, '" + rowHsbaHoiChanClick.tvtg_thanhvien5_cdcv + "' as tvtg_thanhvien5_cdcv, '" + rowHsbaHoiChanClick.tvtg_thanhvien6_ten + "' as tvtg_thanhvien6_ten, '" + rowHsbaHoiChanClick.tvtg_thanhvien6_cdcv + "' as tvtg_thanhvien6_cdcv, '" + rowHsbaHoiChanClick.dbb_tomtatdienbienbenh + "' as dbb_tomtatdienbienbenh, '" + rowHsbaHoiChanClick.kl_ketluan + "' as kl_ketluan, '" + rowHsbaHoiChanClick.yk_phuongphapdieutri + "' as yk_phuongphapdieutri from hosobenhan hsba where hsba.hosobenhanid=" + rowHsbaHoiChanClick.hosobenhanid + "; ";

                DataTable dataTTBenhNhan = condb.GetDataTable_HIS(thongtinbn);
                Aspose.Words.Document documentWord = new Aspose.Words.Document();
                documentWord = Utilities.Common.Word.WordMergeTemplateExport.ExportWordMailMerge(templateFullPath, dataTTBenhNhan, "40. Trich bien ban hoi chuan.doc");

                Aspose.Words.Document doc = new Aspose.Words.Document(Environment.CurrentDirectory + Base.KeyTrongPhanMem.ReportTemps_Path + "\\40. Trich bien ban hoi chuan.doc");

                PrintDialog printDlg = new PrintDialog();
                // Initialize the print dialog with the number of pages in the document.
                printDlg.AllowSomePages = true;
                printDlg.PrinterSettings.MinimumPage = 1;
                printDlg.PrinterSettings.MaximumPage = doc.PageCount;
                printDlg.PrinterSettings.FromPage = 1;
                printDlg.PrinterSettings.ToPage = doc.PageCount;
                AsposeWordsPrintDocument awPrintDoc = new AsposeWordsPrintDocument(doc);
                awPrintDoc.PrinterSettings = printDlg.PrinterSettings;

                using (var dlg = new O2S_MedicalRecord.Utilities.PrintPreview.CoolPrintPreviewDialog())
                {
                    dlg.Document = awPrintDoc;
                    dlg.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void PrintSoBienBanHoiChan_Process(MrdHsbaHoiChanDTO rowHsbaHoiChanClick)
        {
            try
            {
                string templateFullPath = Environment.CurrentDirectory + Base.KeyTrongPhanMem.BienBanHoiChan_Path + "\\So BB hoi chuan.DOC";

                string hoichan_fulltime1 = "ngày ...... tháng ...... năm ..........; lúc ........ giờ ........ phút";
                if (rowHsbaHoiChanClick.mrd_loaihc_id == 1 || rowHsbaHoiChanClick.mrd_loaihc_id == 2)//hoi chan PTTT
                {
                    hoichan_fulltime1 = "ngày " + rowHsbaHoiChanClick.servicepricedate.Day + " tháng " + rowHsbaHoiChanClick.servicepricedate.Month + " năm " + rowHsbaHoiChanClick.servicepricedate.Year + "; lúc " + rowHsbaHoiChanClick.servicepricedate.Hour + " giờ " + rowHsbaHoiChanClick.servicepricedate.Minute + " phút";
                }
                else //hoi chan chuyen vien
                {
                    hoichan_fulltime1 = "ngày " + rowHsbaHoiChanClick.thoigianhoichan.Day + " tháng " + rowHsbaHoiChanClick.thoigianhoichan.Month + " năm " + rowHsbaHoiChanClick.thoigianhoichan.Year + "; lúc " + rowHsbaHoiChanClick.thoigianhoichan.Hour + " giờ " + rowHsbaHoiChanClick.thoigianhoichan.Minute + " phút";
                }

                string thongtinbn = "select '" + hoichan_fulltime1 + "' as hoichan_fulltime1, '" + rowHsbaHoiChanClick.tvtg_chutoa_ten + "' as tvtg_chutoa_ten, '" + rowHsbaHoiChanClick.tvtg_chutoa_cdcv + "' as tvtg_chutoa_cdcv, '" + rowHsbaHoiChanClick.tvtg_thuky_ten + "' as tvtg_thuky_ten, '" + rowHsbaHoiChanClick.tvtg_thuky_cdcv + "' as tvtg_thuky_cdcv, '" + rowHsbaHoiChanClick.tvtg_thanhvien1_ten + "' as tvtg_thanhvien1_ten, '" + rowHsbaHoiChanClick.tvtg_thanhvien1_cdcv + "' as tvtg_thanhvien1_cdcv, '" + rowHsbaHoiChanClick.tvtg_thanhvien2_ten + "' as tvtg_thanhvien2_ten, '" + rowHsbaHoiChanClick.tvtg_thanhvien2_cdcv + "' as tvtg_thanhvien2_cdcv, '" + rowHsbaHoiChanClick.tvtg_thanhvien3_ten + "' as tvtg_thanhvien3_ten, '" + rowHsbaHoiChanClick.tvtg_thanhvien3_cdcv + "' as tvtg_thanhvien3_cdcv, '" + rowHsbaHoiChanClick.tvtg_thanhvien4_ten + "' as tvtg_thanhvien4_ten, '" + rowHsbaHoiChanClick.tvtg_thanhvien4_cdcv + "' as tvtg_thanhvien4_cdcv, '" + rowHsbaHoiChanClick.tvtg_thanhvien5_ten + "' as tvtg_thanhvien5_ten, '" + rowHsbaHoiChanClick.tvtg_thanhvien5_cdcv + "' as tvtg_thanhvien5_cdcv, '" + rowHsbaHoiChanClick.tvtg_thanhvien6_ten + "' as tvtg_thanhvien6_ten, '" + rowHsbaHoiChanClick.tvtg_thanhvien6_cdcv + "' as tvtg_thanhvien6_cdcv, '" + rowHsbaHoiChanClick.diadiemhoichan + "' as diadiemhoichan, hsba.patientname as patientname, cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) as tuoi, hsba.gioitinhname as gioitinh, hsba.hc_dantocname as dantoc, hsba.hc_quocgianame as ngoaikieu, '' as sohochieu, '' as ngay_noicap, hsba.nghenghiepname as nghenghiep, hsba.noilamviec as noilamviec, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, hsba.sovaovien as sovaovien, substr(hsba.bhytcode,1,2) as bhyt_1, substr(hsba.bhytcode,3,1) as bhyt_2, substr(hsba.bhytcode,4,2) as bhyt_3, substr(hsba.bhytcode,6,2) as bhyt_4, substr(hsba.bhytcode,8,8) as bhyt_5, (select to_char(thoigianvaovien, 'hh g\"i\"ờ mi phút, ngà\"y\" dd tháng mm năm yyyy') from medicalrecord where loaibenhanid=1 and hosobenhanid=hsba.hosobenhanid order by medicalrecordid limit 1) as tg_vaovien_fulltime1, (select departmentgroupname from departmentgroup where departmentgroupid=" + rowHsbaHoiChanClick.departmentgroupid + " limit 1) as khoa, '" + rowHsbaHoiChanClick.yeucauhoichanname + "' as yeucauhoichanname, '" + rowHsbaHoiChanClick.dbb_tomtattiensubenh + "' as dbb_tomtattiensubenh, '" + rowHsbaHoiChanClick.dbb_tinhtranglucvaovien + "' as dbb_tinhtranglucvaovien, '" + rowHsbaHoiChanClick.dbb_chandoantuyenduoi + "' as dbb_chandoantuyenduoi, '" + rowHsbaHoiChanClick.dbb_tomtatdienbienbenh + "' as dbb_tomtatdienbienbenh, '" + rowHsbaHoiChanClick.yk_chandoantienluong + "' as yk_chandoantienluong, '" + rowHsbaHoiChanClick.yk_phuongphapdieutri + "' as yk_phuongphapdieutri, '" + rowHsbaHoiChanClick.yk_chamsoc + "' as yk_chamsoc, '" + rowHsbaHoiChanClick.kl_ketluan + "' as kl_ketluan from hosobenhan hsba where hsba.hosobenhanid=" + rowHsbaHoiChanClick.hosobenhanid + "; ";

                DataTable dataTTBenhNhan = condb.GetDataTable_HIS(thongtinbn);
                Aspose.Words.Document documentWord = new Aspose.Words.Document();
                documentWord = Utilities.Common.Word.WordMergeTemplateExport.ExportWordMailMerge(templateFullPath, dataTTBenhNhan, "So BB hoi chuan.doc");

                Aspose.Words.Document doc = new Aspose.Words.Document(Environment.CurrentDirectory + Base.KeyTrongPhanMem.ReportTemps_Path + "\\So BB hoi chuan.doc");

                PrintDialog printDlg = new PrintDialog();
                // Initialize the print dialog with the number of pages in the document.
                printDlg.AllowSomePages = true;
                printDlg.PrinterSettings.MinimumPage = 1;
                printDlg.PrinterSettings.MaximumPage = doc.PageCount;
                printDlg.PrinterSettings.FromPage = 1;
                printDlg.PrinterSettings.ToPage = doc.PageCount;
                AsposeWordsPrintDocument awPrintDoc = new AsposeWordsPrintDocument(doc);
                awPrintDoc.PrinterSettings = printDlg.PrinterSettings;

                using (var dlg = new O2S_MedicalRecord.Utilities.PrintPreview.CoolPrintPreviewDialog())
                {
                    dlg.Document = awPrintDoc;
                    dlg.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }


        #endregion
    }
}
