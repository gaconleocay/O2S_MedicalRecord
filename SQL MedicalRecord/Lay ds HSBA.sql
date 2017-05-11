SELECT ROW_NUMBER () OVER (ORDER BY mrd.medicalrecordstatus, mrd.thoigianvaovien desc) as stt,
	hsba.patientid,
	hsba.patientcode,
	hsba.patientname,
	mrd.medicalrecordid,
	mrd.medicalrecordcode,
	hsba.bhytcode,
	mrd.thoigianvaovien,
	mrd.medicalrecordstatus,
	hsba.hosobenhanid
FROM medicalrecord mrd
	inner join hosobenhan hsba on mrd.hosobenhanid=hsba.hosobenhanid
WHERE mrd.thoigianvaovien>='2017-01-01 00:00:00'
	and mrd.thoigianvaovien<='2017-01-10 23:59:59'
	and mrd.medicalrecordstatus=2
	and mrd.departmentid=226
	
---
---lay thong tin ve benh nhan - tab Thong tin	
	
select degp.departmentgroupname,
	   de.departmentname,
	   hsba.hosobenhanid,
	   mrd.medicalrecordcode,
	   hsba.patientcode,
	   hsba.patientname,
	   vp.vienphicode,
	   (case vp.doituongbenhnhanid when 1 then 'BHYT'
								  when 2 then 'Viện phí'
								  when 3 then 'Dịch vụ'
								  when 4 then 'Người nước ngoài'
								  when 5 then 'Miễn phí'
								  when 6 then 'Hợp đồng'
								  else '' end) as doituongbenhnhan,
		((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) ||
		(case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) ||
		(case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) ||
		(case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) ||
		(case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) ||
		hc_quocgianame) as diachi,
		hsba.bhytcode,
		mrd.thoigianvaovien,
		mrd.thoigianravien
	      
from hosobenhan hsba 
	inner join medicalrecord mrd on mrd.hosobenhanid=hsba.hosobenhanid
	inner join vienphi vp on vp.hosobenhanid=hsba.hosobenhanid
	left join departmentgroup degp on degp.departmentgroupid=mrd.departmentgroupid
	left join department de on de.departmentid=mrd.departmentid
where mrd.medicalrecordid=1160565	

----PTTT
select ROW_NUMBER () OVER (ORDER BY mbp.maubenhphamdate) as stt,
		mbp.maubenhphamid,
		mbp.sophieu,
		mbp.maubenhphamdate,
		mbp.maubenhphamdate_sudung,
		mbp.maubenhphamdate_thuchien,
		mbp.maubenhphamfinishdate,
		degp.departmentgroupname,
		de.departmentname,
		nv.username as nguoichidinh,
		mbp.maubenhphamdatastatus
from maubenhpham mbp
	inner join departmentgroup degp on degp.departmentgroupid=mbp.departmentgroupid
	left join department de on de.departmentid=mbp.departmentid
	left join tools_tblnhanvien nv on nv.userhisid=mbp.userid
where mbp.hosobenhanid='"+this.hosobenhanid+"'
	and mbp.maubenhphamgrouptype=4 and mbp.isdeleted=0;


---








