---table 
CREATE TABLE IF NOT EXISTS mrd_license
(
  licenseid serial NOT NULL,
  datakey text,
  licensekey text,
  CONSTRAINT mrd_license_pkey PRIMARY KEY (licenseid)
)

CREATE INDEX mrd_license_licenseid_idx
  ON mrd_license
  USING btree
  (licenseid);
----------
CREATE TABLE IF NOT EXISTS mrd_option
(
  toolsoptionid serial NOT NULL,
  toolsoptioncode text,
  toolsoptionname text,
  toolsoptionvalue text,
  toolsoptionnote text,
  toolsoptionlook integer,
  toolsoptiondate timestamp without time zone,
  toolsoptioncreateuser text,
  CONSTRAINT mrd_option_pkey PRIMARY KEY (toolsoptionid)
)

CREATE INDEX mrd_option_toolsoptionid_idx
  ON mrd_option
  USING btree
  (toolsoptionid);
---------
CREATE TABLE IF NOT EXISTS mrd_tbllog
(
  logid serial NOT NULL,
  loguser text,
  logvalue text,
  ipaddress text,
  computername text,
  softversion text,
  logtime timestamp without time zone,
  CONSTRAINT mrd_tbllog_pkey PRIMARY KEY (logid)
)

CREATE TABLE IF NOT EXISTS mrd_tbluser
(
  userid serial NOT NULL,
  usercode text NOT NULL,
  username text,
  userpassword text,
  userstatus integer,
  usergnhom integer,
  usernote text,
  userhisid integer,
  CONSTRAINT mrd_tbluser_pkey PRIMARY KEY (userid)
)

CREATE INDEX mrd_tbluser_usercode_idx
  ON mrd_tbluser
  USING btree
  (usercode);
---------
CREATE TABLE IF NOT EXISTS mrd_tbluser_departmentgroup
(
  userdepgid serial NOT NULL,
  departmentgroupid integer,
  departmentid integer,
  departmenttype integer,
  usercode text,
  userdepgidnote text,
  CONSTRAINT tbluser_departmentgroup_pkey PRIMARY KEY (userdepgid)
)

CREATE INDEX mrd_tbluserdep_userdepgid_idx
  ON mrd_tbluser_departmentgroup
  USING btree
  (userdepgid);
CREATE INDEX mrd_tbluserdep_usercode_idx
  ON mrd_tbluser_departmentgroup
  USING btree
  (usercode);
---------
CREATE TABLE IF NOT EXISTS mrd_tbluser_medicinephongluu
(
  userphongluutid serial NOT NULL,
  medicinephongluuid integer,
  medicinestoreid integer,
  usercode text,
  userdepgidnote text,
  CONSTRAINT mrd_tbluser_medicinephongluu_pkey PRIMARY KEY (userphongluutid)
)

CREATE INDEX mrd_tbluserphongluu_medicinephongluuid_idx
  ON mrd_tbluser_medicinephongluu
  USING btree
  (medicinephongluuid);
CREATE INDEX mrd_tbluserphongluu_usercode_idx
  ON mrd_tbluser_medicinephongluu
  USING btree
  (usercode);  
----------
CREATE TABLE IF NOT EXISTS mrd_tbluser_medicinestore
(
  usermestid serial NOT NULL,
  medicinestoreid integer,
  medicinestoretype integer,
  usercode text,
  userdepgidnote text,
  CONSTRAINT mrd_tbluser_medicinestore_pkey PRIMARY KEY (usermestid)
)

CREATE INDEX mrd_tblusermedi_medicinestoreid_idx
  ON mrd_tbluser_medicinestore
  USING btree
  (medicinestoreid);  
CREATE INDEX mrd_tblusermedi_usercode_idx
  ON mrd_tbluser_medicinestore
  USING btree
  (usercode);  
----------
CREATE TABLE IF NOT EXISTS mrd_tbluser_permission
(
  userpermissionid serial NOT NULL,
  permissionid integer,
  permissioncode text,
  permissionname text,
  userid integer,
  usercode text,
  permissioncheck boolean,
  userpermissionnote text,
  CONSTRAINT userpermissionid_pkey PRIMARY KEY (userpermissionid)
)

