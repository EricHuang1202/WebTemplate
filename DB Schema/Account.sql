/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50712
Source Host           : localhost:3306
Source Database       : StockCrawler

Target Server Type    : MYSQL
Target Server Version : 50712
File Encoding         : 65001

Date: 2017-12-12 15:55:43
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for Account
-- ----------------------------
DROP TABLE IF EXISTS `Account`;
CREATE TABLE `Account` (
  `AccId` varchar(15) NOT NULL,
  `AccName` varchar(20) NOT NULL,
  `AccPwd` varchar(50) NOT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `CreateDate` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`AccId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
