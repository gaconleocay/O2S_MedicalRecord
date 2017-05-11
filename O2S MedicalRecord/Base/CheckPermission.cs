using O2S_MedicalRecord.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace O2S_MedicalRecord.Base
{
    public static class CheckPermission
    {
        static O2S_MedicalRecord.DAL.ConnectDatabase condb = new O2S_MedicalRecord.DAL.ConnectDatabase();
        public static bool ChkPerModule(string percode)
        {
            bool result = false;
            try
            {
                if (SessionLogin.SessionUsercode == KeyTrongPhanMem.AdminUser_key)
                {
                    result = true;
                }
                else
                {
                    var checkPhanQuyen = SessionLogin.SessionLstPhanQuyenNguoiDung.Where(s => s.permissioncode.Contains(percode)).ToList();
                    if (checkPhanQuyen != null && checkPhanQuyen.Count > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error(ex);
            }
            return result;
        }

        //Lay danh sach phan quyen khi nguoi dung dang nhap
        public static List<DTO.classPermission> GetListPhanQuyenNguoiDung()
        {
            List<DTO.classPermission> lstPhanQuyen = new List<DTO.classPermission>();
            try
            {
                if (SessionLogin.SessionUsercode == KeyTrongPhanMem.AdminUser_key)
                {
                    lstPhanQuyen = Base.listChucNang.getDanhSachChucNang();
                    foreach (var item in lstPhanQuyen)
                    {
                        item.permissioncheck = true;
                    }
                }
                else
                {
                    string en_usercode = O2S_MedicalRecord.Base.EncryptAndDecrypt.Encrypt(SessionLogin.SessionUsercode, true);
                    string sqlper = "SELECT permissionid, permissioncode, permissionname, userid, usercode, permissioncheck FROM mrd_tbluser_permission WHERE usercode = '" + en_usercode + "' and permissioncheck='1';";
                    DataView dv = new DataView(condb.GetDataTable_HIS(sqlper));
                    if (dv.Count > 0)
                    {
                        for (int i = 0; i < dv.Count; i++)
                        {
                            DTO.classPermission itemPer = new DTO.classPermission();
                            //itemPer.permissionid = Convert.ToInt32(dv[i]["permissionid"]);
                            itemPer.permissioncode = Base.EncryptAndDecrypt.Decrypt(dv[i]["permissioncode"].ToString(), true);
                            itemPer.permissionname = Base.EncryptAndDecrypt.Decrypt(dv[i]["permissionname"].ToString(), true);
                            itemPer.en_permissioncode = dv[i]["permissioncode"].ToString();
                            itemPer.en_permissionname = dv[i]["permissionname"].ToString();
                            itemPer.permissioncheck = Convert.ToBoolean(dv[i]["permissioncheck"]);
                            lstPhanQuyen.Add(itemPer);
                        }
                        foreach (var item_chucnang in lstPhanQuyen)
                        {
                            var chucnang = Base.listChucNang.getDanhSachChucNang().Where(o => o.permissioncode == item_chucnang.permissioncode).SingleOrDefault();
                            if (chucnang != null)
                            {
                                item_chucnang.permissiontype = chucnang.permissiontype;
                                item_chucnang.permissionnote = chucnang.permissionnote;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error(ex);
            }
            return lstPhanQuyen;
        }

        public static List<DTO.classUserDepartment> GetPhanQuyen_KhoaPhong()
        {
            List<DTO.classUserDepartment> lstPhanQuyenKhoaPhong = new List<DTO.classUserDepartment>();
            try
            {
                string sqlper = "";
                if (SessionLogin.SessionUsercode == Base.KeyTrongPhanMem.AdminUser_key)
                {
                    sqlper = "SELECT de.departmentgroupid,de.departmentgroupcode, de.departmentgroupname,de.departmentgrouptype, de.departmentid,de.departmentcode,de.departmentname,de.departmenttype FROM  mrd_depatment de WHERE de.departmentgrouptype in (1,4,10,11) and de.departmenttype in (2,3,7,9) ORDER BY de.departmentgroupname,de.departmentname,de.departmenttype;";
                }
                else
                {
                    string en_usercode = O2S_MedicalRecord.Base.EncryptAndDecrypt.Encrypt(SessionLogin.SessionUsercode, true);
                    sqlper = "SELECT ude.departmentgroupid,de.departmentgroupcode, de.departmentgroupname,de.departmentgrouptype, ude.departmentid,de.departmentcode,de.departmentname,ude.departmenttype, ude.usercode FROM mrd_tbluser_departmentgroup ude inner join mrd_depatment de on ude.departmentid=de.departmentid WHERE usercode = '" + en_usercode + "' ORDER BY de.departmentgroupname,de.departmentname,ude.departmenttype;";
                }
                DataView dv = new DataView(condb.GetDataTable_HSBA(sqlper));
                if (dv.Count > 0)
                {
                    for (int i = 0; i < dv.Count; i++)
                    {
                        DTO.classUserDepartment itemUdepart = new DTO.classUserDepartment();
                        itemUdepart.departmentgroupid = Utilities.Util_TypeConvertParse.ToInt32(dv[i]["departmentgroupid"].ToString());
                        itemUdepart.departmentgroupcode = dv[i]["departmentgroupcode"].ToString();
                        itemUdepart.departmentgroupname = dv[i]["departmentgroupname"].ToString();
                        itemUdepart.departmentgrouptype = Utilities.Util_TypeConvertParse.ToInt32(dv[i]["departmentgrouptype"].ToString());
                        itemUdepart.departmentid = Utilities.Util_TypeConvertParse.ToInt32(dv[i]["departmentid"].ToString());
                        itemUdepart.departmentcode = dv[i]["departmentcode"].ToString();
                        itemUdepart.departmentname = dv[i]["departmentname"].ToString();
                        itemUdepart.departmenttype = Utilities.Util_TypeConvertParse.ToInt32(dv[i]["departmenttype"].ToString());
                        lstPhanQuyenKhoaPhong.Add(itemUdepart);
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error(ex);
            }
            return lstPhanQuyenKhoaPhong;
        }

        public static List<DTO.classUserMedicineStore> GetPhanQuyen_KhoThuoc()
        {
            List<DTO.classUserMedicineStore> lstPhanQuyen_KhoThuoc = new List<DTO.classUserMedicineStore>();
            try
            {
                string sqlper = "";
                if (SessionLogin.SessionUsercode == Base.KeyTrongPhanMem.AdminUser_key)
                {
                    sqlper = "SELECT ms.medicinestoreid, ms.medicinestorecode, ms.medicinestorename, ms.medicinestoretype, (case ms.medicinestoretype when 1 then 'Kho tổng' when 2 then 'Kho ngoại trú' when 3 then 'Kho nội trú' when 4 then 'Nhà thuốc' when 7 then 'Kho vật tư' end) as medicinestoretypename FROM medicine_store ms WHERE ms.medicinestoretype in (1,2,3,4,7) ORDER BY ms.medicinestoretype,ms.medicinestorename;";
                }
                else
                {
                    string en_usercode = O2S_MedicalRecord.Base.EncryptAndDecrypt.Encrypt(SessionLogin.SessionUsercode, true);
                    sqlper = "SELECT ms.medicinestoreid, ms.medicinestorecode, ms.medicinestorename, ms.medicinestoretype, (case ms.medicinestoretype when 1 then 'Kho tổng' when 2 then 'Kho ngoại trú' when 3 then 'Kho nội trú' when 4 then 'Nhà thuốc' when 7 then 'Kho vật tư' end) as medicinestoretypename FROM medicine_store ms INNER JOIN mrd_tbluser_medicinestore ttm on ms.medicinestoreid=ttm.medicinestoreid WHERE ttm.usercode = '" + en_usercode + "' ORDER BY ms.medicinestoretype,ms.medicinestorename;";
                }

                DataView dataKhoThuoc = new DataView(condb.GetDataTable_HIS(sqlper));
                if (dataKhoThuoc.Count > 0)
                {
                    for (int i = 0; i < dataKhoThuoc.Count; i++)
                    {
                        DTO.classUserMedicineStore userMedicineStore = new DTO.classUserMedicineStore();
                        userMedicineStore.MedicineStoreCheck = false;
                        userMedicineStore.MedicineStoreId = Utilities.Util_TypeConvertParse.ToInt32(dataKhoThuoc[i]["medicinestoreid"].ToString());
                        userMedicineStore.MedicineStoreCode = dataKhoThuoc[i]["medicinestorecode"].ToString();
                        userMedicineStore.MedicineStoreName = dataKhoThuoc[i]["medicinestorename"].ToString();
                        userMedicineStore.MedicineStoreType = Utilities.Util_TypeConvertParse.ToInt32(dataKhoThuoc[i]["medicinestoretype"].ToString());
                        userMedicineStore.MedicineStoreTypeName = dataKhoThuoc[i]["medicinestoretypename"].ToString();

                        lstPhanQuyen_KhoThuoc.Add(userMedicineStore);
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error(ex);
            }
            return lstPhanQuyen_KhoThuoc;
        }

        public static List<DTO.classUserMedicinePhongLuu> GetPhanQuyen_PhongLuu()
        {
            List<DTO.classUserMedicinePhongLuu> lstPhanQuyen_PhongLuu = new List<DTO.classUserMedicinePhongLuu>();
            try
            {
                string sqlper = "";
                if (SessionLogin.SessionUsercode == Base.KeyTrongPhanMem.AdminUser_key)
                {
                    sqlper = "SELECT pl.medicinephongluuid, pl.medicinephongluucode, (ms.medicinestorename || '-' ||pl.medicinephongluuname) as medicinephongluuname, ms.medicinestoreid, ms.medicinestorecode, ms.medicinestorename from medicinephongluu pl inner join medicine_store ms on pl.medicinestoreid=ms.medicinestoreid where pl.medicinephongluucode<>'' and pl.medicinephongluuname<>'' order by ms.medicinestorename, pl.medicinephongluuname;";
                }
                else
                {
                    string en_usercode = O2S_MedicalRecord.Base.EncryptAndDecrypt.Encrypt(SessionLogin.SessionUsercode, true);
                    sqlper = "SELECT pl.medicinephongluuid, pl.medicinephongluucode, (ms.medicinestorename || '-' ||pl.medicinephongluuname) as medicinephongluuname, ms.medicinestoreid, ms.medicinestorecode, ms.medicinestorename FROM medicinephongluu pl INNER JOIN mrd_tbluser_medicinephongluu ttm on pl.medicinephongluuid=ttm.medicinephongluuid inner join medicine_store ms on pl.medicinestoreid=ms.medicinestoreid WHERE ttm.usercode = '" + en_usercode + "' ORDER BY ms.medicinestorename, pl.medicinephongluuname;";
                }

                DataView dataPhongluu = new DataView(condb.GetDataTable_HIS(sqlper));
                if (dataPhongluu.Count > 0)
                {
                    for (int i = 0; i < dataPhongluu.Count; i++)
                    {
                        DTO.classUserMedicinePhongLuu userMedicineStore = new DTO.classUserMedicinePhongLuu();
                        userMedicineStore.MedicinePhongLuuCheck = false;
                        userMedicineStore.MedicinePhongLuuId = Utilities.Util_TypeConvertParse.ToInt32(dataPhongluu[i]["medicinephongluuid"].ToString());
                        userMedicineStore.MedicinePhongLuuCode = dataPhongluu[i]["medicinephongluucode"].ToString();
                        userMedicineStore.MedicinePhongLuuName = dataPhongluu[i]["medicinephongluuname"].ToString();
                        userMedicineStore.MedicineStoreId = Utilities.Util_TypeConvertParse.ToInt32(dataPhongluu[i]["medicinestoreid"].ToString());
                        userMedicineStore.MedicineStoreCode = dataPhongluu[i]["medicinestorecode"].ToString();
                        userMedicineStore.MedicineStoreName = dataPhongluu[i]["medicinestorename"].ToString();

                        lstPhanQuyen_PhongLuu.Add(userMedicineStore);
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error(ex);
            }
            return lstPhanQuyen_PhongLuu;
        }

    }
}