CREATE INDEX mrd_tbluserper_usercode_idx
  ON mrd_tbluser_permission
  USING btree
  (usercode);  
  CREATE INDEX mrd_tbluserper_percode_idx
  ON mrd_tbluser_permission
  USING btree
  (permissioncode);  
---------
CREATE TABLE IF NOT EXISTS mrd_version
(
  versionid serial NOT NULL,
  appversion text,
  app_link text,
  app_type integer,
  updateapp bytea,
  appsize integer,
  sqlversion text,
  updatesql bytea,
  sqlsize integer,
  sync_flag integer,
  update_flag integer,
  CONSTRAINT mrd_version_pkey PRIMARY KEY (versionid)
)

-------
CREATE TABLE IF NOT EXISTS mrd_depatment
(
  mrd_depatmentid serial NOT NULL,
  departmentgroupid integer,
  departmentgroupcode text,
  departmentgroupname text,
  departmentgrouptype integer,
  departmentid integer,
  departmentcode text,
  departmentname text,
  departmenttype integer,
  CONSTRAINT mrd_depatment_pkey PRIMARY KEY (mrd_depatmentid)
)
  CREATE INDEX mrd_depatment_groupid_idx
  ON mrd_depatment
  USING btree
  (departmentgroupid);  
  CREATE INDEX mrd_depatment_depid_idx
  ON mrd_depatment
  USING btree
  (departmentid);  
------
CREATE TABLE IF NOT EXISTS mrd_serviceref
(
  mrd_servicerefid serial NOT NULL,
  his_servicepricerefid integer,
  servicegrouptype integer,
  bhyt_groupcode text,
  servicepricegroupcode text,
  servicepricecode text,
  servicepricename text,
  servicepricenamenhandan text,
  servicepricenamebhyt text,
  servicepricenamenuocngoai text,
  servicepriceunit text,
  servicepricefee text,
  servicepricefeenhandan text,
  servicepricefeebhyt text,
  servicepricefeenuocngoai text,
  servicelock integer DEFAULT 0,
  servicepricecodeuser text,
  servicepricesttuser text,
  pttt_hangid integer DEFAULT 0,
  pttt_loaiid integer DEFAULT 0,
  mrd_pttttemid integer,
  mrd_templatename text,
  CONSTRAINT mrd_serviceref_pkey PRIMARY KEY (mrd_servicerefid)
)
  CREATE INDEX mrd_serviceref_serrefid_idx
  ON mrd_serviceref
  USING btree
  (his_servicepricerefid);  
  CREATE INDEX mrd_serviceref_serrecode_idx
  ON mrd_serviceref
  USING btree
  (servicepricecode);  
  CREATE INDEX mrd_serviceref_bhytcode_idx
  ON mrd_serviceref
  USING btree
  (bhyt_groupcode);  
  
---====== Phau thuat thu thuat
--lưu trữ mẫu phiếu template PTTT
CREATE TABLE IF NOT EXISTS mrd_pttt_template
(
  mrd_pttttemid serial NOT NULL,
  mrd_pttttemcode text,
  mrd_pttttemname text,
  mrd_servicerefid integer,
  his_servicepricerefid integer,
  his_servicepricecode text,
  mrd_pttttemnamepath text,
  mrd_pttttemdata bytea,
  CONSTRAINT mrd_pttttemid_pkey PRIMARY KEY (mrd_pttttemid)
)

  CREATE INDEX mrd_pttttemid_idx
  ON mrd_pttt_template
  USING btree
  (mrd_pttttemid); 
  CREATE INDEX his_servicepricecode_idx
  ON mrd_pttt_template
  USING btree
  (his_servicepricecode); 
  
--------Lưu lại PTTT mẫu (lưu nội dung)
CREATE TABLE IF NOT EXISTS mrd_pttt_templatemau
(
  mrd_pttttemmauid serial NOT NULL,
  mrd_pttttemmaucode text,
  mrd_pttttemmauname text,
  mrd_servicerefid integer,
  his_servicepricerefid integer,
  his_servicepricecode text,
  mrd_pttttemid integer,
  mrd_pttttemnamepath text,
  mrd_pttttemmaudata_nd bytea,
  CONSTRAINT mrd_pttttemmauid_pkey PRIMARY KEY (mrd_pttttemmauid)
)

  CREATE INDEX mrd_pttttemidmau_idx
  ON mrd_pttt_templatemau
  USING btree
  (mrd_pttttemid); 
  CREATE INDEX his_servicepricecodemau_idx
  ON mrd_pttt_templatemau
  USING btree
  (his_servicepricecode); 

