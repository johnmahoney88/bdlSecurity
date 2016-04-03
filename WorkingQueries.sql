select  * from tb_badge where b_number_str='53788'
select  * from tb_request where RequestorBadgeNumber='53788'
select * from tb_request_escort

select * 
from tb_request r
join tb_request_escort e on e.RequestID=r.RequestID
and r.RequestorBadgeNumber='53788'


select  * from tb_badge where b_number_str='53788'
select  * from tb_request where RequestorBadgeNumber='53788'

--INSERT INTO [dbo].[tb_request_escort]
--([RequestorBadgeNumber],[RequestID],[EscortNo],[lname],[fname],[mname],[Company],[BadgeNo],[Phone],[Email],[LastUpdate])
--SELECT [RequestorBadgeNumber],[RequestID],1,[lname],[fname],[mname],'',1,[EscortPhone],null,getdate()
--  FROM [dbo].[tb_request]


[RequestorBadgeNumber], from tb_request where RequestorBadgeNumber='53788'



select * from tb_request_escort



select b_number_str,count(*) from tb_badge group by b_number_str order by 2 desc