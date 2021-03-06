--ngay 29/5

SELECT 
(select giamdocname from hospital limit 1) as GIAMDOCBV, 
'" + Base.SessionLogin.SessionUsername + "' as TENUSER, 
'" + datetimenow + "' as CURRENT_FULLTIME, 
(select soytename from hospital limit 1) as SOYTE, 
(select hospitalname from hospital limit 1) as TENBENHVIEN, 
(select departmentgroupname from departmentgroup where departmentgroupid="+this.mrdHsbaHosobenhan.departmentgroupid+") as TENKHOA, 
'' as GIUONG, 
hsba.soluutru as SOLUUTRU, 
hsba.patientname as PATIENTNAME, 
substr(to_char(hsba.birthday, 
'dd'),1,1) as NS_1, 
substr(to_char(hsba.birthday, 
'dd'),2,1) as NS_2, 
substr(to_char(hsba.birthday, 
'mm'),1,1) as NS_3, 
substr(to_char(hsba.birthday, 
'mm'),2,1) as NS_4, 
substr(to_char(hsba.birthday, 
'yyyy'),1,1) as NS_5, 
substr(to_char(hsba.birthday, 
'yyyy'),2,1) as NS_6, 
substr(to_char(hsba.birthday, 
'yyyy'),3,1) as NS_7, 
substr(to_char(hsba.birthday, 
'yyyy'),4,1) as NS_8, 
(case when (cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as integer) <10) then '0' else (substr(cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 
'yyyy') as integer)) as text),1,1)) end) as TUOI_1, 
(case when (cast((cast(to_char(hsba.hosobenhandate, 
'yyyy') as integer) - cast(to_char(hsba.birthday, 
'yyyy') as integer)) as integer) <10) then (cast((cast(to_char(hsba.hosobenhandate, 
'yyyy') as integer) - cast(to_char(hsba.birthday, 
'yyyy') as integer)) as text)) else (substr(cast((cast(to_char(hsba.hosobenhandate, 
'yyyy') as integer) - cast(to_char(hsba.birthday, 
'yyyy') as integer)) as text),2,1)) end) as TUOI_2, 
(case when hsba.gioitinhcode='01' then 'X' end) as GT_NAM, 
(case when hsba.gioitinhcode='02' then 'X' end) as GT_NU, 
hsba.nghenghiepname as NNGHIEP, 
substr(hsba.nghenghiepcode,1,1) as NNGHIEP_1, 
substr(hsba.nghenghiepcode,2,1) as NNGHIEP_2, 
hsba.hc_dantocname as DANTOC, 
substr(hsba.hc_dantoccode,1,1) as DANTOC_1, 
substr(hsba.hc_dantoccode,2,1) as DANTOC_2, 
hsba.hc_quocgianame as NKIEU, 
substr(hsba.hc_quocgiacode,1,1) as NKIEU_1, 
substr(hsba.hc_quocgiacode,2,1) as NKIEU_2, 
hsba.hc_sonha as SONHA, 
hsba.hc_thon as THON, 
(case when hsba.hc_xacode<>'00' then hsba.hc_xaname end) as XA, 
(case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname end) as HUYEN, 
substr(hsba.hc_huyencode,1,1) as HUYEN_1, 
substr(hsba.hc_huyencode,2,1) as HUYEN_2, 
(case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname end) as TINH, 
substr(hsba.hc_tinhcode,1,1) as TINH_1, 
substr(hsba.hc_tinhcode,2,1) as TINH_2, hsba.noilamviec as NOILAMVIEC, 
(case vp.doituongbenhnhanid when 1 then 'X' end) as DT_BHYT, 
(case when vp.doituongbenhnhanid in (2,3) then 'X' end) as DT_VP, 
(case vp.doituongbenhnhanid when 5 then 'X' end) as DT_MP, 
(case when vp.doituongbenhnhanid >6 then 'X' end) as DT_KH, 
(case when bh.bhytcode<>'' then to_char(bh.bhytutildate, 'ngà\"y\" dd tháng mm năm yyyy') end) as BHDEN_FULLTIME, 
substr(bh.bhytcode,1,2) as BHYT_1, 
substr(bh.bhytcode,3,1) as BHYT_2, 
substr(bh.bhytcode,4,2) as BHYT_3, 
substr(bh.bhytcode,6,2) as BHYT_4, 
substr(bh.bhytcode,8,8) as BHYT_5, 
hsba.nguoithan_name || ' ' || hsba.nguoithan_address as NNHA_TEN, 
hsba.nguoithan_phone as NNHA_SDT, 
(select to_char(thoigianvaovien, 'hh g\"i\"ờ mi phút ngà\"y\" dd/mm/yyyy') from medicalrecord where loaibenhanid=1 and hosobenhanid=" + this.mrdHsbaHosobenhan.hosobenhanid + " order by medicalrecordid limit 1) as VVIEN_FULLTIME,
'' as TTIEP_CC, 
'' as TTIEP_KKB, 
'' as TTIEP_KDT, 
'' as NGT_CQYT, 
'X' as NGT_TD, 
'' as NGT_KH, 
'' as VVLANTHU, 
(select degp.departmentgroupcode from medicalrecord mrd inner join departmentgroup degp on degp.departmentgroupid=mrd.departmentgroupid where mrd.loaibenhanid=1 and mrd.hosobenhanid=" + this.mrdHsbaHosobenhan.hosobenhanid + " order by mrd.medicalrecordid limit 1) as VKHOA_MA, 
(select to_char(thoigianvaovien, 'hh g\"i\"ờ mi phút  dd/mm/yyyy') from medicalrecord where loaibenhanid=1 and hosobenhanid=" + this.mrdHsbaHosobenhan.hosobenhanid + " order by medicalrecordid limit 1) as VKHOA_FULLTIME, 
'' as SNDT_1, 
'' as SNDT_2, 
'' as CVIEN_TT, 
'' as CVIEN_TD, 
'' as CVIEN_CK, 
'' as CVIEN_CDEN, 
(case when hsba.hosobenhanstatus=1 then to_char(hsba.hosobenhandate_ravien, 'hh g\"i\"ờ mi phút ngà\"y\" dd/mm/yyyy')
	else '....giờ.... phút ngày ..../..../.....' end) as RVIEN_FULLTIME, 
