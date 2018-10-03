use Pivas20140901
go

create table DeskNoUpdateLog
( 
  ID int identity(1,1) not null,     --标识列
  DEmployeeID int not null,          --员工号
  InsertDT datetime not null,        --修改时间
  LabelNo varchar(22) not null,      --瓶签号
  oldDeskNo varchar(100) not null,   --旧仓位
  newDeskNo varchar(100) not null,   --新仓位
  ActReason varchar(255)             --修改原因
)


