--------PTTT theo dịch vụ

CREATE TABLE IF NOT EXISTS mrd_pttt_service
(
  mrd_pttt_serviceid serial NOT NULL,
  servicepriceid integer,
  servicepricecode text,
  maubenhphamid integer,
  patientid integer,
  vienphiid integer,
  hosobenhanid integer,
  medicalrecordid integer,
  mrd_pttttemid integer,
  departmentgroupid integer,
  departmentid integer,
  mrd_pttt_servicedata bytea,
  mrd_pttt_servicedata_nd bytea,
  mrd_pttt_servicestatus integer, --0=nhap; 1=da luu; 2=da in
  create_mrduserid integer,
  create_mrdusercode text,
  create_hisuserid integer,
  create_hisusercode text,
  create_date timestamp without time zone,
  modify_mrduserid integer,
  modify_mrdusercode text,
  modify_date timestamp without time zone,
  note text,
  CONSTRAINT mrd_pttt_serviceid_pkey PRIMARY KEY (mrd_pttt_serviceid)
)

  CREATE INDEX mrd_pttt_serviceid_idx
  ON mrd_pttt_service
  USING btree
  (mrd_pttt_serviceid); 
  CREATE INDEX mrd_pttt_servicetemid_idx
  ON mrd_pttt_service
  USING btree
  (mrd_pttttemid); 
  CREATE INDEX mrd_pttt_servicepriceid_idx
  ON mrd_pttt_service
  USING btree
  (servicepriceid); 
  CREATE INDEX mrd_pttt_servicepricecode_idx
  ON mrd_pttt_service
  USING btree
  (servicepricecode);  
  CREATE INDEX mrd_pttt_servicevienphiid_idx
  ON mrd_pttt_service
  USING btree
  (vienphiid); 
  CREATE INDEX mrd_pttt_servicepatientid_idx
  ON mrd_pttt_service
  USING btree
  (patientid); 
  CREATE INDEX mrd_pttt_servicehosobenhanid_idx
  ON mrd_pttt_service
  USING btree
  (hosobenhanid); 
  CREATE INDEX mrd_pttt_servicemedicalrecordid_idx
  ON mrd_pttt_service
  USING btree
  (medicalrecordid); 
  CREATE INDEX mrd_pttt_servicemaubenhphamid_idx
  ON mrd_pttt_service
  USING btree
  (maubenhphamid);  
  
----========Phiếu Hồ sơ bệnh án
--lưu trữ mẫu phiếu template HSBA
CREATE TABLE IF NOT EXISTS mrd_hsba_template
(
  mrd_hsbatemid serial NOT NULL,
  mrd_hsbatemcode text,
  mrd_hsbatemname text,
  mrd_hsbatemtypeid integer,
  mrd_hsbatemnamepath text,
  mrd_hsbatemdata bytea,
  CONSTRAINT mrd_hsbatemid_pkey PRIMARY KEY (mrd_hsbatemid)
)

  CREATE INDEX mrd_hsbatemid_idx
  ON mrd_hsba_template
  USING btree
  (mrd_hsbatemid); 
  
--------Lưu lại HSBA mẫu (lưu nội dung)
CREATE TABLE IF NOT EXISTS mrd_hsba_templatemau
(
  mrd_hsbatemmauid serial NOT NULL,
  mrd_hsbatemmaucode text,
  mrd_hsbatemmauname text,
  mrd_hsbatemid integer,
  mrd_hsbatemnamepath text,
  mrd_hsbatemmaudata_nd bytea,
  CONSTRAINT mrd_hsbatemmauid_pkey PRIMARY KEY (mrd_hsbatemmauid)
)

  CREATE INDEX mrd_hsbatemidmau_idx
  ON mrd_hsba_templatemau
  USING btree
  (mrd_hsbatemid); 

