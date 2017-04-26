---table 
CREATE TABLE mrd_license
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
CREATE TABLE mrd_option
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
CREATE TABLE mrd_tbllog
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

CREATE TABLE mrd_tbluser
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
CREATE TABLE mrd_tbluser_departmentgroup
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
CREATE TABLE mrd_tbluser_medicinephongluu
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
CREATE TABLE mrd_tbluser_medicinestore
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
CREATE TABLE mrd_tbluser_permission
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

CREATE TABLE mrd_version
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


















