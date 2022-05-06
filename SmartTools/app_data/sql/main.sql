/*
 Navicat Premium Data Transfer

 Source Server         : cache
 Source Server Type    : SQLite
 Source Server Version : 3030001
 Source Schema         : main

 Target Server Type    : SQLite
 Target Server Version : 3030001
 File Encoding         : 65001

 Date: 31/03/2022 09:31:16
*/

PRAGMA foreign_keys = false;

-- ----------------------------
-- Table structure for APP_SERVER_INFO
-- ----------------------------
DROP TABLE IF EXISTS "APP_SERVER_INFO";
CREATE TABLE "APP_SERVER_INFO" (
  "SEQ_NO" text(64),
  "CREATE_TIME" TEXT(64),
  "UPDATE_TIME" TEXT(64),
  "VERSION" TEXT(64),
  "HOST" TEXT(256)
);

-- ----------------------------
-- Table structure for APP_TOKEN
-- ----------------------------
DROP TABLE IF EXISTS "APP_TOKEN";
CREATE TABLE "APP_TOKEN" (
  "SEQ_NO" text(64),
  "CREATE_TIME" TEXT(64),
  "UPDATE_TIME" TEXT(64),
  "TOKEN" TEXT(1000),
  "REFRESH_TOKEN" TEXT(1000),
  "EXPIRE" TEXT(64)
);

-- ----------------------------
-- Table structure for APP_USER_INFO
-- ----------------------------
DROP TABLE IF EXISTS "APP_USER_INFO";
CREATE TABLE "APP_USER_INFO" (
  "SEQ_NO" text(64),
  "CREATE_TIME" TEXT(64),
  "UPDATE_TIME" TEXT(64),
  "USER_ID" TEXT(64),
  "USER_NAME" TEXT(64),
  "USER_EMAIL" TEXT(128),
  "CROP_ID" TEXT(64),
  "CROP_NAME" TEXT(128),
  "CROP_CODE" TEXT(64),
  "SITE" TEXT(128)
);

PRAGMA foreign_keys = true;
