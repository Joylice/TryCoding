/*
Navicat MySQL Data Transfer

Source Server         : mysql5.0
Source Server Version : 50077
Source Host           : localhost:3306
Source Database       : cactusdb

Target Server Type    : MYSQL
Target Server Version : 50077
File Encoding         : 65001

Date: 2015-09-05 15:12:19
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `activity`
-- ----------------------------
DROP TABLE IF EXISTS `activity`;
CREATE TABLE `activity` (
  `ActivityId` bigint(20) NOT NULL default '0' COMMENT '活动id',
  `StoreId` bigint(20) default NULL COMMENT '商店id',
  `Name` varchar(30) default NULL COMMENT '活动名',
  `AddTime` datetime default NULL COMMENT '添加时间',
  `Des` longtext COMMENT '活动描述',
  PRIMARY KEY  (`ActivityId`)
) ENGINE=InnoDB DEFAULT CHARSET=gbk;

-- ----------------------------
-- Records of activity
-- ----------------------------

-- ----------------------------
-- Table structure for `cms_article`
-- ----------------------------
DROP TABLE IF EXISTS `cms_article`;
CREATE TABLE `cms_article` (
  `Article_Id` int(11) NOT NULL auto_increment,
  `Title` varchar(255) default NULL,
  `Content` longtext,
  `Keywords` varchar(255) default NULL,
  `Description` varchar(255) default NULL,
  `CoverPlan` varchar(255) default NULL,
  `ColumnId` int(11) default NULL,
  `AddTime` datetime default NULL,
  PRIMARY KEY  (`Article_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=gbk;

-- ----------------------------
-- Records of cms_article
-- ----------------------------

-- ----------------------------
-- Table structure for `cms_column`
-- ----------------------------
DROP TABLE IF EXISTS `cms_column`;
CREATE TABLE `cms_column` (
  `Column_Id` int(11) NOT NULL auto_increment,
  `ColumnName` varchar(255) default NULL,
  `FatherId` int(11) default NULL,
  `ColumnUrl` varchar(255) default NULL,
  `IsUrl` bit(1) default NULL COMMENT '是否是链接',
  PRIMARY KEY  (`Column_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=gbk;

-- ----------------------------
-- Records of cms_column
-- ----------------------------

-- ----------------------------
-- Table structure for `store`
-- ----------------------------
DROP TABLE IF EXISTS `store`;
CREATE TABLE `store` (
  `Store_Id` bigint(20) NOT NULL auto_increment COMMENT '商店ID',
  `Account` varchar(30) default NULL COMMENT '账户',
  `Password` varchar(50) default NULL COMMENT '密码',
  `Name` varchar(25) default '商店名' COMMENT '商店名',
  `AddTime` datetime default NULL COMMENT '开店时间',
  `Des` varchar(1000) default '商店描述' COMMENT '商店描述',
  `ThemeCss` varchar(2000) default NULL,
  `ActionIds` varchar(1000) default NULL,
  `PicUrl` varchar(500) default NULL,
  PRIMARY KEY  (`Store_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=gbk;

-- ----------------------------
-- Records of store
-- ----------------------------
INSERT INTO `store` VALUES ('1', 'admin', '19a2854144b63a8f7617a6f22519b12', '商店名', '2015-07-12 20:04:50', '商店描述', '111', null, null);

-- ----------------------------
-- Table structure for `store_category`
-- ----------------------------
DROP TABLE IF EXISTS `store_category`;
CREATE TABLE `store_category` (
  `Category_Id` bigint(20) NOT NULL auto_increment COMMENT '分类id',
  `StoreId` bigint(20) default NULL COMMENT '店铺id',
  `Name` varchar(30) default NULL COMMENT '分类名',
  PRIMARY KEY  (`Category_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=gbk;

-- ----------------------------
-- Records of store_category
-- ----------------------------
INSERT INTO `store_category` VALUES ('1', '1', 'test');
INSERT INTO `store_category` VALUES ('2', '1', 'lww01');
INSERT INTO `store_category` VALUES ('3', '1', 'lww03');
INSERT INTO `store_category` VALUES ('4', '1', 'lww02');

-- ----------------------------
-- Table structure for `store_order`
-- ----------------------------
DROP TABLE IF EXISTS `store_order`;
CREATE TABLE `store_order` (
  `Order_Id` bigint(20) NOT NULL auto_increment COMMENT '订单Id',
  `StoreId` bigint(20) NOT NULL COMMENT '商店id',
  `OpenId` varchar(300) default NULL COMMENT '客户Id（微信）',
  `ProductId` bigint(20) default NULL COMMENT '商品Id',
  `Num` int(11) default NULL COMMENT '购买数量',
  `Money` decimal(10,0) default NULL COMMENT '总金额',
  `Log` varchar(300) default NULL COMMENT '日志信息（记录购买时的金额、数量、折扣）',
  `Des` varchar(500) default NULL COMMENT '用户备注',
  `Address` varchar(500) default NULL COMMENT '地点',
  `Status` int(11) default NULL COMMENT '状态（成功，失败，提交中）',
  PRIMARY KEY  (`Order_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=gbk;

-- ----------------------------
-- Records of store_order
-- ----------------------------

-- ----------------------------
-- Table structure for `stroe_product`
-- ----------------------------
DROP TABLE IF EXISTS `stroe_product`;
CREATE TABLE `stroe_product` (
  `Product_Id` bigint(20) NOT NULL auto_increment COMMENT '商品id',
  `StoreId` bigint(20) default NULL COMMENT '商店id',
  `Name` varchar(50) default NULL COMMENT '商品名',
  `Price` decimal(10,0) default NULL COMMENT '商品单价',
  `CategoryId` bigint(20) default NULL COMMENT '分类',
  `Des` longtext COMMENT '描述',
  `Num` int(11) default NULL COMMENT '库存',
  `Agio` int(11) default NULL COMMENT '折扣1-100',
  `PicUrl` varchar(500) default NULL COMMENT '封面图',
  `AddTime` datetime default NULL COMMENT '上架时间',
  `Status` int(11) default NULL COMMENT '状态（上架，下架，推荐）',
  PRIMARY KEY  (`Product_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=gbk;

-- ----------------------------
-- Records of stroe_product
-- ----------------------------
INSERT INTO `stroe_product` VALUES ('1', '1', 'test', '100', '4', null, '1000', '100', null, '2015-07-20 14:15:32', '1');

-- ----------------------------
-- Table structure for `sys_action`
-- ----------------------------
DROP TABLE IF EXISTS `sys_action`;
CREATE TABLE `sys_action` (
  `Action_Id` bigint(20) NOT NULL auto_increment,
  `ActionName` varchar(20) default NULL,
  `ActionUrl` varchar(255) default NULL,
  `ActionGroupID` int(11) default NULL,
  PRIMARY KEY  (`Action_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=gbk;

-- ----------------------------
-- Records of sys_action
-- ----------------------------

-- ----------------------------
-- Table structure for `sys_actiongroup`
-- ----------------------------
DROP TABLE IF EXISTS `sys_actiongroup`;
CREATE TABLE `sys_actiongroup` (
  `ActionGroup_Id` bigint(20) NOT NULL auto_increment,
  `ActionGroupName` varchar(20) default NULL,
  PRIMARY KEY  (`ActionGroup_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=gbk;

-- ----------------------------
-- Records of sys_actiongroup
-- ----------------------------

-- ----------------------------
-- Table structure for `sys_role`
-- ----------------------------
DROP TABLE IF EXISTS `sys_role`;
CREATE TABLE `sys_role` (
  `Role_Id` bigint(20) NOT NULL auto_increment,
  `RoleName` varchar(20) default NULL,
  `ActionIds` varchar(2000) default NULL,
  PRIMARY KEY  (`Role_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=gbk;

-- ----------------------------
-- Records of sys_role
-- ----------------------------
INSERT INTO `sys_role` VALUES ('1', '超级管理员', '1');

-- ----------------------------
-- Table structure for `sys_user`
-- ----------------------------
DROP TABLE IF EXISTS `sys_user`;
CREATE TABLE `sys_user` (
  `User_Id` bigint(20) NOT NULL auto_increment,
  `RoleId` bigint(20) default NULL,
  `Name` varchar(32) default NULL,
  `Password` varchar(255) default NULL,
  `NickName` varchar(32) default NULL,
  `Avatar` varchar(255) default NULL,
  `Email` varchar(32) default NULL,
  `Phone` varchar(32) default NULL,
  `Qq` varchar(16) default NULL,
  `AddTime` datetime default NULL,
  `LastLoginTime` datetime default NULL,
  `LastLoginIp` varchar(32) default NULL,
  `IsSuperUser` tinyint(1) default NULL,
  `IsLock` tinyint(1) default NULL,
  PRIMARY KEY  (`User_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=gbk;

-- ----------------------------
-- Records of sys_user
-- ----------------------------
INSERT INTO `sys_user` VALUES ('1', '1', 'admin', '19a2854144b63a8f7617a6f22519b12', '漫漫洒洒', '/Upload/Avatars/1.png', '702295399@qq.com', '18714253698', '702295399', '2015-06-09 22:32:21', '2015-09-04 22:23:14', '127.0.0.1', '1', '0');
