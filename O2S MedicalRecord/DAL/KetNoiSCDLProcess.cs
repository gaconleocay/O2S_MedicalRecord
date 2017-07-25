using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace O2S_MedicalRecord.DAL
{
    internal static class KetNoiSCDLProcess
    {
        private static O2S_MedicalRecord.DAL.ConnectDatabase condb = new O2S_MedicalRecord.DAL.ConnectDatabase();

        internal static bool CapNhatCoSoDuLieu()
        {
            bool result = true;
            try
            {
                result = KetNoiSCDLProcess.CreateMrdServiceref();

                //result = KetNoiSCDLProcess.CreateTableTblUser();
                //result = KetNoiSCDLProcess.CreateTableTblPermission();
                result = KetNoiSCDLProcess.CreateTableTblDepartment();
                //result = KetNoiSCDLProcess.CreateTableTblLog();
                //result = KetNoiSCDLProcess.CreateTableLicense();
                //result = KetNoiSCDLProcess.CreateTableOption();
                //result = KetNoiSCDLProcess.CreateTableTblNhanVien();
                //result = KetNoiSCDLProcess.CreateTableUserMedicineStore();
                //result = KetNoiSCDLProcess.CreateTableUserMedicinePhongLuu();
                //result = KetNoiSCDLProcess.CreateTableToolsServicepricePttt();

                //result = KetNoiSCDLProcess.CreateTableUserDepartmentgroup();
                //result = KetNoiSCDLProcess.CreateTableVersion();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error("Lỗi Update DB" + ex.ToString());
            }
            return result;
        }

        #region Tao bang
        private static bool CreateMrdServiceref()
        {
            bool result = false;
            try
            {
                string sql_tbluser = "CREATE TABLE IF NOT EXISTS mrd_serviceref ( mrd_servicerefid serial NOT NULL, his_servicepricerefid integer, servicegrouptype integer, servicepricetype integer, bhyt_groupcode text, servicepricegroupcode text, servicepricecode text, servicepricename text, servicepricenamenhandan text, servicepricenamebhyt text, servicepricenamenuocngoai text, servicepriceunit text, servicepricefee text, servicepricefeenhandan text, servicepricefeebhyt text, servicepricefeenuocngoai text, servicelock integer DEFAULT 0, servicepricecodeuser text, servicepricesttuser text, pttt_hangid integer DEFAULT 0, pttt_loaiid integer DEFAULT 0, mrd_templatename text, CONSTRAINT mrd_serviceref_pkey PRIMARY KEY (mrd_servicerefid) );";
                if (condb.ExecuteNonQuery_HSBA(sql_tbluser))
                {
                    KiemTraVaInsertDuLieu_MrdServiceref();
                    //    result = true;
                    //    string sql_delete = "DELETE FROM mrd_serviceref;";
                    //    string sql_insert_ser = "INSERT INTO mrd_serviceref (his_servicepricerefid, servicegrouptype, servicepricetype, bhyt_groupcode, servicepricegroupcode, servicepricecode, servicepricename, servicepricenamenhandan, servicepricenamebhyt, servicepricenamenuocngoai, servicepriceunit, servicepricefee, servicepricefeenhandan, servicepricefeebhyt, servicepricefeenuocngoai, servicelock, servicepricecodeuser, servicepricesttuser, pttt_hangid, pttt_loaiid) SELECT servicepriceref.* FROM dblink('myconn','SELECT servicepricerefid, servicegrouptype, servicepricetype, bhyt_groupcode, servicepricegroupcode, servicepricecode, servicepricename, servicepricenamenhandan, servicepricenamebhyt, servicepricenamenuocngoai, servicepriceunit, servicepricefee, servicepricefeenhandan, servicepricefeebhyt, servicepricefeenuocngoai, servicelock, servicepricecodeuser, servicepricesttuser, pttt_hangid, pttt_loaiid FROM servicepriceref WHERE servicepricecode <>'' and COALESCE(isremove,0)<>1 and servicepricegroupcode<>'' and servicegrouptype in (1,2,3,4,11) ORDER BY servicegrouptype, bhyt_groupcode, servicepricegroupcode, servicepricename; ') AS servicepriceref(servicepricerefid integer, servicegrouptype integer, servicepricetype integer, bhyt_groupcode text, servicepricegroupcode text, servicepricecode text, servicepricename text, servicepricenamenhandan text, servicepricenamebhyt text, servicepricenamenuocngoai text, servicepriceunit text, servicepricefee text, servicepricefeenhandan text, servicepricefeebhyt text, servicepricefeenuocngoai text, servicelock integer, servicepricecodeuser text, servicepricesttuser text, pttt_hangid integer, pttt_loaiid integer);";
                    //    condb.ExecuteNonQuery_HSBA(sql_delete);
                    //    condb.ExecuteNonQuery_Dblink(sql_insert_ser);
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error("Lỗi CreateMrdServiceref" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableTblPermission()
        {
            bool result = false;
            try
            {
                string sql_tblper = "CREATE TABLE IF NOT EXISTS mrd_tbluser_permission ( userpermissionid serial NOT NULL, permissionid integer, permissioncode text, permissionname text, userid integer, usercode text, permissioncheck boolean, userpermissionnote text, CONSTRAINT userpermissionid_pkey PRIMARY KEY (userpermissionid));";
                if (condb.ExecuteNonQuery_HSBA(sql_tblper))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error("Lỗi CreateTableTblPermission" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableTblDepartment()
        {
            bool result = false;
            try
            {
                string sql_toolsdepatment = "CREATE TABLE IF NOT EXISTS mrd_depatment ( mrd_depatmentid serial NOT NULL, departmentgroupid integer, departmentgroupcode text, departmentgroupname text, departmentgrouptype integer, departmentid integer, departmentcode text, departmentname text, departmenttype integer, CONSTRAINT mrd_depatment_pkey PRIMARY KEY (mrd_depatmentid) );";
                string sql_deletepatient = "DELETE FROM mrd_depatment;";
                string sql_insert = "INSERT INTO mrd_depatment(departmentgroupid, departmentgroupcode, departmentgroupname, departmentgrouptype, departmentid, departmentcode, departmentname, departmenttype) SELECT department.* FROM dblink('myconn','SELECT degp.departmentgroupid as departmentgroupid, degp.departmentgroupcode as departmentgroupcode, degp.departmentgroupname as departmentgroupname, degp.departmentgrouptype, de.departmentid as departmentid, de.departmentcode as departmentcode, de.departmentname as departmentname, de.departmenttype FROM departmentgroup degp,department de WHERE de.departmentgroupid = degp.departmentgroupid ORDER BY degp.departmentgroupid') AS department(departmentgroupid integer, departmentgroupcode text, departmentgroupname text, departmentgrouptype integer, departmentid integer, departmentcode text, departmentname text, departmenttype integer);";

                if (condb.ExecuteNonQuery_HSBA(sql_toolsdepatment) && condb.ExecuteNonQuery_HSBA(sql_deletepatient) && condb.ExecuteNonQuery_Dblink(sql_insert))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error("Lỗi CreateTableTblDepartment" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableTblLog()
        {
            bool result = false;
            try
            {
                string sql_tbllog = "CREATE TABLE IF NOT EXISTS mrd_tbllog (logid serial NOT NULL,loguser text, logvalue text,ipaddress text,computername text,softversion text,logtime timestamp without time zone,CONSTRAINT tools_tbllog_pkey PRIMARY KEY (logid));";
                if (condb.ExecuteNonQuery_HSBA(sql_tbllog))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error("Lỗi CreateTableTblLog" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableOption()
        {
            bool result = false;
            try
            {
                string sqloption = "CREATE TABLE IF NOT EXISTS mrd_option(toolsoptionid serial NOT NULL, toolsoptioncode text, toolsoptionname text, toolsoptionvalue text, toolsoptionnote text, toolsoptionlook integer, toolsoptiondate timestamp without time zone, toolsoptioncreateuser text, CONSTRAINT tools_option_pkey PRIMARY KEY (toolsoptionid));";
                if (condb.ExecuteNonQuery_HSBA(sqloption))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error("Lỗi CreateTableOption" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableUserDepartmentgroup()
        {
            bool result = false;
            try
            {
                string sqloption = "CREATE TABLE IF NOT EXISTS mrd_tbluser_departmentgroup(userdepgid serial NOT NULL, departmentgroupid integer, departmentid integer, departmenttype integer, usercode text,  userdepgidnote text, CONSTRAINT tbluser_departmentgroup_pkey PRIMARY KEY (userdepgid));";
                if (condb.ExecuteNonQuery_HSBA(sqloption))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error("Lỗi CreateTableUserDepartmentgroup" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableVersion()
        {
            bool result = false;
            try
            {
                string sqloption = "CREATE TABLE IF NOT EXISTS mrd_version (versionid serial NOT NULL, appversion text, app_link text,  app_type integer, updateapp bytea, appsize integer, sqlversion text, updatesql bytea, sqlsize integer, sync_flag integer,  update_flag integer,  CONSTRAINT tools_version_pkey PRIMARY KEY (versionid));   ";
                if (condb.ExecuteNonQuery_HSBA(sqloption))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error("Lỗi CreateTableSersion" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableLicense()
        {
            bool result = false;
            try
            {
                string sql_tbltools_license = "CREATE TABLE IF NOT EXISTS mrd_license (licenseid serial NOT NULL, datakey text, licensekey text, CONSTRAINT tools_license_pkey PRIMARY KEY (licenseid));";
                if (condb.ExecuteNonQuery_HSBA(sql_tbltools_license))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error("Lỗi CreateTableLicense" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableTblNhanVien()
        {
            bool result = false;
            try
            {
                string sql_tbluser = "CREATE TABLE IF NOT EXISTS nhompersonnel ( nhanvienid serial NOT NULL, usercode text NOT NULL, username text, userpassword text, userstatus integer, usergnhom integer, usernote text, userhisid integer, CONSTRAINT nhompersonnel_pkey PRIMARY KEY (nhanvienid));";
                if (condb.ExecuteNonQuery_HIS(sql_tbluser))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error("Lỗi CreateTableTblNhanVien" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableUserMedicineStore()
        {
            bool result = false;
            try
            {
                string sql_tbluser = "CREATE TABLE IF NOT EXISTS mrd_tbluser_medicinestore( usermestid serial NOT NULL, medicinestoreid integer, medicinestoretype integer, usercode text, userdepgidnote text, CONSTRAINT tools_tbluser_medicinestore_pkey PRIMARY KEY (usermestid) );";
                if (condb.ExecuteNonQuery_HSBA(sql_tbluser))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error("Lỗi CreateTableUserMedicineStore" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableUserMedicinePhongLuu()
        {
            bool result = false;
            try
            {
                string sql_tbluser = "CREATE TABLE IF NOT EXISTS mrd_tbluser_medicinephongluu( userphongluutid serial NOT NULL, medicinephongluuid integer, medicinestoreid integer, usercode text, userdepgidnote text, CONSTRAINT tools_tbluser_medicinephongluu_pkey PRIMARY KEY (userphongluutid) );";
                if (condb.ExecuteNonQuery_HSBA(sql_tbluser))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error("Lỗi CreateTableUserMedicinePhongLuu" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableToolsServicepricePttt()
        {
            bool result = false;
            try
            {
                string ServicePttt = "CREATE TABLE IF NOT EXISTS tools_serviceprice_pttt ( servicepriceptttid serial NOT NULL, vienphiid integer, patientid integer, bhytid integer, hosobenhanid integer, loaivienphiid integer, vienphistatus integer, khoaravien integer, phongravien integer, doituongbenhnhanid integer, vienphidate timestamp without time zone, vienphidate_ravien timestamp without time zone, duyet_ngayduyet timestamp without time zone, vienphistatus_vp integer, duyet_ngayduyet_vp timestamp without time zone, vienphistatus_bh integer, duyet_ngayduyet_bh timestamp without time zone, bhyt_tuyenbenhvien integer, departmentid integer, departmentgroupid integer, departmentgroup_huong integer, money_khambenh_bh double precision, money_khambenh_vp double precision, money_xetnghiem_bh double precision, money_xetnghiem_vp double precision, money_cdha_bh double precision, money_cdha_vp double precision, money_tdcn_bh double precision, money_tdcn_vp double precision, money_pttt_bh double precision, money_pttt_vp double precision, money_mau_bh double precision, money_mau_vp double precision, money_giuongthuong_bh double precision, money_giuongthuong_vp double precision, money_giuongyeucau_bh double precision, money_giuongyeucau_vp double precision, money_vanchuyen_bh double precision, money_vanchuyen_vp double precision, money_khac_bh double precision, money_khac_vp double precision, money_phuthu_bh double precision, money_phuthu_vp double precision, money_thuoc_bh double precision, money_thuoc_vp double precision, money_vattu_bh double precision, money_vattu_vp double precision, money_vtthaythe_bh double precision, money_vtthaythe_vp double precision, money_dvktc_bh double precision, money_dvktc_vp double precision, money_chiphikhac double precision, money_hpngaygiuong double precision, money_hppttt double precision, money_hpdkpttt_gm_thuoc double precision, money_hpdkpttt_gm_vattu double precision, money_dkpttt_thuoc_bh double precision, money_dkpttt_thuoc_vp double precision, money_dkpttt_vattu_bh double precision, money_dkpttt_vattu_vp double precision, CONSTRAINT tools_serviceprice_pttt_pkey PRIMARY KEY (servicepriceptttid) );";
                if (condb.ExecuteNonQuery_HSBA(ServicePttt))
                {

                    result = true;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error("Lỗi CreateTableToolsServicepricePttt" + ex.ToString());
            }
            return result;
        }
        #endregion

        #region Tao Function
        private static bool CreateFunctionByteaImport()
        {
            bool result = false;
            try
            {
                string sqloption = " create or replace function bytea_import(p_path text, p_result out bytea) language plpgsql as $$ declare l_oid oid; r record; begin p_result := ''; select lo_import(p_path) into l_oid; for r in ( select data from pg_largeobject where loid = l_oid order by pageno ) loop p_result = p_result || r.data; end loop; perform lo_unlink(l_oid); end;$$;";
                if (condb.ExecuteNonQuery_HSBA(sqloption))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error("Lỗi CreateFunctionByteaimport" + ex.ToString());
            }
            return result;
        }

        #endregion

        #region Custom
        private static void KiemTraVaInsertDuLieu_MrdServiceref()
        {
            try
            {
                string laydulieu = "";
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error("Lỗi CreateMrdServiceref" + ex.ToString());
            }
        }



        #endregion
    }
}
