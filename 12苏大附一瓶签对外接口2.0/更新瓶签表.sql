update IVRecord set IVStatus = 3,
PrintDT = case when PrintDT is null then GETDATE(),
PrinterID = '1',PrinterName='LaennecSysadmin'

where LabelNo = '1' and IVStatus <= 3  
and DATEDIFF(dd,GETDATE(),InfusionDT) >= -1