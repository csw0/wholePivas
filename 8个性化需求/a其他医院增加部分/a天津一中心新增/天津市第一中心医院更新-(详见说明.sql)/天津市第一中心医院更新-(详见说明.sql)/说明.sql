/******************************************
** ����е�һ����ҽԺPIVAs MATE v3.6������
** 1���Ĳ�ͳ�� ConsumablesStatic.exe
** 2��ҽ������վ Doctor.exe
** 3����Һ����ͣҩ��ҩƷ�������� ͨ������
**    Notice.exe
** 4����ʿվ��ѯͣҩ����治�㡢�������š�
**    �����ҵ��ºĲ�ͳ�����
**    PivasNurseBall.dll
**----------------------------------------*
** Author��virgle.xi
** DATE  : 2015-01-16
*******************************************
*/
/**
*���Ӳ�����ҽ�������ʶ
*/
if not exists(select * from syscolumns where id=object_id('CPResultRG') and name='statu')
  ALTER TABLE CPResultRG ADD statu bit
GO
/**
*����ͣҩ��ʶ
*/
if not exists(select * from syscolumns where id=object_id('DDrug') and name='isStop')
  ALTER TABLE DDrug ADD isStop bit DEFAULT 0 
Go
UPDATE DDrug SET isStop = 0 WHERE isStop IS NULL 
GO
/**
*ͣҩ�����
*/
IF NOT EXISTS(SELECT name FROM sysobjects WHERE name = 'Notice' AND type = 'U') 
  CREATE TABLE Notice (DrugCode varchar(50), DrugName varchar(100), Discription varchar(500))
GO
/**
*ͣҩ������ϸ��
*/
IF NOT EXISTS(SELECT name FROM sysobjects WHERE name = 'NoticeSet' AND type = 'U') 
    CREATE TABLE NoticeSet (WardCode varchar(20), DrugCode varchar(50), status bit)
GO  
/**
*���ӳ��ұ�ʶ
*/
if not exists(select * from syscolumns where id=object_id('DDrug') and name='firm_id')
  ALTER TABLE DDrug ADD firm_id varchar(100) 
Go