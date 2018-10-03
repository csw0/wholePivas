/******************************************
** 天津市第一中心医院PIVAs MATE v3.6新增项
** 1、耗材统计 ConsumablesStatic.exe
** 2、医生工作站 Doctor.exe
** 3、配液中心停药、药品更换批号 通告设置
**    Notice.exe
** 4、护士站查询停药、库存不足、更换批号、
**    本科室当月耗材统计情况
**    PivasNurseBall.dll
**----------------------------------------*
** Author：virgle.xi
** DATE  : 2015-01-16
*******************************************
*/
/**
*增加不合理医嘱处理标识
*/
if not exists(select * from syscolumns where id=object_id('CPResultRG') and name='statu')
  ALTER TABLE CPResultRG ADD statu bit
GO
/**
*增加停药标识
*/
if not exists(select * from syscolumns where id=object_id('DDrug') and name='isStop')
  ALTER TABLE DDrug ADD isStop bit DEFAULT 0 
Go
UPDATE DDrug SET isStop = 0 WHERE isStop IS NULL 
GO
/**
*停药公告表
*/
IF NOT EXISTS(SELECT name FROM sysobjects WHERE name = 'Notice' AND type = 'U') 
  CREATE TABLE Notice (DrugCode varchar(50), DrugName varchar(100), Discription varchar(500))
GO
/**
*停药公告明细表
*/
IF NOT EXISTS(SELECT name FROM sysobjects WHERE name = 'NoticeSet' AND type = 'U') 
    CREATE TABLE NoticeSet (WardCode varchar(20), DrugCode varchar(50), status bit)
GO  
/**
*增加厂家标识
*/
if not exists(select * from syscolumns where id=object_id('DDrug') and name='firm_id')
  ALTER TABLE DDrug ADD firm_id varchar(100) 
Go