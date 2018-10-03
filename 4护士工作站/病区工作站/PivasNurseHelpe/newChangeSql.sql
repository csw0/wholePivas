use Pivas20140814
go

if  not exists (select Name From SysColumns Where ID = OBJECT_ID('PivasNurseFormSet') And Name = 'LabelPack')
ALTER TABLE   PivasNurseFormSet  ADD LabelPack smallint 
GO

if  not exists (select Name From SysColumns Where ID = OBJECT_ID('PivasNurseFormSet') And Name = 'LabelPackAir')
ALTER TABLE   PivasNurseFormSet  ADD LabelPackAir smallint
GO
if  not exists (select Name From SysColumns Where ID = OBJECT_ID('PivasNurseFormSet') And Name = 'IsKCancel')
ALTER TABLE   PivasNurseFormSet  ADD IsKCancel smallint null
GO
if  not exists (select Name From SysColumns Where ID = OBJECT_ID('BPRecord') And Name = 'BPIsRead')
ALTER TABLE  BPRecord  ADD BPIsRead bit 
go

update PivasNurseFormSet set LabelPack=9,LabelPackAir=9





