--------HSBA theo dịch vụ

CREATE TABLE IF NOT EXISTS mrd_hsba_hosobenhan
(
  mrd_hsba_hosobenhanid serial NOT NULL,
  hosobenhanid integer,
  medicalrecordid integer,
  patientid integer,
  vienphiid integer,
  mrd_hsbatemid integer,
  departmentgroupid integer,
  departmentid integer,
  mrd_hsba_hosobenhandata text,
  mrd_hsba_hosobenhandata_nd text,
  mrd_hsba_hosobenhanstatus integer,
  create_mrduserid integer,
  create_mrdusercode text,
  create_hisuserid integer,
  create_hisusercode text,
  create_date timestamp without time zone,
  modify_mrduserid integer,
  modify_mrdusercode text,
  modify_date timestamp without time zone,
  note text,
  CONSTRAINT mrd_hsba_hsbaanid_pkey PRIMARY KEY (mrd_hsba_hosobenhanid)
)

  CREATE INDEX mrd_hsba_hsbaid_idx
  ON mrd_hsba_hosobenhan
  USING btree
  (mrd_hsba_hosobenhanid); 
  CREATE INDEX mrd_hsba_hsbatemid_idx
  ON mrd_hsba_hosobenhan
  USING btree
  (mrd_hsbatemid);  
  CREATE INDEX mrd_hsba_hsbavienphiid_idx
  ON mrd_hsba_hosobenhan
  USING btree
  (vienphiid); 
  CREATE INDEX mrd_hsba_hsbapatientid_idx
  ON mrd_hsba_hosobenhan
  USING btree
  (patientid); 
  CREATE INDEX mrd_hsba_hsbahosobenhanid_idx
  ON mrd_hsba_hosobenhan
  USING btree
  (hosobenhanid); 
  CREATE INDEX mrd_hsba_hsbamedicalrecordid_idx
  ON mrd_pttt_service
  USING btree
  (medicalrecordid);  
  
  
 ----Danh muc dung chung

CREATE TABLE IF NOT EXISTS mrd_othertypelist
(
  mrd_othertypelistid serial NOT NULL,
  mrd_othertypelistcode text,
  mrd_othertypelistname text,
  mrd_othertypeliststatus integer,
  CONSTRAINT mrd_othertypelist_pkey PRIMARY KEY (mrd_othertypelistid)
)

  CREATE INDEX mrd_othertypelistid_idx
  ON mrd_othertypelist
  USING btree
  (mrd_othertypelistid);  
 -----danh muc 
CREATE TABLE IF NOT EXISTS mrd_otherlist
(
  mrd_otherlistid serial NOT NULL,
  mrd_othertypelistid integer,
  mrd_otherlistcode text,
  mrd_otherlistname text,
  mrd_otherlistvalue text,
  mrd_otherliststatus integer,
  CONSTRAINT mrd_otherlist_pkey PRIMARY KEY (mrd_otherlistid)
)

  CREATE INDEX mrd_otherlistid_idx
  ON mrd_otherlist
  USING btree
  (mrd_otherlistid);    
  
  CREATE INDEX mrd_otothertypelistid_idx
  ON mrd_otherlist
  USING btree
  (mrd_othertypelistid); 

----=================================================Danh mục Hội chẩn thuốc *

