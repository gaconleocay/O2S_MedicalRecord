--------====Lay du lieu hoi chan.
--So bien ban hoi chan
select 
'" + hoichan_fulltime1 + "' as hoichan_fulltime1,
'"+this.HsbaHoiChanSave.tvtg_chutoa_ten+"' as tvtg_chutoa_ten,
'"+this.HsbaHoiChanSave.tvtg_chutoa_cdcv+"' as tvtg_chutoa_cdcv,
'"+this.HsbaHoiChanSave.tvtg_thuky_ten+"' as tvtg_thuky_ten,
'"+this.HsbaHoiChanSave.tvtg_thuky_cdcv+"' as tvtg_thuky_cdcv,
'"+this.HsbaHoiChanSave.tvtg_thanhvien1_ten+"' as tvtg_thanhvien1_ten,
'"+this.HsbaHoiChanSave.tvtg_thanhvien1_cdcv+"' as tvtg_thanhvien1_cdcv,
'"+this.HsbaHoiChanSave.tvtg_thanhvien2_ten+"' as tvtg_thanhvien2_ten,
'"+this.HsbaHoiChanSave.tvtg_thanhvien2_cdcv+"' as tvtg_thanhvien2_cdcv,
'"+this.HsbaHoiChanSave.tvtg_thanhvien3_ten+"' as tvtg_thanhvien3_ten,
'"+this.HsbaHoiChanSave.tvtg_thanhvien3_cdcv+"' as tvtg_thanhvien3_cdcv,
'"+this.HsbaHoiChanSave.tvtg_thanhvien4_ten+"' as tvtg_thanhvien4_ten,
'"+this.HsbaHoiChanSave.tvtg_thanhvien4_cdcv+"' as tvtg_thanhvien4_cdcv,
'"+this.HsbaHoiChanSave.tvtg_thanhvien5_ten+"' as tvtg_thanhvien5_ten,
'"+this.HsbaHoiChanSave.tvtg_thanhvien5_cdcv+"' as tvtg_thanhvien5_cdcv,
'"+this.HsbaHoiChanSave.tvtg_thanhvien6_ten+"' as tvtg_thanhvien6_ten,
'"+this.HsbaHoiChanSave.tvtg_thanhvien6_cdcv+"' as tvtg_thanhvien6_cdcv,
'"+this.HsbaHoiChanSave.diadiemhoichan+"' as diadiemhoichan,
hsba.patientname as patientname,
cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) 
		- cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) as tuoi,
hsba.gioitinhname as gioitinh,
hsba.hc_dantocname as dantoc,
hsba.hc_quocgianame as ngoaikieu,
'' as sohochieu,
'' as ngay_noicap,
hsba.nghenghiepname as nghenghiep,
hsba.noilamviec as noilamviec,
((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) ||
		(case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) ||
		(case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) ||
		(case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) ||
		(case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) ||
		hc_quocgianame) as diachi,
hsba.sovaovien as sovaovien,
substr(hsba.bhytcode,1,2) as bhyt_1, 
substr(hsba.bhytcode,3,1) as bhyt_2, 
substr(hsba.bhytcode,4,2) as bhyt_3, 
substr(hsba.bhytcode,6,2) as bhyt_4, 
substr(hsba.bhytcode,8,8) as bhyt_5, 
(select to_char(thoigianvaovien, 'hh g\"i\"ờ mi phút, ngà\"y\" dd tháng mm năm yyyy') from medicalrecord where loaibenhanid=1 and hosobenhanid=hsba.hosobenhanid order by medicalrecordid limit 1) as tg_vaovien_fulltime1,
'"+this.currentMedicalrecord.departmentgroupname+"' as khoa,
'"+this.HsbaHoiChanSave.yeucauhoichan+"' as yeucauhoichan,
'"+this.HsbaHoiChanSave.dbb_tomtattiensubenh+"' as dbb_tomtattiensubenh,
'"+this.HsbaHoiChanSave.dbb_tinhtranglucvaovien+"' as dbb_tinhtranglucvaovien,
'"+this.HsbaHoiChanSave.dbb_chandoantuyenduoi+"' as dbb_chandoantuyenduoi,
'"+this.HsbaHoiChanSave.dbb_tomtatdienbienbenh+"' as dbb_tomtatdienbienbenh,
'"+this.HsbaHoiChanSave.yk_chandoantienluong+"' as yk_chandoantienluong,
'"+this.HsbaHoiChanSave.yk_phuongphapdieutri+"' as yk_phuongphapdieutri,
'"+this.HsbaHoiChanSave.yk_chamsoc+"' as yk_chamsoc,
'"+this.HsbaHoiChanSave.kl_ketluan+"' as kl_ketluan
from hosobenhan hsba
where hsba.hosobenhanid="+this.HsbaHoiChanSave.hosobenhanid+"


