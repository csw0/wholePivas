/****** Script for SelectTopNRows command from SSMS  ******/
SELECT LabelNo,DrugType,PrintDT
FROM  IVRecord left join Prescription on IVRecord.GroupNo = Prescription.GroupNo
where IVRecord.LabelNo = '20180322101763'
  