(case hsba.hinhthucravienid when 1 then 'X' end) as RVIEN_RV, 
(case hsba.hinhthucravienid when 2 then 'X' end) as RVIEN_XV, 
(case hsba.hinhthucravienid when 3 then 'X' end) as RVIEN_BV, 
(case hsba.hinhthucravienid when 4 then 'X' end) as RVIEN_DV,
(case hsba.hosobenhanstatus when 1 then cast((cast(to_char(hsba.hosobenhandate_ravien, 'yyyy') as integer) - cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) + 1) as text) 
	end) as TSNDT, 
'' as TSNDT_1, 
'' as TSNDT_2, 
'' as TSNDT_3, 
'' as CD_NCD, 
'' as CD_NCD_1, 
'' as CD_NCD_2, 
'' as CD_NCD_3, 
'' as CD_NCD_4, 
'' as CD_KKB, 
'' as CD_KKB_1, 
'' as CD_KKB_2, 
'' as CD_KKB_3, 
'' as CD_KKB_4, 
'' as CD_KDT, 
'' as CD_KDT_1, 
'' as CD_KDT_2, 
'' as CD_KDT_3, 
'' as CD_KDT_4, 
'' as THUTHUAT, 
'' as PHAUTHUAT, 
hsba.chandoanravien as RVIEN_BENHCHINH, 
substr(hsba.chandoanravien_code,1,1) as RVIEN_BENHCHINH_1, 
substr(hsba.chandoanravien_code,2,1) as RVIEN_BENHCHINH_2, 
substr(hsba.chandoanravien_code,3,1) as RVIEN_BENHCHINH_3, 
substr(hsba.chandoanravien_code,4,1) as RVIEN_BENHCHINH_4, 
hsba.chandoanravien_kemtheo as RVIEN_BENHPHU, 
substr(hsba.chandoanravien_kemtheo_code,1,1) as RVIEN_BENHPHU_1, 
substr(hsba.chandoanravien_kemtheo_code,2,1) as RVIEN_BENHPHU_2, 
substr(hsba.chandoanravien_kemtheo_code,3,1) as RVIEN_BENHPHU_3, 
substr(hsba.chandoanravien_kemtheo_code,4,1) as RVIEN_BENHPHU_4, 
'' as RVIEN_TAIBIEN, 
'' as RVIEN_BIENCHUNG, 
(case hsba.ketquadieutriid when 1 then 'X' end) as TTRV_KHOI, 
(case hsba.ketquadieutriid when 2 then 'X' end) as TTRV_DO, 
(case hsba.ketquadieutriid when 3 then 'X' end) as TTRV_KTD, 
(case hsba.ketquadieutriid when 4 then 'X' end) as TTRV_NANG, 
(case hsba.ketquadieutriid when 5 then 'X' end) as TTRV_TUVONG, 
'' as TTRV_GPB_LT, 
'' as TTRV_GPB_NN, 
'' as TTRV_GPB_AT, 
'' as THTV_FULLTIME, 
'' as THTV_DOBENH, 
'' as THTV_TBDT, 
'' as THTV_KH, 
'' as THTV_24H, 
'' as THTV_SAU24H, 
'' as THTV_NNTV, 
'' as THTV_NNTV_1, 
'' as THTV_NNTV_2, 
'' as THTV_NNTV_3, 
'' as THTV_NNTV_4, 
'' as THTV_KNTT, 
'' as THTV_CDGPTT, 
'' as THTV_CDGPTT_1, 
'' as THTV_CDGPTT_2, 
'' as THTV_CDGPTT_3, 
'' as THTV_CDGPTT_4 
FROM hosobenhan hsba 
	inner join vienphi vp on vp.hosobenhanid=hsba.hosobenhanid 
	left join bhyt bh on bh.bhytid=vp.bhytid
	--inner join medicalrecord mrd on mrd.hosobenhanid=hsba.hosobenhanid