CREATE TABLE IF NOT EXISTS mrd_dmhc_thuoc
(
  mrd_dmhc_thuocid serial NOT NULL,
  medicinerefid integer,
  medicinecode text,
  medicinegroupcode text,
  medicinereftype integer,
  medicinetype integer,
  medicinename text,
  tenkhoahoc text,
  donvitinh text,
  nongdo text,
  lieuluong text,
  hoatchat text,
  gianhap double precision DEFAULT 0,
  giaban double precision DEFAULT 0,
  vatnhap double precision DEFAULT 0,
  vatban double precision DEFAULT 0,
  bhyt_groupcode text,
  servicepricefee double precision DEFAULT 0,
  servicepricefeenhandan double precision DEFAULT 0,
  servicepricefeebhyt double precision DEFAULT 0,
  servicepricefeenuocngoai double precision DEFAULT 0,
  mahoatchat text,
  medicinecodeuser text,
  stt_dauthau text,
  medicinename_byt text,
  stt_thuoc_tt40 integer,
  stt_thuoc_tt40text text,
  mrd_dmhc_thuocnamepath text,
  CONSTRAINT mrd_dmhc_thuoc_pkey PRIMARY KEY (mrd_dmhc_thuocid)
)

  CREATE INDEX mrd_dmhc_thuocid_idx
  ON mrd_dmhc_thuoc
  USING btree
  (mrd_dmhc_thuocid);    
  
  CREATE INDEX medicinerefid_idx
  ON mrd_dmhc_thuoc
  USING btree
  (medicinerefid); 
  
  CREATE INDEX medicinecode_idx
  ON mrd_dmhc_thuoc
  USING btree
  (medicinecode);   
  
  
 --=======Luu lai Hoi chan thuoc *
 
 CREATE TABLE IF NOT EXISTS mrd_hsba_hcthuoc
(
  mrd_hsba_hcthuocid serial NOT NULL,	
  mrd_dmhc_thuocid integer,
  maubenhphamid integer,
  medicinerefid_org integer,
  servicepriceid integer,
  servicepricecode text,
  servicepricename text,
  servicepricedate text,
  hosobenhanid integer,
  medicalrecordid integer,
  patientid integer,
  vienphiid integer,
  departmentgroupid integer,
  departmentid integer,
  thoigianhoichan timestamp without time zone,
  yeucauhoichanid integer,
  yeucauhoichanname text,
  diadiemhoichan text,
  dbb_tomtattiensubenh text,
  dbb_tinhtranglucvaovien text,
  dbb_chandoantuyenduoi text,
  dbb_tomtatdienbienbenh text,
  yk_chandoantienluong text,
  yk_phuongphapdieutri text,
  yk_chamsoc text,
  kl_ketluan text,
  tvtg_chutoa_code text,
  tvtg_chutoa_ten text,
  tvtg_chutoa_cdcv text,
  tvtg_thuky_code text,
  tvtg_thuky_ten text,
  tvtg_thuky_cdcv text,
  tvtg_thanhvien1_code text,
  tvtg_thanhvien1_ten text,
  tvtg_thanhvien1_cdcv text,
  tvtg_thanhvien2_code text,
  tvtg_thanhvien2_ten text,
  tvtg_thanhvien2_cdcv text,
  tvtg_thanhvien3_code text,
  tvtg_thanhvien3_ten text,
  tvtg_thanhvien3_cdcv text,
  tvtg_thanhvien4_code text,
  tvtg_thanhvien4_ten text,
  tvtg_thanhvien4_cdcv text,
  tvtg_thanhvien5_code text,
  tvtg_thanhvien5_ten text,
  tvtg_thanhvien5_cdcv text,
  tvtg_thanhvien6_code text,
  tvtg_thanhvien6_ten text,
  tvtg_thanhvien6_cdcv text,
  mrd_hsba_hcthuocstatus integer,
  create_mrduserid integer,
  create_mrdusercode text,
  create_date timestamp without time zone,
  modify_mrduserid integer,
  modify_mrdusercode text,
  modify_date timestamp without time zone,
  note text,
  CONSTRAINT mrd_hsba_hcthuocid_pkey PRIMARY KEY (mrd_hsba_hcthuocid)
)

  CREATE INDEX mrd_hsba_hcthuocid_idx
  ON mrd_hsba_hcthuoc
  USING btree
  (mrd_hsba_hcthuocid);    
  
  CREATE INDEX hsba_hcthuoc_servicepricecode_idx
  ON mrd_hsba_hcthuoc
  USING btree
  (servicepricecode); 
  
  CREATE INDEX hsba_hcthuoc_hosobenhanid_idx
  ON mrd_hsba_hcthuoc
  USING btree
  (hosobenhanid); 
 
  CREATE INDEX hsba_hcthuoc_vienphiid_idx
  ON mrd_hsba_hcthuoc
  USING btree
  (vienphiid); 
 
  CREATE INDEX hsba_hcthuoc_patientid_idx
  ON mrd_hsba_hcthuoc
  USING btree
  (patientid); 
  
  CREATE INDEX hsba_hcthuoc_dmhc_thuocid_idx
  ON mrd_hsba_hcthuoc
  USING btree
  (mrd_dmhc_thuocid);  
  
 
