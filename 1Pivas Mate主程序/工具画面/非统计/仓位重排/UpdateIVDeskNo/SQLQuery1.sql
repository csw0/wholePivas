use Pivas20140901
go

create table DeskNoUpdateLog
( 
  ID int identity(1,1) not null,     --��ʶ��
  DEmployeeID int not null,          --Ա����
  InsertDT datetime not null,        --�޸�ʱ��
  LabelNo varchar(22) not null,      --ƿǩ��
  oldDeskNo varchar(100) not null,   --�ɲ�λ
  newDeskNo varchar(100) not null,   --�²�λ
  ActReason varchar(255)             --�޸�ԭ��
)


























