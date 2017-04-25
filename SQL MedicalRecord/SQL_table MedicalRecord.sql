---table 

CREATE TABLE tools_license
(
  licenseid serial NOT NULL,
  datakey text,
  licensekey text,
  CONSTRAINT tools_license_pkey PRIMARY KEY (licenseid)
)

CREATE TABLE tools_option
(
  toolsoptionid serial NOT NULL,
  toolsoptioncode text,
  toolsoptionname text,
  toolsoptionvalue text,
  toolsoptionnote text,
  toolsoptionlook integer,
  toolsoptiondate timestamp without time zone,
  toolsoptioncreateuser text,
  CONSTRAINT tools_option_pkey PRIMARY KEY (toolsoptionid)
)

CREATE TABLE tools_tbllog
(
  logid serial NOT NULL,
  loguser text,
  logvalue text,
  ipaddress text,
  computername text,
  softversion text,
  logtime timestamp without time zone,
  CONSTRAINT tools_tbllog_pkey PRIMARY KEY (logid)
)

CREATE TABLE tools_tbluser
(
  userid serial NOT NULL,
  usercode text NOT NULL,
  username text,
  userpassword text,
  userstatus integer,
  usergnhom integer,
  usernote text,
  userhisid integer,
  CONSTRAINT tools_tbluser_pkey PRIMARY KEY (userid)
)

CREATE TABLE tools_tbluser_departmentgroup
(
  userdepgid serial NOT NULL,
  departmentgroupid integer,
  departmentid integer,
  departmenttype integer,
  usercode text,
  userdepgidnote text,
  CONSTRAINT tbluser_departmentgroup_pkey PRIMARY KEY (userdepgid)
)

CREATE TABLE tools_tbluser_medicinephongluu
(
  userphongluutid serial NOT NULL,
  medicinephongluuid integer,
  medicinestoreid integer,
  usercode text,
  userdepgidnote text,
  CONSTRAINT tools_tbluser_medicinephongluu_pkey PRIMARY KEY (userphongluutid)
)

CREATE TABLE tools_tbluser_medicinestore
(
  usermestid serial NOT NULL,
  medicinestoreid integer,
  medicinestoretype integer,
  usercode text,
  userdepgidnote text,
  CONSTRAINT tools_tbluser_medicinestore_pkey PRIMARY KEY (usermestid)
)

CREATE TABLE tools_tbluser_permission
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

CREATE TABLE tools_version
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
  CONSTRAINT tools_version_pkey PRIMARY KEY (versionid)
)


