----------Trich bien ban hoi chan 

select 
hsba.sovaovien as sovaovien,
hsba.patientname as patientname,
cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) 
		- cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) as tuoi,
hsba.gioitinhname as gioitinh,
(select to_char(thoigianvaovien, 'dd/mm/yyyy') from medicalrecord where loaibenhanid=1 and hosobenhanid=hsba.hosobenhanid order by medicalrecordid limit 1) as tg_vaovien_fulltime2,
(select (case when thoigianravien <> '0001-01-01 00:00:00' then to_char(thoigianravien, 'dd/mm/yyyy') end) from medicalrecord where loaibenhanid=1 and hosobenhanid=hsba.hosobenhanid order by medicalrecordid limit 1) as tg_ravien_fulltime1,
'"+this.currentMedicalrecord.giuong+"' as giuong,
'"+this.currentMedicalrecord.departmentname+"' as buong,
'"+this.currentMedicalrecord.departmentgroupname+"' as khoa,
'"+this.currentMedicalrecord.chandoanbandau+"' as chandoan,
'" + hoichan_fulltime2 + "' as hoichan_fulltime2,
'"+this.HsbaHoiChanSave.tvtg_chutoa_ten+"' as tvtg_chutoa_ten,
'"+this.HsbaHoiChanSave.tvtg_chutoa_cdcv+"' as tvtg_chutoa_cdcv,
'"+this.HsbaHoiChanSave.tvtg_thuky_ten+"' as tvtg_thuky_ten,
'"+this.HsbaHoiChanSave.tvtg_thuky_cdcv+"' as tvtg_thuky_cdcv,
'"+this.HsbaHoiChanSave.tvtg_thanhvien1_ten+"' as tvtg_thanhvien1_ten,
'"+this.HsbaHoiChanSave.tvtg_thanhvien1_cdcv+"' as tvtg_thanhvien1_cdcv,
'"+this.HsbaHoiChanSave.tvtg_thanhvien2_ten+"' as tvtg_thanhvien2_ten,
'"+this.HsbaHoiChanSave.tvtg_thanhvien2_cdcv+"' as tvtg_thanhvien2_cdcv,
'"+this.HsbaHoiChanSave.tvtg_thanhvien3_ten+"' as tvtg_thanhvien3_ten,
'"+this.HsbaHoiChanSave.tvtg_thanhvien3_cdcv+"' as tvtg_thanhvien3_cdcv,
'"+this.HsbaHoiChanSave.tvtg_thanhvien4_ten+"' as tvtg_thanhvien4_ten,
'"+this.HsbaHoiChanSave.tvtg_thanhvien4_cdcv+"' as tvtg_thanhvien4_cdcv,
'"+this.HsbaHoiChanSave.tvtg_thanhvien5_ten+"' as tvtg_thanhvien5_ten,
'"+this.HsbaHoiChanSave.tvtg_thanhvien5_cdcv+"' as tvtg_thanhvien5_cdcv,
'"+this.HsbaHoiChanSave.tvtg_thanhvien6_ten+"' as tvtg_thanhvien6_ten,
'"+this.HsbaHoiChanSave.tvtg_thanhvien6_cdcv+"' as tvtg_thanhvien6_cdcv,
'"+this.HsbaHoiChanSave.dbb_tomtatdienbienbenh+"' as dbb_tomtatdienbienbenh,
'"+this.HsbaHoiChanSave.kl_ketluan+"' as kl_ketluan,
'"+this.HsbaHoiChanSave.yk_phuongphapdieutri+"' as yk_phuongphapdieutri
from hosobenhan hsba
where hsba.hosobenhanid="+this.HsbaHoiChanSave.hosobenhanid+";






-----------================Da luu tru






















