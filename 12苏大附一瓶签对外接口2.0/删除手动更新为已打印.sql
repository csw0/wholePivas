/****** Script for SelectTopNRows command from SSMS  ******/
delete from IVRecord_Print_AllEmp
where LabelNo = 20180309101573
go
delete from IVRecord_Print
where LabelNo = 20180309101573
--go
--delete from IVRecordToYTJ
--where LabelNo = 20180310102631
go
update IVRecord set IVStatus = 0,PrintDT = NULL, PrinterName = NULL where LabelNo = '20180309101573' 