----================================Danh mục Hội chẩn PTTTT

CREATE TABLE IF NOT EXISTS mrd_dmhc_pttt
(
  mrd_dmhc_ptttid serial NOT NULL,
  servicepricerefid integer,
  servicepricecode text,
  servicepricegroupcode text,
  servicegrouptype integer,
  servicepricetype integer,
  servicepricename text,
  servicepricenamebhyt text,
  servicepricenamenuocngoai text,
  servicepriceunit text,
  servicepricefee text,
  servicepricefeenhandan text,
  servicepricefeebhyt text,
  servicepricefeenuocngoai text,
  bhyt_groupcode text,
  pttt_hangid integer DEFAULT 0,
  pttt_loaiid integer DEFAULT 0,
  servicepricecodeuser text,
  servicepricesttuser text,
  mrd_dmhc_ptttnamepath text,
  CONSTRAINT mrd_dmhc_pttt_pkey PRIMARY KEY (mrd_dmhc_ptttid)
)

  CREATE INDEX mrd_dmhc_ptttid_idx
  ON mrd_dmhc_pttt
  USING btree
  (mrd_dmhc_ptttid);    
  
  CREATE INDEX mrd_dmhc_servicepricerefid_idx
  ON mrd_dmhc_pttt
  USING btree
  (servicepricerefid); 
  
  CREATE INDEX mrd_dmhc_servicepricecode_idx
  ON mrd_dmhc_pttt
  USING btree
  (servicepricecode);     
  
 
 ----Luu lai Hoi chan Phau thuat thu thuat
 
 CREATE TABLE IF NOT EXISTS mrd_hsba_hcpttt
(
  mrd_hsba_hcptttid serial NOT NULL,	
  mrd_dmhc_ptttid integer,
  maubenhphamid integer,
  servicepriceid integer,
  servicepricecode text,
  servicepricename text,
  servicepricedate text,
  hosobenhanid integer,
  medicalrecordid integer,
  patientid integer,
  vienphiid integer,
  departmentgroupid integer,
  departmentid integer,
  thoigianhoichan timestamp without time zone,
  yeucauhoichanid integer,
  yeucauhoichanname text,
  diadiemhoichan text,
  dbb_tomtattiensubenh text,
  dbb_tinhtranglucvaovien text,
  dbb_chandoantuyenduoi text,
  dbb_tomtatdienbienbenh text,
  yk_chandoantienluong text,
  yk_phuongphapdieutri text,
  yk_chamsoc text,
  kl_ketluan text,
  tvtg_chutoa_code text,
  tvtg_chutoa_ten text,
  tvtg_chutoa_cdcv text,
  tvtg_thuky_code text,
  tvtg_thuky_ten text,
  tvtg_thuky_cdcv text,
  tvtg_thanhvien1_code text,
  tvtg_thanhvien1_ten text,
  tvtg_thanhvien1_cdcv text,
  tvtg_thanhvien2_code text,
  tvtg_thanhvien2_ten text,
  tvtg_thanhvien2_cdcv text,
  tvtg_thanhvien3_code text,
  tvtg_thanhvien3_ten text,
  tvtg_thanhvien3_cdcv text,
  tvtg_thanhvien4_code text,
  tvtg_thanhvien4_ten text,
  tvtg_thanhvien4_cdcv text,
  tvtg_thanhvien5_code text,
  tvtg_thanhvien5_ten text,
  tvtg_thanhvien5_cdcv text,
  tvtg_thanhvien6_code text,
  tvtg_thanhvien6_ten text,
  tvtg_thanhvien6_cdcv text,
  mrd_hsba_hcptttstatus integer,
  create_mrduserid integer,
  create_mrdusercode text,
  create_date timestamp without time zone,
  modify_mrduserid integer,
  modify_mrdusercode text,
  modify_date timestamp without time zone,
  note text,
  CONSTRAINT mrd_hsba_hcptttid_pkey PRIMARY KEY (mrd_hsba_hcptttid)
)

  CREATE INDEX mrd_hsba_hcptttid_idx
  ON mrd_hsba_hcpttt
  USING btree
  (mrd_hsba_hcptttid);    
  
  CREATE INDEX hsba_hcpttt_servicepricecode_idx
  ON mrd_hsba_hcpttt
  USING btree
  (servicepricecode); 
  
  CREATE INDEX hsba_hcpttt_hosobenhanid_idx
  ON mrd_hsba_hcpttt
  USING btree
  (hosobenhanid); 
 
  CREATE INDEX hsba_hcpttt_vienphiid_idx
  ON mrd_hsba_hcpttt
  USING btree
  (vienphiid); 
 
  CREATE INDEX hsba_hcpttt_patientid_idx
  ON mrd_hsba_hcpttt
  USING btree
  (patientid); 
  
  CREATE INDEX hsba_hcpttt_dmhc_ptttid_idx
  ON mrd_hsba_hcpttt
  USING btree
  (mrd_dmhc_ptttid);  


 