WHERE hsba.hosobenhanid=" + this.mrdHsbaHosobenhan.hosobenhanid + "
--ORDER by mrd.medicalrecordid







	





















----
CREATE TABLE hosobenhan
(
  hosobenhanid serial NOT NULL,
  soluutru text,
  soluutru_remark text,
  soluutru_vitri text,
  soluutru_nguoiluu integer,
  hosobenhancode text,
  isuploaded integer,
  isdownloaded integer,
  loaibenhanid integer,
  userid integer,
  departmentgroupid integer,
  departmentid integer,
  hinhthucvaovienid integer,
  ketquadieutriid integer,
  xutrikhambenhid integer,
  hinhthucravienid integer,
  hosobenhanstatus integer,
  patientid integer,
  hosobenhandate timestamp without time zone,
  hosobenhandate_ravien timestamp without time zone,
  chandoanvaovien_code text,
  chandoanvaovien text,
  chandoanravien_code text,
  chandoanravien text,
  chandoanravien_kemtheo_code text,
  chandoanravien_kemtheo text,
  lastaccessdate timestamp without time zone,
  hosobenhanremark text,
  version timestamp without time zone,
  sovaovien text DEFAULT ''::text,
  patientname text,
  birthday timestamp without time zone,
  birthday_year integer,
  gioitinhcode text,
  nghenghiepcode text,
  hc_dantoccode text,
  hc_quocgiacode text,
  hc_sonha text,
  hc_thon text,
  hc_xacode text,
  hc_huyencode text,
  hc_tinhcode text,
  noilamviec text,
  nguoithan text,
  nguoithan_name text,
  nguoithan_phone text,
  nguoithan_address text,
  gioitinhname text,
  nghenghiepname text,
  hc_dantocname text,
  hc_quocgianame text,
  hc_xaname text,
  hc_huyenname text,
  hc_tinhname text,
  sync_flag integer,
  update_flag integer,
  patient_id text,
  imagedata bytea,
  imagesize integer DEFAULT 0,
  patientcode text,
  soluutru_thoigianluu timestamp without time zone,
  isencript integer,
  ismocapcuu integer,
  bhytcode text,
  malifegap text,
  CONSTRAINT hosobenhan_pkey PRIMARY KEY (hosobenhanid)
)
ngà7 31 tháng 12 năm 2017
