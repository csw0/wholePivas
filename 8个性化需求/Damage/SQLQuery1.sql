create table Damage
(
	id int not null primary key,
	drugcode varchar(16)not null,
	spec varchar(30) not null,
	quantity int not null,
	price float not null,
	amount float not null,
	damagetime datetime not null,
	reason varchar(50) not null,
	zrrcode varchar(16),
	tbrcode varchar(16),	
	nowtime datetime
)