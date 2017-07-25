 ---Chạy khởi tạo Add-on Dlink trước
 CREATE EXTENSION dblink;

--Connect
SELECT dblink_connect('dbname=test_viettiep port=5432 host=localhost user=postgres password=1234');       
SELECT dblink_connect('myconn', 'dbname=test_viettiep port=5432 host=localhost user=postgres password=1234');


--Disconnect
SELECT dblink_disconnect();
SELECT dblink_disconnect('myconn');

--Querry
SELECT tools_depatment.*
FROM dblink('myconn','SELECT departmentgroupid, departmentgroupname FROM tools_depatment')
    AS tools_depatment(departmentgroupid integer, departmentgroupname text);
    
SELECT tools_depatment.*, log.*
FROM dblink('dbname=test_viettiep port=5432 host=localhost user=postgres password=1234','SELECT departmentgroupid, departmentgroupname FROM tools_depatment')
    AS tools_depatment(departmentgroupid integer, departmentgroupname text)
	left join mrd_tbllog log on log.logid=tools_depatment.departmentgroupid
    


----Excute
SELECT dblink_exec('myconn', 'INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion) 
VALUES (''admin'',''su dung dlink'',''12.3.3.4'',''NhatHm'',''1.1.1'');');


----Send querry
SELECT dblink_send_query('myconn', 'SELECT * FROM tools_tbllog'); 
----------- Khong biet
INSERT INTO mrd_depatment(departmentgroupid, departmentgroupcode, departmentgroupname, departmentgrouptype, departmentid, departmentcode, departmentname, departmenttype) 
SELECT department.*
FROM dblink('myconn','SELECT degp.departmentgroupid as departmentgroupid, degp.departmentgroupcode as departmentgroupcode, degp.departmentgroupname as departmentgroupname, degp.departmentgrouptype, de.departmentid as departmentid, de.departmentcode as departmentcode, de.departmentname as departmentname, de.departmenttype FROM departmentgroup degp,department de WHERE de.departmentgroupid = degp.departmentgroupid ORDER BY degp.departmentgroupid')
    AS department(departmentgroupid integer, departmentgroupcode text, departmentgroupname text, departmentgrouptype integer, departmentid integer, departmentcode text, departmentname text, departmenttype integer)





SELECT dblink_connect('myconn', 'dbname=test_viettiep port=5432 host=localhost user=postgres password=1234');





