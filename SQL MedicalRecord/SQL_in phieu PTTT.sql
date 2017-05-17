select '' as SOVAOVIEN, 
hsba.PATIENTCODE, 
hsba.PATIENTNAME,
cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) as PATIENT_AGE,
gioitinhname as PATIENT_GENDERNAME, 
'KHOA' AS DEPARTMENTGROUPNAME,
'PHONG' as DEPARTMENTNAME,
'' as GIUONG,
to_char(hsba.hosobenhandate, 'hh24') as VIENPHIDATE_NT_GIO,
to_char(hsba.hosobenhandate, 'mi') as VIENPHIDATE_NT_PHUT,
to_char(hsba.hosobenhandate, 'dd') as VIENPHIDATE_NT_NGAY,
to_char(hsba.hosobenhandate, 'mm') as VIENPHIDATE_NT_THANG,
to_char(hsba.hosobenhandate, 'yyyy') as VIENPHIDATE_NT_NAM,
to_char(pttt.phauthuatthuthuatdate, 'hh24') as TG_PTTT_GIO,
to_char(pttt.phauthuatthuthuatdate, 'mi') as TG_PTTT_PHUT,
to_char(pttt.phauthuatthuthuatdate, 'dd') as TG_PTTT_NGAY,
to_char(pttt.phauthuatthuthuatdate, 'mm') as TG_PTTT_THANG,
to_char(pttt.phauthuatthuthuatdate, 'yyyy') as TG_PTTT_NAM,
pttt.chandoanvaokhoa as CHANDOAN,
pttt.chandoantruocphauthuat as CD_TRUOC_PTTT,
pttt.chandoansauphauthuat as CD_SAU_PTTT,
pttt.phuongphappttt as PHUONGPHAP_PTTT,
'' as LOAIPHAP_PTTT,
(case pttt.pttt_phuongphapvocamid when 1 then 'Gây mê tĩnh mạch'
								when 2 then 'Gây mê nội khí quản'
								when 3 then 'Gây tê tại chỗ'
								when 4 then 'Tiền mê + gây tê tại chỗ'
								when 5 then 'Gây tê tủy sống'
								when 6 then 'Gây tê'
								when 7 then 'Gây tê ngoài màng cứng'
								when 8 then 'Gây tê đám rối thần kinh'
								when 9 then 'Gây tê Codan'
								when 10 then 'Gây tê nhãn cầu'
								when 11 then 'Gây tê cạnh sống'
								when 99 then 'Khác'
								end) as PHUONGPHAP_VOCAM,							
ptv.username as BACSI_PTTT,
bsgm.username as BACSI_GAYME,
to_char(now(), 'dd') as CURRENT_NGAY,
to_char(now(), 'mm') as CURRENT_THANG,
to_char(now(), 'yyyy') as CURRENT_NAM
from hosobenhan hsba 
	inner join serviceprice ser on ser.hosobenhanid=hsba.hosobenhanid
	left join phauthuatthuthuat pttt on pttt.servicepriceid=ser.servicepriceid
	left join tools_tblnhanvien ptv on ptv.userhisid=pttt.phauthuatvien
	left join tools_tblnhanvien bsgm on bsgm.userhisid=pttt.bacsigayme
where hsba.hosobenhanid=" + this.mecicalrecordCurrentDTO.hosobenhanid + "
		and ser.servicepriceid=" + this.mecicalrecordCurrentDTO.servicepriceid + ";






