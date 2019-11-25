/*
	Description: Adding three new languages to the kiosk.
	Author: Archana
	Patch Number: 1.0039
	Dependant Patch Number = 1.0034
*/

insert into PatientFlow.Language (LanguageCode, LanguageName, TranslationRefId) values ('hu','Hungarian',76)
insert into PatientFlow.Language (LanguageCode, LanguageName, TranslationRefId) values ('cz','Czech',76)
insert into PatientFlow.Language (LanguageCode, LanguageName, TranslationRefId) values ('ne','Nepali',76) 

go
