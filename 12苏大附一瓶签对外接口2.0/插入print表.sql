if not exists(select 1 from IVRecord_Print where LabelNo='1001') 
Insert into IVRecord_Print values('1001', GETDATE(), '1', 'NULL', 0, 'NULL', 'NULL', 'NULL', 'NULL',  0)