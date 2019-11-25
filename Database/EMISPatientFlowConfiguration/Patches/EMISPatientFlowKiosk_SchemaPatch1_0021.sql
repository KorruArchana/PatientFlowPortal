/*
Description: Update the Doctors name to Full Name in Display
Author: Aravind
Reviewer: 
Patch Number: 1.0021
Dependant Patch Number = 1.0019
*/

update t 
set MenuName = (m.Title + '. ' + m.Firstname +' '+ m.Surname)
from PatientFlow.SiteMenu t join 
PatientFlow.Member m on NodeId = MemberId
where NodeTypeId=35 