/****** Script for SelectTopNRows command from SSMS  ******/

Insert into IVRecord_Print_AllEmp values('20180309101573', '8888636563791120237729', '9999-LaennecSysadmin', 
 '',  '9999-LaennecSysadmin')
 go
 Insert into IVRecord_Print values('20180309101573', GETDATE(), '1', 'NULL', 0, 'NULL', 'NULL', 'NULL', 'NULL', 0)
 go
 update IVRecord set IVStatus = 3,PrintDT = GETDATE(), PrinterID = '1',PrinterName = 'LaennecSysadmin'
 where LabelNo = '20180309101573' 
 and labelover = 0 
 and LabelNo in (select LabelNo from IVRecordToYTJ) and IVStatus < 3 
 and DATEDIFF(dd,GETDATE(),InfusionDT) >= -1
 select PrinterName where  where LabelNo = '20180317101573' 


                            