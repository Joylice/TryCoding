/*
Navicat SQLite Data Transfer

Source Server         : CactusDB
Source Server Version : 30706
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 30706
File Encoding         : 65001

Date: 2016-01-17 21:27:09
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for "main"."sqlite_sequence"
-- ----------------------------
DROP TABLE "main"."sqlite_sequence";
CREATE TABLE sqlite_sequence(name,seq);

-- ----------------------------
-- Records of sqlite_sequence
-- ----------------------------
INSERT INTO "main"."sqlite_sequence" VALUES ('sys_user', null);

-- ----------------------------
-- Table structure for "main"."sys_action"
-- ----------------------------
DROP TABLE "main"."sys_action";
CREATE TABLE "sys_action" (
"Action_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"ActionName"  TEXT NOT NULL,
"ActionUrl"  TEXT NOT NULL,
"ActionGroupID"  INTEGER NOT NULL
);

-- ----------------------------
-- Records of sys_action
-- ----------------------------

-- ----------------------------
-- Table structure for "main"."sys_actionGroup"
-- ----------------------------
DROP TABLE "main"."sys_actionGroup";
CREATE TABLE "sys_actionGroup" (
"ActionGroup_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"ActionGroupName"  TEXT NOT NULL
);

-- ----------------------------
-- Records of sys_actionGroup
-- ----------------------------

-- ----------------------------
-- Table structure for "main"."sys_role"
-- ----------------------------
DROP TABLE "main"."sys_role";
CREATE TABLE "sys_role" (
"Role_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"RoleName"  TEXT NOT NULL,
"ActionIds"  TEXT NOT NULL
);

-- ----------------------------
-- Records of sys_role
-- ----------------------------

-- ----------------------------
-- Table structure for "main"."sys_user"
-- ----------------------------
DROP TABLE "main"."sys_user";
CREATE TABLE "sys_user" (
"User_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"RoleId"  INT,
"Name"  TEXT(255),
"Password"  TEXT,
"NickName"  TEXT,
"Avatar"  TEXT,
"Email"  TEXT,
"Phone"  TEXT,
"Qq"  TEXT,
"AddTime"  DATETIME,
"LastLoginTime"  DATETIME,
"LastLoginUser"  INT,
"IsLock"  INT
);

-- ----------------------------
-- Records of sys_user
-- ----------------------------