--=======Luu lai Hoi chuyen vien
 
 CREATE TABLE IF NOT EXISTS mrd_hsba_hccvien
(
  mrd_hsba_hccvienid serial NOT NULL,	
  mrd_dmhc_cvienid integer,
  his_chuyenvienid integer,
  hosobenhanid integer,
  medicalrecordid integer,
  patientid integer,
  vienphiid integer,
  departmentgroupid integer,
  departmentid integer,
  thoigianhoichan timestamp without time zone,
  yeucauhoichanid integer,
  yeucauhoichanname text,
  diadiemhoichan text,
  dbb_tomtattiensubenh text,
  dbb_tinhtranglucvaovien text,
  dbb_chandoantuyenduoi text,
  dbb_tomtatdienbienbenh text,
  yk_chandoantienluong text,
  yk_phuongphapdieutri text,
  yk_chamsoc text,
  kl_ketluan text,
  tvtg_chutoa_code text,
  tvtg_chutoa_ten text,
  tvtg_chutoa_cdcv text,
  tvtg_thuky_code text,
  tvtg_thuky_ten text,
  tvtg_thuky_cdcv text,
  tvtg_thanhvien1_code text,
  tvtg_thanhvien1_ten text,
  tvtg_thanhvien1_cdcv text,
  tvtg_thanhvien2_code text,
  tvtg_thanhvien2_ten text,
  tvtg_thanhvien2_cdcv text,
  tvtg_thanhvien3_code text,
  tvtg_thanhvien3_ten text,
  tvtg_thanhvien3_cdcv text,
  tvtg_thanhvien4_code text,
  tvtg_thanhvien4_ten text,
  tvtg_thanhvien4_cdcv text,
  tvtg_thanhvien5_code text,
  tvtg_thanhvien5_ten text,
  tvtg_thanhvien5_cdcv text,
  tvtg_thanhvien6_code text,
  tvtg_thanhvien6_ten text,
  tvtg_thanhvien6_cdcv text,
  mrd_hsba_hccvienstatus integer,
  create_mrduserid integer,
  create_mrdusercode text,
  create_date timestamp without time zone,
  modify_mrduserid integer,
  modify_mrdusercode text,
  modify_date timestamp without time zone,
  note text,
  CONSTRAINT mrd_hsba_hccvienid_pkey PRIMARY KEY (mrd_hsba_hccvienid)
)

  CREATE INDEX mrd_hsba_hccvienid_idx
  ON mrd_hsba_hccvien
  USING btree
  (mrd_hsba_hccvienid);    
  
  CREATE INDEX hsba_hccvien_hosobenhanid_idx
  ON mrd_hsba_hccvien
  USING btree
  (hosobenhanid); 
 
  CREATE INDEX hsba_hccvien_vienphiid_idx
  ON mrd_hsba_hccvien
  USING btree
  (vienphiid); 
 
  CREATE INDEX hsba_hccvien_patientid_idx
  ON mrd_hsba_hccvien
  USING btree
  (patientid); 
  
  CREATE INDEX hsba_hccvien_dmhc_cvienid_idx
  ON mrd_hsba_hccvien
  USING btree
  (mrd_dmhc_cvienid); 

  






  
    
  
  
  