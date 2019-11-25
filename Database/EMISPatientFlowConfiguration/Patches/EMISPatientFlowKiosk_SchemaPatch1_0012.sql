insert into [PatientFlow].[Language] ([LanguageCode], 
[LanguageName]) values ('zh', 'Chinese')
insert into [PatientFlow].[Language] ([LanguageCode], 
[LanguageName]) values ('ar', 'Arabic')
insert into [PatientFlow].[Language] ([LanguageCode], 
[LanguageName]) values ('el', 'Greek')
insert into [PatientFlow].[Language] ([LanguageCode], 
[LanguageName]) values ('hi', 'Hindi')
insert into [PatientFlow].[Language] ([LanguageCode], 
[LanguageName]) values ('it', 'Italian')
insert into [PatientFlow].[Language] ([LanguageCode], 
[LanguageName]) values ('pl', 'Polish')
insert into [PatientFlow].[Language] ([LanguageCode], 
[LanguageName]) values ('pt', 'Portuguese')
insert into [PatientFlow].[Language] ([LanguageCode], 
[LanguageName]) values ('ro', 'Romanian')
insert into [PatientFlow].[Language] ([LanguageCode], 
[LanguageName]) values ('sk', 'Slovakian')
insert into [PatientFlow].[Language] ([LanguageCode], 
[LanguageName]) values ('th', 'Thai')
insert into [PatientFlow].[Language] ([LanguageCode], 
[LanguageName]) values ('ur', 'Urdu')
insert into [PatientFlow].[Language] ([LanguageCode], 
[LanguageName]) values ('cy', 'Welsh')
insert into [PatientFlow].[Language] ([LanguageCode], 
[LanguageName]) values ('sq', 'Albanian')
insert into [PatientFlow].[Language] ([LanguageCode], 
[LanguageName]) values ('bn', 'Bengali')
insert into [PatientFlow].[Language] ([LanguageCode], 
[LanguageName]) values ('so', 'Somalian')
insert into [PatientFlow].[Language] ([LanguageCode], 
[LanguageName]) values ('pa', 'Punjabi')
insert into [PatientFlow].[Language] ([LanguageCode], 
[LanguageName]) values ('gu', 'Gujarati')
insert into [PatientFlow].[Language] ([LanguageCode], 
[LanguageName]) values ('mis', 'Iraqi')
insert into [PatientFlow].[Language] ([LanguageCode], 
[LanguageName]) values ('ku', 'Kurdish')
insert into [PatientFlow].[Language] ([LanguageCode], 
[LanguageName]) values ('fa', 'Farsi')
insert into [PatientFlow].[Language] ([LanguageCode], 
[LanguageName]) values ('ta', 'Tamil')

insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (3)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (3)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (3)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (3)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (4)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (4)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (4)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (4)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (4)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (4)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (4)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (4)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (4)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (4)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (4)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (4)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (4)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (4)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (4)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (4)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (4)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (4)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (4)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (4)
insert into [PatientFlow].[TranslationRef] ([TranslationTypeId]) values (4)

truncate table [PatientFlow].[Translation]
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 1, convert(nvarchar(max), 0x4100720072006900760061006C00), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 2, convert(nvarchar(max), 0x530075007200760065007900), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 3, convert(nvarchar(max), 0x530069007400650020004D0061007000), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 4, convert(nvarchar(max), 0x4D0061006B00650020004100700070006F0069006E0074006D0065006E007400), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 5, convert(nvarchar(max), 0x4100700070006F0069006E0074006D0065006E0074007300200046006F007200), '2015-03-16 16:33:48.827')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 6, convert(nvarchar(max), 0x43006F006E006600690072006D00), '2015-03-16 16:33:48.827')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 7, convert(nvarchar(max), 0x430061006E00630065006C00), '2015-03-16 16:33:48.827')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 8, convert(nvarchar(max), 0x53006F007200720079002000770065002000610072006500200075006E00610062006C006500200074006F002000660069006E006400200079006F00750072002000640065007400610069006C007300), '2015-03-16 16:33:48.827')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 9, convert(nvarchar(max), 0x50006C006500610073006500200063006F006E007400610063007400200052006500630065007000740069006F006E0020006F007200200043006C00690063006B0020007400680065002000620065006C006F007700200062007500740074006F006E00200074006F002000740072007900200061006700610069006E00), '2015-03-16 16:33:48.827')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 10, convert(nvarchar(max), 0x540072007900200041006700610069006E00), '2015-03-16 16:33:48.827')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 11, convert(nvarchar(max), 0x43006F006E006600690072006D00200041006C006C00), '2015-03-16 16:33:48.827')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 12, convert(nvarchar(max), 0x4500610072006C00790020004100720072006900760061006C00), '2015-03-16 16:33:48.827')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 13, convert(nvarchar(max), 0x53006F007200720079002C00200079006F007500720020006100700070006F0069006E0074006D0065006E0074002000690073002000230023002C002000770065002000610072006500200075006E00610062006C006500200074006F00200063006800650063006B00200079006F007500200069006E00200075006E00740069006C0020002300230020006D0069006E00750074006500730020006200650066006F0072006500200079006F007500720020006100700070006F0069006E0074006D0065006E0074002E002000200050006C0065006100730065002000740072007900200061006700610069006E0020006C0061007400650072002E00), '2015-03-16 16:33:48.827')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 14, convert(nvarchar(max), 0x4C0061007400650020004100720072006900760061006C00), '2015-03-16 16:33:48.827')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 15, convert(nvarchar(max), 0x53006F007200720079002C00200079006F007500720020006100700070006F0069006E0074006D0065006E0074002000740069006D00650020006F00660020002300230020006800610073002000700061007300730065006400200061006E0064002000770065002000610072006500200075006E00610062006C006500200074006F00200063006800650063006B00200079006F007500200069006E002E002000200050006C006500610073006500200063006F006E007400610063007400200072006500630065007000740069006F006E00), '2015-03-16 16:33:48.827')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 16, convert(nvarchar(max), 0x4F004B00), '2015-03-16 16:33:48.830')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 17, convert(nvarchar(max), 0x53006F007200720079002100200055006E00610062006C006500200074006F002000700072006F006300650073007300200079006F0075007200200072006500710075006500730074002E00), '2015-03-16 16:33:48.830')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 18, convert(nvarchar(max), 0x50006C00650061007300650020007200650070006F0072007400200074006F00200072006500630065007000740069006F006E00), '2015-03-16 16:33:48.830')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 19, convert(nvarchar(max), 0x50006C0065006100730065002000730065006C006500630074002000610020007000720065006600650072007200650064002000640061007900), '2015-03-24 14:20:58.307')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 20, convert(nvarchar(max), 0x53006F007200720079002000770065002000610072006500200075006E00610062006C006500200074006F002000660069006E006400200079006F00750072002000640065007400610069006C007300), '2015-03-24 14:20:58.307')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 21, convert(nvarchar(max), 0x50006C006500610073006500200063006F006E007400610063007400200052006500630065007000740069006F006E0020006F007200200043006C00690063006B0020007400680065002000620065006C006F007700200062007500740074006F006E00200074006F002000740072007900200061006700610069006E00), '2015-03-24 14:20:58.307')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 22, convert(nvarchar(max), 0x540072007900200041006700610069006E00), '2015-03-24 14:20:58.307')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 23, convert(nvarchar(max), 0x570065006C0063006F006D006500), '2015-03-24 14:20:58.307')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 24, convert(nvarchar(max), 0x50006C006500610073006500200063006F006E006600690072006D00200079006F007500720020007200650071007500650073007400), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 25, convert(nvarchar(max), 0x43006F006E006600690072006D00), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 26, convert(nvarchar(max), 0x430061006E00630065006C00), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 27, convert(nvarchar(max), 0x61007400), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 28, convert(nvarchar(max), 0x50006C0065006100730065002000730065006C00650063007400200079006F0075007200200064006100790020006F006600200062006900720074006800), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 29, convert(nvarchar(max), 0x4E006F00200053006C006F0074007300200041007600610069006C00610062006C006500), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 30, convert(nvarchar(max), 0x53006F007200720079002100200055006E00610062006C006500200074006F002000700072006F006300650073007300200079006F0075007200200072006500710075006500730074002E00), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 31, convert(nvarchar(max), 0x50006C00650061007300650020007200650070006F0072007400200074006F00200072006500630065007000740069006F006E00), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 32, convert(nvarchar(max), 0x4F004B00), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 33, convert(nvarchar(max), 0x460069006E00690073006800), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 34, convert(nvarchar(max), 0x5400680061006E006B00200059006F007500), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 35, convert(nvarchar(max), 0x59006F007500720020006100720072006900760061006C00200069007300200063006F006E006600690072006D0065006400), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 36, convert(nvarchar(max), 0x590065007300), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 37, convert(nvarchar(max), 0x4E006F00), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 38, convert(nvarchar(max), 0x44006F00200079006F0075002000770061006E007400200074006F002000740061006B006500200074006800650020007300750072007600650079003F00), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 39, convert(nvarchar(max), 0x460069006E00690073006800), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 40, convert(nvarchar(max), 0x66006F007200200079006F007500720020006100700070006F0069006E0074006D0065006E00740020007200650071007500650073007400), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 41, convert(nvarchar(max), 0x5400680061006E006B00200059006F007500), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 42, convert(nvarchar(max), 0x61007400), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 43, convert(nvarchar(max), 0x5400680061006E006B00200059006F007500), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 44, convert(nvarchar(max), 0x460069006E00690073006800), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 45, convert(nvarchar(max), 0x59006F00750072002000730075007200760065007900200068006100730020006200650065006E0020007200650063006F007200640065006400), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 46, convert(nvarchar(max), 0x50006C0065006100730065002000730065006C0065006300740020006600690072007300740020006C006500740074006500720020006F006600200079006F00750072002000660069007200730074006E0061006D006500), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 47, convert(nvarchar(max), 0x50006C0065006100730065002000730065006C0065006300740020006600690072007300740020006C006500740074006500720020006F006600200079006F007500720020007300750072006E0061006D006500), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 48, convert(nvarchar(max), 0x45006E007400650072002000640061007400650020006F006600200062006900720074006800), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 49, convert(nvarchar(max), 0x440061007900), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 50, convert(nvarchar(max), 0x4D006F006E0074006800), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 51, convert(nvarchar(max), 0x5900650061007200), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 52, convert(nvarchar(max), 0x4E00650078007400), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 53, convert(nvarchar(max), 0x50006C0065006100730065002000730065006C00650063007400200079006F00750072002000670065006E00640065007200), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 54, convert(nvarchar(max), 0x4D0061006C006500), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 55, convert(nvarchar(max), 0x460065006D0061006C006500), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 56, convert(nvarchar(max), 0x53006B0069007000), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 57, convert(nvarchar(max), 0x4B0049004F0053004B002000480069006200650072006E006100740069006E0067002E002E002E002E00), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 58, convert(nvarchar(max), 0x540069006D0069006E00670020004F0075007400200049006E002E002E002E00), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 59, convert(nvarchar(max), 0x570065006C0063006F006D006500200054006F00), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 60, convert(nvarchar(max), 0x730065006C006500630074002000610020006C0061006E0067007500610067006500200074006F00200073007400610072007400), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 61, convert(nvarchar(max), 0x50006C0065006100730065002000730065006C00650063007400200061006E0020006F007000740069006F006E00), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 62, convert(nvarchar(max), 0x50006C0065006100730065002000730065006C00650063007400200079006F007500720020006D006F006E007400680020006F006600200062006900720074006800), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 63, convert(nvarchar(max), 0x54006F002000730074006100720074002C00200070006C0065006100730065002000730065006C0065006300740020006F006E00650020006F006600200074006800650020006F007000740069006F006E0073002000620065006C006F007700), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 64, convert(nvarchar(max), 0x570065006C0063006F006D006500200054006F00), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 65, convert(nvarchar(max), 0x50006C0065006100730065002000730065006C0065006300740020006100200073006C006F00740020007400790070006500), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 66, convert(nvarchar(max), 0x4E00650078007400), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 67, convert(nvarchar(max), 0x50006C006500610073006500200061006E0073007700650072002000740068006500200066006F006C006C006F00770069006E00670020007100750065007300740069006F006E007300), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 68, convert(nvarchar(max), 0x53006B0069007000), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 69, convert(nvarchar(max), 0x50006C0065006100730065002000630068006F006F0073006500200061006E0020006F007000740069006F006E00), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 70, convert(nvarchar(max), 0x53007500720076006500790073002000630075007200720065006E0074006C007900200075006E0061007600610069006C00610062006C006500), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 71, convert(nvarchar(max), 0x50006C0065006100730065002000730065006C00650063007400200079006F00750072002000790065006100720020006F006600200062006900720074006800), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 72, convert(nvarchar(max), 0x4F004B00), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 73, convert(nvarchar(max), 0x4E006F006E00650020006F00660020007400680065002000410062006F0076006500), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 74, convert(nvarchar(max), 0x590065006100720020004E006F007400200046006F0075006E006400), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 75, convert(nvarchar(max), 0x50006C006500610073006500200063006F006E007400610063007400200052006500630065007000740069006F006E00), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 76, convert(nvarchar(max), 0x45006E0067006C00690073006800), '2015-03-16 15:38:59.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 82, convert(nvarchar(max), 0x4D006500730073006100670065007300), '2015-04-14 04:10:48.553')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 83, convert(nvarchar(max), 0x4E00650078007400), '2015-04-14 04:10:48.553')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 84, convert(nvarchar(max), 0x50006C0065006100730065002000730065006C00650063007400200079006F00750072002000790065006100720020006F006600200062006900720074006800), '2015-04-07 12:54:49.503')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (1, 85, convert(nvarchar(max), 0x4E00650078007400), '2015-04-07 12:54:49.507')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 1, convert(nvarchar(max), 0x4C006C0065006700610064006100), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 2, convert(nvarchar(max), 0x4500730074007500640069006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 3, convert(nvarchar(max), 0x4D006100700061002000640065006C00200053006900740069006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 4, convert(nvarchar(max), 0x48006100630065007200200075006E00610020004300690074006100), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 5, convert(nvarchar(max), 0x4C006100730020006300690074006100730020007000610072006100), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 6, convert(nvarchar(max), 0x43006F006E006600690072006D0061007200), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 7, convert(nvarchar(max), 0x430061006E00630065006C0061007200), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 8, convert(nvarchar(max), 0x4C006F002000730065006E00740069006D006F00730020006E006F00200073006F006D006F00730020006300610070006100630065007300200064006500200065006E0063006F006E007400720061007200200073007500730020006400610074006F007300), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 9, convert(nvarchar(max), 0x50006F00720020006600610076006F0072002C0020007000F3006E006700610073006500200065006E00200063006F006E0074006100630074006F00200063006F006E0020006C00610020007200650063006500700063006900F3006E0020006F0020004800610067006100200063006C0069006300200065006E00200065006C00200062006F007400F3006E0020006400650020006100620061006A006F0020007000610072006100200076006F006C0076006500720020006100200069006E00740065006E007400610072006C006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 10, convert(nvarchar(max), 0x49006E007400E9006E00740061006C006F0020004400650020004E007500650076006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 11, convert(nvarchar(max), 0x43006F006E006600690072006D006500200054006F0064006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 12, convert(nvarchar(max), 0x410020007000720069006E0063006900700069006F00730020006400650020006C006C0065006700610064006100), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 13, convert(nvarchar(max), 0x4C006F002000730065006E00740069006D006F0073002C00200073007500200063006900740061002000650073002000230023002C0020006E006F00200070006F00640065006D006F007300200063006F006D00700072006F006200610072002000710075006500200065006E0020006800610073007400610020002300230020006D0069006E00750074006F007300200061006E00740065007300200064006500200073007500200063006900740061002E00200049006E007400C300A9006E00740061006C006F0020006400650020006E007500650076006F0020006D00C300A10073002000740061007200640065002E00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 14, convert(nvarchar(max), 0x4C006C006500670061006400610020007400610072006400ED006100), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 15, convert(nvarchar(max), 0x4C006F0020007300690065006E0074006F002C0020006C006100200068006F007200610020006400650020006C006100200063006900740061002000640065002000230023002000680061002000700061007300610064006F0020007900200065007300740061006D006F00730020006E006F00200073006500200070007500650064006500200063006F006D00700072006F006200610072002E00200050006F00720020006600610076006F00720020007000C300B3006E006700610073006500200065006E00200063006F006E0074006100630074006F00200063006F006E0020006C00610020007200650063006500700063006900C300B3006E00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 16, convert(nvarchar(max), 0x4F004B0041005900), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 17, convert(nvarchar(max), 0xA1004C006F0020007300690065006E0074006F0021002000), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 18, convert(nvarchar(max), 0x50006F00720020006600610076006F0072002C00200069006E0066006F0072006D0065002000610020006C00610020007200650063006500700063006900F3006E00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 19, convert(nvarchar(max), 0x530065006C0065006300630069006F006E006500200075006E0020006400ED0061002000700072006500660065007200690064006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 20, convert(nvarchar(max), 0x4C006F002000730065006E00740069006D006F00730020006E006F00200073006F006D006F00730020006300610070006100630065007300200064006500200065006E0063006F006E007400720061007200200073007500730020006400610074006F007300), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 21, convert(nvarchar(max), 0x50006F00720020006600610076006F0072002C0020007000F3006E006700610073006500200065006E00200063006F006E0074006100630074006F00200063006F006E0020006C00610020007200650063006500700063006900F3006E0020006F0020004800610067006100200063006C0069006300200065006E00200065006C00200062006F007400F3006E0020006400650020006100620061006A006F0020007000610072006100200076006F006C0076006500720020006100200069006E00740065006E007400610072006C006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 22, convert(nvarchar(max), 0x49006E007400E9006E00740061006C006F0020004400650020004E007500650076006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 23, convert(nvarchar(max), 0x4200690065006E00760065006E00690064006100), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 24, convert(nvarchar(max), 0x50006F00720020006600610076006F0072002C00200063006F006E006600690072006D006500200073007500200073006F006C00690063006900740075006400), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 25, convert(nvarchar(max), 0x43006F006E006600690072006D0061007200), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 26, convert(nvarchar(max), 0x430061006E00630065006C0061007200), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 27, convert(nvarchar(max), 0x65006E00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 28, convert(nvarchar(max), 0x50006F00720020006600610076006F0072002000730065006C0065006300630069006F006E00650020007300750020006600650063006800610020006400650020006E006100630069006D00690065006E0074006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 29, convert(nvarchar(max), 0x4E006F0020006800610079002000720061006E007500720061007300200064006900730070006F006E00690062006C0065007300), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 30, convert(nvarchar(max), 0xA1004C006F0020007300690065006E0074006F0021002000), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 31, convert(nvarchar(max), 0x50006F00720020006600610076006F0072002C00200069006E0066006F0072006D0065002000610020006C00610020007200650063006500700063006900F3006E00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 32, convert(nvarchar(max), 0x4F004B0041005900), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 33, convert(nvarchar(max), 0x4100630061006200610064006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 34, convert(nvarchar(max), 0x4700720061006300690061007300), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 35, convert(nvarchar(max), 0x5300750020006C006C0065006700610064006100200073006500200063006F006E006600690072006D006100), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 36, convert(nvarchar(max), 0x5300ED00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 37, convert(nvarchar(max), 0x4E006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 38, convert(nvarchar(max), 0xBF00510075006900650072006500730020007000610072007400690063006900700061007200200065006E0020006C006100200065006E006300750065007300740061003F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 39, convert(nvarchar(max), 0x4100630061006200610064006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 40, convert(nvarchar(max), 0x7000610072006100200073007500200073006F006C0069006300690074007500640020006400650020006300690074006100), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 41, convert(nvarchar(max), 0x4700720061006300690061007300), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 42, convert(nvarchar(max), 0x65006E00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 43, convert(nvarchar(max), 0x4700720061006300690061007300), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 44, convert(nvarchar(max), 0x4100630061006200610064006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 45, convert(nvarchar(max), 0x5300750020006500730074007500640069006F0020006800610020007300690064006F0020007200650067006900730074007200610064006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 46, convert(nvarchar(max), 0x50006F00720020006600610076006F0072002C002000730065006C0065006300630069006F006E00650020006C00610020007000720069006D0065007200610020006C00650074007200610020006400650020007300750020006100700065006C006C00690064006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 47, convert(nvarchar(max), 0x50006F00720020006600610076006F0072002C002000730065006C0065006300630069006F006E00650020006C00610020007000720069006D0065007200610020006C00650074007200610020006400650020007300750020006100700065006C006C00690064006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 48, convert(nvarchar(max), 0x49006E006700720065007300650020006C00610020006600650063006800610020006400650020006E006100630069006D00690065006E0074006F00), '2015-04-08 04:35:44.197')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 49, convert(nvarchar(max), 0x4400ED006100), '2015-04-08 04:35:44.197')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 50, convert(nvarchar(max), 0x4D0065007300), '2015-04-08 04:35:44.197')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 51, convert(nvarchar(max), 0x4100F1006F00), '2015-04-08 04:35:44.197')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 52, convert(nvarchar(max), 0x5300690067007500690065006E0074006500), '2015-04-08 04:35:44.197')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 53, convert(nvarchar(max), 0x50006F00720020006600610076006F0072002000730065006C0065006300630069006F006E00650020007300750020006700E9006E00650072006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 54, convert(nvarchar(max), 0x4D0061007300630075006C0069006E006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 55, convert(nvarchar(max), 0x460065006D0065006E0069006E006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 56, convert(nvarchar(max), 0x4F006D006900740069007200), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 57, convert(nvarchar(max), 0x5100550049004F00530043004F002000480069006200650072006E006100740069006E00670020002E002E002E002E00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 58, convert(nvarchar(max), 0x45006E00200065006C0020007400690065006D0070006F00200064006500200065007300700065007200610020002E002E002E00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 59, convert(nvarchar(max), 0x4200690065006E00760065006E00690064006F0020006100), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 60, convert(nvarchar(max), 0x730065006C0065006300630069006F006E0061007200200075006E0020006900640069006F006D00610020007000610072006100200063006F006D0065006E007A0061007200), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 61, convert(nvarchar(max), 0x50006F00720020006600610076006F0072002000730065006C0065006300630069006F006E006500200075006E00610020006F00700063006900F3006E00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 62, convert(nvarchar(max), 0x50006F00720020006600610076006F0072002000730065006C0065006300630069006F006E00650020007300750020006D006500730020006400650020006E006100630069006D00690065006E0074006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 63, convert(nvarchar(max), 0x5000610072006100200065006D00700065007A00610072002C00200070006F00720020006600610076006F0072002000730065006C0065006300630069006F006E006500200075006E00610020006400650020006C006100730020007300690067007500690065006E0074006500730020006F007000630069006F006E0065007300), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 64, convert(nvarchar(max), 0x4200690065006E00760065006E00690064006F0020006100), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 65, convert(nvarchar(max), 0x530065006C0065006300630069006F006E006500200075006E0020007400690070006F002000640065002000720061006E00750072006100), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 66, convert(nvarchar(max), 0x5300690067007500690065006E0074006500), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 67, convert(nvarchar(max), 0x50006F00720020006600610076006F007200200072006500730070006F006E00640061002000610020006C006100730020007300690067007500690065006E007400650073002000700072006500670075006E00740061007300), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 68, convert(nvarchar(max), 0x4F006D006900740069007200), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 69, convert(nvarchar(max), 0x50006F00720020006600610076006F007200200065006C0069006A006100200075006E00610020006F00700063006900F3006E00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 70, convert(nvarchar(max), 0x45006E006300750065007300740061007300200064006900730070006F006E00690062006C006500200065006E002000650073007400650020006D006F006D0065006E0074006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 71, convert(nvarchar(max), 0x50006F00720020006600610076006F0072002000730065006C0065006300630069006F006E00650020007300750020006100F1006F0020006400650020006E006100630069006D00690065006E0074006F00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 72, convert(nvarchar(max), 0x4F004B0041005900), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 73, convert(nvarchar(max), 0x4E0069006E00670075006E00610020006400650020006C0061007300200061006E0074006500720069006F00720065007300), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 74, convert(nvarchar(max), 0x4100F1006F0020004E006F007400200046006F0075006E006400), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 75, convert(nvarchar(max), 0x50006F00720020006600610076006F0072002C0020007000F3006E006700610073006500200065006E00200063006F006E0074006100630074006F00200063006F006E0020007200650063006500700063006900F3006E00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (2, 77, convert(nvarchar(max), 0x6500730070006100F1006F006C00), '2015-03-16 15:44:06.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 1, convert(nvarchar(max), 0x41007200720069007600E9006500), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 2, convert(nvarchar(max), 0x45006E0071007500EA0074006500), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 3, convert(nvarchar(max), 0x50006C0061006E0020006400750020007300690074006500), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 4, convert(nvarchar(max), 0x5000720065006E0065007A00200075006E002000720065006E00640065007A002D00), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 5, convert(nvarchar(max), 0x520065006E00640065007A002D0076006F0075007300200070006F0075007200), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 6, convert(nvarchar(max), 0x43006F006E006600690072006D0065007200), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 7, convert(nvarchar(max), 0x41006E006E0075006C0065007200), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 8, convert(nvarchar(max), 0x4400E90073006F006C00E9002C0020006E006F0075007300200073006F006D006D0065007300200069006E00630061007000610062006C00650073002000640065002000740072006F007500760065007200200076006F007300200063006F006F00720064006F006E006E00E90065007300), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 9, convert(nvarchar(max), 0x53006500200069006C00200076006F0075007300200070006C006100EE007400200063006F006E0074006100630074006500720020006C00610020007200E900630065007000740069006F006E0020006F007500200043006C0069007100750065007A00200073007500720020006C006500200062006F00750074006F006E002000630069002D0064006500730073006F0075007300200070006F0075007200200065007300730061007900650072002000E00020006E006F0075007600650061007500), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 10, convert(nvarchar(max), 0x45007300730061007900650072002000C00020004E006F0075007600650061007500), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 11, convert(nvarchar(max), 0x43006F006E006600690072006D0065007A00200054006F0075007300), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 12, convert(nvarchar(max), 0x41007200720069007600E900650020007400F4007400), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 13, convert(nvarchar(max), 0x4400C300A90073006F006C00C300A9002C00200076006F007400720065002000720065006E00640065007A002D0076006F007500730020006500730074002000230023002C0020006E006F0075007300200073006F006D006D0065007300200069006E00630061007000610062006C0065007300200064006500200076006F0075007300200065006E0072006500670069007300740072006500720020006A0075007300710075002700C300A00020002300230020006D0069006E00750074006500730020006100760061006E007400200076006F007400720065002000720065006E00640065007A002D0076006F00750073002E00200053006500200069006C00200076006F0075007300200070006C006100C300AE00740020007200C300A9006500730073006100790065007200200070006C0075007300200074006100720064002E00), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 14, convert(nvarchar(max), 0x41007200720069007600E900650020007400610072006400690076006500), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 15, convert(nvarchar(max), 0x4400C300A90073006F006C00C300A9002C00200076006F007400720065002000720065006E00640065007A002D0076006F00750073002000640065002000230023002000610020007000610073007300C300A90020006500740020006E006F0075007300200073006F006D006D0065007300200069006E00630061007000610062006C006500730020006400650020007600C300A900720069006600690065007200200065006E00200076006F00750073002E00200053006500200069006C00200076006F0075007300200070006C006100C300AE007400200063006F006E0074006100630074006500720020006C00610020007200C300A900630065007000740069006F006E00), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 16, convert(nvarchar(max), 0x440027004100430043004F0052004400), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 17, convert(nvarchar(max), 0x4400E90073006F006C00E90021002000), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 18, convert(nvarchar(max), 0x53006500200069006C00200076006F0075007300200070006C006100EE007400200070007200E900730065006E007400650072002000E00020006C0027006100630063007500650069006C00), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 19, convert(nvarchar(max), 0x53006500200069006C00200076006F0075007300200070006C006100C300AE00740020007300C300A9006C0065006300740069006F006E006E0065007200200075006E0020006A006F0075007200200070007200C300A9006600C300A9007200C300A900), '2015-04-13 14:04:33.283')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 20, convert(nvarchar(max), 0x4400E90073006F006C00E9002C0020006E006F0075007300200073006F006D006D0065007300200069006E00630061007000610062006C00650073002000640065002000740072006F007500760065007200200076006F007300200063006F006F00720064006F006E006E00E90065007300), '2015-04-13 14:04:33.283')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 21, convert(nvarchar(max), 0x53006500200069006C00200076006F0075007300200070006C006100EE007400200063006F006E0074006100630074006500720020006C00610020007200E900630065007000740069006F006E0020006F007500200043006C0069007100750065007A00200073007500720020006C006500200062006F00750074006F006E002000630069002D0064006500730073006F0075007300200070006F0075007200200065007300730061007900650072002000E00020006E006F0075007600650061007500), '2015-04-13 14:04:33.283')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 22, convert(nvarchar(max), 0x45007300730061007900650072002000C00020004E006F00750076006500), '2015-04-13 14:04:33.283')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 23, convert(nvarchar(max), 0x4200690065006E00760065006E0075006500), '2015-04-13 14:04:33.283')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 24, convert(nvarchar(max), 0x53006500200069006C00200076006F0075007300200070006C006100EE007400200063006F006E006600690072006D0065007200200076006F007400720065002000640065006D0061006E0064006500), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 25, convert(nvarchar(max), 0x43006F006E006600690072006D0065007200), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 26, convert(nvarchar(max), 0x41006E006E0075006C0065007200), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 27, convert(nvarchar(max), 0xE000), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 28, convert(nvarchar(max), 0x53006500200069006C00200076006F0075007300200070006C006100EE00740020007300E9006C0065006300740069006F006E006E0065007A00200076006F0074007200650020006A006F007500720020006400650020006E00610069007300730061006E0063006500), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 29, convert(nvarchar(max), 0x500061007300200064006500200063007200E9006E006500610075007800200064006900730070006F006E00690062006C0065007300), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 30, convert(nvarchar(max), 0x4400E90073006F006C00E90021002000), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 31, convert(nvarchar(max), 0x53006500200069006C00200076006F0075007300200070006C006100EE007400200070007200E900730065006E007400650072002000E00020006C0027006100630063007500650069006C00), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 32, convert(nvarchar(max), 0x440027004100430043004F0052004400), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 33, convert(nvarchar(max), 0x460069006E006900740069006F006E00), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 34, convert(nvarchar(max), 0x4D006500720063006900), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 35, convert(nvarchar(max), 0x56006F00740072006500200061007200720069007600E90065002000650073007400200063006F006E006600690072006D00E9006500), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 36, convert(nvarchar(max), 0x4F0075006900), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 37, convert(nvarchar(max), 0x41007500630075006E00), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 38, convert(nvarchar(max), 0x56006F0075006C0065007A002D0076006F0075007300200070006100720074006900630069007000650072002000E00020006C00270065006E0071007500EA00740065003F00), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 39, convert(nvarchar(max), 0x460069006E006900740069006F006E00), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 40, convert(nvarchar(max), 0x70006F0075007200200076006F007400720065002000640065006D0061006E00640065002000640065002000720065006E00640065007A002D00), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 41, convert(nvarchar(max), 0x4D006500720063006900), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 42, convert(nvarchar(max), 0xE000), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 43, convert(nvarchar(max), 0x4D006500720063006900), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 44, convert(nvarchar(max), 0x460069006E006900740069006F006E00), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 45, convert(nvarchar(max), 0x56006F00740072006500200065006E0071007500EA0074006500200061002000E9007400E900200065006E007200650067006900730074007200E9006500), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 46, convert(nvarchar(max), 0x53006500200069006C00200076006F0075007300200070006C006100EE00740020007300E9006C0065006300740069006F006E006E0065007A0020007000720065006D006900E8007200650020006C0065007400740072006500200064006500200076006F00740072006500200070007200E9006E006F006D00), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 47, convert(nvarchar(max), 0x53006500200069006C00200076006F0075007300200070006C006100EE00740020007300E9006C0065006300740069006F006E006E0065007A0020007000720065006D006900E8007200650020006C0065007400740072006500200064006500200076006F0074007200650020006E006F006D00), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 48, convert(nvarchar(max), 0x45006E007400720065007A0020006C0061002000640061007400650020006400650020006E00610069007300730061006E0063006500), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 49, convert(nvarchar(max), 0x4A006F0075007200), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 50, convert(nvarchar(max), 0x4D006F0069007300), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 51, convert(nvarchar(max), 0x41006E006E00E9006500), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 52, convert(nvarchar(max), 0x530075006900760061006E007400), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 53, convert(nvarchar(max), 0x53006500200069006C00200076006F0075007300200070006C006100EE00740020007300E9006C0065006300740069006F006E006E0065007A00200076006F0074007200650020007300650078006500), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 54, convert(nvarchar(max), 0x4D00E2006C006500), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 55, convert(nvarchar(max), 0x460065006D0065006C006C006500), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 56, convert(nvarchar(max), 0x530061007500740065007200), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 57, convert(nvarchar(max), 0x4B0049004F0053004B002000680069006200650072006E006100740069006F006E0020002E002E002E002E00), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 58, convert(nvarchar(max), 0x540069006D0069006E00670020004F0075007400200049006E0020002E002E002E00), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 59, convert(nvarchar(max), 0x4200690065006E00760065006E00750065002000E000), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 60, convert(nvarchar(max), 0x7300E9006C0065006300740069006F006E006E0065007200200075006E00650020006C0061006E00670075006500200070006F0075007200200063006F006D006D0065006E00630065007200), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 61, convert(nvarchar(max), 0x53006500200069006C00200076006F0075007300200070006C006100EE00740020007300E9006C0065006300740069006F006E006E0065007200200075006E00650020006F007000740069006F006E00), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 62, convert(nvarchar(max), 0x53006500200069006C00200076006F0075007300200070006C006100EE00740020007300E9006C0065006300740069006F006E006E0065007A00200076006F0074007200650020006D006F006900730020006400650020006E00610069007300730061006E0063006500), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 63, convert(nvarchar(max), 0x50006F0075007200200063006F006D006D0065006E006300650072002C00200073006500200069006C00200076006F0075007300200070006C006100EE00740020007300E9006C0065006300740069006F006E006E0065007A0020006C00270075006E006500200064006500730020006F007000740069006F006E0073002000630069002D0064006500730073006F0075007300), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 64, convert(nvarchar(max), 0x4200690065006E00760065006E00750065002000E000), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 65, convert(nvarchar(max), 0x53006500200069006C00200076006F0075007300200070006C006100EE00740020007300E9006C0065006300740069006F006E006E0065007200200075006E002000740079007000650020006400650020006C006F00670065006D0065006E007400), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 66, convert(nvarchar(max), 0x530075006900760061006E007400), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 67, convert(nvarchar(max), 0x53006500200069006C00200076006F0075007300200070006C006100EE00740020007200E90070006F006E00640072006500200061007500780020007100750065007300740069006F006E0073002000730075006900760061006E00740065007300), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 68, convert(nvarchar(max), 0x530061007500740065007200), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 69, convert(nvarchar(max), 0x53006500200069006C00200076006F0075007300200070006C006100EE0074002000630068006F006900730069007200200075006E00650020006F007000740069006F006E00), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 70, convert(nvarchar(max), 0x45006E0071007500EA007400650073002000610063007400750065006C006C0065006D0065006E007400200069006E0064006900730070006F006E00690062006C0065007300), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 71, convert(nvarchar(max), 0x53006500200069006C00200076006F0075007300200070006C006100EE00740020007300E9006C0065006300740069006F006E006E0065007200200076006F00740072006500200061006E006E00E900650020006400650020006E00610069007300730061006E0063006500), '2015-04-07 12:18:19.447')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 72, convert(nvarchar(max), 0x440027004100430043004F0052004400), '2015-04-07 12:18:19.447')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 73, convert(nvarchar(max), 0x41007500630075006E006500200064006500200063006500730020007200E90070006F006E00730065007300), '2015-04-07 12:18:19.447')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 74, convert(nvarchar(max), 0x41006E006E00E9006500200049006E00740072006F0075007600610062006C006500), '2015-04-07 12:18:19.447')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 75, convert(nvarchar(max), 0x53006500200069006C00200076006F0075007300200070006C006100EE007400200063006F006E0074006100630074006500720020006C00610020007200E900630065007000740069006F006E00), '2015-04-07 12:18:19.447')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 78, convert(nvarchar(max), 0x6600720061006E00E700610069007300), '2015-03-16 15:49:13.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 84, convert(nvarchar(max), 0x53006500200069006C00200076006F0075007300200070006C006100EE00740020007300E9006C0065006300740069006F006E006E0065007200200076006F00740072006500200061006E006E00E900650020006400650020006E00610069007300730061006E0063006500), '2015-04-07 12:18:10.753')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (3, 85, convert(nvarchar(max), 0x410075002000530075006900760061006E007400), '2015-04-07 12:18:10.753')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 1, convert(nvarchar(max), 0x41006E006B0075006E0066007400), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 2, convert(nvarchar(max), 0x55006D0066007200610067006500), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 3, convert(nvarchar(max), 0x53006900740065006D0061007000), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 4, convert(nvarchar(max), 0x5400650072006D0069006E002000760065007200650069006E0062006100720065006E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 5, convert(nvarchar(max), 0x5400650072006D0069006E00650020006600FC007200), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 6, convert(nvarchar(max), 0x4200650073007400E40074006900670065006E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 7, convert(nvarchar(max), 0x5200FC0063006B006700E4006E0067006900670020004D0061006300680065006E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 8, convert(nvarchar(max), 0x54007200610075007200690067002000730069006E006400200077006900720020006E006900630068007400200069006E00200064006500720020004C006100670065002C0020004900680072006500200044006100740065006E0020007A0075002000660069006E00640065006E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 9, convert(nvarchar(max), 0x4200690074007400650020006B006F006E00740061006B00740069006500720065006E002000520065007A0065007000740069006F006E0020006F0064006500720020004B006C00690063006B0065006E00200053006900650020006100750066002000640069006500200053006300680061006C00740066006C00E400630068006500200075006E00740065006E002C00200075006D002000650073002000650072006E0065007500740020007A0075002000760065007200730075006300680065006E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 10, convert(nvarchar(max), 0x450072006E006500750074002000560065007200730075006300680065006E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 11, convert(nvarchar(max), 0x4200650073007400E40074006900670065006E002000530069006500200061006C006C006500), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 12, convert(nvarchar(max), 0x46007200FC0068006500200041006E0072006500690073006500), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 13, convert(nvarchar(max), 0x4C0065006900640065007200200068006100620065006E0020007700690072002000640065006E0020005400650072006D0069006E002000230023002C00200077006900720020006E006900630068007400200069006D007300740061006E00640065002000730069006E0064002C00200069006D0020005A0069006D006D0065007200200062006900730020002300230020004D0069006E007500740065006E00200076006F0072002000640065006D0020005400650072006D0069006E002E002000420069007400740065002000760065007200730075006300680065006E002000530069006500200065007300200073007000C300A40074006500720020006E006F00630068002000650069006E006D0061006C002E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 14, convert(nvarchar(max), 0x56006500720073007000E400740075006E006700), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 15, convert(nvarchar(max), 0x4C00650069006400650072002000690073007400200049006800720020005400650072006D0069006E00200076006F006E002000230023002000760065007200670061006E00670065006E0020006900730074002C00200075006E00640020007700690072002000730069006E00640020006E006900630068007400200069006E00200064006500720020004C006100670065002C00200069006D0020005A0069006D006D00650072002E0020004200690074007400650020006B006F006E00740061006B00740069006500720065006E00200053006900650020006400690065002000520065007A0065007000740069006F006E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 16, convert(nvarchar(max), 0x4F004B00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 17, convert(nvarchar(max), 0x450073002000740075007400200075006E00730020006C0065006900640021002000), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 18, convert(nvarchar(max), 0x4200690074007400650020006D0065006C00640065006E002000530069006500200061006E0020006400650072002000520065007A0065007000740069006F006E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 19, convert(nvarchar(max), 0x4200690074007400650020007700E40068006C0065006E0020005300690065002000650069006E0065006E002000), '2015-04-07 10:54:40.827')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 20, convert(nvarchar(max), 0x54007200610075007200690067002000730069006E006400200077006900720020006E006900630068007400200069006E00200064006500720020004C006100670065002C0020004900680072006500200044006100740065006E0020007A0075002000660069006E00640065006E00), '2015-04-07 10:54:40.827')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 21, convert(nvarchar(max), 0x4200690074007400650020006B006F006E00740061006B00740069006500720065006E002000520065007A0065007000740069006F006E0020006F0064006500720020004B006C00690063006B0065006E00200053006900650020006100750066002000640069006500200053006300680061006C00740066006C00E400630068006500200075006E00740065006E002C00200075006D002000650073002000650072006E0065007500740020007A0075002000760065007200730075006300680065006E00), '2015-04-07 10:54:40.827')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 22, convert(nvarchar(max), 0x450072006E006500750074002000560065007200730075006300680065006E00), '2015-04-07 10:54:40.827')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 23, convert(nvarchar(max), 0x4800650072007A006C006900630068002000570069006C006C006B006F006D006D0065006E00), '2015-04-07 10:54:40.830')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 24, convert(nvarchar(max), 0x4200690074007400650020006200650073007400E40074006900670065006E00200053006900650020004900680072006500200041006E0066007200610067006500), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 25, convert(nvarchar(max), 0x4200650073007400E40074006900670065006E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 26, convert(nvarchar(max), 0x5200FC0063006B006700E4006E0067006900670020004D0061006300680065006E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 27, convert(nvarchar(max), 0x620065006900), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 28, convert(nvarchar(max), 0x5700E40068006C0065006E0020005300690065002000650069006E0065002000640065007200200054006100670020006400650072002000470065006200750072007400), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 29, convert(nvarchar(max), 0x4B00650069006E006500200053006C006F007400730020007600650072006600FC006700620061007200), '2015-03-20 14:49:33.407')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 30, convert(nvarchar(max), 0x450073002000740075007400200075006E00730020006C0065006900640021002000), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 31, convert(nvarchar(max), 0x4200690074007400650020006D0065006C00640065006E002000530069006500200061006E0020006400650072002000520065007A0065007000740069006F006E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 32, convert(nvarchar(max), 0x4F004B00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 33, convert(nvarchar(max), 0x460069006E00690073006800), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 34, convert(nvarchar(max), 0x440061006E006B006500), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 35, convert(nvarchar(max), 0x4900680072006500200041006E006B0075006E00660074002000770069007200640020006200650073007400E4007400690067007400), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 36, convert(nvarchar(max), 0x4A006100), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 37, convert(nvarchar(max), 0x4B00650069006E006500), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 38, convert(nvarchar(max), 0x4D00F60063006800740065006E002000530069006500200061006E002000640065007200200055006D006600720061006700650020007400650069006C006E00650068006D0065006E003F00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 39, convert(nvarchar(max), 0x460069006E00690073006800), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 40, convert(nvarchar(max), 0x6600FC0072002000490068007200650020005400650072006D0069006E0061006E0066007200610067006500), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 41, convert(nvarchar(max), 0x440061006E006B006500), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 42, convert(nvarchar(max), 0x620065006900), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 43, convert(nvarchar(max), 0x440061006E006B006500), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 44, convert(nvarchar(max), 0x460069006E00690073006800), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 45, convert(nvarchar(max), 0x49006800720065002000530074007500640069006500200077007500720064006500200067006500730070006500690063006800650072007400), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 46, convert(nvarchar(max), 0x5700E40068006C0065006E00200041006E00660061006E00670073006200750063006800730074006100620065006E00200049006800720065007300200056006F0072006E0061006D006500), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 47, convert(nvarchar(max), 0x5700E40068006C0065006E00200041006E00660061006E00670073006200750063006800730074006100620065006E0020004900680072006500730020004E006100630068006E0061006D0065006E007300), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 48, convert(nvarchar(max), 0x47006500620065006E00200053006900650020006400610073002000470065006200750072007400730064006100740075006D00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 49, convert(nvarchar(max), 0x540061006700), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 50, convert(nvarchar(max), 0x4D006F006E0061007400), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 51, convert(nvarchar(max), 0x4A00610068007200), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 52, convert(nvarchar(max), 0x41006C00730020004E00E400630068007300740065007300), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 53, convert(nvarchar(max), 0x4200690074007400650020007700E40068006C0065006E00200053006900650020004900680072002000470065007300630068006C006500630068007400), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 54, convert(nvarchar(max), 0x4D00E4006E006E006C00690063006800), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 55, convert(nvarchar(max), 0x57006500690062006C00690063006800), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 56, convert(nvarchar(max), 0xDC0062006500720073007000720069006E00670065006E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 57, convert(nvarchar(max), 0x4B0049004F0053004B002000570069006E007400650072007300630068006C006100660020002E002E002E002E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 58, convert(nvarchar(max), 0x5A0065006900740070006C0061006E002C002000640069006500200069006D0020002E002E002E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 59, convert(nvarchar(max), 0x570069006C006C006B006F006D006D0065006E002000420065006900), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 60, convert(nvarchar(max), 0x7700E40068006C0065006E0020005300690065002000650069006E0065002000530070007200610063006800650020007A00750020007300740061007200740065006E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 61, convert(nvarchar(max), 0x4200690074007400650020007700E40068006C0065006E0020005300690065002000650069006E00650020004F007000740069006F006E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 62, convert(nvarchar(max), 0x5700E40068006C0065006E0020005300690065002000650069006E0065002000640065007200200047006500620075007200740073006D006F006E0061007400), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 63, convert(nvarchar(max), 0x55006D0020007A007500200062006500670069006E006E0065006E002C0020007700E40068006C0065006E0020005300690065002000620069007400740065002000650069006E0065002000640065007200200066006F006C00670065006E00640065006E0020004F007000740069006F006E0065006E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 64, convert(nvarchar(max), 0x570069006C006C006B006F006D006D0065006E002000420065006900), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 65, convert(nvarchar(max), 0x4200690074007400650020007700E40068006C0065006E0020005300690065002000650069006E0065006E00200053006C006F0074002000540079007000), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 66, convert(nvarchar(max), 0x41006C00730020004E00E400630068007300740065007300), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 67, convert(nvarchar(max), 0x4200690074007400650020006200650061006E00740077006F007200740065006E0020005300690065002000640069006500200066006F006C00670065006E00640065006E002000460072006100670065006E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 68, convert(nvarchar(max), 0xDC0062006500720073007000720069006E00670065006E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 69, convert(nvarchar(max), 0x4200690074007400650020007700E40068006C0065006E0020005300690065002000650069006E00650020004F007000740069006F006E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 70, convert(nvarchar(max), 0x55006D00660072006100670065006E0020006400650072007A0065006900740020006E00690063006800740020007600650072006600FC006700620061007200), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 71, convert(nvarchar(max), 0x4200690074007400650020007700E40068006C0065006E0020005300690065002000490068007200200047006500620075007200740073006A00610068007200), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 72, convert(nvarchar(max), 0x4F004B00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 73, convert(nvarchar(max), 0x4E0069006300680074007300200064006500730020006F00620065006E002000470065006E0061006E006E00740065006E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 74, convert(nvarchar(max), 0x4A0061006800720020006E006900630068007400200067006500660075006E00640065006E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 75, convert(nvarchar(max), 0x4200690074007400650020006B006F006E00740061006B00740069006500720065006E002000520065007A0065007000740069006F006E00), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (4, 79, convert(nvarchar(max), 0x4400650075007400730063006800), '2015-03-16 15:54:19.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 1, convert(nvarchar(max), 0x1F044004380431044B04420438043504), '2015-04-07 12:02:03.227')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 2, convert(nvarchar(max), 0x1E04310437043E044004), '2015-04-07 12:02:03.227')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 3, convert(nvarchar(max), 0x1A043004400442043004200041043004390442043004), '2015-04-07 12:02:03.227')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 4, convert(nvarchar(max), 0x1D04300437043D0430044704380442044C0420001204410442044004350447044304), '2015-04-07 12:02:03.227')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 5, convert(nvarchar(max), 0x14043E043B0436043D043E04410442043804200037043004), '2015-04-07 11:59:44.400')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 6, convert(nvarchar(max), 0x1F043E04340442043204350440043404380442044C04), '2015-04-07 11:59:44.400')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 7, convert(nvarchar(max), 0x1E0442043C0435043D04380442044C04), '2015-04-07 11:59:44.400')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 8, convert(nvarchar(max), 0x1A04200041043E04360430043B0435043D0438044E042C0020003C044B0420003D043504200041043C043E0433043B04380420003D0430043904420438042000410432043E0438042000340430043D043D044B043504), '2015-04-07 11:59:44.400')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 9, convert(nvarchar(max), 0x1F043E04360430043B04430439044104420430042C002000410432044F04360438044204350441044C04200041042000300434043C0438043D04380441044204400430044604380435043904200038043B04380420003D04300436043C0438044204350420003D04300420003A043D043E043F043A044304200032043D043804370443042C002000470442043E0431044B0420003F043E04320442043E044004380442044C0420003F043E043F044B0442043A044304), '2015-04-07 11:59:44.400')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 10, convert(nvarchar(max), 0x1F043E043F0440043E043104430439042000150449043504), '2015-04-07 11:59:44.400')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 11, convert(nvarchar(max), 0x230431043504340438044204350441044C042C002000470442043E042000320441043504), '2015-04-07 11:59:44.400')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 12, convert(nvarchar(max), 0x200430043D043D0438043904200037043004350437043404), '2015-04-07 11:59:44.400')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 13, convert(nvarchar(max), 0xD00061012000D1008100D000BE00D000B600D000B000D000BB00D000B500D000BD00D000B800D1007D012C002000D000B200D000B000D100C6022000D000BD00D000B000D000B700D000BD00D000B000D1002120D000B500D000BD00D000B800D000B5002000230023002C002000D000BC00D10039202000D000BD00D000B5002000D000BC00D000BE00D000B600D000B500D000BC002000D000BD00D000B5002000D000BF00D100AC20D000BE00D000B200D000B500D100AC20D1008F00D1001A20D10052012000D000B200D000B000D10081002C002000D000BF00D000BE00D000BA00D000B0002000230023002000D000BC00D000B800D000BD00D1009201D1001A202000D000B400D000BE002000D000BD00D000B000D000B700D000BD00D000B000D1002120D000B500D000BD00D000BD00D000BE00D000B300D000BE002000D000B200D100AC20D000B500D000BC00D000B500D000BD00D000B8002E002000D0007801D000BE00D000B600D000B000D000BB00D1009201D000B900D1008100D1001A20D000B0002C002000D000BF00D000BE00D000B200D1001A20D000BE00D100AC20D000B800D1001A20D000B5002000D000BF00D000BE00D000BF00D1003920D1001A20D000BA00D10092012000D000BF00D000BE00D000B700D000B600D000B5002E00), '2015-04-07 11:59:44.400')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 14, convert(nvarchar(max), 0x1F043E04370434043D043504350420001F044004380431044B04420438043504), '2015-04-07 11:59:44.400')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 15, convert(nvarchar(max), 0xD00061012000D1008100D000BE00D000B600D000B000D000BB00D000B500D000BD00D000B800D1007D012C002000D000B200D000B000D100C602D000B5002000D000B200D100AC20D000B500D000BC00D1008F002000D000BD00D000B000D000B700D000BD00D000B000D1002120D000B500D000BD00D000B800D000B5002000230023002000D000BF00D100AC20D000BE00D100C602D000BB00D000BE002C002000D000B8002000D000BC00D10039202000D000BD00D000B5002000D000BC00D000BE00D000B600D000B500D000BC002000D000BF00D100AC20D000BE00D000B200D000B500D100AC20D000B800D1001A20D10052012000D000B200D000B000D10081002E002000D0007801D000BE00D000B600D000B000D000BB00D1009201D000B900D1008100D1001A20D000B0002C002000D1008100D000B200D1008F00D000B600D000B800D1001A20D000B500D1008100D10052012000D10081002000D000B000D000B400D000BC00D000B800D000BD00D000B800D1008100D1001A20D100AC20D000B000D1002020D000B800D000B500D000B900), '2015-04-07 11:59:44.400')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 16, convert(nvarchar(max), 0x1E041A04), '2015-04-07 11:59:44.400')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 17, convert(nvarchar(max), 0x1D044304200018043704320438043D04380421002000), '2015-04-07 11:59:44.400')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 18, convert(nvarchar(max), 0x1F043E04360430043B04430439044104420430042C00200041043E043E0431044904300439044204350420003D04300420003F044004380435043C04), '2015-04-07 11:59:44.400')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 19, convert(nvarchar(max), 0x1F043E04360430043B04430439044104420430042C00200032044B0431043504400438044204350420003F044004350434043F043E04470442043804420435043B044C043D044B0439042000340435043D044C04), '2015-04-07 11:58:38.773')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 20, convert(nvarchar(max), 0x1A04200041043E04360430043B0435043D0438044E042C0020003C044B0420003D043504200041043C043E0433043B04380420003D0430043904420438042000410432043E0438042000340430043D043D044B043504), '2015-04-07 11:58:38.773')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 21, convert(nvarchar(max), 0x1F043E04360430043B04430439044104420430042C002000410432044F04360438044204350441044C04200041042000300434043C0438043D04380441044204400430044604380435043904200038043B04380420003D04300436043C0438044204350420003D04300420003A043D043E043F043A044304200032043D043804370443042C002000470442043E0431044B0420003F043E04320442043E044004380442044C0420003F043E043F044B0442043A044304), '2015-04-07 11:58:38.773')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 22, convert(nvarchar(max), 0x1F043E043F0440043E043104430439042000150449043504), '2015-04-07 11:58:38.773')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 23, convert(nvarchar(max), 0x1C0438043B043E0441044204380420001F0440043E04410438043C04), '2015-04-07 11:58:38.773')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 24, convert(nvarchar(max), 0x1F043E04360430043B04430439044104420430042C0020003F043E043404420432043504400434043804420435042000410432043E0439042000370430043F0440043E044104), '2015-04-07 12:06:58.040')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 25, convert(nvarchar(max), 0x1F043E04340442043204350440043404380442044C04), '2015-04-07 12:06:58.040')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 26, convert(nvarchar(max), 0x1E0442043C0435043D04380442044C04), '2015-04-07 12:06:58.040')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 27, convert(nvarchar(max), 0x3D043004), '2015-04-07 12:06:58.040')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 28, convert(nvarchar(max), 0x1F043E04360430043B04430439044104420430042C00200032044B043104350440043804420435042000410432043E0439042000340435043D044C04200040043E043604340435043D0438044F04), '2015-04-07 12:02:39.403')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 29, convert(nvarchar(max), 0x1D0435044204200034043E044104420443043F043D044B044504200041043B043E0442043E043204), '2015-04-07 12:01:49.430')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 30, convert(nvarchar(max), 0x1D044304200018043704320438043D04380421002000), '2015-04-07 12:07:17.913')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 31, convert(nvarchar(max), 0x1F043E04360430043B04430439044104420430042C00200041043E043E0431044904300439044204350420003D04300420003F044004380435043C04), '2015-04-07 12:07:17.913')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 32, convert(nvarchar(max), 0x1E041A04), '2015-04-07 12:07:17.917')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 33, convert(nvarchar(max), 0x1E044204340435043B043A043004), '2015-04-07 12:01:00.900')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 34, convert(nvarchar(max), 0x21043F0430044104380431043E04), '2015-04-07 12:01:00.900')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 35, convert(nvarchar(max), 0x12043004480420003F044004380435043704340420003F043E04340442043204350440043604340430043504420441044F04), '2015-04-07 12:01:00.900')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 36, convert(nvarchar(max), 0x14043004), '2015-04-07 12:01:00.900')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 37, convert(nvarchar(max), 0x1D0435044204), '2015-04-07 12:01:00.900')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 38, convert(nvarchar(max), 0x12044B04200045043E04420438044204350420003F04400438043D044F0442044C04200043044704300441044204380435042000320420003E043F0440043E04410435043F00), '2015-04-07 12:01:00.903')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 39, convert(nvarchar(max), 0x1E044204340435043B043A043004), '2015-04-07 12:01:12.787')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 40, convert(nvarchar(max), 0x1F043E04200032043004480435043C0443042000370430043F0440043E044104430420003D04300437043D043004470435043D0438044F04), '2015-04-07 12:01:12.787')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 41, convert(nvarchar(max), 0x21043F0430044104380431043E04), '2015-04-07 12:01:12.787')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 42, convert(nvarchar(max), 0x3D043004), '2015-04-07 12:01:12.787')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 43, convert(nvarchar(max), 0x21043F0430044104380431043E04), '2015-04-07 12:01:45.937')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 44, convert(nvarchar(max), 0x1E044204340435043B043A043004), '2015-04-07 12:01:45.937')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 45, convert(nvarchar(max), 0x12043004480420003E043F0440043E044104200031044B043B042000370430043F043804410430043D04), '2015-04-07 12:01:45.940')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 46, convert(nvarchar(max), 0x1F043E04360430043B04430439044104420430042C00200032044B0431043504400438044204350420003F0435044004320443044E042000310443043A04320443042000320430044804350433043E042000460069007200730074004E0061006D006500), '2015-04-07 12:02:44.870')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 47, convert(nvarchar(max), 0x1F043E04360430043B04430439044104420430042C00200032044B0431043504400438044204350420003F0435044004320443044E042000310443043A04320443042000350433043E042000410432043E04350439042000440430043C0438043B04380435043904), '2015-04-07 12:02:50.443')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 48, convert(nvarchar(max), 0x1404300442043004200040043E043604340435043D0438044F04200045006E00740065007200), '2015-04-08 04:36:01.517')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 49, convert(nvarchar(max), 0x140435043D044C04), '2015-04-08 04:36:01.517')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 50, convert(nvarchar(max), 0x1C04350441044F044604), '2015-04-08 04:36:01.517')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 51, convert(nvarchar(max), 0x13043E043404), '2015-04-08 04:36:01.517')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 52, convert(nvarchar(max), 0x21043B043504340443044E04490438043904), '2015-04-08 04:36:01.520')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 53, convert(nvarchar(max), 0x1F043E04360430043B04430439044104420430042C00200032044B043104350440043804420435042000410432043E04390420003F043E043B04), '2015-04-07 12:03:24.060')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 54, convert(nvarchar(max), 0x1C044304360441043A043E04390420001F043E043B04), '2015-04-07 12:03:24.060')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 55, convert(nvarchar(max), 0x160435043D0441043A043804390420001F043E043B04), '2015-04-07 12:03:24.060')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 56, convert(nvarchar(max), 0x1F0440043E043F04430441043A04300442044C04), '2015-04-07 12:03:24.060')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 57, convert(nvarchar(max), 0x1A0438043E0441043A042000480069006200650072006E006100740069006E00670020002E002E002E002E00), '2015-04-07 12:01:54.070')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 58, convert(nvarchar(max), 0x210440043E043A04380420004F0075007400200049006E0020002E002E002E00), '2015-04-07 12:02:09.860')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 59, convert(nvarchar(max), 0x14043E04310440043E0420001F043E04360430043B043E043204300442044C0420001204), '2015-04-07 12:02:16.263')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 60, convert(nvarchar(max), 0x32044B0431044004300442044C0420004F0437044B043A042C002000470442043E0431044B0420003D0430044704300442044C04), '2015-04-07 12:02:16.263')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 61, convert(nvarchar(max), 0x1F043E04360430043B04430439044104420430042C00200032044B0431043504400438044204350420003E043F04460438044E04), '2015-04-07 12:02:22.633')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 62, convert(nvarchar(max), 0x1F043E04360430043B04430439044104420430042C00200032044B0431043504400438044204350420003C04350441044F044604200040043E043604340435043D0438044F04), '2015-04-07 12:03:30.040')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 63, convert(nvarchar(max), 0x14043B044F0420003D043004470430043B0430042C0020003F043E04360430043B04430439044104420430042C00200032044B0431043504400438044204350420003E04340438043D04200038043704200041043B043504340443044E044904380445042000320430044004380430043D0442043E043204), '2015-04-07 12:02:28.197')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 64, convert(nvarchar(max), 0x14043E04310440043E0420001F043E04360430043B043E043204300442044C0420001204), '2015-04-07 12:02:33.933')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 65, convert(nvarchar(max), 0x1F043E04360430043B04430439044104420430042C00200032044B043104350440043804420435042000420438043F04200033043D043504370434043004), '2015-04-07 12:03:53.847')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 66, convert(nvarchar(max), 0x21043B043504340443044E04490438043904), '2015-04-07 12:04:08.107')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 67, convert(nvarchar(max), 0x1F043E04360430043B04430439044104420430042C0020003E0442043204350442044C044204350420003D043004200041043B043504340443044E04490438043504200032043E043F0440043E0441044B04), '2015-04-07 12:04:08.107')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 68, convert(nvarchar(max), 0x1F0440043E043F04430441043A04300442044C04), '2015-04-07 12:04:08.107')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 69, convert(nvarchar(max), 0x1F043E04360430043B04430439044104420430042C00200032044B0431043504400438044204350420003E043F04460438044E04), '2015-04-07 12:04:00.000')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 70, convert(nvarchar(max), 0x1E04310437043E0440044B0420003204400435043C0435043D043D043E0420003D04350434043E044104420443043F0435043D04), '2015-04-07 12:04:00.000')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 71, convert(nvarchar(max), 0x1F043E04360430043B04430439044104420430042C00200032044B04310435044004380442043504200033043E043404200040043E043604340435043D0438044F04), '2015-04-07 12:03:47.870')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 72, convert(nvarchar(max), 0x1E041A04), '2015-04-07 12:03:47.870')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 73, convert(nvarchar(max), 0x3D04380420003E04340438043D04200038043704200032044B04480435043F043504400435044704380441043B0435043D043D044B044504), '2015-04-07 12:03:47.870')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 74, convert(nvarchar(max), 0x13043E04340420003D04350420003D0430043904340435043D043004), '2015-04-07 12:03:47.870')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 75, convert(nvarchar(max), 0x1F043E04360430043B04430439044104420430042C002000410432044F04360438044204350441044C04200041042000300434043C0438043D04380441044204400430044604380435043904), '2015-04-07 12:03:47.870')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 80, convert(nvarchar(max), 0x40044304410441043A0438043904), '2015-04-10 05:24:22.590')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 82, convert(nvarchar(max), 0x21043E043E043104490435043D0438044F04), '2015-04-07 12:05:31.887')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 83, convert(nvarchar(max), 0x21043B043504340443044E04490438043904), '2015-04-07 12:05:31.890')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 84, convert(nvarchar(max), 0x1F043E04360430043B04430439044104420430042C00200032044B04310435044004380442043504200033043E043404200040043E043604340435043D0438044F04), '2015-04-07 12:03:36.990')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (5, 85, convert(nvarchar(max), 0x21043B043504340443044E04490438043904), '2015-04-07 12:03:36.993')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 1, convert(nvarchar(max), 0x560061007200FD00FE00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 2, convert(nvarchar(max), 0x41006E006B0065007400), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 3, convert(nvarchar(max), 0x530069007400650020004800610072006900740061007300FD00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 4, convert(nvarchar(max), 0x520061006E00640065007600750020006F006C0075006E00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 5, convert(nvarchar(max), 0x520061006E0064006500760075006C006100720020006900E70069006E00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 6, convert(nvarchar(max), 0x4F006E00610079006C0061006D0061006B00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 7, convert(nvarchar(max), 0x49007000740061006C00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 8, convert(nvarchar(max), 0x4D00610061006C0065007300650066002000620069007A002000610079007200FD006E007400FD006C0061007200FD002000620075006C0061006D00FD0079006F007200730061006E00FD007A00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 9, convert(nvarchar(max), 0x520065007300650070007300690079006F006E002000740065006D0061007300200076006500790061002000740065006B007200610072002000640065006E0065006D0065006B0020006900E70069006E0020006100FE006100F000FD00640061006B00690020006200750074006F006E00610020007400FD006B006C0061007900FD006E00FD007A0020004C00FC007400660065006E00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 10, convert(nvarchar(max), 0x540065006B007200610072002000440065006E006500790069006E00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 11, convert(nvarchar(max), 0x5400FC006D00FC006E00FC0020004F006E00610079006C006100), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 12, convert(nvarchar(max), 0x450072006B0065006E002000470065006C006900FE00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 13, convert(nvarchar(max), 0xC30053017A006700C300BC006E00C300BC007A002C002000720061006E0064006500760075002000620069007A0020006100740061006E006D006100640061006E002000C300B6006E00630065002000230023002000640061006B0069006B00610020006B0061006400610072002000730069007A00690020006B006F006E00740072006F006C0020006500640065006D00690079006F00720075007A002C0020002300230020006F006C0064007500C400780175006E0075002E0020004C00C300BC007400660065006E0020006400610068006100200073006F006E00720061002000740065006B007200610072002000640065006E006500790069006E0069007A002E00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 14, convert(nvarchar(max), 0x47006500E7002000470065006C006900FE00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 15, convert(nvarchar(max), 0xC30053017A006700C300BC006E00C300BC007A002C002000230023002000760065002000720061006E00640065007600750020007A0061006D0061006E00C400B100200067006500C300A700740069002000760065002000620069007A002000730069007A00690020006B006F006E00740072006F006C0020006500640065006D00690079006F00720075007A002E002000520065007300650070007300690079006F006E006100200062006100C500780176007500720075006E00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 16, convert(nvarchar(max), 0x540041004D0041004D00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 17, convert(nvarchar(max), 0x4D00610061006C00650073006500660021002000), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 18, convert(nvarchar(max), 0x41006C00FD006D00FD0020006900E70069006E0020007200610070006F00720020006500640069006E00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 19, convert(nvarchar(max), 0x54006500720063006900680020006700FC006E00200073006500E70069006E0069007A00), '2015-04-14 08:08:22.923')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 20, convert(nvarchar(max), 0x4D00610061006C0065007300650066002000620069007A002000610079007200FD006E007400FD006C0061007200FD002000620075006C0061006D00FD0079006F007200730061006E00FD007A00), '2015-04-14 08:08:22.923')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 21, convert(nvarchar(max), 0x520065007300650070007300690079006F006E002000740065006D0061007300200076006500790061002000740065006B007200610072002000640065006E0065006D0065006B0020006900E70069006E0020006100FE006100F000FD00640061006B00690020006200750074006F006E00610020007400FD006B006C0061007900FD006E00FD007A0020004C00FC007400660065006E00), '2015-04-14 08:08:22.923')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 22, convert(nvarchar(max), 0x540065006B007200610072002000440065006E006500790069006E00), '2015-04-14 08:08:22.923')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 23, convert(nvarchar(max), 0x4B0061007200FE00FD006C0061006D006100), '2015-04-14 08:08:22.923')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 24, convert(nvarchar(max), 0x4900730074006500F00069006E0069007A00690020006F006E00610079006C0061007900FD006E0020004C00FC007400660065006E00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 25, convert(nvarchar(max), 0x4F006E00610079006C0061006D0061006B00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 26, convert(nvarchar(max), 0x49007000740061006C00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 27, convert(nvarchar(max), 0x61007400), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 28, convert(nvarchar(max), 0x44006F00F00075006D0020006700FC006E00FC006E00FC007A00FC00200073006500E70069006E0069007A00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 29, convert(nvarchar(max), 0x4D0065007600630075007400200044006500F00069006C00200059007500760061006C0061007200FD00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 30, convert(nvarchar(max), 0x4D00610061006C00650073006500660021002000), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 31, convert(nvarchar(max), 0x41006C00FD006D00FD0020006900E70069006E0020007200610070006F00720020006500640069006E00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 32, convert(nvarchar(max), 0x540041004D0041004D00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 33, convert(nvarchar(max), 0x4200690074006900FE00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 34, convert(nvarchar(max), 0x54006500FE0065006B006B00FC0072002000450064006500720069006D00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 35, convert(nvarchar(max), 0x560061007200FD00FE0020006F006E00610079006C0061006E00FD007200), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 36, convert(nvarchar(max), 0x4500760065007400), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 37, convert(nvarchar(max), 0x480061007900FD007200), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 38, convert(nvarchar(max), 0x4500F00065007200200061006E006B00650074006900200061006C006D0061006B002000690073007400690079006F00720020006D007500730075006E0075007A003F00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 39, convert(nvarchar(max), 0x4200690074006900FE00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 40, convert(nvarchar(max), 0x520061006E0064006500760075002000740061006C0065006200690020006900E70069006E00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 41, convert(nvarchar(max), 0x54006500FE0065006B006B00FC0072002000450064006500720069006D00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 42, convert(nvarchar(max), 0x61007400), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 43, convert(nvarchar(max), 0x54006500FE0065006B006B00FC0072002000450064006500720069006D00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 44, convert(nvarchar(max), 0x4200690074006900FE00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 45, convert(nvarchar(max), 0x530069007A0069006E00200061006E006B006500740020006B006100790064006500640069006C0064006900), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 46, convert(nvarchar(max), 0x530065006E0069006E002000660069007200730074006E0061006D006500200069006C006B002000680061007200660069006E006900200073006500E70069006E0069007A00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 47, convert(nvarchar(max), 0x530065006E0069006E00200073006F00790061006400FD006E00FD006E00200069006C006B002000680061007200660069006E006900200073006500E70069006E0069007A00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 48, convert(nvarchar(max), 0x44006F00F00075006D0020007400610072006900680069006E006900200067006900720069006E00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 49, convert(nvarchar(max), 0x4700FC006E00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 50, convert(nvarchar(max), 0x41007900), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 51, convert(nvarchar(max), 0x5900FD006C00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 52, convert(nvarchar(max), 0x53006F006E00720061006B006900), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 53, convert(nvarchar(max), 0x4C00FC007400660065006E002000630069006E007300690079006500740069006E0069007A006900200073006500E70069006E0069007A00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 54, convert(nvarchar(max), 0x450072006B0065006B00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 55, convert(nvarchar(max), 0x4B0061006400FD006E00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 56, convert(nvarchar(max), 0x410074006C0061006D0061006B00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 57, convert(nvarchar(max), 0x4B00D600DE004B0020004B00FD00FE002000750079006B007500730075006E006100200079006100740061006E0020002E002E002E002E00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 58, convert(nvarchar(max), 0x4F007500740020005A0061006D0061006E006C0061006D00610020002E002E002E00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 59, convert(nvarchar(max), 0x48006F00FE00670065006C00640069006E0069007A00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 60, convert(nvarchar(max), 0x62006100FE006C00610074006D0061006B0020006900E70069006E0020006200690072002000640069006C00200073006500E70069006E00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 61, convert(nvarchar(max), 0x420069007200200073006500E70065006E0065006B00200073006500E70069006E0069007A00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 62, convert(nvarchar(max), 0x44006F00F00075006D00200061007900FD00200073006500E70069006E0069007A00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 63, convert(nvarchar(max), 0x42006100FE006C00610074006D0061006B0020006900E70069006E002C0020006100FE006100F000FD00640061006B006900200073006500E70065006E0065006B006C0065007200640065006E00200062006900720069006E006900200073006500E70069006E0069007A00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 64, convert(nvarchar(max), 0x48006F00FE00670065006C00640069006E0069007A00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 65, convert(nvarchar(max), 0x4200690072002000790075007600610020007400FC007200FC00200073006500E70069006E0069007A00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 66, convert(nvarchar(max), 0x53006F006E00720061006B006900), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 67, convert(nvarchar(max), 0x4100FE006100F000FD00640061006B006900200073006F00720075006C0061007200610020006300650076006100700020006C00FC007400660065006E00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 68, convert(nvarchar(max), 0x410074006C0061006D0061006B00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 69, convert(nvarchar(max), 0x420069007200200073006500E70065006E0065006B00200073006500E70069006E0069007A00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 70, convert(nvarchar(max), 0xDE007500200061006E0064006100200041006E006B00650074006C0065007200), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 71, convert(nvarchar(max), 0x44006F00F00075006D0020007900FD006C00FD006E00FD00200073006500E70069006E0069007A00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 72, convert(nvarchar(max), 0x540041004D0041004D00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 73, convert(nvarchar(max), 0x590075006B0061007200FD00640061006B0069006C006500720069006E00200068006900E7006200690072006900), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 74, convert(nvarchar(max), 0x5900FD006C002000420075006C0075006E0061006D0061006400FD00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 75, convert(nvarchar(max), 0x52006500630065007000740069006F006E00200062006100FE0076007500720075006E00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (6, 81, convert(nvarchar(max), 0x5400FC0072006B00), '2015-03-16 16:00:31.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 1, convert(nvarchar(max), 0x30526567), '2015-04-14 06:22:12.323')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 2, convert(nvarchar(max), 0x038CE567), '2015-04-14 06:22:12.323')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 3, convert(nvarchar(max), 0x517FD97A3057FE56), '2015-04-14 06:22:12.323')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 4, convert(nvarchar(max), 0x8498A67E), '2015-04-14 06:22:12.323')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 5, convert(nvarchar(max), 0x8498A67E), '2015-04-14 06:20:34.633')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 6, convert(nvarchar(max), 0x6E78A48B), '2015-04-14 06:20:34.633')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 7, convert(nvarchar(max), 0xD653886D), '2015-04-14 06:20:34.633')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 8, convert(nvarchar(max), 0x885FB162496B0CFF1162EC4EE065D56C7E623052A8608476E68BC67EE14F6F60), '2015-04-14 06:20:34.633')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 9, convert(nvarchar(max), 0xF78B5480FB7CA563855F1662B970FB510B4E629784760963AE948D51D58B004E216B), '2015-04-14 06:20:34.633')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 10, convert(nvarchar(max), 0x8D51D58B004E216B), '2015-04-14 06:20:34.633')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 11, convert(nvarchar(max), 0x6851E8906E78A48B), '2015-04-14 06:20:34.637')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 12, convert(nvarchar(max), 0xD0634D5230526567), '2015-04-14 06:20:34.637')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 13, convert(nvarchar(max), 0xF95B0D4E778D0CFFA8608476FB4E7D542F66230023000CFF1162EC4EE065D56C2857C068E567604F0CFFF4763052604FA67E1A4F4D522300230006529F940230F78B0D7A0E548D51D58B0230), '2015-04-14 06:20:34.637')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 14, convert(nvarchar(max), 0xDF8F3052), '2015-04-14 06:20:34.637')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 15, convert(nvarchar(max), 0xF95B0D4E778D0CFF23002300604FA67E1A4F8476F665F495F25DCF7EC78FBB53864E0CFF1162EC4E2F66E065D56CC068E567604F0CFFF78B5480FB7C4D52F053), '2015-04-14 06:20:34.637')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 16, convert(nvarchar(max), 0x4C88), '2015-04-14 06:20:34.637')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 17, convert(nvarchar(max), 0xF95B0D4E778D01FFE065D56C04590674A8608476F78B426C0230), '2015-04-14 06:20:34.637')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 18, convert(nvarchar(max), 0xF78B1154A563855F0459), '2015-04-14 06:20:34.637')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 19, convert(nvarchar(max), 0xF78B0990E962004E2A4E969909908476004E2959), '2015-04-14 06:20:02.003')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 20, convert(nvarchar(max), 0x885FB162496B0CFF1162EC4EE065D56C7E623052A8608476E68BC67EE14F6F60), '2015-04-14 06:20:02.003')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 21, convert(nvarchar(max), 0xF78B5480FB7CA563855F1662B970FB510B4E629784760963AE948D51D58B004E216B), '2015-04-14 06:20:02.003')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 22, convert(nvarchar(max), 0x8D51D58B004E216B), '2015-04-14 06:20:02.003')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 23, convert(nvarchar(max), 0x226BCE8F), '2015-04-14 06:20:02.003')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 24, convert(nvarchar(max), 0xF78B6E78A48BA8608476F78B426C), '2015-04-14 06:21:01.180')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 25, convert(nvarchar(max), 0x6E78A48B), '2015-04-14 06:21:01.180')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 26, convert(nvarchar(max), 0xD653886D), '2015-04-14 06:21:01.180')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 27, convert(nvarchar(max), 0x2857), '2015-04-14 06:21:01.180')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 28, convert(nvarchar(max), 0xF78B0990E962FA511F758476004E2959), '2015-04-14 06:22:47.450')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 29, convert(nvarchar(max), 0xE065EF532875D263FD69), '2015-04-14 06:20:09.457')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 30, convert(nvarchar(max), 0xF95B0D4E778D01FFE065D56C04590674A8608476F78B426C0230), '2015-04-14 06:21:11.790')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 31, convert(nvarchar(max), 0xF78B1154A563855F0459), '2015-04-14 06:21:11.790')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 32, convert(nvarchar(max), 0x4C88), '2015-04-14 06:21:11.790')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 33, convert(nvarchar(max), 0x8C5B), '2015-04-14 06:21:24.577')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 34, convert(nvarchar(max), 0x228C228C), '2015-04-14 06:21:24.577')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 35, convert(nvarchar(max), 0xA8608476305265676E78A48B), '2015-04-14 06:21:24.577')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 36, convert(nvarchar(max), 0x2F668476), '2015-04-14 06:21:24.577')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 37, convert(nvarchar(max), 0xA16C0967), '2015-04-14 06:21:24.577')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 38, convert(nvarchar(max), 0x604FF360C253A052038CE5671FFF), '2015-04-14 06:21:24.577')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 39, convert(nvarchar(max), 0x8C5B), '2015-04-14 06:21:44.373')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 40, convert(nvarchar(max), 0xA86084768498A67E3375F78B), '2015-04-14 06:21:44.373')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 41, convert(nvarchar(max), 0x228C228C), '2015-04-14 06:21:44.373')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 42, convert(nvarchar(max), 0x2857), '2015-04-14 06:21:44.377')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 43, convert(nvarchar(max), 0x228C228C), '2015-04-14 06:21:53.597')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 44, convert(nvarchar(max), 0x8C5B), '2015-04-14 06:21:53.597')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 45, convert(nvarchar(max), 0xA8608476038CE567F25DB08B555F), '2015-04-14 06:21:53.597')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 46, convert(nvarchar(max), 0xF78B0990E962A86084760D54575B84762C7B004E2A4E575BCD6B), '2015-04-14 06:22:53.250')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 47, convert(nvarchar(max), 0xF78B0990E962A8608476D3590F6C84762C7B004E2A4E575BCD6B), '2015-04-14 06:22:59.007')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 48, convert(nvarchar(max), 0x938F6551FA511F75E5651F67), '2015-04-14 06:23:13.273')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 49, convert(nvarchar(max), 0x2959), '2015-04-14 06:23:13.273')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 50, convert(nvarchar(max), 0x0867), '2015-04-14 06:23:13.273')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 51, convert(nvarchar(max), 0x745E), '2015-04-14 06:23:13.273')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 52, convert(nvarchar(max), 0x0B4E004E2A4E), '2015-04-14 06:23:13.273')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 53, convert(nvarchar(max), 0xF78B0990E962A860847627602B52), '2015-04-14 06:23:23.593')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 54, convert(nvarchar(max), 0x3775), '2015-04-14 06:23:23.593')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 55, convert(nvarchar(max), 0x7359), '2015-04-14 06:23:23.593')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 56, convert(nvarchar(max), 0xF38DC38D), '2015-04-14 06:23:23.597')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 57, convert(nvarchar(max), 0x4B0049004F0053004B00AC5120772E002E002E002E00), '2015-04-14 06:22:03.107')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 58, convert(nvarchar(max), 0x858DF66528572E002E002E00), '2015-04-14 06:22:19.150')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 59, convert(nvarchar(max), 0x226BCE8F4951344E), '2015-04-14 06:22:25.877')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 60, convert(nvarchar(max), 0x0990E962004ECD79ED8B008A005FCB59), '2015-04-14 06:22:25.880')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 61, convert(nvarchar(max), 0xF78B0990E962004E2A4E09907998), '2015-04-14 06:22:31.463')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 62, convert(nvarchar(max), 0xF78B0990E962A86084761F75E5650867FD4E), '2015-04-14 06:23:29.743')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 63, convert(nvarchar(max), 0x8189005FCB590CFFF78B0990E9620B4E1752099079984B4E004E), '2015-04-14 06:22:36.440')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 64, convert(nvarchar(max), 0x226BCE8F4951344E), '2015-04-14 06:22:41.820')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 65, convert(nvarchar(max), 0xF78B0990E962004E2A4ED263FD697B7C8B57), '2015-04-14 06:23:54.883')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 66, convert(nvarchar(max), 0x0B4E004E2A4E), '2015-04-14 06:24:11.150')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 67, convert(nvarchar(max), 0xF78BDE56547B0B4E1752EE959898), '2015-04-14 06:24:11.150')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 68, convert(nvarchar(max), 0xF38DC38D), '2015-04-14 06:24:11.153')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 69, convert(nvarchar(max), 0xF78B0990E962004E2A4E09907998), '2015-04-14 06:24:02.617')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 70, convert(nvarchar(max), 0x038CE567535F4D520D4EEF532875), '2015-04-14 06:24:02.617')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 71, convert(nvarchar(max), 0xF78B0990E962A8608476FA511F75745EFD4E), '2015-04-14 06:23:47.653')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 72, convert(nvarchar(max), 0x4C88), '2015-04-14 06:23:47.653')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 73, convert(nvarchar(max), 0xE54E0A4EFD900D4E2F66), '2015-04-14 06:23:47.653')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 74, convert(nvarchar(max), 0x004E745E2A677E623052), '2015-04-14 06:23:47.653')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 75, convert(nvarchar(max), 0xF78B5480FB7CA563855F), '2015-04-14 06:23:47.653')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 82, convert(nvarchar(max), 0xE14F6F60), '2015-04-14 06:20:51.043')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 83, convert(nvarchar(max), 0x0B4E004E2A4E), '2015-04-14 06:20:51.043')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 84, convert(nvarchar(max), 0xF78B0990E962A8608476FA511F75745EFD4E), '2015-04-14 06:23:36.130')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 85, convert(nvarchar(max), 0x0B4E004E2A4E), '2015-04-14 06:23:36.130')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (7, 86, convert(nvarchar(max), 0x2D4EFD56), '')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 1, convert(nvarchar(max), 0x4806350648064406), '2015-04-14 06:41:55.747')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 2, convert(nvarchar(max), 0x450633062D06), '2015-04-14 06:41:55.747')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 3, convert(nvarchar(max), 0x2E0631064A06370629062000270644064506480642063906), '2015-04-14 06:41:55.747')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 4, convert(nvarchar(max), 0x2C06390644062000270644062A0639064A064A064606), '2015-04-14 06:41:55.747')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 5, convert(nvarchar(max), 0x2A0639064A064A06460627062A0620004406), '2015-04-14 06:40:40.077')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 6, convert(nvarchar(max), 0x230643062F06), '2015-04-14 06:40:40.077')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 7, convert(nvarchar(max), 0x250644063A0627062106), '2015-04-14 06:40:40.077')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 8, convert(nvarchar(max), 0x220633064106200046062D06460620003A064A0631062000420627062F0631064A0646062000390644064906200025064A062C0627062F062000270644062A064106270635064A0644062000270644062E06270635062906200028064306), '2015-04-14 06:40:40.077')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 9, convert(nvarchar(max), 0x4A0631062C06490620002706440627062A06350627064406200027064406270633062A06420628062706440620002306480620002706460642063106200039064406490620002706440632063106200023062F0646062706470620004406440645062D0627064806440629062000450631062906200023062E0631064906), '2015-04-14 06:40:40.077')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 10, convert(nvarchar(max), 0x2D062706480644062000450631062906200023062E0631064906), '2015-04-14 06:40:40.077')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 11, convert(nvarchar(max), 0x2A06230643064A062F0620002C0645064A063906), '2015-04-14 06:40:40.077')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 12, convert(nvarchar(max), 0x480635064806440620004506280643063106), '2015-04-14 06:40:40.077')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 13, convert(nvarchar(max), 0x39063006310627060C0620002A0639064A064A0646062000270644062E06270635062000280643062000470648062000230023000C062000480646062D06460620003A064A0631062000420627062F0631064A06460620003906440649062000270644062A062D06420642062000440643064506200041064A0620002D062A06490620002300230020002F0642064A0642062906200042062806440620004506480639062F0643062E0020004A0631062C06490620002706440645062D0627064806440629062000450631062906200023062E0631064906200041064A062000480642062A062000440627062D0642062E00), '2015-04-14 06:40:40.077')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 14, convert(nvarchar(max), 0x2A0623062E06310620004806350648064406), '2015-04-14 06:40:40.080')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 15, convert(nvarchar(max), 0x2206330641060C062000480627064406480642062A0620004506480639062F064306200045064606200023002300200042062F062000450631062A062000480646062D06460620003A064A0631062000420627062F0631064A06460620003906440649062000270644062A062D06420642062000440643064506200041064A062E0020004A0631062C06490620002706440627062A06350627064406200027064406270633062A064206280627064406), '2015-04-14 06:40:40.080')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 16, convert(nvarchar(max), 0x2D06330646062706), '2015-04-14 06:40:40.080')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 17, convert(nvarchar(max), 0x220633064106210020003A064A0631062000420627062F0631062000390644064906200045063906270644062C062906200037064406280643062E00), '2015-04-14 06:40:40.080')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 18, convert(nvarchar(max), 0x4A0631062C064906200025062806440627063A06200027064406270633062A064206280627064406), '2015-04-14 06:40:40.080')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 19, convert(nvarchar(max), 0x2706440631062C062706210620002A062D062F064A062F062000270644064A06480645062000270644064506410636064406), '2015-04-14 06:40:05.780')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 20, convert(nvarchar(max), 0x220633064106200046062D06460620003A064A0631062000420627062F0631064A0646062000390644064906200025064A062C0627062F062000270644062A064106270635064A0644062000270644062E06270635062906200028064306), '2015-04-14 06:40:05.780')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 21, convert(nvarchar(max), 0x4A0631062C06490620002706440627062A06350627064406200027064406270633062A06420628062706440620002306480620002706460642063106200039064406490620002706440632063106200023062F0646062706470620004406440645062D0627064806440629062000450631062906200023062E0631064906), '2015-04-14 06:40:05.780')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 22, convert(nvarchar(max), 0x2D062706480644062000450631062906200023062E0631064906), '2015-04-14 06:40:05.780')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 23, convert(nvarchar(max), 0x2A0631062D064A062806), '2015-04-14 06:40:05.780')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 24, convert(nvarchar(max), 0x2706440631062C06270621062000270644062A06230643062F0620004506460620003706440628064306), '2015-04-14 06:40:56.640')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 25, convert(nvarchar(max), 0x230643062F06), '2015-04-14 06:40:56.643')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 26, convert(nvarchar(max), 0x250644063A0627062106), '2015-04-14 06:40:56.643')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 27, convert(nvarchar(max), 0x41064A06), '2015-04-14 06:40:56.643')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 28, convert(nvarchar(max), 0x4A0631062C06490620002A062D062F064A062F0620004A0648064506200045064A06440627062F064306), '2015-04-14 06:42:29.587')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 29, convert(nvarchar(max), 0x44062706200041062A062D0627062A0620002706440645062A064806410631062906), '2015-04-14 06:40:12.177')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 30, convert(nvarchar(max), 0x220633064106210020003A064A0631062000420627062F0631062000390644064906200045063906270644062C062906200037064406280643062E00), '2015-04-14 06:41:07.610')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 31, convert(nvarchar(max), 0x4A0631062C064906200025062806440627063A06200027064406270633062A064206280627064406), '2015-04-14 06:41:07.610')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 32, convert(nvarchar(max), 0x2D06330646062706), '2015-04-14 06:41:07.610')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 33, convert(nvarchar(max), 0x4606470627064A062906), '2015-04-14 06:41:21.267')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 34, convert(nvarchar(max), 0x3406430631062706), '2015-04-14 06:41:21.267')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 35, convert(nvarchar(max), 0x2A06230643064A062F06200048063506480644064306), '2015-04-14 06:41:21.270')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 36, convert(nvarchar(max), 0x460639064506), '2015-04-14 06:41:21.270')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 37, convert(nvarchar(max), 0x44062706), '2015-04-14 06:41:21.270')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 38, convert(nvarchar(max), 0x4706440620002A0631064A062F0620002306460620002A0623062E063006200027064406450633062D061F06), '2015-04-14 06:41:21.270')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 39, convert(nvarchar(max), 0x4606470627064A062906), '2015-04-14 06:41:30.567')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 40, convert(nvarchar(max), 0x440637064406280620004506480639062F064306), '2015-04-14 06:41:30.567')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 41, convert(nvarchar(max), 0x3406430631062706), '2015-04-14 06:41:30.567')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 42, convert(nvarchar(max), 0x41064A06), '2015-04-14 06:41:30.567')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 43, convert(nvarchar(max), 0x3406430631062706), '2015-04-14 06:41:39.423')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 44, convert(nvarchar(max), 0x4606470627064A062906), '2015-04-14 06:41:39.423')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 45, convert(nvarchar(max), 0x480642062F06200033062C0644062A062000270644062F0631062706330629062000270644062E06270635062906200028064306), '2015-04-14 06:41:39.423')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 46, convert(nvarchar(max), 0x4506460620004106360644064306200027062E062A0631062000270644062D06310641062000270644062306480644062000450646062000270644062706330645062000270644062306480644062000270644062E0627063506200028064306), '2015-04-14 06:42:34.640')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 47, convert(nvarchar(max), 0x4506460620004106360644064306200027062E062A0631062000270644062D06310641062000270644062306480644062000450646062000270633064506200027064406390627062606440629062000270644062E0627063506200028064306), '2015-04-14 06:42:39.850')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 48, convert(nvarchar(max), 0x2A06270631064A062E0620002706440645064A06440627062F06200023062F062E064406), '2015-04-14 06:42:52.073')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 49, convert(nvarchar(max), 0x4A0648064506), '2015-04-14 06:42:52.073')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 50, convert(nvarchar(max), 0x340647063106), '2015-04-14 06:42:52.073')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 51, convert(nvarchar(max), 0x390627064506), '2015-04-14 06:42:52.073')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 52, convert(nvarchar(max), 0x270644062A06270644064906), '2015-04-14 06:42:52.073')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 53, convert(nvarchar(max), 0x4A0631062C06490620002A062D062F064A062F0620002C06460633064306), '2015-04-14 06:43:01.400')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 54, convert(nvarchar(max), 0x300643063106), '2015-04-14 06:43:01.400')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 55, convert(nvarchar(max), 0x230646062B064906), '2015-04-14 06:43:01.400')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 56, convert(nvarchar(max), 0x2A062E0637064906), '2015-04-14 06:43:01.400')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 57, convert(nvarchar(max), 0x4B0049004F0053004B002000270644063306280627062A0620002E002E002E002E00), '2015-04-14 06:41:46.323')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 58, convert(nvarchar(max), 0x270646062A064706270621062000270644064506470644062906200041064A0620002E002E002E00), '2015-04-14 06:42:02.630')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 59, convert(nvarchar(max), 0x450631062D06280627062000280643064506200041064A06), '2015-04-14 06:42:08.970')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 60, convert(nvarchar(max), 0x2A062D062F064A062F06200044063A0629062000440628062F062106), '2015-04-14 06:42:08.973')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 61, convert(nvarchar(max), 0x2706440631062C062706210620002A062D062F064A062F0620002E064A0627063106), '2015-04-14 06:42:13.477')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 62, convert(nvarchar(max), 0x4A0631062C06490620002A062D062F064A062F06200027064406340647063106200045064A06440627062F064306), '2015-04-14 06:43:06.887')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 63, convert(nvarchar(max), 0x4406440628062F0621060C0620004A0631062C064906200027062E062A064A06270631062000480627062D062F062000450646062000270644062E064A062706310627062A06200023062F06460627064706), '2015-04-14 06:42:19.207')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 64, convert(nvarchar(max), 0x450631062D06280627062000280643064506200041064A06), '2015-04-14 06:42:24.260')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 65, convert(nvarchar(max), 0x2706440631062C0627062106200027062E062A064A06270631062000460648063906200041062A062D062906), '2015-04-14 06:43:36.050')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 66, convert(nvarchar(max), 0x270644062A06270644064906), '2015-04-14 06:43:49.080')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 67, convert(nvarchar(max), 0x2706440631062C062706210620002706440625062C062706280629062000390644064906200027064406230633062606440629062000270644062A06270644064A062906), '2015-04-14 06:43:49.080')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 68, convert(nvarchar(max), 0x2A062E0637064906), '2015-04-14 06:43:49.080')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 69, convert(nvarchar(max), 0x2706440631062C0627062106200025062E062A064A0627063106200023062D062F062000270644062E064A062706310627062A06), '2015-04-14 06:43:42.047')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 70, convert(nvarchar(max), 0x270644064506330648062D0627062A0620003A064A063106200045062A06480641063106290620002D06270644064A062706), '2015-04-14 06:43:42.047')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 71, convert(nvarchar(max), 0x2706440631062C062706210620002A062D062F064A062F06200027064406330646062906200045064A06440627062F064306), '2015-04-14 06:43:30.310')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 72, convert(nvarchar(max), 0x2D06330646062706), '2015-04-14 06:43:30.310')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 73, convert(nvarchar(max), 0x44062706200034064A06210620004506450627062000330628064206), '2015-04-14 06:43:30.310')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 74, convert(nvarchar(max), 0x2706440633064606290620004406450620004A062A06450620002706440639062B06480631062000390644064906), '2015-04-14 06:43:30.310')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 75, convert(nvarchar(max), 0x4A0631062C06490620002706440627062A06350627064406200027064406270633062A064206280627064406), '2015-04-14 06:43:30.310')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 82, convert(nvarchar(max), 0x2706440631063306270626064406), '2015-04-14 06:40:47.740')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 83, convert(nvarchar(max), 0x270644062A06270644064906), '2015-04-14 06:40:47.740')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 84, convert(nvarchar(max), 0x2706440631062C062706210620002A062D062F064A062F06200027064406330646062906200045064A06440627062F064306), '2015-04-14 06:43:12.720')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 85, convert(nvarchar(max), 0x270644062A06270644064906), '2015-04-14 06:43:12.720')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (8, 87, convert(nvarchar(max), 0x270644063906310628064A062906), '')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 1, convert(nvarchar(max), 0x8603C603B903BE03B703), '2015-04-14 06:46:27.323')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 2, convert(nvarchar(max), 0x9503C003B903C303BA03CC03C003B703C303B703), '2015-04-14 06:46:27.327')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 3, convert(nvarchar(max), 0x530069007400650020004D0061007000), '2015-04-14 06:46:27.327')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 4, convert(nvarchar(max), 0xA103B103BD03C403B503B203BF03CD03), '2015-04-14 06:46:27.327')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 5, convert(nvarchar(max), 0xA103B103BD03C403B503B203BF03CD032000B303B903B103), '2015-04-14 06:45:04.010')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 6, convert(nvarchar(max), 0x9503C003B903B203B503B203B103B903CE03BD03C903), '2015-04-14 06:45:04.010')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 7, convert(nvarchar(max), 0x9103BA03CD03C103C903C303B703), '2015-04-14 06:45:04.010')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 8, convert(nvarchar(max), 0x9403C503C303C403C503C703CE03C2032000B403B503BD032000B503AF03BC03B103C303C403B5032000C303B5032000B803AD03C303B7032000BD03B1032000B203C103B503B9032000C403B1032000C303C403BF03B903C703B503AF03B1032000C303B103C203), '2015-04-14 06:45:04.010')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 9, convert(nvarchar(max), 0x9503C003B903BA03BF03B903BD03C903BD03AE03C303C403B5032000BC03B5032000C403B7032000C103B503C303B503C803B903CC03BD032000AE0320009A03AC03BD03C403B5032000BA03BB03B903BA032000C303C403BF032000BA03BF03C503BC03C003AF032000C003B103C103B103BA03AC03C403C9032000B303B903B1032000BD03B1032000C003C103BF03C303C003B103B803AE03C303B503C403B5032000BE03B103BD03AC03), '2015-04-14 06:45:04.010')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 10, convert(nvarchar(max), 0xA003C103BF03C303C003AC03B803B703C303B50320009E03B103BD03AC03), '2015-04-14 06:45:04.010')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 11, convert(nvarchar(max), 0x9503C003B903B203B503B203B103AF03C903C303B70320008C03BB03B103), '2015-04-14 06:45:04.010')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 12, convert(nvarchar(max), 0xA003C103CC03C903C103B7032000AC03C603B903BE03B703), '2015-04-14 06:45:04.010')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 13, convert(nvarchar(max), 0x9403C503C303C403C503C703CE03C2032C002000C403BF032000C103B103BD03C403B503B203BF03CD032000C303B103C2032000B503AF03BD03B103B9032000230023002C002000B403B503BD032000B503AF03BC03B103C303C403B5032000C303B5032000B803AD03C303B7032000BD03B1032000BA03AC03BD03B503C403B503200063006800650063006B002D0069006E002000BC03AD03C703C103B9032000230023002000BB03B503C003C403AC032000C003C103B903BD032000C403BF032000C103B103BD03C403B503B203BF03CD032000C303B103C2032E002000A003B103C103B103BA03B103BB03CE032000B403BF03BA03B903BC03AC03C303C403B5032000BE03B103BD03AC032000B103C103B303CC03C403B503C103B1032E00), '2015-04-14 06:45:04.010')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 14, convert(nvarchar(max), 0x9A03B103B803C503C303C403B503C103B703BC03AD03BD03B7032000AC03C603B903BE03B703), '2015-04-14 06:45:04.010')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 15, convert(nvarchar(max), 0x9403C503C303C403C503C703CE03C2032C002000C403B703BD032000CE03C103B1032000C403BF03C5032000C103B103BD03C403B503B203BF03CD032000C303B103C2032000230023002000AD03C703B503B9032000C003B503C103AC03C303B503B9032000BA03B103B9032000B503AF03BC03B103C303C403B5032000C303B5032000B803AD03C303B7032000BD03B1032000BA03AC03BD03B503C403B503200063006800650063006B002D0069006E002E002000A003B103C103B103BA03B103BB03B503AF03C303C403B5032000BD03B1032000B503C003B903BA03BF03B903BD03C903BD03AE03C303B503C403B5032000BC03B5032000C403B7032000C103B503C303B503C803B903CC03BD03), '2015-04-14 06:45:04.013')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 16, convert(nvarchar(max), 0x95039D03A40386039E0395039903), '2015-04-14 06:45:04.013')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 17, convert(nvarchar(max), 0xA303C503B303B303BD03CE03BC03B703210020009403B503BD032000B503AF03BD03B103B9032000B403C503BD03B103C403AE032000B7032000B503C003B503BE03B503C103B303B103C303AF03B1032000C403BF03C5032000B103B903C403AE03BC03B103C403CC03C2032000C303B103C2032E00), '2015-04-14 06:45:04.013')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 18, convert(nvarchar(max), 0xA003B103C103B103BA03B103BB03BF03CD03BC03B5032000B103BD03B103C603AD03C103B503C403B5032000C403B703BD032000C503C003BF03B403BF03C703AE03), '2015-04-14 06:45:04.013')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 19, convert(nvarchar(max), 0xA003B103C103B103BA03B103BB03CE032000B503C003B903BB03AD03BE03C403B5032000BC03B903B1032000B703BC03AD03C103B1032000C403B703C2032000C003C103BF03C403AF03BC03B703C303AE03C203), '2015-04-14 06:44:31.467')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 20, convert(nvarchar(max), 0x9403C503C303C403C503C703CE03C2032000B403B503BD032000B503AF03BC03B103C303C403B5032000C303B5032000B803AD03C303B7032000BD03B1032000B203C103B503B9032000C403B1032000C303C403BF03B903C703B503AF03B1032000C303B103C203), '2015-04-14 06:44:31.470')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 21, convert(nvarchar(max), 0x9503C003B903BA03BF03B903BD03C903BD03AE03C303C403B5032000BC03B5032000C403B7032000C103B503C303B503C803B903CC03BD032000AE0320009A03AC03BD03C403B5032000BA03BB03B903BA032000C303C403BF032000BA03BF03C503BC03C003AF032000C003B103C103B103BA03AC03C403C9032000B303B903B1032000BD03B1032000C003C103BF03C303C003B103B803AE03C303B503C403B5032000BE03B103BD03AC03), '2015-04-14 06:44:31.470')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 22, convert(nvarchar(max), 0xA003C103BF03C303C003AC03B803B703C303B50320009E03B103BD03AC03), '2015-04-14 06:44:31.470')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 23, convert(nvarchar(max), 0x9A03B103BB03C903C303CC03C103B903C303BC03B103), '2015-04-14 06:44:31.470')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 24, convert(nvarchar(max), 0xA003B103C103B103BA03B103BB03BF03CD03BC03B5032000BD03B1032000B503C003B903B203B503B203B103B903CE03C303B503C403B5032000C403BF032000B103AF03C403B703BC03AC032000C303B103C203), '2015-04-14 06:45:25.737')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 25, convert(nvarchar(max), 0x9503C003B903B203B503B203B103B903CE03BD03C903), '2015-04-14 06:45:25.737')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 26, convert(nvarchar(max), 0x9103BA03CD03C103C903C303B703), '2015-04-14 06:45:25.737')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 27, convert(nvarchar(max), 0xC303C403BF03), '2015-04-14 06:45:25.737')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 28, convert(nvarchar(max), 0xA003B103C103B103BA03B103BB03CE032000B503C003B903BB03AD03BE03C403B5032000B703BC03AD03C103B1032000C403B703C2032000B303AD03BD03BD03B703C303AE03C2032000C303B103C203), '2015-04-14 06:47:01.753')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 29, convert(nvarchar(max), 0x9403B503BD032000C503C003AC03C103C703BF03C503BD032000B403B903B103B803AD03C303B903BC03B503C2032000C703C103BF03BD03BF03B803C503C103AF03B403B503C203), '2015-04-14 06:44:38.393')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 30, convert(nvarchar(max), 0xA303C503B303B303BD03CE03BC03B703210020009403B503BD032000B503AF03BD03B103B9032000B403C503BD03B103C403AE032000B7032000B503C003B503BE03B503C103B303B103C303AF03B1032000C403BF03C5032000B103B903C403AE03BC03B103C403CC03C2032000C303B103C2032E00), '2015-04-14 06:45:35.817')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 31, convert(nvarchar(max), 0xA003B103C103B103BA03B103BB03BF03CD03BC03B5032000B103BD03B103C603AD03C103B503C403B5032000C403B703BD032000C503C003BF03B403BF03C703AE03), '2015-04-14 06:45:35.820')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 32, convert(nvarchar(max), 0x95039D03A40386039E0395039903), '2015-04-14 06:45:35.820')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 33, convert(nvarchar(max), 0xA603B903BD03AF03C103B903C303BC03B103), '2015-04-14 06:45:48.503')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 34, convert(nvarchar(max), 0x9503C503C703B103C103B903C303C403CE03), '2015-04-14 06:45:48.503')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 35, convert(nvarchar(max), 0x8603C603B903BE03B7032000C303B103C2032000AD03C703B503B9032000B503C003B903B203B503B203B103B903C903B803B503AF03), '2015-04-14 06:45:48.503')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 36, convert(nvarchar(max), 0x9D03B103AF03), '2015-04-14 06:45:48.503')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 37, convert(nvarchar(max), 0x8C03C703B903), '2015-04-14 06:45:48.503')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 38, convert(nvarchar(max), 0x9803AD03BB03B503C403B5032000BD03B1032000BB03AC03B203B503C403B5032000BC03AD03C103BF03C2032000C303C403B703BD032000AD03C103B503C503BD03B1033B00), '2015-04-14 06:45:48.503')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 39, convert(nvarchar(max), 0xA603B903BD03AF03C103B903C303BC03B103), '2015-04-14 06:45:58.640')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 40, convert(nvarchar(max), 0xB303B903B1032000B103AF03C403B703BC03B1032000B303B903B1032000C103B103BD03C403B503B203BF03CD032000C303B103C203), '2015-04-14 06:45:58.640')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 41, convert(nvarchar(max), 0x9503C503C703B103C103B903C303C403CE03), '2015-04-14 06:45:58.640')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 42, convert(nvarchar(max), 0xC303C403BF03), '2015-04-14 06:45:58.643')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 43, convert(nvarchar(max), 0x9503C503C703B103C103B903C303C403CE03), '2015-04-14 06:46:07.343')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 44, convert(nvarchar(max), 0xA603B903BD03AF03C103B903C303BC03B103), '2015-04-14 06:46:07.343')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 45, convert(nvarchar(max), 0x8803C103B503C503BD03B1032000C303B103C2032000AD03C703B503B9032000BA03B103C403B103B303C103B103C603B503AF03), '2015-04-14 06:46:07.347')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 46, convert(nvarchar(max), 0xA003B103C103B103BA03B103BB03CE032000B503C003B903BB03AD03BE03C403B5032000C403BF032000C003C103CE03C403BF032000B303C103AC03BC03BC03B1032000C403BF03C50320008C03BD03BF03BC03B1032000C303B103C203), '2015-04-14 06:47:07.617')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 47, convert(nvarchar(max), 0xA003B103C103B103BA03B103BB03CE032000B503C003B903BB03AD03BE03C403B5032000C403BF032000C003C103CE03C403BF032000B303C103AC03BC03BC03B1032000C403BF03C5032000B503C003C903BD03CD03BC03BF03C5032000C303B103C203), '2015-04-14 06:47:13.873')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 48, convert(nvarchar(max), 0x9503B903C303AC03B303B503C403B5032000C403B703BD032000B703BC03B503C103BF03BC03B703BD03AF03B1032000B303AD03BD03BD03B703C303B703C203), '2015-04-14 06:47:28.500')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 49, convert(nvarchar(max), 0x9703BC03AD03C103B103), '2015-04-14 06:47:28.500')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 50, convert(nvarchar(max), 0x9C03AE03BD03B103C203), '2015-04-14 06:47:28.500')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 51, convert(nvarchar(max), 0x8803C403BF03C203), '2015-04-14 06:47:28.500')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 52, convert(nvarchar(max), 0x9503C003CC03BC03B503BD03BF03C203), '2015-04-14 06:47:28.500')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 53, convert(nvarchar(max), 0x9503C003B903BB03AD03BE03C403B5032000C403BF032000C603CD03BB03BF032000C303B103C203), '2015-04-14 06:47:38.430')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 54, convert(nvarchar(max), 0x9103C103C303B503BD03B903BA03CC03C203), '2015-04-14 06:47:38.430')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 55, convert(nvarchar(max), 0x9803AE03BB03C503), '2015-04-14 06:47:38.430')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 56, convert(nvarchar(max), 0x9C03B503C403B103C603BF03C103AC03), '2015-04-14 06:47:38.430')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 57, convert(nvarchar(max), 0x4B0049004F0053004B002000B403B903B103C703B503B903BC03AC03B603BF03C503BD0320002E002E002E002E00), '2015-04-14 06:46:16.183')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 58, convert(nvarchar(max), 0xA703C103BF03BD03BF03B403B903AC03B303C103B103BC03BC03B1032000C303C40320002E002E002E00), '2015-04-14 06:46:33.533')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 59, convert(nvarchar(max), 0x9A03B103BB03C903C303CC03C103B903C303B503C2032000A303C403BF03), '2015-04-14 06:46:40.297')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 60, convert(nvarchar(max), 0x9503C003B903BB03AD03BE03C403B5032000BC03B903B1032000B303BB03CE03C303C303B1032000B303B903B1032000BD03B1032000BE03B503BA03B903BD03AE03C303B503B903), '2015-04-14 06:46:40.297')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 61, convert(nvarchar(max), 0xA003B103C103B103BA03B103BB03CE032000B503C003B903BB03AD03BE03C403B5032000BC03B903B1032000B503C003B903BB03BF03B303AE03), '2015-04-14 06:46:45.477')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 62, convert(nvarchar(max), 0xA003B103C103B103BA03B103BB03BF03CD03BC03B5032000B503C003B903BB03AD03BE03C403B5032000C403BF032000BC03AE03BD03B1032000B303AD03BD03BD03B703C303AE03C2032000C303B103C203), '2015-04-14 06:47:44.460')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 63, convert(nvarchar(max), 0x9303B903B1032000BD03B1032000BE03B503BA03B903BD03AE03C303B503C403B5032C002000B503C003B903BB03AD03BE03C403B5032000BC03AF03B1032000B103C003CC032000C403B903C2032000C003B103C103B103BA03AC03C403C9032000B503C003B903BB03BF03B303AD03C203), '2015-04-14 06:46:51.017')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 64, convert(nvarchar(max), 0x9A03B103BB03C903C303CC03C103B903C303B503C2032000A303C403BF03), '2015-04-14 06:46:56.093')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 65, convert(nvarchar(max), 0xA003B103C103B103BA03B103BB03CE032000B503C003B903BB03AD03BE03C403B5032000AD03BD03B1032000B503AF03B403BF03C2032000C303C703B903C303BC03AE03), '2015-04-14 06:48:09.333')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 66, convert(nvarchar(max), 0x9503C003CC03BC03B503BD03BF03C203), '2015-04-14 06:48:23.610')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 67, convert(nvarchar(max), 0x9D03B1032000B103C003B103BD03C403AE03C303B503C403B5032000C303C403B903C2032000B103BA03CC03BB03BF03C503B803B503C2032000B503C103C903C403AE03C303B503B903C203), '2015-04-14 06:48:23.610')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 68, convert(nvarchar(max), 0x9C03B503C403B103C603BF03C103AC03), '2015-04-14 06:48:23.610')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 69, convert(nvarchar(max), 0xA003B103C103B103BA03B103BB03CE032000B503C003B903BB03AD03BE03C403B5032000BC03B903B1032000B503C003B903BB03BF03B303AE03), '2015-04-14 06:48:15.450')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 70, convert(nvarchar(max), 0x8803C103B503C503BD03B503C2032000B403B903B103B803AD03C303B903BC03BF032000C003C103BF03C2032000C403BF032000C003B103C103CC03BD03), '2015-04-14 06:48:15.450')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 71, convert(nvarchar(max), 0xA003B103C103B103BA03B103BB03CE032000B503C003B903BB03AD03BE03C403B5032000C403BF032000AD03C403BF03C2032000B303AD03BD03BD03B703C303AE03C2032000C303B103C203), '2015-04-14 06:48:03.753')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 72, convert(nvarchar(max), 0x95039D03A40386039E0395039903), '2015-04-14 06:48:03.753')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 73, convert(nvarchar(max), 0x9A03B103BD03AD03BD03B1032000B103C003CC032000C403B1032000C003B103C103B103C003AC03BD03C903), '2015-04-14 06:48:03.753')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 74, convert(nvarchar(max), 0x8803C403BF03C20320009403B503BD0320009203C103AD03B803B703BA03B503), '2015-04-14 06:48:03.753')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 75, convert(nvarchar(max), 0x9503C003B903BA03BF03B903BD03C903BD03AE03C303C403B5032000BC03B5032000C403B7032000C103B503C303B503C803B903CC03BD03), '2015-04-14 06:48:03.753')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 82, convert(nvarchar(max), 0x9C03B703BD03CD03BC03B103C403B103), '2015-04-14 06:45:16.630')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 83, convert(nvarchar(max), 0x9503C003CC03BC03B503BD03BF03C203), '2015-04-14 06:45:16.630')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 84, convert(nvarchar(max), 0xA003B103C103B103BA03B103BB03CE032000B503C003B903BB03AD03BE03C403B5032000C403BF032000AD03C403BF03C2032000B303AD03BD03BD03B703C303AE03C2032000C303B103C203), '2015-04-14 06:47:51.993')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 85, convert(nvarchar(max), 0x9503C003CC03BC03B503BD03BF03C203), '2015-04-14 06:47:51.997')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (9, 88, convert(nvarchar(max), 0xB503BB03BB03B703BD03B903BA03AC03), '')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 1, convert(nvarchar(max), 0x060917092E092809), '2015-04-14 06:54:42.263')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 2, convert(nvarchar(max), 0x380930094D093509470915094D0937092309), '2015-04-14 06:54:42.263')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 3, convert(nvarchar(max), 0x38093E0907091F0920002E093E0928091A093F0924094D093009), '2015-04-14 06:54:42.263')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 4, convert(nvarchar(max), 0x28093F092F09410915094D0924093F09200015093009240947092000390948090209), '2015-04-14 06:54:42.263')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 5, convert(nvarchar(max), 0x15094709200032093F090F09200028093F092F09410915094D0924093F09), '2015-04-14 06:53:27.317')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 6, convert(nvarchar(max), 0x2A09410937094D091F093F0920001509300947090209), '2015-04-14 06:53:27.317')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 7, convert(nvarchar(max), 0x300926094D09260920001509300928093E09), '2015-04-14 06:53:27.317')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 8, convert(nvarchar(max), 0x2E093E092B0920001509300928093E092C00200039092E09200006092A0915094709200035093F0935093009230920002A094D0930093E092A094D0924092000150930092809470920002E09470902092000050938092E0930094D09250920003009390947092000390948090209), '2015-04-14 06:53:27.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 9, convert(nvarchar(max), 0x30093F09380947092A094D09360928092000380947092000380902092A0930094D0915092000150930094709020920002F093E0920002B093F09300920002A094D0930092F093E09380920001509300928094709200015094709200032093F090F092000280940091A094709200026093F090F09200017090F0920002C091F09280920002A093009200015094D0932093F091509200015093009470902092000150943092A092F093E09), '2015-04-14 06:53:27.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 10, convert(nvarchar(max), 0x2A0941092809030920002A094D0930092F093E09380920001509300947090209), '2015-04-14 06:53:27.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 11, convert(nvarchar(max), 0x38092D09400920001509400920002A09410937094D091F093F0920001509300947090209), '2015-04-14 06:53:27.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 12, convert(nvarchar(max), 0x1C0932094D09260940092000060917092E092809), '2015-04-14 06:53:27.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 13, convert(nvarchar(max), 0x15094D0937092E093E09200015093009470902092C00200006092A0915094009200028093F092F09410915094D0924093F09200039092E09200005092A0928094009200028093F092F09410915094D0924093F0920003809470920002A0939093209470920002300230020002E093F0928091F0920002409150920002E0947090209200006092A0920001509400920001C093E0901091A092000150930092809470920002E09470902092000050938092E0930094D09250920003909480902092C0020002300230020003909480964092000150943092A092F093E0920002C093E09260920002E094709020920002A09410928093A0020002A094D0930092F093E093809200015093009470902096409), '2015-04-14 06:53:27.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 14, convert(nvarchar(max), 0x2609470930092000380947092000060917092E092809), '2015-04-14 06:53:27.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 15, convert(nvarchar(max), 0x15094D0937092E093E09200015093009470902092C00200023002300200015094009200005092A0928094009200028093F092F09410915094D0924093F09200015094709200038092E092F0920002C094009240920001A09410915093E09200039094809200014093009200039092E0920002E0947090209200038094709200006092A0920001509400920001C093E0901091A092000150930092809470920002E09470902092000050938092E0930094D09250920003909480902096409200038094D0935093E09170924092000380947092000380902092A0930094D09150920001509300947090209), '2015-04-14 06:53:27.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 16, convert(nvarchar(max), 0x200940091509), '2015-04-14 06:53:27.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 17, convert(nvarchar(max), 0x15094D0937092E093E09200015093009470902092100200006092A0915093E09200005092809410930094B09270920003809020938093E0927093F0924092000150930092809470920002E09470902092000050938092E0930094D0925096409), '2015-04-14 06:53:27.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 18, convert(nvarchar(max), 0x38094D0935093E091709240920001509300928094709200015094709200032093F090F09200030093F092A094B0930094D091F0920001509300947090209), '2015-04-14 06:53:27.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 19, convert(nvarchar(max), 0x0F09150920002A09380902092609400926093E09200026093F092809200015093E0920001A092F09280920001509300947090209), '2015-04-14 06:49:41.510')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 20, convert(nvarchar(max), 0x2E093E092B0920001509300928093E092C00200039092E09200006092A0915094709200035093F0935093009230920002A094D0930093E092A094D0924092000150930092809470920002E09470902092000050938092E0930094D09250920003009390947092000390948090209), '2015-04-14 06:49:41.510')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 21, convert(nvarchar(max), 0x30093F09380947092A094D09360928092000380947092000380902092A0930094D0915092000150930094709020920002F093E0920002B093F09300920002A094D0930092F093E09380920001509300928094709200015094709200032093F090F092000280940091A094709200026093F090F09200017090F0920002C091F09280920002A093009200015094D0932093F091509200015093009470902092000150943092A092F093E09), '2015-04-14 06:49:41.510')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 22, convert(nvarchar(max), 0x2A0941092809030920002A094D0930092F093E09380920001509300947090209), '2015-04-14 06:49:41.510')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 23, convert(nvarchar(max), 0x38094D0935093E0917092409), '2015-04-14 06:49:41.510')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 24, convert(nvarchar(max), 0x06092A0915094709200005092809410930094B09270920001509400920002A09410937094D091F093F0920001509300947090209), '2015-04-14 06:53:41.993')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 25, convert(nvarchar(max), 0x2A09410937094D091F093F0920001509300947090209), '2015-04-14 06:53:41.993')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 26, convert(nvarchar(max), 0x300926094D09260920001509300928093E09), '2015-04-14 06:53:41.993')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 27, convert(nvarchar(max), 0x2A093009), '2015-04-14 06:53:41.993')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 28, convert(nvarchar(max), 0x1C0928094D092E09200015094709200005092A0928094709200026093F092809200015093E0920001A092F09280920001509300947090209), '2015-04-14 06:55:15.647')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 29, convert(nvarchar(max), 0x15094B090809200009092A0932092C094D092709200038094D09320949091F09), '2015-04-14 06:52:26.593')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 30, convert(nvarchar(max), 0x15094D0937092E093E09200015093009470902092100200006092A0915093E09200005092809410930094B09270920003809020938093E0927093F0924092000150930092809470920002E09470902092000050938092E0930094D0925096409), '2015-04-14 06:53:51.693')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 31, convert(nvarchar(max), 0x38094D0935093E091709240920001509300928094709200015094709200032093F090F09200030093F092A094B0930094D091F0920001509300947090209), '2015-04-14 06:53:51.693')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 32, convert(nvarchar(max), 0x200940091509), '2015-04-14 06:53:51.693')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 33, convert(nvarchar(max), 0x050902092409), '2015-04-14 06:54:03.083')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 34, convert(nvarchar(max), 0x270928094D092F0935093E092609), '2015-04-14 06:54:03.083')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 35, convert(nvarchar(max), 0x05092A09280947092000060917092E09280920001509400920002A09410937094D091F093F09200015094009200039094809), '2015-04-14 06:54:03.083')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 36, convert(nvarchar(max), 0x39093E090209), '2015-04-14 06:54:03.083')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 37, convert(nvarchar(max), 0x2809390940090209), '2015-04-14 06:54:03.083')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 38, convert(nvarchar(max), 0x06092A092000380930094D093509470915094D093709230920003209470928094709200015094709200032093F090F0920001509300928093E0920001A093E0939092409470920003909480902093F00), '2015-04-14 06:54:03.083')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 39, convert(nvarchar(max), 0x050902092409), '2015-04-14 06:54:15.157')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 40, convert(nvarchar(max), 0x05092A0928094009200028093F092F09410915094D0924093F09200015094709200005092809410930094B092709200015094709200032093F090F09), '2015-04-14 06:54:15.157')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 41, convert(nvarchar(max), 0x270928094D092F0935093E092609), '2015-04-14 06:54:15.157')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 42, convert(nvarchar(max), 0x2A093009), '2015-04-14 06:54:15.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 43, convert(nvarchar(max), 0x270928094D092F0935093E092609), '2015-04-14 06:54:23.443')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 44, convert(nvarchar(max), 0x050902092409), '2015-04-14 06:54:23.443')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 45, convert(nvarchar(max), 0x06092A0915093E092000380930094D093509470915094D09370923092000260930094D091C09200015093F092F093E09200017092F093E09200039094809), '2015-04-14 06:54:23.443')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 46, convert(nvarchar(max), 0x05092A09280947092000460069007200730074004E0061006D00650020001509470920002A093909320947092000050915094D0937093009200015093E0920001A092F09280920001509300947090209), '2015-04-14 06:55:20.467')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 47, convert(nvarchar(max), 0x05092A0928094709200009092A0928093E092E0920001509470920002A093909320947092000050915094D0937093009200015093E0920001A092F09280920001509300947090209), '2015-04-14 06:55:25.900')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 48, convert(nvarchar(max), 0x1C0928094D092E09200024093E093009400916092000260930094D091C0920001509300947090209), '2015-04-14 06:55:39.210')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 49, convert(nvarchar(max), 0x26093F092809), '2015-04-14 06:55:39.210')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 50, convert(nvarchar(max), 0x2E093E093909), '2015-04-14 06:55:39.210')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 51, convert(nvarchar(max), 0x350930094D093709), '2015-04-14 06:55:39.210')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 52, convert(nvarchar(max), 0x0509170932093E09), '2015-04-14 06:55:39.210')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 53, convert(nvarchar(max), 0x150943092A092F093E09200005092A0928093E09200032093F090209170920001A094109280947090209), '2015-04-14 06:55:48.233')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 54, convert(nvarchar(max), 0x28093009), '2015-04-14 06:55:48.233')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 55, convert(nvarchar(max), 0x2E0939093F0932093E09), '2015-04-14 06:55:48.233')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 56, convert(nvarchar(max), 0x1B094B0921093C0947090209), '2015-04-14 06:55:48.237')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 57, convert(nvarchar(max), 0x15093F092F09490938094D0915092000380941092A094D0924093E09350938094D0925093E0920002E094709020920002E002E002E002E00), '2015-04-14 06:54:32.773')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 58, convert(nvarchar(max), 0x060909091F09200038092E092F0920002E002E002E00), '2015-04-14 06:54:48.127')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 59, convert(nvarchar(max), 0x2E0947090209200038094D0935093E0917092409200039094809), '2015-04-14 06:54:53.760')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 60, convert(nvarchar(max), 0x360941093009420920001509300928094709200015094709200032093F090F0920000F09150920002D093E0937093E09200015093E0920001A092F092809), '2015-04-14 06:54:53.760')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 61, convert(nvarchar(max), 0x150943092A092F093E09200035093F09150932094D092A09200015093E0920001A092F09280920001509300947090209), '2015-04-14 06:54:58.463')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 62, convert(nvarchar(max), 0x1C0928094D092E09200015094709200005092A092809470920002E093909400928094709200015093E0920001A092F09280920001509300947090209), '2015-04-14 06:55:53.250')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 63, convert(nvarchar(max), 0x2A094D0930093E09300902092D0920001509300928094709200015094709200032093F090F092000280940091A094709200026093F090F09200017090F09200035093F09150932094D092A094B09020920002E094709020920003809470920000F091509200015093E0920001A092F09280920001509300947090209), '2015-04-14 06:55:05.390')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 64, convert(nvarchar(max), 0x2E0947090209200038094D0935093E0917092409200039094809), '2015-04-14 06:55:10.413')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 65, convert(nvarchar(max), 0x0F091509200038094D09320949091F0920002A094D09300915093E093009200015093E0920001A092F09280920001509300947090209), '2015-04-14 06:56:13.987')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 66, convert(nvarchar(max), 0x0509170932093E09), '2015-04-14 06:56:27.267')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 67, convert(nvarchar(max), 0x28093F092E094D09280932093F0916093F09240920002A094D09300936094D0928094B090209200015093E092000090924094D09240930092000260947090209), '2015-04-14 06:56:27.267')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 68, convert(nvarchar(max), 0x1B094B0921093C0947090209), '2015-04-14 06:56:27.267')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 69, convert(nvarchar(max), 0x0F091509200035093F09150932094D092A09200015093E0920001A092F09280920001509300947090209), '2015-04-14 06:56:20.010')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 70, convert(nvarchar(max), 0x350930094D0924092E093E09280920002E094709020920000509280941092A0932092C094D0927092000380930094D093509470915094D0937092309), '2015-04-14 06:56:20.010')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 71, convert(nvarchar(max), 0x1C0928094D092E09200015094709200005092A09280947092000350930094D093709200015093E0920001A092F09280920001509300947090209), '2015-04-14 06:56:09.550')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 72, convert(nvarchar(max), 0x200940091509), '2015-04-14 06:56:09.550')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 73, convert(nvarchar(max), 0x070928092E0947090209200038094709200015094B09080920002D09400920002809390940090209), '2015-04-14 06:56:09.550')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 74, convert(nvarchar(max), 0x350930094D0937092000280939094009020920002E093F0932093E09), '2015-04-14 06:56:09.550')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 75, convert(nvarchar(max), 0x30093F09380947092A094D09360928092000380947092000380902092A0930094D09150920001509300947090209), '2015-04-14 06:56:09.550')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 82, convert(nvarchar(max), 0x38090209260947093609), '2015-04-14 06:53:34.227')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 83, convert(nvarchar(max), 0x0509170932093E09), '2015-04-14 06:53:34.227')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 84, convert(nvarchar(max), 0x1C0928094D092E09200015094709200005092A09280947092000350930094D093709200015093E0920001A092F09280920001509300947090209), '2015-04-14 06:55:59.097')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 85, convert(nvarchar(max), 0x0509170932093E09), '2015-04-14 06:55:59.097')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (10, 89, convert(nvarchar(max), 0x39093F0928094D0926094009), '')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 1, convert(nvarchar(max), 0x410072007200690076006F00), '2015-04-14 06:58:39.667')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 2, convert(nvarchar(max), 0x53006F006E00640061006700670069006F00), '2015-04-14 06:58:39.667')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 3, convert(nvarchar(max), 0x4D0061007000700061002000640065006C0020007300690074006F00), '2015-04-14 06:58:39.667')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 4, convert(nvarchar(max), 0x5000720065006E006400650072006500200041007000700075006E00740061006D0065006E0074006F00), '2015-04-14 06:58:39.667')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 19, convert(nvarchar(max), 0x530065006C0065007A0069006F006E006100200075006E0061002000670069006F0072006E006100740061002000700072006500660065007200690074006100), '2015-04-14 06:56:55.003')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 20, convert(nvarchar(max), 0x4300690020006400690073007000690061006300650020006E006F006E0020007300690061006D006F00200069006E00200067007200610064006F002000640069002000740072006F007600610072006500200069002000740075006F00690020006400610074006900), '2015-04-14 06:56:55.003')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 21, convert(nvarchar(max), 0x53006900200070007200650067006100200064006900200063006F006E00740061007400740061007200650020006C006100200072006500630065007000740069006F006E0020006F00200063006C0069006300630061002000730075006C002000700075006C00730061006E00740065002000710075006900200073006F00740074006F002000700065007200200072006900700072006F007600610072006500), '2015-04-14 06:56:55.003')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 22, convert(nvarchar(max), 0x52006900700072006F0076006100), '2015-04-14 06:56:55.003')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 23, convert(nvarchar(max), 0x420065006E00760065006E00750074006F00), '2015-04-14 06:56:55.003')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 24, convert(nvarchar(max), 0x43006F006E006600650072006D00610020006C00610020007400750061002000720069006300680069006500730074006100), '2015-04-14 06:57:39.173')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 25, convert(nvarchar(max), 0x43006F006E006600650072006D00610072006500), '2015-04-14 06:57:39.173')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 26, convert(nvarchar(max), 0x430061006E00630065006C006C00610072006500), '2015-04-14 06:57:39.173')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 27, convert(nvarchar(max), 0x6100), '2015-04-14 06:57:39.173')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 28, convert(nvarchar(max), 0x530065006C0065007A0069006F006E006100200069006C002000740075006F002000670069006F0072006E006F0020006400690020006E00610073006300690074006100), '2015-04-14 07:03:29.710')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 29, convert(nvarchar(max), 0x4E006F00200053006C006F007400200064006900730070006F006E006900620069006C006900), '2015-04-14 06:57:00.703')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 30, convert(nvarchar(max), 0x53006F007200720079002100200049006D0070006F00730073006900620069006C006500200065006C00610062006F00720061007200650020006C006100200076006F00730074007200610020007200690063006800690065007300740061002E00), '2015-04-14 06:57:48.007')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 31, convert(nvarchar(max), 0x5300690020007000720065006700610020006400690020007300650067006E0061006C00610072006500200061006C006C006100200072006500630065007000740069006F006E00), '2015-04-14 06:57:48.007')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 32, convert(nvarchar(max), 0x4F004B00), '2015-04-14 06:57:48.007')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 33, convert(nvarchar(max), 0x460069006E0069007400750072006100), '2015-04-14 06:57:59.180')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 34, convert(nvarchar(max), 0x4700720061007A0069006500), '2015-04-14 06:57:59.180')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 35, convert(nvarchar(max), 0x49006C002000740075006F002000610072007200690076006F002000E800200063006F006E006600650072006D00610074006100), '2015-04-14 06:57:59.180')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 36, convert(nvarchar(max), 0x5300EC00), '2015-04-14 06:57:59.180')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 37, convert(nvarchar(max), 0x4E006F00), '2015-04-14 06:57:59.180')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 38, convert(nvarchar(max), 0x560075006F006900200070006100720074006500630069007000610072006500200061006C00200073006F006E00640061006700670069006F003F00), '2015-04-14 06:57:59.180')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 39, convert(nvarchar(max), 0x460069006E0069007400750072006100), '2015-04-14 06:58:10.053')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 40, convert(nvarchar(max), 0x70006500720020006C0061002000720069006300680069006500730074006100200064006900200061007000700075006E00740061006D0065006E0074006F00), '2015-04-14 06:58:10.053')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 41, convert(nvarchar(max), 0x4700720061007A0069006500), '2015-04-14 06:58:10.053')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 42, convert(nvarchar(max), 0x6100), '2015-04-14 06:58:10.053')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 43, convert(nvarchar(max), 0x4700720061007A0069006500), '2015-04-14 06:58:18.273')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 44, convert(nvarchar(max), 0x460069006E0069007400750072006100), '2015-04-14 06:58:18.273')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 45, convert(nvarchar(max), 0x49006C00200076006F007300740072006F00200073006F006E00640061006700670069006F002000E800200073007400610074006F0020007200650067006900730074007200610074006F00), '2015-04-14 06:58:18.273')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 46, convert(nvarchar(max), 0x530065006C0065007A0069006F006E00610020007000720069006D00610020006C006500740074006500720061002000640065006C00200063006F0067006E006F006D006500), '2015-04-14 07:03:34.273')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 47, convert(nvarchar(max), 0x530065006C0065007A0069006F006E00610020007000720069006D00610020006C006500740074006500720061002000640065006C00200063006F0067006E006F006D006500), '2015-04-14 07:03:38.920')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 48, convert(nvarchar(max), 0x49006E0073006500720069007200650020006C0061002000640061007400610020006400690020006E00610073006300690074006100), '2015-04-14 07:03:51.080')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 49, convert(nvarchar(max), 0x470069006F0072006E006F00), '2015-04-14 07:03:51.080')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 50, convert(nvarchar(max), 0x4D00650073006500), '2015-04-14 07:03:51.080')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 51, convert(nvarchar(max), 0x41006E006E006F00), '2015-04-14 07:03:51.080')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 52, convert(nvarchar(max), 0x49006C002000500072006F007300730069006D006F00), '2015-04-14 07:03:51.080')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 53, convert(nvarchar(max), 0x530065006C0065007A0069006F006E006100200069006C002000740075006F00200073006500730073006F00), '2015-04-14 07:03:59.670')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 54, convert(nvarchar(max), 0x4D00610073006300680069006F00), '2015-04-14 07:03:59.670')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 55, convert(nvarchar(max), 0x460065006D006D0069006E0069006C006500), '2015-04-14 07:03:59.670')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 56, convert(nvarchar(max), 0x530061006C007400610072006500), '2015-04-14 07:03:59.670')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 57, convert(nvarchar(max), 0x4B0049004F0053004B002000480069006200650072006E006100740069006E00670020002E002E002E002E00), '2015-04-14 06:58:24.637')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 58, convert(nvarchar(max), 0x540069006D0069006E00670020004F0075007400200049006E0020002E002E002E00), '2015-04-14 06:58:44.617')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 59, convert(nvarchar(max), 0x420065006E00760065006E00750074006F0020004100), '2015-04-14 07:01:54.293')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 60, convert(nvarchar(max), 0x730065006C0065007A0069006F006E00610072006500200075006E00610020006C0069006E006700750061002000700065007200200069006E0069007A006900610072006500), '2015-04-14 07:01:54.293')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 61, convert(nvarchar(max), 0x530065006C0065007A0069006F006E006100200075006E0027006F0070007A0069006F006E006500), '2015-04-14 07:03:14.563')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 62, convert(nvarchar(max), 0x530065006C0065007A0069006F006E006100200069006C0020006D0065007300650020006400690020006E00610073006300690074006100), '2015-04-14 07:04:04.323')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 63, convert(nvarchar(max), 0x500065007200200069006E0069007A0069006100720065002C002000730069002000700072006500670061002000640069002000730065006C0065007A0069006F006E00610072006500200075006E0061002000640065006C006C00650020006F0070007A0069006F006E0069002000710075006900200073006F00740074006F00), '2015-04-14 07:03:19.983')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 64, convert(nvarchar(max), 0x420065006E00760065006E00750074006F0020004100), '2015-04-14 07:03:25.113')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 65, convert(nvarchar(max), 0x530065006C0065007A0069006F006E00610072006500200075006E0020007400690070006F00200064006900200073006C006F007400), '2015-04-14 07:04:23.193')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 66, convert(nvarchar(max), 0x49006C002000500072006F007300730069006D006F00), '2015-04-14 07:04:34.607')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 67, convert(nvarchar(max), 0x53006900200070007200650067006100200064006900200072006900730070006F006E006400650072006500200061006C006C0065002000730065006700750065006E0074006900200064006F006D0061006E0064006500), '2015-04-14 07:04:34.607')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 68, convert(nvarchar(max), 0x530061006C007400610072006500), '2015-04-14 07:04:34.607')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 71, convert(nvarchar(max), 0x530065006C0065007A0069006F006E006100200069006C002000740075006F00200061006E006E006F0020006400690020006E00610073006300690074006100), '2015-04-14 07:04:18.523')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 72, convert(nvarchar(max), 0x4F004B00), '2015-04-14 07:04:18.523')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 73, convert(nvarchar(max), 0x4E0065007300730075006E0061002000640065006C006C006500200070007200650063006500640065006E0074006900), '2015-04-14 07:04:18.523')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 74, convert(nvarchar(max), 0x41006E006E006F0020004E006F007400200046006F0075006E006400), '2015-04-14 07:04:18.523')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 75, convert(nvarchar(max), 0x53006900200070007200650067006100200064006900200063006F006E00740061007400740061007200650020006C006100200072006500630065007000740069006F006E00), '2015-04-14 07:04:18.523')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 82, convert(nvarchar(max), 0x4D006500730073006100670067006900), '2015-04-14 06:57:29.723')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 83, convert(nvarchar(max), 0x49006C002000500072006F007300730069006D006F00), '2015-04-14 06:57:29.723')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 84, convert(nvarchar(max), 0x530065006C0065007A0069006F006E006100200069006C002000740075006F00200061006E006E006F0020006400690020006E00610073006300690074006100), '2015-04-14 07:04:09.610')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 85, convert(nvarchar(max), 0x49006C002000500072006F007300730069006D006F00), '2015-04-14 07:04:09.610')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (11, 90, convert(nvarchar(max), 0x6900740061006C00690061006E006F00), '')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 1, convert(nvarchar(max), 0x500072007A0079006A0061007A006400), '2015-04-14 07:06:16.443')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 2, convert(nvarchar(max), 0x42006100640061006E0069006500), '2015-04-14 07:06:16.443')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 3, convert(nvarchar(max), 0x4D0061007000610020007300740072006F006E007900), '2015-04-14 07:06:16.447')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 4, convert(nvarchar(max), 0x44006F00640061000701200050006F0077006F0042017900770061006E0069006500), '2015-04-14 07:06:16.447')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 5, convert(nvarchar(max), 0x4E006F006D0069006E00610063006A006500200064006C006100), '2015-04-14 07:05:15.070')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 6, convert(nvarchar(max), 0x50006F007400770069006500720064007A0061000701), '2015-04-14 07:05:15.070')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 7, convert(nvarchar(max), 0x41006E0075006C006F00770061000701), '2015-04-14 07:05:15.070')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 8, convert(nvarchar(max), 0x4E00690065007300740065007400790020006E006900650020006A0065007300740065005B016D0079002000770020007300740061006E006900650020007A006E0061006C0065007A0107012000730077006F006A0065002000640061006E006500), '2015-04-14 07:05:15.070')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 9, convert(nvarchar(max), 0x500072006F00730069006D00790020006F0020006B006F006E00740061006B00740020007A0020007200650063006500700063006A00050120006C007500620020004B006C0069006B006E0069006A002000700072007A0079006300690073006B00200070006F006E0069007C0165006A002C0020006100620079002000730070007200F30062006F00770061000701200070006F006E006F0077006E0069006500), '2015-04-14 07:05:15.073')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 10, convert(nvarchar(max), 0x530070007200F300620075006A00200050006F006E006F0077006E0069006500), '2015-04-14 07:05:15.073')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 11, convert(nvarchar(max), 0x50006F007400770069006500720064007A012000770073007A007900730074006B006F00), '2015-04-14 07:05:15.073')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 12, convert(nvarchar(max), 0x570063007A00650073006E0065002000500072007A0079006A0061007A006400), '2015-04-14 07:05:15.073')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 13, convert(nvarchar(max), 0x4E0069006500730074006500740079002C00200054007700F3006A0020007400650072006D0069006E0020006A006500730074002000230023002C0020006E006900650020006A0065007300740065005B016D0079002000770020007300740061006E006900650020007300700072006100770064007A0069000701200073006900190120007700200061007C0120002300230020006D0069006E00750074002000700072007A00650064002000770069007A007900740005012E002000500072006F0073007A0019012000730070007200F300620075006A00200070006F006E006F0077006E006900650020007000F3007A016E00690065006A002E00), '2015-04-14 07:05:15.073')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 14, convert(nvarchar(max), 0x5000F3007A016E00650067006F002000700072007A0079006A0061007A0064007500), '2015-04-14 07:05:15.073')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 15, convert(nvarchar(max), 0x4E0069006500730074006500740079002C00200063007A0061007300200070006F0077006F00420161006E006900610020002300230020006D0069006E00190142016F002000690020006E006900650020006A0065007300740065005B016D0079002000770020007300740061006E006900650020007300700072006100770064007A006900070120007300690019012E002000500072006F0073007A00190120006F0020006B006F006E00740061006B00740020007A0020007200650063006500700063006A000501), '2015-04-14 07:05:15.073')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 16, convert(nvarchar(max), 0x4F004B00), '2015-04-14 07:05:15.073')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 17, convert(nvarchar(max), 0x500072007A0079006B0072006F0020006D006900210020004E006900650020006D006F007C016E0061002000700072007A006500740077006F0072007A007900070120007C010501640061006E00690061002E00), '2015-04-14 07:05:15.073')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 18, convert(nvarchar(max), 0x500072006F0073007A00190120007A00670042016F007300690007012000730069001901200064006F0020007200650063006500700063006A006900), '2015-04-14 07:05:15.073')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 19, convert(nvarchar(max), 0x500072006F0073007A001901200077007900620072006100070120007000720065006600650072006F00770061006E007900200064007A00690065004401), '2015-04-14 07:04:50.117')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 20, convert(nvarchar(max), 0x4E00690065007300740065007400790020006E006900650020006A0065007300740065005B016D0079002000770020007300740061006E006900650020007A006E0061006C0065007A0107012000730077006F006A0065002000640061006E006500), '2015-04-14 07:04:50.117')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 21, convert(nvarchar(max), 0x500072006F00730069006D00790020006F0020006B006F006E00740061006B00740020007A0020007200650063006500700063006A00050120006C007500620020004B006C0069006B006E0069006A002000700072007A0079006300690073006B00200070006F006E0069007C0165006A002C0020006100620079002000730070007200F30062006F00770061000701200070006F006E006F0077006E0069006500), '2015-04-14 07:04:50.117')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 22, convert(nvarchar(max), 0x530070007200F300620075006A00200050006F006E006F0077006E0069006500), '2015-04-14 07:04:50.117')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 23, convert(nvarchar(max), 0x50006F0077006900740061006E0069006500), '2015-04-14 07:04:50.120')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 24, convert(nvarchar(max), 0x500072006F0073007A001901200070006F007400770069006500720064007A00690007012000700072006F005B0162001901), '2015-04-14 07:05:28.570')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 25, convert(nvarchar(max), 0x50006F007400770069006500720064007A0061000701), '2015-04-14 07:05:28.570')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 26, convert(nvarchar(max), 0x41006E0075006C006F00770061000701), '2015-04-14 07:05:28.570')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 27, convert(nvarchar(max), 0x7700), '2015-04-14 07:05:28.570')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 28, convert(nvarchar(max), 0x500072006F0073007A0019012000770079006200720061000701200073007700F3006A00200064007A006900650044012000750072006F0064007A0069006E00), '2015-04-14 07:06:46.413')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 29, convert(nvarchar(max), 0x4200720061006B00200077006F006C006E00790063006800200073006C006F007400F3007700), '2015-04-14 07:04:54.507')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 30, convert(nvarchar(max), 0x500072007A0079006B0072006F0020006D006900210020004E006900650020006D006F007C016E0061002000700072007A006500740077006F0072007A007900070120007C010501640061006E00690061002E00), '2015-04-14 07:05:37.023')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 31, convert(nvarchar(max), 0x500072006F0073007A00190120007A00670042016F007300690007012000730069001901200064006F0020007200650063006500700063006A006900), '2015-04-14 07:05:37.023')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 32, convert(nvarchar(max), 0x4F004B00), '2015-04-14 07:05:37.023')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 33, convert(nvarchar(max), 0x570079006B006F00440163007A0065006E0069006500), '2015-04-14 07:05:47.573')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 34, convert(nvarchar(max), 0x44007A00690019016B0075006A001901), '2015-04-14 07:05:47.573')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 35, convert(nvarchar(max), 0x540077006F006A0065002000700072007A00790062007900630069006500200070006F007400770069006500720064007A006100), '2015-04-14 07:05:47.573')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 36, convert(nvarchar(max), 0x540061006B00), '2015-04-14 07:05:47.573')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 37, convert(nvarchar(max), 0x4E0069006500), '2015-04-14 07:05:47.573')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 38, convert(nvarchar(max), 0x43007A0079002000630068006300650073007A00200077007A00690005010701200061006E006B0069006500740019013F00), '2015-04-14 07:05:47.573')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 39, convert(nvarchar(max), 0x570079006B006F00440163007A0065006E0069006500), '2015-04-14 07:05:55.370')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 40, convert(nvarchar(max), 0x6E00610020007C010501640061006E0069006500200070006F0077006F00420161006E0069006100), '2015-04-14 07:05:55.370')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 41, convert(nvarchar(max), 0x44007A00690019016B0075006A001901), '2015-04-14 07:05:55.370')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 42, convert(nvarchar(max), 0x7700), '2015-04-14 07:05:55.370')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 43, convert(nvarchar(max), 0x44007A00690019016B0075006A001901), '2015-04-14 07:06:01.873')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 44, convert(nvarchar(max), 0x570079006B006F00440163007A0065006E0069006500), '2015-04-14 07:06:01.873')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 45, convert(nvarchar(max), 0x540077006F006A006100200061006E006B00690065007400610020007A006F007300740061004201610020006E0061006700720061006E006100), '2015-04-14 07:06:01.873')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 46, convert(nvarchar(max), 0x5700790062006900650072007A0020007000690065007200770073007A00050120006C00690074006500720019012000540077006F006A006500200069006D0069001901), '2015-04-14 07:06:50.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 47, convert(nvarchar(max), 0x5700790062006900650072007A0020007000690065007200770073007A00050120006C006900740065007200190120006E0061007A007700690073006B006100), '2015-04-14 07:06:56.000')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 48, convert(nvarchar(max), 0x50006F00640061006A00200064006100740019012000750072006F0064007A0065006E0069006100), '2015-04-14 07:07:07.223')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 49, convert(nvarchar(max), 0x44007A00690065004401), '2015-04-14 07:07:07.223')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 50, convert(nvarchar(max), 0x4D00690065007300690005016300), '2015-04-14 07:07:07.223')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 51, convert(nvarchar(max), 0x52006F006B00), '2015-04-14 07:07:07.223')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 52, convert(nvarchar(max), 0x4E00610073007400190170006E007900), '2015-04-14 07:07:07.227')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 53, convert(nvarchar(max), 0x5700790062006900650072007A002000730077006F006A00050120007000420165000701), '2015-04-14 07:07:15.223')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 54, convert(nvarchar(max), 0x4D0019017C0163007A0079007A006E006100), '2015-04-14 07:07:15.223')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 55, convert(nvarchar(max), 0x7B016500440173006B006900), '2015-04-14 07:07:15.223')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 56, convert(nvarchar(max), 0x53006B0069007000), '2015-04-14 07:07:15.223')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 57, convert(nvarchar(max), 0x4B0049004F0053004B002000480069006200650072006E0075006A0005016300790020002E002E002E002E00), '2015-04-14 07:06:08.730')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 58, convert(nvarchar(max), 0x540069006D0069006E00670020004F007500740020002E002E002E00), '2015-04-14 07:06:21.540')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 59, convert(nvarchar(max), 0x57006900740061006D00790020005700), '2015-04-14 07:06:28.110')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 60, convert(nvarchar(max), 0x77007900620072006100070120006A0019017A0079006B002C002000610062007900200072006F007A0070006F0063007A0005010701), '2015-04-14 07:06:28.113')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 61, convert(nvarchar(max), 0x5700790062006900650072007A0020006A00650064006E00050120007A0020006F00700063006A006900), '2015-04-14 07:06:32.437')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 62, convert(nvarchar(max), 0x500072006F0073007A001901200077007900620072006100070120006D006900650073006900050163002000750072006F0064007A0065006E0069006100), '2015-04-14 07:07:20.067')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 63, convert(nvarchar(max), 0x410062007900200072006F007A0070006F0063007A00050107012C0020006E0061006C0065007C017900200077007900620072006100070120006A00650064006E00050120007A00200070006F006E0069007C0173007A0079006300680020006F00700063006A006900), '2015-04-14 07:06:37.010')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 64, convert(nvarchar(max), 0x57006900740061006D00790020005700), '2015-04-14 07:06:41.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 65, convert(nvarchar(max), 0x500072006F0073007A0019012000770079006200720061000701200072006F0064007A0061006A00200067006E00690061007A0064006100), '2015-04-14 07:07:38.800')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 66, convert(nvarchar(max), 0x4E00610073007400190170006E007900), '2015-04-14 07:07:51.607')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 67, convert(nvarchar(max), 0x500072006F0073007A00190120006F00640070006F0077006900650064007A0069006500070120006E00610020006E006100730074001901700075006A00050163006500200070007900740061006E0069006100), '2015-04-14 07:07:51.607')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 68, convert(nvarchar(max), 0x53006B0069007000), '2015-04-14 07:07:51.607')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 69, convert(nvarchar(max), 0x500072006F0073007A001901200077007900620072006100070120006F00700063006A001901), '2015-04-14 07:07:44.243')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 70, convert(nvarchar(max), 0x53006F006E00640061007C01650020006F006200650063006E006900650020006E006900650064006F0073007400190170006E006500), '2015-04-14 07:07:44.243')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 71, convert(nvarchar(max), 0x500072006F0073007A0019012000770079006200720061000701200073007700F3006A00200072006F006B002000750072006F0064007A0065006E0069006100), '2015-04-14 07:07:34.670')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 72, convert(nvarchar(max), 0x4F004B00), '2015-04-14 07:07:34.670')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 73, convert(nvarchar(max), 0x7B01610064006E00650020007A00200070006F00770079007C0173007A00790063006800), '2015-04-14 07:07:34.670')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 74, convert(nvarchar(max), 0x52006F006B0020004E006F007400200046006F0075006E006400), '2015-04-14 07:07:34.670')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 75, convert(nvarchar(max), 0x500072006F00730069006D00790020006F0020006B006F006E00740061006B00740020007A0020007200650063006500700063006A000501), '2015-04-14 07:07:34.670')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 82, convert(nvarchar(max), 0x57006900610064006F006D006F005B0163006900), '2015-04-14 07:05:20.970')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 83, convert(nvarchar(max), 0x4E00610073007400190170006E007900), '2015-04-14 07:05:20.970')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 84, convert(nvarchar(max), 0x500072006F0073007A0019012000770079006200720061000701200073007700F3006A00200072006F006B002000750072006F0064007A0065006E0069006100), '2015-04-14 07:07:25.630')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 85, convert(nvarchar(max), 0x4E00610073007400190170006E007900), '2015-04-14 07:07:25.630')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (12, 91, convert(nvarchar(max), 0x70006F006C0073006B006900), '')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (13, 19, convert(nvarchar(max), 0x50006F00720020006600610076006F0072002000730065006C0065006300630069006F006E006500200075006D0020006400690061002000700072006500660065007200690064006F00), '2015-04-14 07:30:32.077')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (13, 20, convert(nvarchar(max), 0x440065007300630075006C00700065002C0020006D006100730020006E00E3006F0020007300E3006F00200063006100700061007A0065007300200064006500200065006E0063006F006E00740072006100720020007300650075007300200064006500740061006C00680065007300), '2015-04-14 07:30:32.077')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (13, 21, convert(nvarchar(max), 0x50006F00720020006600610076006F0072002C00200063006F006E007400610063007400650020006100200072006500630065007000E700E3006F0020006F007500200063006C00690071007500650020006E006F00200062006F007400E3006F002000610062006100690078006F00200070006100720061002000740065006E0074006100720020006E006F00760061006D0065006E0074006500), '2015-04-14 07:30:32.077')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (13, 22, convert(nvarchar(max), 0x540065006E007400650020004E006F00760061006D0065006E0074006500), '2015-04-14 07:30:32.077')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (13, 23, convert(nvarchar(max), 0x420065006D002D00760069006E0064006F00), '2015-04-14 07:30:32.077')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (13, 92, convert(nvarchar(max), 0x70006F0072007400750067007500EA007300), '')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 1, convert(nvarchar(max), 0x53006F007300690072006500), '2015-04-14 09:06:41.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 2, convert(nvarchar(max), 0x530074007500640069007500), '2015-04-14 09:06:41.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 3, convert(nvarchar(max), 0x48006100720074006100200073006900740065002D0075006C0075006900), '2015-04-14 09:06:41.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 4, convert(nvarchar(max), 0x41007300690067007500720061001B02690020004E0075006D006900720065006100), '2015-04-14 09:06:41.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 5, convert(nvarchar(max), 0x4E0075006D006900720069002000700065006E00740072007500), '2015-04-14 09:05:44.050')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 6, convert(nvarchar(max), 0x43006F006E006600690072006D006100), '2015-04-14 09:05:44.050')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 7, convert(nvarchar(max), 0x41006E0075006C006100), '2015-04-14 09:05:44.050')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 8, convert(nvarchar(max), 0xCE006D00690020007000610072006500200072000301750020006300030120006E0075002000730075006E0074002000EE006E0020006D00030173007500720003012000640065002000610020006700030173006900200064006100740065006C0065002000740061006C006500), '2015-04-14 09:05:44.050')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 9, convert(nvarchar(max), 0x56000301200072007500670003016D00200073000301200063006F006E00740061006300740061001B0269002000720065006300650070001B02690061002000730061007500200066006100630065001B026900200063006C006900630020007000650020006200750074006F006E0075006C0020006400650020006D006100690020006A006F0073002000700065006E00740072007500200061002000EE006E00630065007200630061002000640069006E0020006E006F007500), '2015-04-14 09:05:44.050')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 10, convert(nvarchar(max), 0x49006E006300650061007200630061002000440069006E0020004E006F007500), '2015-04-14 09:05:44.050')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 11, convert(nvarchar(max), 0x43006F006E006600690072006D0061001B026900200054006F00610074006500), '2015-04-14 09:05:44.050')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 12, convert(nvarchar(max), 0x53006F0073006900720065002000740069006D0070007500720069006500), '2015-04-14 09:05:44.050')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 13, convert(nvarchar(max), 0x4E0065002000700061007200650020007200030175002C0020006E0075006D0069007200650061002000640075006D006E006500610076006F0061007300740072000301200065007300740065002000230023002C0020006E007500200070007500740065006D002000730003012000760003012000760065007200690066006900630061001B0269002000EE006E0020007000E2006E00030120006C00610020002300230020006D0069006E007500740065002000EE006E00610069006E007400650020006400650020006E0075006D0069007200650061002000640075006D006E006500610076006F00610073007400720003012E00200056000301200072007500670003016D002000730003012000EE006E00630065007200630061001B0269002000640069006E0020006E006F00750020006D006100690020007400E20072007A00690075002E00), '2015-04-14 09:05:44.050')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 14, convert(nvarchar(max), 0x4C00610074006500200053006F007300690072006500), '2015-04-14 09:05:44.050')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 15, convert(nvarchar(max), 0x4E0065002000700061007200650020007200030175002C002000740069006D00700075006C0020006E0075006D0069007200650061002000640065002000230023002000610020007400720065006300750074002000190269002000730075006E00740065006D002000EE006E00200069006D0070006F0073006900620069006C00690074006100740065006100200064006500200061002000760003012000760065007200690066006900630061002E00200056000301200072007500670003016D00200073000301200063006F006E00740061006300740061001B0269002000720065006300650070001B0269006100), '2015-04-14 09:05:44.050')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 16, convert(nvarchar(max), 0x4F004B00), '2015-04-14 09:05:44.050')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 17, convert(nvarchar(max), 0x4E006500200070006100720065002000720003017500210020004E007500200073006500200070006F006100740065002000700072006F006300650073006100200063006500720065007200650061002000640075006D006E006500610076006F00610073007400720003012E00), '2015-04-14 09:05:44.050')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 18, convert(nvarchar(max), 0x56000301200072007500670003016D0020007200610070006F007200740061001B02690020006C0061002000720065006300650070001B0269006500), '2015-04-14 09:05:44.053')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 19, convert(nvarchar(max), 0x56000301200072007500670003016D002000730003012000730065006C0065006300740061001B02690020006F0020007A0069002000700072006500660065007200610074000301), '2015-04-14 09:05:21.683')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 20, convert(nvarchar(max), 0xCE006D00690020007000610072006500200072000301750020006300030120006E0075002000730075006E0074002000EE006E0020006D00030173007500720003012000640065002000610020006700030173006900200064006100740065006C0065002000740061006C006500), '2015-04-14 09:05:21.683')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 21, convert(nvarchar(max), 0x56000301200072007500670003016D00200073000301200063006F006E00740061006300740061001B0269002000720065006300650070001B02690061002000730061007500200066006100630065001B026900200063006C006900630020007000650020006200750074006F006E0075006C0020006400650020006D006100690020006A006F0073002000700065006E00740072007500200061002000EE006E00630065007200630061002000640069006E0020006E006F007500), '2015-04-14 09:05:21.683')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 22, convert(nvarchar(max), 0x49006E006300650061007200630061002000440069006E0020004E006F007500), '2015-04-14 09:05:21.683')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 23, convert(nvarchar(max), 0x420075006E002000760065006E0069007400), '2015-04-14 09:05:21.687')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 24, convert(nvarchar(max), 0x56000301200072007500670003016D00200073000301200063006F006E006600690072006D0061001B0269002000630065007200650072006500), '2015-04-14 09:05:57.710')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 25, convert(nvarchar(max), 0x43006F006E006600690072006D006100), '2015-04-14 09:05:57.710')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 26, convert(nvarchar(max), 0x41006E0075006C006100), '2015-04-14 09:05:57.710')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 27, convert(nvarchar(max), 0x6C006100), '2015-04-14 09:05:57.710')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 28, convert(nvarchar(max), 0x56000301200072007500670003016D002000730003012000730065006C0065006300740061001B02690020007A0069007500610020006400650020006E00610019027400650072006500), '2015-04-14 09:07:08.690')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 29, convert(nvarchar(max), 0x4600610072006100200064006900730070006F006E006900620069006C006500), '2015-04-14 09:05:27.203')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 30, convert(nvarchar(max), 0x4E006500200070006100720065002000720003017500210020004E007500200073006500200070006F006100740065002000700072006F006300650073006100200063006500720065007200650061002000640075006D006E006500610076006F00610073007400720003012E00), '2015-04-14 09:06:05.227')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 31, convert(nvarchar(max), 0x56000301200072007500670003016D0020007200610070006F007200740061001B02690020006C0061002000720065006300650070001B0269006500), '2015-04-14 09:06:05.227')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 32, convert(nvarchar(max), 0x4F004B00), '2015-04-14 09:06:05.227')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 33, convert(nvarchar(max), 0x460069006E006900730061006A00), '2015-04-14 09:06:13.767')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 34, convert(nvarchar(max), 0x4D0075006C001B0275006D00650073006300), '2015-04-14 09:06:13.767')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 35, convert(nvarchar(max), 0x53006F00730069007200650020006400760073002E0020006500730074006500200063006F006E006600690072006D0061007400), '2015-04-14 09:06:13.767')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 36, convert(nvarchar(max), 0x44006100), '2015-04-14 09:06:13.767')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 37, convert(nvarchar(max), 0x4E007500), '2015-04-14 09:06:13.767')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 38, convert(nvarchar(max), 0x5600720065006900200073000301200069006100200073006F006E00640061006A003F00), '2015-04-14 09:06:13.770')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 39, convert(nvarchar(max), 0x460069006E006900730061006A00), '2015-04-14 09:06:21.033')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 40, convert(nvarchar(max), 0x700065006E00740072007500200063006500720065007200650061002000640075006D006E006500610076006F00610073007400720003012000700072006F006700720061006D00610072006500), '2015-04-14 09:06:21.033')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 41, convert(nvarchar(max), 0x4D0075006C001B0275006D00650073006300), '2015-04-14 09:06:21.033')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 42, convert(nvarchar(max), 0x6C006100), '2015-04-14 09:06:21.033')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 43, convert(nvarchar(max), 0x4D0075006C001B0275006D00650073006300), '2015-04-14 09:06:26.760')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 44, convert(nvarchar(max), 0x460069006E006900730061006A00), '2015-04-14 09:06:26.760')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 45, convert(nvarchar(max), 0x5300740075006400690075002000640075006D006E006500610076006F006100730074007200610020006100200066006F0073007400200069006E007200650067006900730074007200610074006100), '2015-04-14 09:06:26.760')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 46, convert(nvarchar(max), 0x56000301200072007500670003016D002000730003012000730065006C0065006300740061001B02690020007000720069006D00610020006C00690074006500720003012000640069006E0020007000720065006E0075006D0065006C0065002000640075006D006E006500610076006F0061007300740072000301), '2015-04-14 09:07:15.220')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 47, convert(nvarchar(max), 0x56000301200072007500670003016D002000730003012000730065006C0065006300740061001B02690020007000720069006D00610020006C00690074006500720003012000640069006E0020006E0075006D0065006C0065002000640075006D006E006500610076006F0061007300740072000301), '2015-04-14 09:07:19.297')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 48, convert(nvarchar(max), 0x440061007400610020006E006100190274006500720069006900200045006E00740065007200), '2015-04-14 09:07:30.820')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 49, convert(nvarchar(max), 0x5A006900), '2015-04-14 09:07:30.820')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 50, convert(nvarchar(max), 0x4C0075006E000301), '2015-04-14 09:07:30.820')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 51, convert(nvarchar(max), 0x41006E00), '2015-04-14 09:07:30.820')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 52, convert(nvarchar(max), 0x550072006D00030174006F00720075006C00), '2015-04-14 09:07:30.820')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 53, convert(nvarchar(max), 0x56000301200072007500670003016D002000730003012000730065006C0065006300740061001B026900200073006500780075006C00), '2015-04-14 09:07:39.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 54, convert(nvarchar(max), 0x4D0061007300630075006C0069006E00), '2015-04-14 09:07:39.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 55, convert(nvarchar(max), 0x460065006D00650069006500), '2015-04-14 09:07:39.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 56, convert(nvarchar(max), 0x5300030172006900), '2015-04-14 09:07:39.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 57, convert(nvarchar(max), 0x4B0049004F0053004B002000480069006200650072006E00650061007A00030120002E002E002E002E00), '2015-04-14 09:06:33.427')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 58, convert(nvarchar(max), 0x540069006D0069006E0067002000EE006E0020002E002E002E00), '2015-04-14 09:06:47.030')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 59, convert(nvarchar(max), 0x420075006E002000560065006E006900740020004C006100), '2015-04-14 09:06:51.557')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 60, convert(nvarchar(max), 0x730065006C0065006300740061001B02690020006F0020006C0069006D00620003012000700065006E00740072007500200061002000EE006E006300650070006500), '2015-04-14 09:06:51.557')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 61, convert(nvarchar(max), 0x530065006C0065006300740061001B02690020006F0020006F0070001B02690075006E006500), '2015-04-14 09:06:56.070')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 62, convert(nvarchar(max), 0x56000301200072007500670003016D002000730003012000730065006C0065006300740061001B02690020006C0075006E00610020007400610020006400650020006E00610019027400650072006500), '2015-04-14 09:07:43.660')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 63, convert(nvarchar(max), 0x500065006E00740072007500200061002000EE006E0063006500700065002C00200076000301200072007500670003016D002000730003012000730065006C0065006300740061001B026900200075006E0061002000640069006E0074007200650020006F0070001B02690075006E0069006C00650020006400650020006D006100690020006A006F007300), '2015-04-14 09:07:00.220')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 64, convert(nvarchar(max), 0x420075006E002000560065006E006900740020004C006100), '2015-04-14 09:07:04.223')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 65, convert(nvarchar(max), 0x56000301200072007500670003016D002000730003012000730065006C0065006300740061001B026900200075006E0020007400690070002000640065002000660061006E0074000301), '2015-04-14 09:07:59.443')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 66, convert(nvarchar(max), 0x550072006D00030174006F00720075006C00), '2015-04-14 09:08:10.430')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 67, convert(nvarchar(max), 0x56000301200072007500670003016D002000730003012000720003017300700075006E00640065001B02690020006C0061002000750072006D00030174006F006100720065006C0065002000EE006E007400720065006200030172006900), '2015-04-14 09:08:10.430')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 68, convert(nvarchar(max), 0x5300030172006900), '2015-04-14 09:08:10.430')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 69, convert(nvarchar(max), 0x56000301200072007500670003016D00200073000301200061006C006500670065001B02690020006F0020006F0070001B02690075006E006500), '2015-04-14 09:08:03.960')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 70, convert(nvarchar(max), 0x53006F006E00640061006A0065002000630072006900740065007200690069006C0065002000EE006E0020007000720065007A0065006E007400), '2015-04-14 09:08:03.960')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 71, convert(nvarchar(max), 0x56000301200072007500670003016D002000730003012000730065006C0065006300740061001B026900200061006E0075006C0020006E006100190274006500720069006900), '2015-04-14 09:07:55.110')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 72, convert(nvarchar(max), 0x4F004B00), '2015-04-14 09:07:55.110')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 73, convert(nvarchar(max), 0x4E00690063006900200075006E0061002000640069006E007400720065002000630065006C00650020006400650020006D00610069002000730075007300), '2015-04-14 09:07:55.110')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 74, convert(nvarchar(max), 0x41006E0075006C0020004E00750020006100200066006F0073007400200067000301730069007400), '2015-04-14 09:07:55.110')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 75, convert(nvarchar(max), 0x56006100200072007500670061006D00200073006100200063006F006E0074006100630074006100740069002000520065006300650070001B0269006500), '2015-04-14 09:07:55.110')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 82, convert(nvarchar(max), 0x4D006500730061006A006500), '2015-04-14 09:05:51.213')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 83, convert(nvarchar(max), 0x550072006D00030174006F00720075006C00), '2015-04-14 09:05:51.213')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 84, convert(nvarchar(max), 0x56000301200072007500670003016D002000730003012000730065006C0065006300740061001B026900200061006E0075006C0020006E006100190274006500720069006900), '2015-04-14 09:07:48.107')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 85, convert(nvarchar(max), 0x550072006D00030174006F00720075006C00), '2015-04-14 09:07:48.107')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (14, 93, convert(nvarchar(max), 0x72006F006D00E2006E00), '')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 1, convert(nvarchar(max), 0x50007200ED00630068006F006400), '2015-04-14 09:09:47.053')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 2, convert(nvarchar(max), 0x50007200650068003E0161006400), '2015-04-14 09:09:47.053')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 3, convert(nvarchar(max), 0x4D006100700061002000530074007200E1006E006F006B00), '2015-04-14 09:09:47.053')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 4, convert(nvarchar(max), 0x560079006B006F006E0061006A007400650020004F0062006A00650064006E0061006A0074006500200073006100), '2015-04-14 09:09:47.053')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 5, convert(nvarchar(max), 0x5500640061006C006F007300740069002000700072006500), '2015-04-14 09:08:47.470')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 6, convert(nvarchar(max), 0x50006F00740076007200640069006501), '2015-04-14 09:08:47.470')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 7, convert(nvarchar(max), 0x5A0072007500610169006501), '2015-04-14 09:08:47.470')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 8, convert(nvarchar(max), 0x4A00650020006E00E1006D0020003E01FA0074006F0020006E0069006500200073006D00650020007300630068006F0070006E00ED0020006E00E1006A00730065012000730076006F006A0065002000FA00640061006A006500), '2015-04-14 09:08:47.470')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 9, convert(nvarchar(max), 0x4F0062007200E1006501740065002000730061002000700072006F007300ED006D0020006E006100200072006500630065007000630069006900200061006C00650062006F0020006B006C0069006B006E0069007400650020006E006100200074006C0061000D01690064006C006F0020006E0069007E0161016900650020006100200073006B00FA00730074006500200074006F0020007A006E006F0076006100), '2015-04-14 09:08:47.470')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 10, convert(nvarchar(max), 0x53006B00FA0073007400650020005A006E006F0076007500), '2015-04-14 09:08:47.473')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 11, convert(nvarchar(max), 0x50006F00740076007200640069006501200076006101650074006B006F00), '2015-04-14 09:08:47.473')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 12, convert(nvarchar(max), 0x0C016F0073006B006F0072006F00200050007200ED00630068006F006400), '2015-04-14 09:08:47.473')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 13, convert(nvarchar(max), 0x4A00650020006E00E1006D0020003E01FA0074006F002C0020007600E10061012000760079006D0065006E006F00760061006E006900650020006A0065002000230023002C00200073006D00650020007300630068006F0070006E00ED0020007600E1006D0020006F00640062006100760069006501200061007E0120002300230020006D0069006E00FA007400200070007200650064002000730076006F006A006F006D002000760079006D0065006E006F00760061006E00ED002E002000500072006F007300ED006D00200073006B00FA00730074006500200074006F0020007A006E006F007600610020006E00650073006B00F40072002E00), '2015-04-14 09:08:47.473')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 14, convert(nvarchar(max), 0x4E00650073006B006F007200E900200050007200ED00630068006F006400), '2015-04-14 09:08:47.473')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 15, convert(nvarchar(max), 0x4A00650020006E00E1006D0020003E01FA0074006F002C0020007600E10061012000760079006D0065006E006F00760061006E006900650020000D016100730020002300230020006A00650020007A00610020006E0061006D0069002000610020006D007900200073006D00650020007300630068006F0070006E00ED0020007600E1007300200070007200690068006C00E1007300690065012E0020004F0062007200E1006501740065002000730061002000700072006F007300ED006D0020006E006100200072006500630065007000630069006900), '2015-04-14 09:08:47.473')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 16, convert(nvarchar(max), 0x4F004B00), '2015-04-14 09:08:47.473')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 17, convert(nvarchar(max), 0x4A00650020006E00E1006D0020003E01FA0074006F00210020004E0065006D006F007E016E006F002000730070007200610063006F0076006100650120007600610061017500200070006F007E01690061006400610076006B0075002E00), '2015-04-14 09:08:47.473')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 18, convert(nvarchar(max), 0x500072006F007300ED006D002C00200068006C00E100730074006500200073006100200070007200ED006A0065006D00), '2015-04-14 09:08:47.473')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 19, convert(nvarchar(max), 0x500072006F007300ED006D002C002000760079006200650072007400650020007000720065006600650072006F00760061006E00FA002000640065004801), '2015-04-14 09:08:25.523')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 20, convert(nvarchar(max), 0x4A00650020006E00E1006D0020003E01FA0074006F0020006E0069006500200073006D00650020007300630068006F0070006E00ED0020006E00E1006A00730065012000730076006F006A0065002000FA00640061006A006500), '2015-04-14 09:08:25.523')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 21, convert(nvarchar(max), 0x4F0062007200E1006501740065002000730061002000700072006F007300ED006D0020006E006100200072006500630065007000630069006900200061006C00650062006F0020006B006C0069006B006E0069007400650020006E006100200074006C0061000D01690064006C006F0020006E0069007E0161016900650020006100200073006B00FA00730074006500200074006F0020007A006E006F0076006100), '2015-04-14 09:08:25.523')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 22, convert(nvarchar(max), 0x53006B00FA0073007400650020005A006E006F0076007500), '2015-04-14 09:08:25.523')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 23, convert(nvarchar(max), 0x56006900740061006A0074006500), '2015-04-14 09:08:25.523')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 24, convert(nvarchar(max), 0x50006F007400760072000F01740065002000700072006F007300ED006D002000730076006F006A007500200070006F007E01690061006400610076006B007500), '2015-04-14 09:08:59.770')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 25, convert(nvarchar(max), 0x50006F00740076007200640069006501), '2015-04-14 09:08:59.770')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 26, convert(nvarchar(max), 0x5A0072007500610169006501), '2015-04-14 09:08:59.770')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 27, convert(nvarchar(max), 0x6E006100), '2015-04-14 09:08:59.770')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 28, convert(nvarchar(max), 0x56007900620065007200740065002000730069002000730076006F006A00200064006500480120006E00610072006F00640065006E0069006100), '2015-04-14 09:10:14.010')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 29, convert(nvarchar(max), 0x7D016900610064006E006500200053006C006F0074007300200044006F0073007400750070006E00E900), '2015-04-14 09:08:30.580')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 30, convert(nvarchar(max), 0x4A00650020006E00E1006D0020003E01FA0074006F00210020004E0065006D006F007E016E006F002000730070007200610063006F0076006100650120007600610061017500200070006F007E01690061006400610076006B0075002E00), '2015-04-14 09:09:08.463')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 31, convert(nvarchar(max), 0x500072006F007300ED006D002C00200068006C00E100730074006500200073006100200070007200ED006A0065006D00), '2015-04-14 09:09:08.463')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 32, convert(nvarchar(max), 0x4F004B00), '2015-04-14 09:09:08.463')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 33, convert(nvarchar(max), 0xDA0070007200610076006100), '2015-04-14 09:09:19.600')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 34, convert(nvarchar(max), 0x0E0161006B0075006A0065006D00), '2015-04-14 09:09:19.600')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 35, convert(nvarchar(max), 0x5600E1006101200070007200ED00630068006F00640020006A006500200070006F00740076007200640065006E00E100), '2015-04-14 09:09:19.600')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 36, convert(nvarchar(max), 0xC1006E006F00), '2015-04-14 09:09:19.600')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 37, convert(nvarchar(max), 0x4E0069006500), '2015-04-14 09:09:19.600')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 38, convert(nvarchar(max), 0x43006800630065007400650020006D0061006501200070007200650068003E01610064003F00), '2015-04-14 09:09:19.600')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 39, convert(nvarchar(max), 0xDA0070007200610076006100), '2015-04-14 09:09:27.520')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 40, convert(nvarchar(max), 0x7000720065002000760061006101750020007E016900610064006F00730065012000760079006D0065006E006F00760061006E0069006500), '2015-04-14 09:09:27.520')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 41, convert(nvarchar(max), 0x0E0161006B0075006A0065006D00), '2015-04-14 09:09:27.520')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 42, convert(nvarchar(max), 0x6E006100), '2015-04-14 09:09:27.523')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 43, convert(nvarchar(max), 0x0E0161006B0075006A0065006D00), '2015-04-14 09:09:34.133')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 44, convert(nvarchar(max), 0xDA0070007200610076006100), '2015-04-14 09:09:34.133')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 45, convert(nvarchar(max), 0x5600E10061012000700072006900650073006B0075006D00200062006F006C0020007A0061007A006E0061006D0065006E0061006E00FD00), '2015-04-14 09:09:34.133')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 46, convert(nvarchar(max), 0x56007900620065007200740065002000700072007600E90020007000ED0073006D0065006E006F0020007600E100610168006F0020006B007200730074006E00E90020006D0065006E006F00), '2015-04-14 09:10:17.667')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 47, convert(nvarchar(max), 0x56007900620065007200740065002000700072007600E90020007000ED0073006D0065006E006F00200070007200690065007A007600690073006B006100), '2015-04-14 09:10:21.840')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 48, convert(nvarchar(max), 0x5A006100640061006A007400650020006400E100740075006D0020006E00610072006F00640065006E0069006100), '2015-04-14 09:10:33.410')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 49, convert(nvarchar(max), 0x440065004801), '2015-04-14 09:10:33.410')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 50, convert(nvarchar(max), 0x4D0065007300690061006300), '2015-04-14 09:10:33.410')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 51, convert(nvarchar(max), 0x52006F006B00), '2015-04-14 09:10:33.413')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 52, convert(nvarchar(max), 0x0E0161006C006101ED00), '2015-04-14 09:10:33.413')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 53, convert(nvarchar(max), 0x500072006F007300ED006D002C0020007600790062006500720074006500200070006F0068006C006100760069006500), '2015-04-14 09:10:41.110')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 54, convert(nvarchar(max), 0x530061006D0065006300), '2015-04-14 09:10:41.110')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 55, convert(nvarchar(max), 0x530061006D00690063006500), '2015-04-14 09:10:41.110')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 56, convert(nvarchar(max), 0x50007200650073006B006F000D0169006501), '2015-04-14 09:10:41.110')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 57, convert(nvarchar(max), 0x4B0049004F0053004B002000720065007E0169006D007500200073007000E1006E006B00750020002E002E002E002E00), '2015-04-14 09:09:40.073')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 58, convert(nvarchar(max), 0x4E0061000D01610073006F00760061006E006900650020004F0075007400200049006E0020002E002E002E00), '2015-04-14 09:09:51.900')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 59, convert(nvarchar(max), 0x56006900740061006A007400650020005600), '2015-04-14 09:09:56.013')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 60, convert(nvarchar(max), 0x760079006200650072007400650020006A0061007A0079006B0020006E00610020007A0061000D016900610074006F006B00), '2015-04-14 09:09:56.013')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 61, convert(nvarchar(max), 0x500072006F007300ED006D002C002000760079006200650072007400650020006A00650064006E00750020007A0020006D006F007E016E006F0073007400ED00), '2015-04-14 09:10:01.510')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 62, convert(nvarchar(max), 0x500072006F007300ED006D002C002000760079006200650072007400650020006D006500730069006100630020006E00610072006F00640065006E0069006100), '2015-04-14 09:10:45.957')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 63, convert(nvarchar(max), 0x41006B00200063006800630065007400650020007A0061000D01610065012C002000760079006200650072007400650020006A00650064006E00750020007A0020006E0069007E016101690065002000750076006500640065006E00FD006300680020006D006F007E016E006F0073007400ED00), '2015-04-14 09:10:05.497')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 64, convert(nvarchar(max), 0x56006900740061006A007400650020005600), '2015-04-14 09:10:10.100')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 65, convert(nvarchar(max), 0x56007900620065007200740065002000740079007000200073006C006F0074007500), '2015-04-14 09:11:02.943')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 66, convert(nvarchar(max), 0x0E0161006C006101ED00), '2015-04-14 09:11:14.080')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 67, convert(nvarchar(max), 0x4F00640070006F007600650064007A00740065002C002000700072006F007300ED006D002C0020006E00610020006E00610073006C006500640075006A00FA006300650020006F007400E1007A006B007900), '2015-04-14 09:11:14.080')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 68, convert(nvarchar(max), 0x50007200650073006B006F000D0169006501), '2015-04-14 09:11:14.080')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 69, convert(nvarchar(max), 0x500072006F007300ED006D002C002000760079006200650072007400650020006D006F007E016E006F0073006501), '2015-04-14 09:11:08.023')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 70, convert(nvarchar(max), 0x500072006900650073006B0075006D00790020006D006F006D0065006E007400E1006C006E00650020006E006900650020006A00650020006B00200064006900730070006F007A00ED00630069006900), '2015-04-14 09:11:08.023')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 71, convert(nvarchar(max), 0x500072006F007300ED006D002C0020007600790062006500720074006500200072006F006B0020006E00610072006F00640065006E0069006100), '2015-04-14 09:10:59.297')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 72, convert(nvarchar(max), 0x4F004B00), '2015-04-14 09:10:59.297')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 73, convert(nvarchar(max), 0x7D016900610064006E00610020007A0020007600790061016101690065002000750076006500640065006E00FD0063006800), '2015-04-14 09:10:59.297')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 74, convert(nvarchar(max), 0x52006F006B0020004E006F007400200046006F0075006E006400), '2015-04-14 09:10:59.297')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 75, convert(nvarchar(max), 0x4F0062007200E1006501740065002000730061002000700072006F007300ED006D0020006E006100200070007200ED006A006D006500), '2015-04-14 09:10:59.300')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 82, convert(nvarchar(max), 0x530070007200E10076007900), '2015-04-14 09:08:52.850')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 83, convert(nvarchar(max), 0x0E0161006C006101ED00), '2015-04-14 09:08:52.850')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 84, convert(nvarchar(max), 0x500072006F007300ED006D002C0020007600790062006500720074006500200072006F006B0020006E00610072006F00640065006E0069006100), '2015-04-14 09:10:51.060')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 85, convert(nvarchar(max), 0x0E0161006C006101ED00), '2015-04-14 09:10:51.060')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (15, 94, convert(nvarchar(max), 0x73006C006F007600610061016B006900), '')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 1, convert(nvarchar(max), 0x010E320E230E210E320E160E360E070E), '2015-04-14 09:13:44.933')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 2, convert(nvarchar(max), 0x2A0E330E230E270E080E), '2015-04-14 09:13:44.933')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 3, convert(nvarchar(max), 0x410E1C0E190E1C0E310E070E400E270E470E1A0E440E0B0E150E4C0E), '2015-04-14 09:13:44.933')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 4, convert(nvarchar(max), 0x190E310E140E), '2015-04-14 09:13:44.933')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 5, convert(nvarchar(max), 0x2A0E330E2B0E230E310E1A0E010E320E230E190E310E140E2B0E210E320E220E), '2015-04-14 09:12:40.293')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 6, convert(nvarchar(max), 0x220E370E190E220E310E190E), '2015-04-14 09:12:40.293')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 7, convert(nvarchar(max), 0x220E010E400E250E340E010E), '2015-04-14 09:12:40.293')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 8, convert(nvarchar(max), 0x020E2D0E2D0E200E310E220E400E230E320E440E210E480E2A0E320E210E320E230E160E170E350E480E080E300E2B0E320E230E320E220E250E300E400E2D0E350E220E140E020E2D0E070E040E380E130E), '2015-04-14 09:12:40.293')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 9, convert(nvarchar(max), 0x010E230E380E130E320E150E340E140E150E480E2D0E410E1C0E190E010E150E490E2D0E190E230E310E1A0E2B0E230E370E2D0E040E250E340E010E170E350E480E1B0E380E480E210E140E490E320E190E250E480E320E070E400E1E0E370E480E2D0E250E2D0E070E2D0E350E010E040E230E310E490E070E), '2015-04-14 09:12:40.293')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 10, convert(nvarchar(max), 0x250E2D0E070E430E2B0E210E480E2D0E350E010E040E230E310E490E070E), '2015-04-14 09:12:40.293')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 11, convert(nvarchar(max), 0x220E370E190E220E310E190E170E310E490E070E2B0E210E140E), '2015-04-14 09:12:40.293')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 12, convert(nvarchar(max), 0x210E320E160E360E070E430E190E0A0E480E270E070E150E490E190E), '2015-04-14 09:12:40.293')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 13, convert(nvarchar(max), 0x020E2D0E2D0E200E310E220E190E310E140E2B0E210E320E220E020E2D0E070E040E380E130E400E1B0E470E190E2000230023002000400E230E320E440E210E480E2A0E320E210E320E230E160E170E350E480E080E300E150E230E270E080E2A0E2D0E1A0E040E380E130E080E190E010E230E300E170E310E480E070E2000230023002000190E320E170E350E010E480E2D0E190E170E350E480E080E300E440E140E490E230E310E1A0E010E320E230E410E150E480E070E150E310E490E070E020E2D0E070E040E380E130E2000420E1B0E230E140E250E2D0E070E2D0E350E010E040E230E310E490E070E430E190E200E320E220E2B0E250E310E070E), '2015-04-14 09:12:40.293')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 14, convert(nvarchar(max), 0x400E140E340E190E170E320E070E210E320E160E360E070E250E480E320E0A0E490E320E), '2015-04-14 09:12:40.293')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 15, convert(nvarchar(max), 0x020E2D0E2D0E200E310E220E400E270E250E320E190E310E140E2B0E210E320E220E020E2D0E070E040E380E130E020E2D0E070E2000230023002000440E140E490E1C0E480E320E190E410E250E300E400E230E320E440E210E480E2A0E320E210E320E230E160E170E350E480E080E300E150E230E270E080E2A0E2D0E1A0E040E380E130E430E190E2E002000010E230E380E130E320E150E340E140E150E480E2D0E410E1C0E190E010E150E490E2D0E190E230E310E1A0E), '2015-04-14 09:12:40.293')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 16, convert(nvarchar(max), 0x150E010E250E070E), '2015-04-14 09:12:40.293')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 17, convert(nvarchar(max), 0x020E2D0E2D0E200E310E220E21002000440E210E480E2A0E320E210E320E230E160E140E330E400E190E340E190E010E320E230E150E320E210E040E330E020E2D0E020E2D0E070E040E380E130E), '2015-04-14 09:12:40.293')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 18, convert(nvarchar(max), 0x010E230E380E130E320E230E320E220E070E320E190E440E1B0E220E310E070E410E1C0E190E010E150E490E2D0E190E230E310E1A0E), '2015-04-14 09:12:40.297')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 19, convert(nvarchar(max), 0x010E230E380E130E320E400E250E370E2D0E010E270E310E190E170E350E480E150E490E2D0E070E010E320E230E), '2015-04-14 09:12:07.650')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 20, convert(nvarchar(max), 0x020E2D0E2D0E200E310E220E400E230E320E440E210E480E2A0E320E210E320E230E160E170E350E480E080E300E2B0E320E230E320E220E250E300E400E2D0E350E220E140E020E2D0E070E040E380E130E), '2015-04-14 09:12:07.650')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 21, convert(nvarchar(max), 0x010E230E380E130E320E150E340E140E150E480E2D0E410E1C0E190E010E150E490E2D0E190E230E310E1A0E2B0E230E370E2D0E040E250E340E010E170E350E480E1B0E380E480E210E140E490E320E190E250E480E320E070E400E1E0E370E480E2D0E250E2D0E070E2D0E350E010E040E230E310E490E070E), '2015-04-14 09:12:07.650')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 22, convert(nvarchar(max), 0x250E2D0E070E430E2B0E210E480E2D0E350E010E040E230E310E490E070E), '2015-04-14 09:12:07.650')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 23, convert(nvarchar(max), 0x220E340E190E140E350E150E490E2D0E190E230E310E1A0E), '2015-04-14 09:12:07.650')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 24, convert(nvarchar(max), 0x010E230E380E130E320E220E370E190E220E310E190E040E330E020E2D0E020E2D0E070E040E380E130E), '2015-04-14 09:12:53.200')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 25, convert(nvarchar(max), 0x220E370E190E220E310E190E), '2015-04-14 09:12:53.200')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 26, convert(nvarchar(max), 0x220E010E400E250E340E010E), '2015-04-14 09:12:53.200')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 27, convert(nvarchar(max), 0x170E350E480E), '2015-04-14 09:12:53.200')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 28, convert(nvarchar(max), 0x420E1B0E230E140E400E250E370E2D0E010E270E310E190E400E010E340E140E020E2D0E070E040E380E130E), '2015-04-14 09:14:11.130')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 29, convert(nvarchar(max), 0x2A0E250E470E2D0E150E440E210E480E210E350E), '2015-04-14 09:12:14.377')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 30, convert(nvarchar(max), 0x020E2D0E2D0E200E310E220E21002000440E210E480E2A0E320E210E320E230E160E140E330E400E190E340E190E010E320E230E150E320E210E040E330E020E2D0E020E2D0E070E040E380E130E), '2015-04-14 09:13:03.783')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 31, convert(nvarchar(max), 0x010E230E380E130E320E230E320E220E070E320E190E440E1B0E220E310E070E410E1C0E190E010E150E490E2D0E190E230E310E1A0E), '2015-04-14 09:13:03.783')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 32, convert(nvarchar(max), 0x150E010E250E070E), '2015-04-14 09:13:03.787')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 33, convert(nvarchar(max), 0x400E2A0E230E470E080E), '2015-04-14 09:13:13.887')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 34, convert(nvarchar(max), 0x020E2D0E020E2D0E1A0E040E380E130E040E380E130E), '2015-04-14 09:13:13.890')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 35, convert(nvarchar(max), 0x010E320E230E210E320E160E360E070E020E2D0E070E040E380E130E440E140E490E230E310E1A0E010E320E230E220E370E190E220E310E190E), '2015-04-14 09:13:13.890')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 36, convert(nvarchar(max), 0x430E0A0E480E), '2015-04-14 09:13:13.890')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 37, convert(nvarchar(max), 0x440E210E480E), '2015-04-14 09:13:13.890')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 38, convert(nvarchar(max), 0x040E380E130E150E490E2D0E070E010E320E230E170E350E480E080E300E430E0A0E490E400E270E250E320E430E190E010E320E230E2A0E330E230E270E080E3F00), '2015-04-14 09:13:13.890')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 39, convert(nvarchar(max), 0x400E2A0E230E470E080E), '2015-04-14 09:13:23.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 40, convert(nvarchar(max), 0x2A0E330E2B0E230E310E1A0E010E320E230E230E490E2D0E070E020E2D0E010E320E230E190E310E140E2B0E210E320E220E020E2D0E070E040E380E130E), '2015-04-14 09:13:23.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 41, convert(nvarchar(max), 0x020E2D0E020E2D0E1A0E040E380E130E040E380E130E), '2015-04-14 09:13:23.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 42, convert(nvarchar(max), 0x170E350E480E), '2015-04-14 09:13:23.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 43, convert(nvarchar(max), 0x020E2D0E020E2D0E1A0E040E380E130E040E380E130E), '2015-04-14 09:13:30.887')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 44, convert(nvarchar(max), 0x400E2A0E230E470E080E), '2015-04-14 09:13:30.887')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 45, convert(nvarchar(max), 0x010E320E230E2A0E330E230E270E080E020E2D0E070E040E380E130E440E140E490E230E310E1A0E010E320E230E1A0E310E190E170E360E010E440E270E490E), '2015-04-14 09:13:30.887')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 46, convert(nvarchar(max), 0x010E230E380E130E320E400E250E370E2D0E010E150E310E270E2D0E310E010E290E230E150E310E270E410E230E010E020E2D0E070E2000460069007200730074004E0061006D0065002000020E2D0E070E040E380E130E), '2015-04-14 09:14:15.810')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 47, convert(nvarchar(max), 0x010E230E380E130E320E400E250E370E2D0E010E150E310E270E2D0E310E010E290E230E150E310E270E410E230E010E020E2D0E070E190E320E210E2A0E010E380E250E020E2D0E070E040E380E130E), '2015-04-14 09:14:20.990')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 48, convert(nvarchar(max), 0x430E2A0E480E270E310E190E400E140E370E2D0E190E1B0E350E400E010E340E140E), '2015-04-14 09:14:33.460')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 49, convert(nvarchar(max), 0x270E310E190E), '2015-04-14 09:14:33.460')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 50, convert(nvarchar(max), 0x400E140E370E2D0E190E), '2015-04-14 09:14:33.460')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 51, convert(nvarchar(max), 0x1B0E350E), '2015-04-14 09:14:33.460')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 52, convert(nvarchar(max), 0x160E310E140E440E1B0E), '2015-04-14 09:14:33.460')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 53, convert(nvarchar(max), 0x010E230E380E130E320E400E250E370E2D0E010E400E1E0E280E020E2D0E070E040E380E130E), '2015-04-14 09:14:40.423')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 54, convert(nvarchar(max), 0x0A0E320E220E), '2015-04-14 09:14:40.423')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 55, convert(nvarchar(max), 0x2B0E0D0E340E070E), '2015-04-14 09:14:40.423')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 56, convert(nvarchar(max), 0x010E230E300E420E140E140E), '2015-04-14 09:14:40.423')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 57, convert(nvarchar(max), 0x4B0049004F0053004B002000080E330E280E350E250E20002E002E002E002E00), '2015-04-14 09:13:37.973')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 58, convert(nvarchar(max), 0x2B0E210E140E400E270E250E320E430E190E20002E002E002E00), '2015-04-14 09:13:49.047')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 59, convert(nvarchar(max), 0x220E340E190E140E350E150E490E2D0E190E230E310E1A0E), '2015-04-14 09:13:54.677')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 60, convert(nvarchar(max), 0x400E250E370E2D0E010E200E320E290E320E170E350E480E080E300E400E230E340E480E210E150E490E190E), '2015-04-14 09:13:54.680')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 61, convert(nvarchar(max), 0x420E1B0E230E140E400E250E370E2D0E010E150E310E270E400E250E370E2D0E010E), '2015-04-14 09:13:59.420')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 62, convert(nvarchar(max), 0x010E230E380E130E320E400E250E370E2D0E010E400E140E370E2D0E190E400E010E340E140E020E2D0E070E040E380E130E), '2015-04-14 09:14:45.183')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 63, convert(nvarchar(max), 0x430E190E010E320E230E400E230E340E480E210E150E490E190E400E1E0E350E220E070E410E040E480E400E250E370E2D0E010E2B0E190E360E480E070E430E190E150E310E270E400E250E370E2D0E010E140E490E320E190E250E480E320E070E), '2015-04-14 09:14:03.313')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 64, convert(nvarchar(max), 0x220E340E190E140E350E150E490E2D0E190E230E310E1A0E), '2015-04-14 09:14:06.940')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 65, convert(nvarchar(max), 0x420E1B0E230E140E400E250E370E2D0E010E1B0E230E300E400E200E170E2A0E250E470E2D0E150E), '2015-04-14 09:15:02.857')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 66, convert(nvarchar(max), 0x160E310E140E440E1B0E), '2015-04-14 09:15:13.693')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 67, convert(nvarchar(max), 0x010E230E380E130E320E150E2D0E1A0E040E330E160E320E210E140E310E070E150E480E2D0E440E1B0E190E350E490E), '2015-04-14 09:15:13.693')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 68, convert(nvarchar(max), 0x010E230E300E420E140E140E), '2015-04-14 09:15:13.693')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 69, convert(nvarchar(max), 0x010E230E380E130E320E400E250E370E2D0E010E150E310E270E400E250E370E2D0E010E), '2015-04-14 09:15:07.860')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 70, convert(nvarchar(max), 0x010E320E230E2A0E330E230E270E080E430E190E020E130E300E190E350E490E), '2015-04-14 09:15:07.860')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 71, convert(nvarchar(max), 0x010E230E380E130E320E400E250E370E2D0E010E1B0E350E400E010E340E140E020E2D0E070E040E380E130E), '2015-04-14 09:14:58.260')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 72, convert(nvarchar(max), 0x150E010E250E070E), '2015-04-14 09:14:58.263')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 73, convert(nvarchar(max), 0x440E210E480E210E350E020E2D0E070E2000410062006F0076006500), '2015-04-14 09:14:58.263')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 74, convert(nvarchar(max), 0x1B0E350E170E350E480E440E210E480E1E0E1A0E), '2015-04-14 09:14:58.263')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 75, convert(nvarchar(max), 0x010E230E380E130E320E150E340E140E150E480E2D0E410E1C0E190E010E150E490E2D0E190E230E310E1A0E), '2015-04-14 09:14:58.263')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 82, convert(nvarchar(max), 0x020E490E2D0E040E270E320E210E), '2015-04-14 09:12:45.803')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 83, convert(nvarchar(max), 0x160E310E140E440E1B0E), '2015-04-14 09:12:45.803')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 84, convert(nvarchar(max), 0x010E230E380E130E320E400E250E370E2D0E010E1B0E350E400E010E340E140E020E2D0E070E040E380E130E), '2015-04-14 09:14:50.810')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 85, convert(nvarchar(max), 0x160E310E140E440E1B0E), '2015-04-14 09:14:50.810')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (16, 95, convert(nvarchar(max), 0x440E170E220E), '')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 1, convert(nvarchar(max), 0x220645062F06), '2015-04-14 09:15:24.900')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 2, convert(nvarchar(max), 0x330631064806D206), '2015-04-14 09:15:24.900')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 3, convert(nvarchar(max), 0x4806CC062806200033062706260679062000A90627062000460642063406C106), '2015-04-14 09:15:24.900')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 4, convert(nvarchar(max), 0x450644062706420627062A062000A90627062000480642062A06), '2015-04-14 09:15:24.903')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 5, convert(nvarchar(max), 0xA906D206200044062606D20620002A06420631063106CC064806BA06), '2015-04-14 09:13:55.107')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 6, convert(nvarchar(max), 0x2A0635062F06CC0642062000A9063106CC06BA06), '2015-04-14 09:13:55.107')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 7, convert(nvarchar(max), 0x45064606330648062E062000A9063106CC06BA06), '2015-04-14 09:13:55.107')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 8, convert(nvarchar(max), 0x270641063306480633062000C1064506200022067E062000A906CC0620002A0641063506CC06440627062A062000A906480620002A064406270634062000A90631064606D2062000A906D2062000420627062806440620004606C106CC06BA062000C106CC06BA06), '2015-04-14 09:13:55.107')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 9, convert(nvarchar(max), 0x3106CC063306CC067E063406460620003306D20620003106270628063706C1062000A9063106CC06BA062000CC06270620007E06BE0631062000A9064806340634062000A90631064606D2062000A906D206200044062606D20620003006CC064406200028067906460620007E0631062000A9064406A9062000A9063106CC06BA062000280631062706C10620004506C1063106280627064606CC06), '2015-04-14 09:13:55.107')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 10, convert(nvarchar(max), 0x2F064806280627063106C1062000A9064806340634062000A9063106CC06BA06), '2015-04-14 09:13:55.107')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 11, convert(nvarchar(max), 0x330628062000A906CC0620002A0635062F06CC064206), '2015-04-14 09:13:55.107')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 12, convert(nvarchar(max), 0x270628062A062F0627062606CC062000220645062F06), '2015-04-14 09:13:55.107')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 13, convert(nvarchar(max), 0x45063906300631062A060C06200022067E062000A906CC0620002A06420631063106CC062000C1064506200022067E062000A906CC0620002A06420631063106CC0620003306D20620007E06C1064406D206200023002300200045064606790620002A06A90620004506CC06BA06200022067E062000A906CC0620002C0627064606860620007E0691062A06270644062000A90631064606D2062000A906D2062000420627062806440620004606C106CC06BA062000C106CC06BA060C062000230023002000C106D2062E002000A9068606BE0620002F06CC0631062000280639062F0620002F064806280627063106C1062000A9064806340634062000A9063106CC06BA062E00), '2015-04-14 09:13:55.107')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 14, convert(nvarchar(max), 0x2F06CC06310620003306D2062000220645062F06), '2015-04-14 09:13:55.110')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 15, convert(nvarchar(max), 0x45063906300631062A060C0620002300230020003306D206200027067E064606D20620004506420631063106C1062000480642062A062000AF063206310620008606A90627062000C106D20620002706480631062000C106450620004506CC06BA06200022067E062000A906CC0620002C0627064606860620007E0691062A06270644062000A90631064606D2062000A906D2062000420627062806440620004606C106CC06BA062000C106CC06BA062E002000270633062A064206280627064406CC06C10620003306D20620003106270628063706C1062000A9063106CC06BA06), '2015-04-14 09:13:55.110')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 16, convert(nvarchar(max), 0x27064806A906D206), '2015-04-14 09:13:55.110')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 17, convert(nvarchar(max), 0x45063906270641062000A906CC062C062606D2062000AF0627062100200022067E062000A906CC0620002F0631062E064806270633062A0620007E0631062000A906270631063106480627062606CC062000A90631064606D20620003306D206200042062706350631062E00), '2015-04-14 09:13:55.110')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 18, convert(nvarchar(max), 0x270633062A064206280627064406CC06C1062000A906CC06200031067E064806310679062000A9063106CC06BA06), '2015-04-14 09:13:55.110')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 19, convert(nvarchar(max), 0x2706CC06A90620002A0631062C06CC062D06CC0620002F0646062000280631062706C10620004506C1063106280627064606CC062000450646062A062E0628062000A9063106CC06BA06), '2015-04-14 09:13:11.153')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 20, convert(nvarchar(max), 0x270641063306480633062000C1064506200022067E062000A906CC0620002A0641063506CC06440627062A062000A906480620002A064406270634062000A90631064606D2062000A906D2062000420627062806440620004606C106CC06BA062000C106CC06BA06), '2015-04-14 09:13:11.153')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 21, convert(nvarchar(max), 0x3106CC063306CC067E063406460620003306D20620003106270628063706C1062000A9063106CC06BA062000CC06270620007E06BE0631062000A9064806340634062000A90631064606D2062000A906D206200044062606D20620003006CC064406200028067906460620007E0631062000A9064406A9062000A9063106CC06BA062000280631062706C10620004506C1063106280627064606CC06), '2015-04-14 09:13:11.153')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 22, convert(nvarchar(max), 0x2F064806280627063106C1062000A9064806340634062000A9063106CC06BA06), '2015-04-14 09:13:11.153')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 23, convert(nvarchar(max), 0x270633062A064206280627064406), '2015-04-14 09:13:11.153')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 24, convert(nvarchar(max), 0x22067E062000A906CC0620002F0631062E064806270633062A062000A906CC0620002A0635062F06CC0642062000A9063106CC06BA06), '2015-04-14 09:14:18.003')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 25, convert(nvarchar(max), 0x2A0635062F06CC0642062000A9063106CC06BA06), '2015-04-14 09:14:18.003')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 26, convert(nvarchar(max), 0x45064606330648062E062000A9063106CC06BA06), '2015-04-14 09:14:18.003')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 27, convert(nvarchar(max), 0x4506CC06BA06), '2015-04-14 09:14:18.003')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 28, convert(nvarchar(max), 0x22067E062000A906CC0620007E06CC062F062706260634062000A906CC0620002F0646062000280631062706C10620004506C1063106280627064606CC062000450646062A062E0628062000A9063106CC06BA06), '2015-04-14 09:16:07.913')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 29, convert(nvarchar(max), 0x2F0633062A06CC062706280620004606C106CC06BA06200033064406270679063306), '2015-04-14 09:13:25.610')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 30, convert(nvarchar(max), 0x45063906270641062000A906CC062C062606D2062000AF0627062100200022067E062000A906CC0620002F0631062E064806270633062A0620007E0631062000A906270631063106480627062606CC062000A90631064606D20620003306D206200042062706350631062E00), '2015-04-14 09:14:32.557')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 31, convert(nvarchar(max), 0x270633062A064206280627064406CC06C1062000A906CC06200031067E064806310679062000A9063106CC06BA06), '2015-04-14 09:14:32.557')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 32, convert(nvarchar(max), 0x27064806A906D206), '2015-04-14 09:14:32.557')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 33, convert(nvarchar(max), 0x2E062A0645062000A906D206), '2015-04-14 09:14:44.253')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 34, convert(nvarchar(max), 0x3406A9063106CC06C106200027062F0627062000A906CC062706), '2015-04-14 09:14:44.253')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 35, convert(nvarchar(max), 0x22067E062000A906CC062000220645062F062000A906CC0620002A0635062F06CC0642062000A906CC062000C106D206), '2015-04-14 09:14:44.257')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 36, convert(nvarchar(max), 0x2C06CC062000C1062706BA06), '2015-04-14 09:14:44.257')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 37, convert(nvarchar(max), 0xA90648062606CC06), '2015-04-14 09:14:44.257')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 38, convert(nvarchar(max), 0x22067E062000A90648062000330631064806D20620004406CC064606D2062000A906D206200044062606D206200086062706C1062A06D2062000C106CC06BA061F06), '2015-04-14 09:14:44.257')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 39, convert(nvarchar(max), 0x2E062A0645062000A906D206), '2015-04-14 09:14:53.620')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 40, convert(nvarchar(max), 0x22067E062000A906CC0620002A06420631063106CC062000A906CC0620002F0631062E064806270633062A062000A906D206200044062606D206), '2015-04-14 09:14:53.620')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 41, convert(nvarchar(max), 0x3406A9063106CC06C106200027062F0627062000A906CC062706), '2015-04-14 09:14:53.620')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 42, convert(nvarchar(max), 0x4506CC06BA06), '2015-04-14 09:14:53.620')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 43, convert(nvarchar(max), 0x3406A9063106CC06C106200027062F0627062000A906CC062706), '2015-04-14 09:15:04.193')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 44, convert(nvarchar(max), 0x2E062A0645062000A906D206), '2015-04-14 09:15:04.193')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 45, convert(nvarchar(max), 0x22067E062000A906D2062000330631064806D20620003106CC06A9062706310688062000A906CC0627062000AF06CC0627062000C106D206), '2015-04-14 09:15:04.193')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 46, convert(nvarchar(max), 0x22067E062000A906D20620007E06C1064406270620004606270645062000A906D20620007E06C1064406D20620002D06310641062000280631062706C10620004506C1063106280627064606CC062000450646062A062E0628062000A9063106CC06BA06), '2015-04-14 09:16:13.820')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 47, convert(nvarchar(max), 0x22067E062000A906D206200045062E062A063506310620004606270645062000A906D20620007E06C1064406D20620002D06310641062000280631062706C10620004506C1063106280627064606CC062000450646062A062E0628062000A9063106CC06BA06), '2015-04-14 09:16:21.830')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 48, convert(nvarchar(max), 0x2A0627063106CC062E0620007E06CC062F0627062606340620002F0631062C062000A9063106CC06BA06), '2015-04-14 09:16:35.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 49, convert(nvarchar(max), 0x2F064606), '2015-04-14 09:16:35.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 50, convert(nvarchar(max), 0x4506C106CC064606C106), '2015-04-14 09:16:35.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 51, convert(nvarchar(max), 0x330627064406), '2015-04-14 09:16:35.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 52, convert(nvarchar(max), 0x2706AF064406D206), '2015-04-14 09:16:35.623')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 53, convert(nvarchar(max), 0x27067E064606CC0620003506460641062000280631062706C10620004506C1063106280627064606CC062000450646062A062E0628062000A9063106CC06BA06), '2015-04-14 09:16:44.760')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 54, convert(nvarchar(max), 0x44069106A9062706), '2015-04-14 09:16:44.760')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 55, convert(nvarchar(max), 0x2E06480627062A06CC064606), '2015-04-14 09:16:44.760')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 56, convert(nvarchar(max), 0x2C0627062606CC06BA06), '2015-04-14 09:16:44.760')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 57, convert(nvarchar(max), 0xA906CC0627063306A9062000480069006200650072006E006100740069006E00670020002E002E002E002E00), '2015-04-14 09:15:12.980')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 58, convert(nvarchar(max), 0x4506CC06BA06200028062706C10631062000480642062A0620002E002E002E00), '2015-04-14 09:15:32.060')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 59, convert(nvarchar(max), 0xA90631064606D2062000A906270620002E06CC063106450642062F064506), '2015-04-14 09:15:39.037')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 60, convert(nvarchar(max), 0x34063106480639062000A90631064606D2062000A906D206200044062606D20620002706CC06A906200032062806270646062000A90627062000270646062A062E06270628062000A9063106CC06BA06), '2015-04-14 09:15:39.037')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 61, convert(nvarchar(max), 0x2706CC06A906200027062E062A06CC06270631062000A90648062000450646062A062E0628062000A9063106CC06BA062000280631062706C10620004506C1063106280627064606CC06), '2015-04-14 09:15:51.150')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 62, convert(nvarchar(max), 0x22067E062000A906CC0620007E06CC062F062706260634062000A906CC06200045062706C1062000280631062706C10620004506C1063106280627064606CC062000450646062A062E0628062000A9063106CC06BA06), '2015-04-14 09:16:51.280')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 63, convert(nvarchar(max), 0x34063106480639062000A90631064606D2062000A906D206200044062606D2060C0620003006CC06440620004506CC06BA0620002F06CC062606D2062000AF062606D206200027062E062A06CC062706310627062A0620004506CC06BA0620003306D2062000A90648062606CC0620002706CC06A9062000450646062A062E0628062000A9063106CC06BA06), '2015-04-14 09:15:57.267')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 64, convert(nvarchar(max), 0xA90631064606D2062000A906270620002E06CC063106450642062F064506), '2015-04-14 09:16:02.857')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 65, convert(nvarchar(max), 0x2706CC06A906200033064406270679062000A906CC0620004206330645062000A90627062000270646062A062E06270628062000A9063106CC06BA06), '2015-04-14 09:17:13.920')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 66, convert(nvarchar(max), 0x2706AF064406D206), '2015-04-14 09:17:28.310')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 67, convert(nvarchar(max), 0x450646062F0631062C06C10620003006CC0644062000330648062706440627062A062000A906270620002C0648062706280620002F06CC06BA06), '2015-04-14 09:17:28.310')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 68, convert(nvarchar(max), 0x2C0627062606CC06BA06), '2015-04-14 09:17:28.310')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 69, convert(nvarchar(max), 0x2706CC06A906200022067E06340646062000450646062A062E0628062000A9063106CC06BA062000280631062706C10620004506C1063106280627064606CC06), '2015-04-14 09:17:20.740')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 70, convert(nvarchar(max), 0x4106CC062000270644062D062706440620002F0633062A06CC062706280620004606C106CC06BA062000330631064806D206), '2015-04-14 09:17:20.743')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 71, convert(nvarchar(max), 0x7E06CC062F0627062606340620002F0631062C062000A9063106CC06BA062000280631062706C10620004506C1063106280627064606CC062000450646062A062E0628062000A9063106CC06BA06), '2015-04-14 09:17:06.810')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 72, convert(nvarchar(max), 0x27064806A906D206), '2015-04-14 09:17:06.810')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 73, convert(nvarchar(max), 0x2F0631062C062000280627064406270620004506CC06BA0620003306D2062000A90648062606CC0620002806BE06CC06), '2015-04-14 09:17:06.810')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 74, convert(nvarchar(max), 0x33062706440620004606C106CC06BA062000450644062706), '2015-04-14 09:17:06.810')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 75, convert(nvarchar(max), 0x3106CC063306CC067E063406460620003106270628063706C1062000A9063106CC06BA06), '2015-04-14 09:17:06.810')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 82, convert(nvarchar(max), 0x7E06CC063A062706450627062A06), '2015-04-14 09:14:04.420')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 83, convert(nvarchar(max), 0x2706AF064406D206), '2015-04-14 09:14:04.420')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 84, convert(nvarchar(max), 0x7E06CC062F0627062606340620002F0631062C062000A9063106CC06BA062000280631062706C10620004506C1063106280627064606CC062000450646062A062E0628062000A9063106CC06BA06), '2015-04-14 09:16:57.570')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 85, convert(nvarchar(max), 0x2706AF064406D206), '2015-04-14 09:16:57.570')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (17, 96, convert(nvarchar(max), 0x270631062F064806), '')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 1, convert(nvarchar(max), 0x43007900720072006100650064006400), '2015-04-14 09:21:28.807')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 2, convert(nvarchar(max), 0x410072006F006C0077006700), '2015-04-14 09:21:28.807')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 3, convert(nvarchar(max), 0x4D006100700020006F003B00720020005300610066006C006500), '2015-04-14 09:21:28.807')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 4, convert(nvarchar(max), 0x470077006E00650075006400200041007000770079006E007400690061006400), '2015-04-14 09:21:28.807')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 5, convert(nvarchar(max), 0x500065006E006F0064006900610064006100750020004900), '2015-04-14 09:19:46.913')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 6, convert(nvarchar(max), 0x430061006400610072006E00680061007500), '2015-04-14 09:19:46.913')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 7, convert(nvarchar(max), 0x440069006400640079006D007500), '2015-04-14 09:19:46.913')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 8, convert(nvarchar(max), 0x4D00610065003B006E002000640064007200770067002000670065006E006E0079006D002C0020006E006900200061006C006C0077006E002000640064006F00640020006F002000680079006400200069003B006300680020006D0061006E0079006C0069006F006E00), '2015-04-14 09:19:46.913')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 9, convert(nvarchar(max), 0x43007900730079006C006C0074007700630068002000E2003B00720020004400640065007200620079006E006600610020006E0065007500200043006C0069006300690077006300680020006100720020007900200062006F00740077006D002000690073006F006400200069002000670065006900730069006F002000650074006F00), '2015-04-14 09:19:46.913')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 10, convert(nvarchar(max), 0x54007200EF007700630068002000650074006F00), '2015-04-14 09:19:46.913')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 11, convert(nvarchar(max), 0x430061006400610072006E00680061007500200048006F006C006C00), '2015-04-14 09:19:46.913')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 12, convert(nvarchar(max), 0x43007900720072006100650064006400200079006E002000670079006E006E0061007200), '2015-04-14 09:19:46.913')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 13, convert(nvarchar(max), 0x4D00610065003B006E002000640064007200770067002000670065006E006E0079006D002C0020006500690063006800200061007000770079006E007400690061006400200079006E002000230023002C0020006E006900640020007900640079006D00200079006E002000670061006C006C0075002000670077006900720069006F002000630068006900200079006E0020006E006500730020002300230020006D0075006E00750064002000630079006E0020006500690063006800200061007000770079006E0074006900610064002E002000430065006900730069007700630068002000650074006F00200079006E0020006E0065007300200079006D006C00610065006E002E00), '2015-04-14 09:19:46.913')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 14, convert(nvarchar(max), 0x43007900720072006100650064006400200079006E0020004800770079007200), '2015-04-14 09:19:46.913')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 15, convert(nvarchar(max), 0x4D00610065003B006E002000640064007200770067002000670065006E006E0079006D002C0020006500690063006800200061006D007300650072002000700065006E006F00640069002000230023002000770065006400690020006D0079006E0064002000680065006900620069006F0020006100630020006E006900200061006C006C0077006E00200073006900630072006800610075002000650069006300680020006D00650077006E002E00200043007900730079006C006C0074007700630068002000E2003B00720020006400640065007200620079006E0066006100), '2015-04-14 09:19:46.913')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 16, convert(nvarchar(max), 0x4F004B00), '2015-04-14 09:19:46.913')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 17, convert(nvarchar(max), 0x4D00610065003B006E002000640064007200770067002000670065006E006E0079006D0021002000470061006C006C0075002000700072006F00730065007300750020006500690063006800200063006100690073002E00), '2015-04-14 09:19:46.913')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 18, convert(nvarchar(max), 0x4F00730020006700770065006C00770063006800200079006E002000640064006100200079006E0020006100640072006F0064006400200069003B00720020006400640065007200620079006E0066006100), '2015-04-14 09:19:46.913')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 19, convert(nvarchar(max), 0x440065007700690073007700630068002000640064006900770072006E006F0064002000610020006600660065006600720069007200), '2015-04-14 09:18:34.290')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 20, convert(nvarchar(max), 0x4D00610065003B006E002000640064007200770067002000670065006E006E0079006D002C0020006E006900200061006C006C0077006E002000640064006F00640020006F002000680079006400200069003B006300680020006D0061006E0079006C0069006F006E00), '2015-04-14 09:18:34.290')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 21, convert(nvarchar(max), 0x43007900730079006C006C0074007700630068002000E2003B00720020004400640065007200620079006E006600610020006E0065007500200043006C0069006300690077006300680020006100720020007900200062006F00740077006D002000690073006F006400200069002000670065006900730069006F002000650074006F00), '2015-04-14 09:18:34.290')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 22, convert(nvarchar(max), 0x54007200EF007700630068002000650074006F00), '2015-04-14 09:18:34.290')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 23, convert(nvarchar(max), 0x430072006F00650073006F00), '2015-04-14 09:18:34.290')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 24, convert(nvarchar(max), 0x430061006400610072006E00680065007700630068002000650069006300680020006300610069007300), '2015-04-14 09:20:03.793')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 25, convert(nvarchar(max), 0x430061006400610072006E00680061007500), '2015-04-14 09:20:03.793')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 26, convert(nvarchar(max), 0x440069006400640079006D007500), '2015-04-14 09:20:03.793')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 27, convert(nvarchar(max), 0x79006E00), '2015-04-14 09:20:03.793')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 28, convert(nvarchar(max), 0x4400650077006900730077006300680020006500690063006800200064006900770072006E006F0064002000670065006E006900), '2015-04-14 09:22:11.777')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 29, convert(nvarchar(max), 0x440069006D00200053006C006F00740069006100750020004100720020006700610065006C00), '2015-04-14 09:18:41.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 30, convert(nvarchar(max), 0x4D00610065003B006E002000640064007200770067002000670065006E006E0079006D0021002000470061006C006C0075002000700072006F00730065007300750020006500690063006800200063006100690073002E00), '2015-04-14 09:20:20.727')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 31, convert(nvarchar(max), 0x4F00730020006700770065006C00770063006800200079006E002000640064006100200079006E0020006100640072006F0064006400200069003B00720020006400640065007200620079006E0066006100), '2015-04-14 09:20:20.730')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 32, convert(nvarchar(max), 0x4F004B00), '2015-04-14 09:20:20.730')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 33, convert(nvarchar(max), 0x47006F0072006600660065006E00), '2015-04-14 09:20:33.300')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 34, convert(nvarchar(max), 0x440069006F006C0063006800), '2015-04-14 09:20:33.300')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 35, convert(nvarchar(max), 0x4500690063006800200062006F00640020007700650064006900200063007900720072006100650064006400200079006E0020006300610065006C002000650069002000670061006400610072006E00680061007500), '2015-04-14 09:20:33.300')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 36, convert(nvarchar(max), 0x590064007700), '2015-04-14 09:20:33.300')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 37, convert(nvarchar(max), 0x4E006100630020006F0065007300), '2015-04-14 09:20:33.300')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 38, convert(nvarchar(max), 0x4100200079006400790063006800200061006D002000670079006D007200790064002000790072002000610072006F006C00770067003F00), '2015-04-14 09:20:33.300')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 39, convert(nvarchar(max), 0x47006F0072006600660065006E00), '2015-04-14 09:20:43.550')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 40, convert(nvarchar(max), 0x610072002000670079006600650072002000650069006300680020006300610069007300200061007000770079006E007400690061006400), '2015-04-14 09:20:43.550')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 41, convert(nvarchar(max), 0x440069006F006C0063006800), '2015-04-14 09:20:43.550')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 42, convert(nvarchar(max), 0x79006E00), '2015-04-14 09:20:43.550')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 43, convert(nvarchar(max), 0x440069006F006C0063006800), '2015-04-14 09:20:53.277')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 44, convert(nvarchar(max), 0x47006F0072006600660065006E00), '2015-04-14 09:20:53.277')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 45, convert(nvarchar(max), 0x45006900630068002000610072006F006C007700670020007700650064006900200065006900200067006F0066006E006F0064006900), '2015-04-14 09:20:53.277')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 46, convert(nvarchar(max), 0x4400650077006900730077006300680020006C006C007900740068007900720065006E002000670079006E0074006100660020006500690063006800200065006E0077002000630079006E00740061006600), '2015-04-14 09:22:18.230')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 47, convert(nvarchar(max), 0x4400650077006900730077006300680020006C007900740068007900720065006E002000670079006E0074006100660020006500690063006800200063007900660065006E007700), '2015-04-14 09:22:23.823')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 48, convert(nvarchar(max), 0x44007900640064006900610064002000670065006E0069002000520068006F00770063006800), '2015-04-14 09:22:36.667')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 49, convert(nvarchar(max), 0x44006900770072006E006F006400), '2015-04-14 09:22:36.667')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 50, convert(nvarchar(max), 0x4D0069007300), '2015-04-14 09:22:36.667')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 51, convert(nvarchar(max), 0x42006C00770079006400640079006E00), '2015-04-14 09:22:36.667')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 52, convert(nvarchar(max), 0x4E006500730061006600), '2015-04-14 09:22:36.667')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 53, convert(nvarchar(max), 0x440065007700690073007700630068002000650069006300680020007200680079007700), '2015-04-14 09:22:46.470')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 54, convert(nvarchar(max), 0x47007700720079007700), '2015-04-14 09:22:46.470')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 55, convert(nvarchar(max), 0x420065006E0079007700), '2015-04-14 09:22:46.470')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 56, convert(nvarchar(max), 0x53006B0069007000), '2015-04-14 09:22:46.470')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 57, convert(nvarchar(max), 0x430049004F00530047002000670061006500610066006700790073006700750020002E002E002E002E00), '2015-04-14 09:21:13.380')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 58, convert(nvarchar(max), 0x41006D007300650072007500200041006C006C0061006E00200059006E0020002E002E002E00), '2015-04-14 09:21:35.000')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 59, convert(nvarchar(max), 0x430072006F00650073006F0020006900), '2015-04-14 09:21:45.787')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 60, convert(nvarchar(max), 0x6400650077006900730020006900610069007400680020006900200064006400650063006800720061007500), '2015-04-14 09:21:45.787')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 61, convert(nvarchar(max), 0x4400650077006900730077006300680020006F0070007300690077006E00), '2015-04-14 09:21:50.600')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 62, convert(nvarchar(max), 0x440065007700690073007700630068002000650069006300680020006D00690073002000670065006E006900), '2015-04-14 09:22:53.743')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 63, convert(nvarchar(max), 0x49002000640064006500630068007200610075002C00200064006500770069007300770063006800200075006E0020006F003B00720020006F0070007300690079006E00610075002000690073006F006400), '2015-04-14 09:22:01.720')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 64, convert(nvarchar(max), 0x430072006F00650073006F0020006900), '2015-04-14 09:22:06.533')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 65, convert(nvarchar(max), 0x4400650077006900730077006300680020006D00610074006800200073006C006F007400), '2015-04-14 09:23:22.710')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 66, convert(nvarchar(max), 0x4E006500730061006600), '2015-04-14 09:23:45.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 67, convert(nvarchar(max), 0x410074006500620077006300680020007900200063007700650073007400690079006E00610075002000630061006E006C0079006E006F006C00), '2015-04-14 09:23:45.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 68, convert(nvarchar(max), 0x53006B0069007000), '2015-04-14 09:23:45.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 69, convert(nvarchar(max), 0x4F00730020006700770065006C00770063006800200079006E00200064006400610020006400650077006900730077006300680020006F0070007300690077006E00), '2015-04-14 09:23:33.800')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 70, convert(nvarchar(max), 0x410072006F006C00790067006F006E0020006400640069006D0020006100720020006700610065006C002000610072002000680079006E0020006F0020006200720079006400), '2015-04-14 09:23:33.803')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 71, convert(nvarchar(max), 0x4400650077006900730077006300680020006500690063006800200062006C00770079006400640079006E002000670065006E006900), '2015-04-14 09:23:16.490')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 72, convert(nvarchar(max), 0x4F004B00), '2015-04-14 09:23:16.490')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 73, convert(nvarchar(max), 0x440069006D00200075006E0020006F003B00720020005500630068006F006400), '2015-04-14 09:23:16.490')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 74, convert(nvarchar(max), 0x42006C00770079006400640079006E00200048006500620020006500690020006400640061007200670061006E0066006F006400), '2015-04-14 09:23:16.490')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 75, convert(nvarchar(max), 0x43007900730079006C006C0074007700630068002000E2003B00720020004400640065007200620079006E0066006100), '2015-04-14 09:23:16.490')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 82, convert(nvarchar(max), 0x4E006500670065007300650075006F006E00), '2015-04-14 09:19:54.210')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 83, convert(nvarchar(max), 0x4E006500730061006600), '2015-04-14 09:19:54.210')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 84, convert(nvarchar(max), 0x4400650077006900730077006300680020006500690063006800200062006C00770079006400640079006E002000670065006E006900), '2015-04-14 09:22:59.553')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 85, convert(nvarchar(max), 0x4E006500730061006600), '2015-04-14 09:22:59.557')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (18, 97, convert(nvarchar(max), 0x430079006D007200610065006700), '')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 1, convert(nvarchar(max), 0x4D006200EB0072007200690074006A006500), '2015-04-14 09:17:35.510')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 2, convert(nvarchar(max), 0x530074007500640069006D00), '2015-04-14 09:17:35.510')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 3, convert(nvarchar(max), 0x4800610072007400610020006500200066006100710065007300), '2015-04-14 09:17:35.513')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 4, convert(nvarchar(max), 0x4200EB006E006900200045006D00EB00720069006D006900), '2015-04-14 09:17:35.513')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 5, convert(nvarchar(max), 0x45006D00EB00720069006D006500740020005000EB007200), '2015-04-14 09:16:17.920')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 6, convert(nvarchar(max), 0x4B006F006E006600690072006D006F006A00), '2015-04-14 09:16:17.920')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 7, convert(nvarchar(max), 0x41006E0075006C006F006A00), '2015-04-14 09:16:17.920')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 8, convert(nvarchar(max), 0x4E006100200076006A0065006E0020006B00650071002C0020006E00650020006E0075006B0020006A0065006D00690020006E00EB00200067006A0065006E0064006A00650020007000EB00720020007400EB00200067006A00650074007500720020007400EB00200064006800EB006E006100740020007400750061006A006100), '2015-04-14 09:16:17.920')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 9, convert(nvarchar(max), 0x4A00750020006C007500740065006D0020006B006F006E00740061006B0074006F006E006900200070007200690074006A006500730020006F007300650020004B006C0069006B006F006E00690020006200750074006F006E0069006E0020006D00EB00200070006F00730068007400EB0020007000EB00720020007400EB002000700072006F0076006F006E00690020007000EB0072007300EB0072006900), '2015-04-14 09:16:17.920')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 10, convert(nvarchar(max), 0x500072006F0076006F006E00690020005000EB0072007300EB0072006900), '2015-04-14 09:16:17.920')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 11, convert(nvarchar(max), 0x4B006F006E006600690072006D006F00200041006C006C00), '2015-04-14 09:16:17.920')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 12, convert(nvarchar(max), 0x410072007200690074006A00610020006E00EB002000660069006C006C0069006D00), '2015-04-14 09:16:17.920')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 13, convert(nvarchar(max), 0x4E006100200076006A0065006E0020006B00650071002C00200065006D00EB00720069006D00690020006A00750061006A002000EB00730068007400EB002000230023002C0020006E00650020006E0075006B0020006A0065006D00690020006E00EB00200067006A0065006E0064006A00650020007000EB00720020007400EB002000700061007200EB0020006A00750020006E00EB002000640065007200690020002300230020006D0069006E00750074006100200070006100720061002000740061006B0069006D006900740020007400750061006A002E0020004A00750020006C007500740065006D00690020007400EB002000700072006F0076006F006E00690020007000EB0072007300EB007200690020006D00EB00200076006F006E00EB002E00), '2015-04-14 09:16:17.920')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 14, convert(nvarchar(max), 0x410072007200690074006A006100200076006F006E00EB00), '2015-04-14 09:16:17.923')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 15, convert(nvarchar(max), 0x4E006100200076006A0065006E0020006B00650071002C0020006B006F006800610020006A00750061006A00200065006D00EB00720069006D0069002000690020002300230020006B00610020006B0061006C00750061007200200064006800650020006E00650020006E0075006B0020006A0065006D00690020006E00EB00200067006A0065006E0064006A00650020007000EB00720020007400EB002000700061007200EB0020006A00750020006E00EB002E0020004A00750020006C007500740065006D0020006B006F006E00740061006B0074006F006E006900200070007200690074006A006500), '2015-04-14 09:16:17.923')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 16, convert(nvarchar(max), 0x4E00EB002000720072006500670075006C006C00), '2015-04-14 09:16:17.923')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 17, convert(nvarchar(max), 0x4E006100200076006A0065006E0020006B0065007100210020004E00EB002000700061006D0075006E006400EB007300690020007000EB00720020007400EB0020007000EB007200700075006E0075006100720020006B00EB0072006B0065007300EB006E0020007400750061006A002E00), '2015-04-14 09:16:17.923')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 18, convert(nvarchar(max), 0x4A00750020006C007500740065006D0020007200610070006F00720074006F006E00690020006E00EB00200070007200690074006A006500), '2015-04-14 09:16:17.923')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 19, convert(nvarchar(max), 0x4A00750020006C007500740065006D002C0020007000EB0072007A0067006A006900640068006E00690020006E006A00EB002000640069007400EB00200065002000700072006500660065007200750061007200), '2015-04-14 09:15:53.287')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 20, convert(nvarchar(max), 0x4E006100200076006A0065006E0020006B00650071002C0020006E00650020006E0075006B0020006A0065006D00690020006E00EB00200067006A0065006E0064006A00650020007000EB00720020007400EB00200067006A00650074007500720020007400EB00200064006800EB006E006100740020007400750061006A006100), '2015-04-14 09:15:53.287')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 21, convert(nvarchar(max), 0x4A00750020006C007500740065006D0020006B006F006E00740061006B0074006F006E006900200070007200690074006A006500730020006F007300650020004B006C0069006B006F006E00690020006200750074006F006E0069006E0020006D00EB00200070006F00730068007400EB0020007000EB00720020007400EB002000700072006F0076006F006E00690020007000EB0072007300EB0072006900), '2015-04-14 09:15:53.287')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 22, convert(nvarchar(max), 0x500072006F0076006F006E00690020005000EB0072007300EB0072006900), '2015-04-14 09:15:53.287')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 23, convert(nvarchar(max), 0x490020006D0069007200EB00700072006900740075007200), '2015-04-14 09:15:53.287')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 24, convert(nvarchar(max), 0x4A00750020006C007500740065006D0020006B006F006E006600690072006D006F006E00690020006B00EB0072006B0065007300EB006E0020007400750061006A00), '2015-04-14 09:16:38.873')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 25, convert(nvarchar(max), 0x4B006F006E006600690072006D006F006A00), '2015-04-14 09:16:38.873')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 26, convert(nvarchar(max), 0x41006E0075006C006F006A00), '2015-04-14 09:16:38.873')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 27, convert(nvarchar(max), 0x6E00EB00), '2015-04-14 09:16:38.873')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 28, convert(nvarchar(max), 0x4A00750020006C007500740065006D002C0020007000EB0072007A0067006A006900640068006E0069002000640069007400EB006E0020007400750061006A002000650020006C0069006E0064006A0065007300), '2015-04-14 09:18:07.790')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 29, convert(nvarchar(max), 0x4E0075006B00200053006C006F0074007300200041007600610069006C00610062006C006500), '2015-04-14 09:15:59.757')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 30, convert(nvarchar(max), 0x4E006100200076006A0065006E0020006B0065007100210020004E00EB002000700061006D0075006E006400EB007300690020007000EB00720020007400EB0020007000EB007200700075006E0075006100720020006B00EB0072006B0065007300EB006E0020007400750061006A002E00), '2015-04-14 09:16:47.900')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 31, convert(nvarchar(max), 0x4A00750020006C007500740065006D0020007200610070006F00720074006F006E00690020006E00EB00200070007200690074006A006500), '2015-04-14 09:16:47.900')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 32, convert(nvarchar(max), 0x4E00EB002000720072006500670075006C006C00), '2015-04-14 09:16:47.900')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 33, convert(nvarchar(max), 0x460069006E00690073006800), '2015-04-14 09:16:59.840')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 34, convert(nvarchar(max), 0x5400680061006E006B00200059006F007500), '2015-04-14 09:16:59.840')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 35, convert(nvarchar(max), 0x41007200640068006A00610020006A00750061006A002000EB00730068007400EB0020006B006F006E006600690072006D00750061007200), '2015-04-14 09:16:59.840')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 36, convert(nvarchar(max), 0x50006F00), '2015-04-14 09:16:59.840')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 37, convert(nvarchar(max), 0x4A006F00), '2015-04-14 09:16:59.840')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 38, convert(nvarchar(max), 0x4100200064006F006E00690020007400EB0020006D006500720072006E006900200073006F006E00640061007A0068003F00), '2015-04-14 09:16:59.843')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 39, convert(nvarchar(max), 0x460069006E00690073006800), '2015-04-14 09:17:09.420')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 40, convert(nvarchar(max), 0x7000EB00720020006B00EB0072006B0065007300EB006E0020007400750061006A0020007400EB00200065006D00EB00720069006D0069007400), '2015-04-14 09:17:09.420')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 41, convert(nvarchar(max), 0x5400680061006E006B00200059006F007500), '2015-04-14 09:17:09.420')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 42, convert(nvarchar(max), 0x6E00EB00), '2015-04-14 09:17:09.420')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 43, convert(nvarchar(max), 0x5400680061006E006B00200059006F007500), '2015-04-14 09:17:18.950')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 44, convert(nvarchar(max), 0x460069006E00690073006800), '2015-04-14 09:17:18.950')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 45, convert(nvarchar(max), 0x53006F006E00640061007A006800690020006A00750061006A002000750020007200650067006A006900730074007200750061007200), '2015-04-14 09:17:18.950')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 46, convert(nvarchar(max), 0x4A00750020006C007500740065006D00690020007A0067006A006900640068006E0069002000730068006B0072006F006E006A00EB006E00200065002000700061007200EB0020007400EB002000460069007200730074006E0061006D00650020007400750061006A00), '2015-04-14 09:18:16.240')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 47, convert(nvarchar(max), 0x4A00750020006C007500740065006D00690020007A0067006A006900640068006E0069002000730068006B0072006F006E006A00EB006E00200065002000700061007200EB0020007400EB0020006D006200690065006D0072006900740020007400750061006A00), '2015-04-14 09:18:21.693')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 48, convert(nvarchar(max), 0x530068006B007200750061006E0069002000640061007400EB006E002000650020006C0069006E0064006A0065007300), '2015-04-14 09:18:32.930')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 49, convert(nvarchar(max), 0x440069007400EB00), '2015-04-14 09:18:32.930')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 50, convert(nvarchar(max), 0x4D00750061006A00), '2015-04-14 09:18:32.930')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 51, convert(nvarchar(max), 0x560069007400), '2015-04-14 09:18:32.930')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 52, convert(nvarchar(max), 0x54006A0065007400EB007200), '2015-04-14 09:18:32.930')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 53, convert(nvarchar(max), 0x4A00750020006C007500740065006D002C0020007000EB0072007A0067006A006900640068006E006900200067006A0069006E0069006E00EB0020007400750061006A00), '2015-04-14 09:18:41.643')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 54, convert(nvarchar(max), 0x4D006100730068006B0075006C006C00), '2015-04-14 09:18:41.643')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 55, convert(nvarchar(max), 0x460065006D00EB007200), '2015-04-14 09:18:41.643')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 56, convert(nvarchar(max), 0x4B0061006C006F00), '2015-04-14 09:18:41.643')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 57, convert(nvarchar(max), 0x4B0069006F0073006B002000680069006200650072006E006100740069006E00670020002E002E002E002E00), '2015-04-14 09:17:26.920')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 58, convert(nvarchar(max), 0x4B006F006800610020004F007500740020004E00EB0020002E002E002E00), '2015-04-14 09:17:41.107')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 59, convert(nvarchar(max), 0x4D0069007200EB002000730065002000760069006E00690020006E00EB00), '2015-04-14 09:17:46.343')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 60, convert(nvarchar(max), 0x7A0067006A006900640068006E00690020006E006A00EB00200067006A0075006800EB0020007000EB00720020007400EB002000660069006C006C00750061007200), '2015-04-14 09:17:46.343')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 61, convert(nvarchar(max), 0x4A00750020006C007500740065006D002C0020007000EB0072007A0067006A006900640068006E00690020006E006A00EB0020006F007000730069006F006E00), '2015-04-14 09:17:51.600')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 62, convert(nvarchar(max), 0x4A00750020006C007500740065006D002C0020007000EB0072007A0067006A006900640068006E00690020006D00750061006A0069006E0020006A00750061006A002000650020006C0069006E0064006A0065007300), '2015-04-14 09:18:47.330')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 63, convert(nvarchar(max), 0x5000EB00720020007400EB002000660069006C006C007500610072002C0020006A00750020006C007500740065006D0020007A0067006A006900640068006E00690020006E006A00EB0020006E006700610020006F007000730069006F006E006500740020006D00EB00200070006F00730068007400EB00), '2015-04-14 09:17:56.303')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 64, convert(nvarchar(max), 0x4D0069007200EB002000730065002000760069006E00690020006E00EB00), '2015-04-14 09:18:01.947')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 65, convert(nvarchar(max), 0x4A00750020006C007500740065006D0020007A0067006A006900640068006E00690020006E006A00EB0020006C006C006F006A00200073006C006F007400), '2015-04-14 09:19:11.940')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 66, convert(nvarchar(max), 0x54006A0065007400EB007200), '2015-04-14 09:19:22.913')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 67, convert(nvarchar(max), 0x4A00750020006C007500740065006D00690020007000EB00720067006A00690067006A0075006E006900200070007900650074006A0065007600650020007400EB0020006D00EB0070006F007300680074006D006500), '2015-04-14 09:19:22.913')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 68, convert(nvarchar(max), 0x4B0061006C006F00), '2015-04-14 09:19:22.913')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 69, convert(nvarchar(max), 0x4A00750020006C007500740065006D00690020007A0067006A006900640068006E00690020006E006A00EB0020006F007000730069006F006E00), '2015-04-14 09:19:16.943')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 70, convert(nvarchar(max), 0x53006F006E00640061007A0068006500200061006B007400750061006C00690073006800740020007000610064006900730070006F006E0075006500730068006D006500), '2015-04-14 09:19:16.943')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 71, convert(nvarchar(max), 0x4A00750020006C007500740065006D002C0020007000EB0072007A0067006A006900640068006E006900200076006900740069006E0020007400750061006A0020007400EB0020006C0069006E0064006A0065007300), '2015-04-14 09:19:06.490')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 72, convert(nvarchar(max), 0x4E00EB002000720072006500670075006C006C00), '2015-04-14 09:19:06.490')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 73, convert(nvarchar(max), 0x410073006E006A00EB0020006E006700610020006D00EB0020006C00610072007400EB00), '2015-04-14 09:19:06.490')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 74, convert(nvarchar(max), 0x560069007400690020004E006F007400200046006F0075006E006400), '2015-04-14 09:19:06.490')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 75, convert(nvarchar(max), 0x4A00750020006C007500740065006D0020006B006F006E00740061006B0074006F006E006900200070007200690074006A0065007300), '2015-04-14 09:19:06.490')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 82, convert(nvarchar(max), 0x4D006500730061007A00680065007400), '2015-04-14 09:16:30.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 83, convert(nvarchar(max), 0x54006A0065007400EB007200), '2015-04-14 09:16:30.863')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 84, convert(nvarchar(max), 0x4A00750020006C007500740065006D002C0020007000EB0072007A0067006A006900640068006E006900200076006900740069006E0020007400750061006A0020007400EB0020006C0069006E0064006A0065007300), '2015-04-14 09:18:53.210')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 85, convert(nvarchar(max), 0x54006A0065007400EB007200), '2015-04-14 09:18:53.210')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (19, 98, convert(nvarchar(max), 0x41006C00620061006E00690061006E00), '')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 1, convert(nvarchar(max), 0x86099709AE09A809), '2015-04-14 09:13:56.533')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 2, convert(nvarchar(max), 0x9C09B009BF09AA09), '2015-04-14 09:13:56.533')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 3, convert(nvarchar(max), 0xB809BE0987099F092000AE09CD09AF09BE09AA09), '2015-04-14 09:13:56.533')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 4, convert(nvarchar(max), 0xA809BF09AF09BC09CB09970920009509B009BE09), '2015-04-14 09:13:56.533')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 5, convert(nvarchar(max), 0x9C09A809CD09AF0920009509B2099509AC09CD099C09BE09), '2015-04-14 09:12:51.997')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 6, convert(nvarchar(max), 0xA809BF09B609CD099A09BF09A40920009509B009BE09), '2015-04-14 09:12:51.997')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 7, convert(nvarchar(max), 0xAC09BE09A409BF09B209), '2015-04-14 09:12:51.997')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 8, convert(nvarchar(max), 0xA609C10983099609BF09A4092C0020008609AE09B009BE0920008609AA09A809BE09B0092000AC09BF09AC09B009A30920009609C10981099C09C7092000AA09C709A409C70920008509B809AE09B009CD09A5092000B909A809), '2015-04-14 09:12:51.997')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 9, convert(nvarchar(max), 0xB009BF09B809C709AA09B609A8092000B809BE09A509C7092000AF09CB099709BE09AF09CB09970920009509B009C109A8092000AC09BE0920008609AC09BE09B00920009A09C709B709CD099F09BE0920009509B009C109A8092000A809C0099A09C709B0092000AC09BE099F09A80920009509CD09B209BF09950920009509B009C109A809), '2015-04-14 09:12:51.997')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 10, convert(nvarchar(max), 0x8609AC09BE09B00920009A09C709B709CD099F09BE0920009509B009C109A809), '2015-04-14 09:12:51.997')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 11, convert(nvarchar(max), 0xB809AE09B809CD09A4092000A809BF09B609CD099A09BF09A40920009509B009C109A809), '2015-04-14 09:12:51.997')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 12, convert(nvarchar(max), 0xAA09CD09B009BE09B009AE09CD09AD09BF099509200086099709AE09A809), '2015-04-14 09:12:51.997')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 13, convert(nvarchar(max), 0xA609C10983099609BF09A4092C0020008609AA09A809BE09B00920008509CD09AF09BE09AA09AF09BC09C709A809CD099F09AE09C709A809CD099F0920008609AE09B009BE0920008609AA09A809BE09B00920008509CD09AF09BE09AA09AF09BC09C709A809CD099F09AE09C709A809CD099F09200086099709C7092000230023002000AE09BF09A809BF099F092000AA09B009CD09AF09A809CD09A40920008609AA09A809BF0920009A09C709950920009509B009A409C709200085099509CD09B709AE092C002000230023002000B909AF09BC092E002000AA09B009C70920008609AC09BE09B00920009A09C709B709CD099F09BE0920009509B009C109A8092E00), '2015-04-14 09:12:51.997')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 14, convert(nvarchar(max), 0xA609C709B009BF09A409C7092000AA09CC099B09BE09A809CB09), '2015-04-14 09:12:51.997')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 15, convert(nvarchar(max), 0xA609C10983099609BF09A4092C0020002300230020008F09B00920008609AA09A809BE09B00920008509CD09AF09BE09AA09AF09BC09C709A809CD099F09AE09C709A809CD099F092000B809AE09AF09BC092000AA09C709B009BF09AF09BC09C70920009709C7099B09C70920008F09AC09820920008609AE09B009BE092000AF09C70920008609AA09A809BF0920009A09C709950920009509B009A409C709200085099509CD09B709AE092E0020008509AD09CD09AF09B009CD09A509A809BE092000B809BE09A509C7092000AF09CB099709BE09AF09CB09970920009509B009C109A809), '2015-04-14 09:12:51.997')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 16, convert(nvarchar(max), 0xA009BF099509200086099B09C709), '2015-04-14 09:12:51.997')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 17, convert(nvarchar(max), 0xA609C10983099609BF09A409210020008609AA09A809BE09B00920008509A809C109B009CB09A7092000AA09CD09B0099509CD09B009BF09AF09BC09BE0920009509B009A409C709200085099509CD09B709AE092E00), '2015-04-14 09:12:52.000')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 18, convert(nvarchar(max), 0x8509AD09CD09AF09B009CD09A509A809BE092000B009BF09AA09CB09B009CD099F0920009509B009C109A8092000A609AF09BC09BE0920009509B009C709), '2015-04-14 09:12:52.000')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 19, convert(nvarchar(max), 0x8F0995099F09BF092000AA099B09A809CD09A609B80987092000A609BF09A8092000A809BF09B009CD09AC09BE099A09A80920009509B009C109A809), '2015-04-14 09:12:23.520')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 20, convert(nvarchar(max), 0xA609C10983099609BF09A4092C0020008609AE09B009BE0920008609AA09A809BE09B0092000AC09BF09AC09B009A30920009609C10981099C09C7092000AA09C709A409C70920008509B809AE09B009CD09A5092000B909A809), '2015-04-14 09:12:23.520')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 21, convert(nvarchar(max), 0xB009BF09B809C709AA09B609A8092000B809BE09A509C7092000AF09CB099709BE09AF09CB09970920009509B009C109A8092000AC09BE0920008609AC09BE09B00920009A09C709B709CD099F09BE0920009509B009C109A8092000A809C0099A09C709B0092000AC09BE099F09A80920009509CD09B209BF09950920009509B009C109A809), '2015-04-14 09:12:23.520')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 22, convert(nvarchar(max), 0x8609AC09BE09B00920009A09C709B709CD099F09BE0920009509B009C109A809), '2015-04-14 09:12:23.520')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 23, convert(nvarchar(max), 0xB809CD09AC09BE099709A409), '2015-04-14 09:12:23.520')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 24, convert(nvarchar(max), 0x8609AA09A809BE09B00920008509A809C109B009CB09A7092000A609AF09BC09BE0920009509B009C7092000A809BF09B609CD099A09BF09A40920009509B009C109A809), '2015-04-14 09:13:05.987')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 25, convert(nvarchar(max), 0xA809BF09B609CD099A09BF09A40920009509B009BE09), '2015-04-14 09:13:05.987')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 26, convert(nvarchar(max), 0xAC09BE09A409BF09B209), '2015-04-14 09:13:05.987')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 27, convert(nvarchar(max), 0x8F09), '2015-04-14 09:13:05.987')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 28, convert(nvarchar(max), 0x8609AA09A809BE09B00920009C09A809CD09AE092000A609BF09A8092000A809BF09B009CD09AC09BE099A09A80920009509B009C109A809), '2015-04-14 09:14:26.073')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 29, convert(nvarchar(max), 0xAA09BE099309AF09BC09BE092000AF09BE09AF09BC0920009509CB09A8092000B809CD09B2099F09), '2015-04-14 09:12:29.547')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 30, convert(nvarchar(max), 0xA609C10983099609BF09A409210020008609AA09A809BE09B00920008509A809C109B009CB09A7092000AA09CD09B0099509CD09B009BF09AF09BC09BE0920009509B009A409C709200085099509CD09B709AE092E00), '2015-04-14 09:13:16.020')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 31, convert(nvarchar(max), 0x8509AD09CD09AF09B009CD09A509A809BE092000B009BF09AA09CB09B009CD099F0920009509B009C109A8092000A609AF09BC09BE0920009509B009C709), '2015-04-14 09:13:16.020')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 32, convert(nvarchar(max), 0xA009BF099509200086099B09C709), '2015-04-14 09:13:16.023')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 33, convert(nvarchar(max), 0xB609C709B709), '2015-04-14 09:13:27.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 34, convert(nvarchar(max), 0xA709A809CD09AF09AC09BE09A609), '2015-04-14 09:13:27.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 35, convert(nvarchar(max), 0x8609AA09A809BE09B009200086099709AE09A809C709B0092000A809BF09B609CD099A09BF09A40920009509B009BE092000B909AF09BC09), '2015-04-14 09:13:27.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 36, convert(nvarchar(max), 0xB909BE098109), '2015-04-14 09:13:27.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 37, convert(nvarchar(max), 0xA809BE09), '2015-04-14 09:13:27.323')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 38, convert(nvarchar(max), 0x8609AA09A809BF0920009C09B009BF09AA0920009709CD09B009B909A30920009509B009A409C70920009A09BE09A8093F00), '2015-04-14 09:13:27.323')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 39, convert(nvarchar(max), 0xB609C709B709), '2015-04-14 09:13:34.627')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 40, convert(nvarchar(max), 0x8609AA09A809BE09B00920008509CD09AF09BE09AA09AF09BC09C709A809CD099F09AE09C709A809CD099F0920008509A809C109B009CB09A70920009C09A809CD09AF09), '2015-04-14 09:13:34.627')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 41, convert(nvarchar(max), 0xA709A809CD09AF09AC09BE09A609), '2015-04-14 09:13:34.627')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 42, convert(nvarchar(max), 0x8F09), '2015-04-14 09:13:34.627')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 43, convert(nvarchar(max), 0xA709A809CD09AF09AC09BE09A609), '2015-04-14 09:13:42.500')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 44, convert(nvarchar(max), 0xB609C709B709), '2015-04-14 09:13:42.500')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 45, convert(nvarchar(max), 0x8609AA09A809BE09B00920009C09B009BF09AA092000B009C7099509B009CD09A10920009509B009BE092000B909AF09BC09C7099B09C709), '2015-04-14 09:13:42.500')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 46, convert(nvarchar(max), 0x8609AA09A809BE09B0092000B609C109A709C109AE09BE09A409CD09B0092000660069007200730074006E0061006D0065002000AA09CD09B009A509AE09200085099509CD09B709B0092000A809BF09B009CD09AC09BE099A09A80920009509B009C109A809), '2015-04-14 09:14:31.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 47, convert(nvarchar(max), 0x8609AA09A809BE09B00920008909AA09BE09A709BF092000AA09CD09B009A509AE09200085099509CD09B709B0092000A809BF09B009CD09AC09BE099A09A80920009509B009C109A809), '2015-04-14 09:14:36.973')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 48, convert(nvarchar(max), 0x9C09A809CD09AE092000A409BE09B009BF0996092000B209BF099609C109A809), '2015-04-14 09:14:46.653')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 49, convert(nvarchar(max), 0xA609BF09A809), '2015-04-14 09:14:46.653')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 50, convert(nvarchar(max), 0xAE09BE09B809), '2015-04-14 09:14:46.653')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 51, convert(nvarchar(max), 0xAC099B09B009), '2015-04-14 09:14:46.653')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 52, convert(nvarchar(max), 0xAA09B009AC09B009CD09A409C009), '2015-04-14 09:14:46.653')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 53, convert(nvarchar(max), 0x8609AA09A809BE09B0092000B209BF099909CD0997092000A809BF09B009CD09AC09BE099A09A80920009509B009C109A809), '2015-04-14 09:14:56.313')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 54, convert(nvarchar(max), 0xAA09C109B009C109B709), '2015-04-14 09:14:56.313')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 55, convert(nvarchar(max), 0xAE09B909BF09B209BE09), '2015-04-14 09:14:56.313')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 56, convert(nvarchar(max), 0xB209BE09AB09BE09B209BE09AB09BF0920009509B009BE09), '2015-04-14 09:14:56.313')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 57, convert(nvarchar(max), 0x9509BF09AF09BC09B809CD0995092000680069006200650072006E006100740069006E00670020002E002E002E002E00), '2015-04-14 09:13:50.260')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 58, convert(nvarchar(max), 0xAE09A709CD09AF09C7092000860989099F0920009F09BE098709AE09BF09820920002E002E002E00), '2015-04-14 09:14:01.980')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 59, convert(nvarchar(max), 0xB809CD09AC09BE099709A409AE09), '2015-04-14 09:14:07.110')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 60, convert(nvarchar(max), 0xB609C109B009C10920009509B009A409C70920008F0995099F09BF092000AD09BE09B709BE092000A809BF09B009CD09AC09BE099A09A80920009509B009C109A809), '2015-04-14 09:14:07.110')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 61, convert(nvarchar(max), 0x8F0995099F09BF092000AC09BF099509B209CD09AA092000A809BF09B009CD09AC09BE099A09A80920009509B009C109A809), '2015-04-14 09:14:11.850')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 62, convert(nvarchar(max), 0x8609AA09A809BE09B00920009C09A809CD09AE092000AE09BE09B8092000A809BF09B009CD09AC09BE099A09A80920009509B009C109A809), '2015-04-14 09:15:01.587')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 63, convert(nvarchar(max), 0xB609C109B009C10920009509B009BE09B00920009C09A809CD09AF092C002000A809BF099A09C709B00920008509AA09B609A809C709B00920008F0995099F09BF092000A809BF09B009CD09AC09BE099A09A80920009509B009C109A809), '2015-04-14 09:14:16.100')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 64, convert(nvarchar(max), 0xB809CD09AC09BE099709A409AE09), '2015-04-14 09:14:21.213')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 65, convert(nvarchar(max), 0x8F0995099F09BF092000B809CD09B2099F0920009F09BE098709AA092000A809BF09B009CD09AC09BE099A09A80920009509B009C109A809), '2015-04-14 09:15:22.473')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 66, convert(nvarchar(max), 0xAA09B009AC09B009CD09A409C009), '2015-04-14 09:15:34.820')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 67, convert(nvarchar(max), 0xA809BF09AE09CD09A809B209BF099609BF09A4092000AA09CD09B009B609CD09A809C709B00920008909A409CD09A409B0092000A609BF09A409C7092000A609AF09BC09BE0920009509B009C709), '2015-04-14 09:15:34.820')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 68, convert(nvarchar(max), 0xB209BE09AB09BE09B209BE09AB09BF0920009509B009BE09), '2015-04-14 09:15:34.820')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 69, convert(nvarchar(max), 0x8F0995099F09BF092000AC09BF099509B209CD09AA092000A809BF09B009CD09AC09BE099A09A80920009509B009C109A809), '2015-04-14 09:15:28.227')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 70, convert(nvarchar(max), 0xAC09B009CD09A409AE09BE09A809C70920008509A809C109AA09B209AC09CD09A7092000B809BE09B009CD09AD09C709B009), '2015-04-14 09:15:28.227')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 71, convert(nvarchar(max), 0x8609AA09A809BE09B00920009C09A809CD09AE092000AC099B09B0092000A809BF09B009CD09AC09BE099A09A80920009509B009C109A809), '2015-04-14 09:15:17.240')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 72, convert(nvarchar(max), 0xA009BF099509200086099B09C709), '2015-04-14 09:15:17.240')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 73, convert(nvarchar(max), 0x8909AA09B009C709B00920009509CB09A8099F09BF0987092000A809AF09BC09), '2015-04-14 09:15:17.240')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 74, convert(nvarchar(max), 0xAC099B09B0092000AA09BE099309AF09BC09BE092000AF09BE09AF09BC09A809BF09), '2015-04-14 09:15:17.240')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 75, convert(nvarchar(max), 0xB009BF09B809C709AA09B609A8092000B809BE09A509C7092000AF09CB099709BE09AF09CB09970920009509B009C109A809), '2015-04-14 09:15:17.243')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 82, convert(nvarchar(max), 0xAC09BE09B009CD09A409BE09), '2015-04-14 09:12:58.253')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 83, convert(nvarchar(max), 0xAA09B009AC09B009CD09A409C009), '2015-04-14 09:12:58.253')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 84, convert(nvarchar(max), 0x8609AA09A809BE09B00920009C09A809CD09AE092000AC099B09B0092000A809BF09B009CD09AC09BE099A09A80920009509B009C109A809), '2015-04-14 09:15:07.007')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 85, convert(nvarchar(max), 0xAA09B009AC09B009CD09A409C009), '2015-04-14 09:15:07.007')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (20, 99, convert(nvarchar(max), 0xAC09BE099909BE09B209BF09), '')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 1, convert(nvarchar(max), 0x4100720072006900760061006C00), '2015-04-14 09:09:26.130')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 2, convert(nvarchar(max), 0x530075007200760065007900), '2015-04-14 09:09:26.130')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 3, convert(nvarchar(max), 0x530069007400650020004D0061007000), '2015-04-14 09:09:26.130')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 4, convert(nvarchar(max), 0x4100610064002000620061006C006C0061006E0020007500), '2015-04-14 09:09:26.130')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 5, convert(nvarchar(max), 0x420061006C006C0061006D006900790065007900), '2015-04-14 09:08:22.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 6, convert(nvarchar(max), 0x580061007100690069006A006900), '2015-04-14 09:08:22.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 7, convert(nvarchar(max), 0x430061006E00630065006C00), '2015-04-14 09:08:22.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 8, convert(nvarchar(max), 0x53006F007200720079002000610061006E002000610077006F006F00640069006E00200069006E002000610079002000680065006C00610061006E0020006600610061006800660061006100680069006E0074006100), '2015-04-14 09:08:22.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 9, convert(nvarchar(max), 0x4600610064006C0061006E0020006C0061002000780069007200690069007200200052006500630065007000740069006F006E00200061006D0061002000670075006A0069002000620061006400680061006E006B006100200068006F006F0073006500200073006900200061006100640020006D006100720020006B0061006C0065002000690073006B0075002000640061007900), '2015-04-14 09:08:22.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 10, convert(nvarchar(max), 0x490073006B007500200064006100790020004D0061007200), '2015-04-14 09:08:22.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 11, convert(nvarchar(max), 0x580061007100690069006A006900200041006C006C00), '2015-04-14 09:08:22.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 12, convert(nvarchar(max), 0x49006D00610061006E007300680061006800610020004500610072006C007900), '2015-04-14 09:08:22.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 13, convert(nvarchar(max), 0x53006F007200720079002C002000620061006C006C0061006E007400610061006400610020007700610061002000230023002C002000610061006E002000610077006F006F00640069006E00200069006E0020006100610064002000680075006200690073006F00200069006E00200069006C006100610020006400610071006900690071006F0020002300230020006B006100200068006F0072002000620061006C006C0061006E00740061006100640061002E0020004600610064006C0061006E002000690073006B007500200064006100790020006D00610072002000640061006D00620065002E00), '2015-04-14 09:08:22.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 14, convert(nvarchar(max), 0x49006D00610061006E007300680061006800610020004C00610074006500), '2015-04-14 09:08:22.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 15, convert(nvarchar(max), 0x53006F007200720079002C002000770061007100740069006700610020006100610064002000620061006C0061006E0020006F0066002000230023002000610079006100610020006D00610072006100790020006F006F002000610061006E002000610077006F006F00640069006E00200069006E0020006100610064002000680075006200690073006F00200069006E002E0020004600610064006C0061006E0020006C0061002000780069007200690069007200200073006F006F0020006400680061007700650079006E0074006100), '2015-04-14 09:08:22.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 16, convert(nvarchar(max), 0x4F004B00), '2015-04-14 09:08:22.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 17, convert(nvarchar(max), 0x53006F00720072007900210020004B0061007200740069002000750020007900650065006C0061006E00200063006F00640073006900670061006100670061002E00), '2015-04-14 09:08:22.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 18, convert(nvarchar(max), 0x4600610064006C0061006E00200078006100610064006900720069002000710061006100620069006C0061006100640064006100), '2015-04-14 09:08:22.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 19, convert(nvarchar(max), 0x4600610064006C0061006E00200064006F006F0072006F0020006D00610061006C0069006E00200064006F006F0072006200690064006100790073006F00), '2015-04-14 09:07:54.450')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 20, convert(nvarchar(max), 0x53006F007200720079002000610061006E002000610077006F006F00640069006E00200069006E002000610079002000680065006C00610061006E0020006600610061006800660061006100680069006E0074006100), '2015-04-14 09:07:54.450')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 21, convert(nvarchar(max), 0x4600610064006C0061006E0020006C0061002000780069007200690069007200200052006500630065007000740069006F006E00200061006D0061002000670075006A0069002000620061006400680061006E006B006100200068006F006F0073006500200073006900200061006100640020006D006100720020006B0061006C0065002000690073006B0075002000640061007900), '2015-04-14 09:07:54.450')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 22, convert(nvarchar(max), 0x490073006B007500200064006100790020004D0061007200), '2015-04-14 09:07:54.450')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 23, convert(nvarchar(max), 0x570065006C0063006F006D006500), '2015-04-14 09:07:54.450')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 24, convert(nvarchar(max), 0x4600610064006C0061006E002000780061007100690069006A006900200063006F0064007300690067006100610067006100), '2015-04-14 09:08:37.140')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 25, convert(nvarchar(max), 0x580061007100690069006A006900), '2015-04-14 09:08:37.140')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 26, convert(nvarchar(max), 0x430061006E00630065006C00), '2015-04-14 09:08:37.140')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 27, convert(nvarchar(max), 0x61007400), '2015-04-14 09:08:37.140')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 28, convert(nvarchar(max), 0x4600610064006C0061006E0020006B0061002000780075006C006F0020006D00610061006C0069006E007400610020006400680061006C00610073006800610064006100), '2015-04-14 09:09:59.317')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 29, convert(nvarchar(max), 0x4E006F00200062006F006F007300610073006B00610020006C0061002000680065006C00690020006B00610072006F00), '2015-04-14 09:08:01.743')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 30, convert(nvarchar(max), 0x53006F00720072007900210020004B0061007200740069002000750020007900650065006C0061006E00200063006F00640073006900670061006100670061002E00), '2015-04-14 09:08:45.453')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 31, convert(nvarchar(max), 0x4600610064006C0061006E00200078006100610064006900720069002000710061006100620069006C0061006100640064006100), '2015-04-14 09:08:45.457')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 32, convert(nvarchar(max), 0x4F004B00), '2015-04-14 09:08:45.457')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 33, convert(nvarchar(max), 0x460069006E00690073006800), '2015-04-14 09:08:55.560')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 34, convert(nvarchar(max), 0x57006100780061006100640020006B00750020006D006100680061006400730061006E00), '2015-04-14 09:08:55.560')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 35, convert(nvarchar(max), 0x49006D0061006100740069006E006B00610061006700610020007700610078006100610020006C006100670075002000780061007100690069006A006900790065007900), '2015-04-14 09:08:55.560')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 36, convert(nvarchar(max), 0x480061006100), '2015-04-14 09:08:55.560')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 37, convert(nvarchar(max), 0x4E006F00), '2015-04-14 09:08:55.560')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 38, convert(nvarchar(max), 0x4D0061002000720061006200740061006100200069006E00200061006100640020006200610061006400680069007400610061006E0020006B0075002000710061006100640061006E003F00), '2015-04-14 09:08:55.560')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 39, convert(nvarchar(max), 0x460069006E00690073006800), '2015-04-14 09:09:04.830')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 40, convert(nvarchar(max), 0x620061006C006C0061006E0074006100610064006100200063006F00640073006900), '2015-04-14 09:09:04.830')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 41, convert(nvarchar(max), 0x57006100780061006100640020006B00750020006D006100680061006400730061006E00), '2015-04-14 09:09:04.830')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 42, convert(nvarchar(max), 0x61007400), '2015-04-14 09:09:04.830')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 43, convert(nvarchar(max), 0x57006100780061006100640020006B00750020006D006100680061006400730061006E00), '2015-04-14 09:09:11.450')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 44, convert(nvarchar(max), 0x460069006E00690073006800), '2015-04-14 09:09:11.450')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 45, convert(nvarchar(max), 0x59006F0075007200200073006100680061006E002000610079006100610020006C00610067006100200073006F006F002000640075007500620061007900), '2015-04-14 09:09:11.450')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 46, convert(nvarchar(max), 0x4600610064006C0061006E00200064006F006F0072006F00200077006100720071006100640020006B006F006F0077006100610064002000650065002000660069007200730074006E0061006D006500), '2015-04-14 09:10:04.690')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 47, convert(nvarchar(max), 0x4600610064006C0061006E00200064006F006F0072006F0020007700610072007100610064002000750067007500200068006F00720072006500650079006100200065006500200061006100640020006C00610020006F006400680061006E00), '2015-04-14 09:10:09.563')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 48, convert(nvarchar(max), 0x4B007500200071006F00720020007400610061007200690069006B0068006400610020006400680061006C00610073006800610064006100), '2015-04-14 09:11:00.637')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 49, convert(nvarchar(max), 0x440061007900), '2015-04-14 09:11:00.637')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 50, convert(nvarchar(max), 0x42006900730068006100), '2015-04-14 09:11:00.637')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 51, convert(nvarchar(max), 0x530061006E00610064006B006100), '2015-04-14 09:11:00.637')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 52, convert(nvarchar(max), 0x4E00650078007400), '2015-04-14 09:11:00.637')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 53, convert(nvarchar(max), 0x4600610064006C0061006E0020006B0061002000780075006C006F0020006A0069006E007300690067006100), '2015-04-14 09:11:09.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 54, convert(nvarchar(max), 0x4D0061006C006500), '2015-04-14 09:11:09.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 55, convert(nvarchar(max), 0x460065006D0061006C006500), '2015-04-14 09:11:09.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 56, convert(nvarchar(max), 0x53006B0069007000), '2015-04-14 09:11:09.910')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 57, convert(nvarchar(max), 0x440075006B00610061006E0020007900610072002000480069006200650072006E006100740069006E00670020002E002E002E002E00), '2015-04-14 09:09:17.460')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 58, convert(nvarchar(max), 0x570061007100740069006700610020004F0075007400200049006E0020002E002E002E00), '2015-04-14 09:09:31.803')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 59, convert(nvarchar(max), 0x53006F006F00200044006800610077006F007700), '2015-04-14 09:09:37.087')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 60, convert(nvarchar(max), 0x6B006100200064006F006F0072006F0020006C0075007500710061006400640061002000730069002000610061006400200075002000620069006C006F00770064006F00), '2015-04-14 09:09:37.087')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 61, convert(nvarchar(max), 0x4600610064006C0061006E00200064006F006F0072006F00200069006B0068007400690079006100610072006B006100200061006800), '2015-04-14 09:09:44.480')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 62, convert(nvarchar(max), 0x4600610064006C0061006E0020006B0061002000780075006C006F0020006200690073006800610020006400680061006C00610073006800610064006100), '2015-04-14 09:11:14.580')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 63, convert(nvarchar(max), 0x530069002000610061006400200075002000620069006C006F00770064006F002C0020006600610064006C0061006E00200064006F006F0072006F0020006D006900640020006B00610020006D00690064002000610068002000660075007200730061006400610068006100200068006F006F00730020006B007500200071006F00720061006E00), '2015-04-14 09:09:49.133')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 64, convert(nvarchar(max), 0x53006F006F00200044006800610077006F007700), '2015-04-14 09:09:53.700')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 65, convert(nvarchar(max), 0x4600610064006C0061006E00200064006F006F0072006F0020006E006F006F0063006100200062006F006F0073006B00610020006100), '2015-04-14 09:11:34.503')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 69, convert(nvarchar(max), 0x4600610064006C0061006E00200064006F006F0072006F00200069006B0068007400690079006100610072006B006100200061006800), '2015-04-14 09:11:39.927')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 70, convert(nvarchar(max), 0x4200610061006400690067006F006F0062006B00610020006800610064006400610020006B00610020006D006100710061006E00), '2015-04-14 09:11:39.927')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 71, convert(nvarchar(max), 0x4600610064006C0061006E00200064006F006F0072006F002000730061006E006E00610064006B006100200061006100640020006400680061006C006100740061007900), '2015-04-14 09:11:28.270')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 72, convert(nvarchar(max), 0x4F004B00), '2015-04-14 09:11:28.270')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 73, convert(nvarchar(max), 0x4D00610020006A00690072006F0020006D006900640020006B0061002000730061007200650065007900610020006B0061006E00), '2015-04-14 09:11:28.270')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 74, convert(nvarchar(max), 0x590065006100720020006D0061002000610079002000680065006C0069006E00), '2015-04-14 09:11:28.270')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 75, convert(nvarchar(max), 0x4600610064006C0061006E0020006C0061002000780069007200690069007200200052006500630065007000740069006F006E00), '2015-04-14 09:11:28.270')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 82, convert(nvarchar(max), 0x4D006500730073006100670065007300), '2015-04-14 09:08:29.630')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 83, convert(nvarchar(max), 0x4E00650078007400), '2015-04-14 09:08:29.630')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 84, convert(nvarchar(max), 0x4600610064006C0061006E00200064006F006F0072006F002000730061006E006E00610064006B006100200061006100640020006400680061006C006100740061007900), '2015-04-14 09:11:20.150')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 85, convert(nvarchar(max), 0x4E00650078007400), '2015-04-14 09:11:20.150')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (21, 100, convert(nvarchar(max), 0x53006F006F006D00610061006C006900), '')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 1, convert(nvarchar(max), 0x060A170A2E0A280A), '2015-04-14 07:45:57.713')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 2, convert(nvarchar(max), 0x380A300A350A470A), '2015-04-14 07:45:57.713')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 3, convert(nvarchar(max), 0x530069007400650020004D0061007000), '2015-04-14 07:45:57.717')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 4, convert(nvarchar(max), 0x2E0A410A320A3E0A150A3E0A240A2000150A300A4B0A), '2015-04-14 07:45:57.717')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 5, convert(nvarchar(max), 0x320A080A2000280A3F0A2F0A410A150A240A400A), '2015-04-14 07:44:10.360')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 6, convert(nvarchar(max), 0x2A0A410A380A3C0A1F0A400A), '2015-04-14 07:44:10.360')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 7, convert(nvarchar(max), 0x300A710A260A2000150A300A4B0A), '2015-04-14 07:44:10.363')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 8, convert(nvarchar(max), 0x050A2B0A380A4B0A380A2000380A3E0A280A420A700A2000240A410A390A3E0A210A470A2000350A470A300A350A470A2000260A3E0A20002A0A240A3E0A2000150A300A280A2000320A080A2000050A380A2E0A300A710A250A2000390A410A700A260A470A2000390A280A), '2015-04-14 07:44:10.363')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 9, convert(nvarchar(max), 0x52006500630065007000740069006F006E002000280A3E0A320A2000380A700A2A0A300A150A2000150A300A4B0A20001C0A20002E0A410A5C0A2000150A4B0A380A3C0A3F0A380A3C0A2000150A300A280A2000320A080A2000390A470A200A2000260A3F0A710A240A470A20002C0A1F0A280A2000240A470A2000150A320A3F0A710A150A2000150A300A4B0A), '2015-04-14 07:44:10.363')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 10, convert(nvarchar(max), 0x2B0A3F0A300A2000150A4B0A380A3C0A3F0A380A3C0A2000150A300A4B0A), '2015-04-14 07:44:10.363')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 11, convert(nvarchar(max), 0x380A3E0A300A470A20002A0A410A380A3C0A1F0A400A), '2015-04-14 07:44:10.363')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 12, convert(nvarchar(max), 0x1C0A320A260A400A20002A0A390A410A700A1A0A230A3E0A), '2015-04-14 07:44:10.363')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 13, convert(nvarchar(max), 0x050A2B0A380A4B0A380A2C002000060A2A0A230A400A20002E0A410A320A3E0A150A3E0A240A2000380A3E0A280A420A700A2000060A2A0A230A400A20002E0A410A320A3E0A150A3E0A240A2000260A470A2000050A710A170A470A20002300230020002E0A3F0A700A1F0A2C0020001C0A260A2000240A710A150A2000350A3F0A710A1A0A2000240A410A390A3E0A280A420A700A20001A0A480A710A150A2000150A300A280A2000320A080A2000050A380A2E0A300A710A250A2000390A410A700A260A470A2000390A280A2C002000230023002000390A480A2E0020002C0A3E0A050A260A2000350A3F0A710A1A0A2000260A410A2C0A3E0A300A3E0A2000150A4B0A380A3C0A3F0A380A3C0A2000150A300A4B0A20001C0A400A2E00), '2015-04-14 07:44:10.363')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 14, convert(nvarchar(max), 0x260A470A300A2000060A170A2E0A280A), '2015-04-14 07:44:10.363')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 15, convert(nvarchar(max), 0x050A2B0A380A4B0A380A2C002000230023002000260A400A2000060A2A0A230A400A20002E0A410A320A3E0A150A3E0A240A2000350A470A320A470A20002A0A3E0A380A2000150A300A2000260A3F0A710A240A3E0A2000390A480A2000050A240A470A2000380A3E0A280A420A700A2000350A3F0A710A1A0A2000240A410A390A3E0A280A420A700A20001A0A480A710A150A2000150A300A280A2000320A080A2000050A380A2E0A300A710A250A2000390A410A700A260A470A2000390A280A2E002000300A3F0A380A480A2A0A380A3C0A280A2000280A3E0A320A2000380A700A2A0A300A150A2000150A300A4B0A20001C0A400A), '2015-04-14 07:44:10.363')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 16, convert(nvarchar(max), 0x200A400A150A2000390A480A), '2015-04-14 07:44:10.363')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 17, convert(nvarchar(max), 0x050A2B0A380A4B0A380A2000390A480A21002000240A410A390A3E0A210A400A20002E0A700A170A2000090A710A240A470A2000150A3E0A300A350A3E0A080A2000150A300A280A2000320A080A2000050A380A2E0A300A710A250A2000390A480A2E00), '2015-04-14 07:44:10.363')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 18, convert(nvarchar(max), 0x300A3F0A380A480A2A0A380A3C0A280A2000280A420A700A2000300A3F0A2A0A4B0A300A1F0A2000150A300A4B0A), '2015-04-14 07:44:10.363')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 19, convert(nvarchar(max), 0x2A0A380A700A260A400A260A3E0A2000260A3F0A280A2C002000260A400A20001A0A4B0A230A2000150A300A4B0A20001C0A400A), '2015-04-14 07:40:35.823')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 20, convert(nvarchar(max), 0x050A2B0A380A4B0A380A2000380A3E0A280A420A700A2000240A410A390A3E0A210A470A2000350A470A300A350A470A2000260A3E0A20002A0A240A3E0A2000150A300A280A2000320A080A2000050A380A2E0A300A710A250A2000390A410A700A260A470A2000390A280A), '2015-04-14 07:40:35.823')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 21, convert(nvarchar(max), 0x52006500630065007000740069006F006E002000280A3E0A320A2000380A700A2A0A300A150A2000150A300A4B0A20001C0A20002E0A410A5C0A2000150A4B0A380A3C0A3F0A380A3C0A2000150A300A280A2000320A080A2000390A470A200A2000260A3F0A710A240A470A20002C0A1F0A280A2000240A470A2000150A320A3F0A710A150A2000150A300A4B0A), '2015-04-14 07:40:35.823')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 22, convert(nvarchar(max), 0x2B0A3F0A300A2000150A4B0A380A3C0A3F0A380A3C0A2000150A300A4B0A), '2015-04-14 07:40:35.823')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 23, convert(nvarchar(max), 0x570065006C0063006F006D006500), '2015-04-14 07:40:35.823')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 24, convert(nvarchar(max), 0x060A2A0A230A400A20002C0A470A280A240A400A2000260A400A20002A0A410A380A3C0A1F0A400A2000150A300A4B0A20001C0A400A), '2015-04-14 07:44:57.450')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 25, convert(nvarchar(max), 0x2A0A410A380A3C0A1F0A400A), '2015-04-14 07:44:57.450')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 26, convert(nvarchar(max), 0x300A710A260A2000150A300A4B0A), '2015-04-14 07:44:57.450')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 27, convert(nvarchar(max), 0x240A470A), '2015-04-14 07:44:57.453')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 28, convert(nvarchar(max), 0x1C0A280A2E0A2000260A470A2000060A2A0A230A470A2000260A3F0A280A2000260A400A20001A0A4B0A230A2000150A300A4B0A20001C0A400A), '2015-04-14 09:06:28.110')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 29, convert(nvarchar(max), 0x150A4B0A080A2000380A320A3E0A1F0A), '2015-04-14 07:41:30.053')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 30, convert(nvarchar(max), 0x050A2B0A380A4B0A380A2000390A480A21002000240A410A390A3E0A210A400A20002E0A700A170A2000090A710A240A470A2000150A3E0A300A350A3E0A080A2000150A300A280A2000320A080A2000050A380A2E0A300A710A250A2000390A480A2E00), '2015-04-14 07:45:05.607')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 31, convert(nvarchar(max), 0x300A3F0A380A480A2A0A380A3C0A280A2000280A420A700A2000300A3F0A2A0A4B0A300A1F0A2000150A300A4B0A), '2015-04-14 07:45:05.607')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 32, convert(nvarchar(max), 0x200A400A150A2000390A480A), '2015-04-14 07:45:05.607')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 33, convert(nvarchar(max), 0x2E0A410A150A700A2E0A320A), '2015-04-14 07:45:15.707')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 34, convert(nvarchar(max), 0x240A410A390A3E0A210A3E0A2000270A700A280A350A3E0A260A), '2015-04-14 07:45:15.707')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 35, convert(nvarchar(max), 0x2A0A390A410A700A1A0A230A2000260A400A20002A0A410A380A3C0A1F0A400A2000150A400A240A400A2000390A480A2C00), '2015-04-14 07:45:15.707')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 36, convert(nvarchar(max), 0x1C0A400A), '2015-04-14 07:45:15.707')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 37, convert(nvarchar(max), 0x150A4B0A080A), '2015-04-14 07:45:15.707')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 38, convert(nvarchar(max), 0x240A410A390A3E0A280A420A700A2000380A300A350A470A160A230A2000320A480A230A2000320A080A20001A0A3E0A390A410A700A260A470A2000390A4B0A3F00), '2015-04-14 07:45:15.710')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 39, convert(nvarchar(max), 0x2E0A410A150A700A2E0A320A), '2015-04-14 07:45:29.743')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 40, convert(nvarchar(max), 0x240A410A390A3E0A210A400A20002E0A410A320A3E0A150A3E0A240A20002C0A470A280A240A400A2000320A080A), '2015-04-14 07:45:29.743')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 41, convert(nvarchar(max), 0x240A410A390A3E0A210A3E0A2000270A700A280A350A3E0A260A), '2015-04-14 07:45:29.743')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 42, convert(nvarchar(max), 0x240A470A), '2015-04-14 07:45:29.743')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 43, convert(nvarchar(max), 0x240A410A390A3E0A210A3E0A2000270A700A280A350A3E0A260A), '2015-04-14 07:45:38.170')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 44, convert(nvarchar(max), 0x2E0A410A150A700A2E0A320A), '2015-04-14 07:45:38.170')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 45, convert(nvarchar(max), 0x240A410A390A3E0A210A3E0A2000380A300A350A470A160A230A2000260A300A1C0A2000150A400A240A3E0A2000170A3F0A060A2000390A480A), '2015-04-14 07:45:38.170')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 46, convert(nvarchar(max), 0x240A410A390A3E0A210A470A2000660069007200730074006E0061006D0065002000260A470A20002A0A390A3F0A320A470A2000050A710A160A300A2000260A400A20001A0A4B0A230A2000150A300A4B0A20001C0A400A), '2015-04-14 09:06:31.737')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 47, convert(nvarchar(max), 0x060A2A0A230A470A2000170A4B0A240A2000260A470A20002A0A390A3F0A320A470A2000050A710A160A300A2000260A400A20001A0A4B0A230A2000150A300A4B0A20001C0A400A), '2015-04-14 09:06:37.070')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 48, convert(nvarchar(max), 0x1C0A280A2E0A2000260A400A20003F0A2E0A240A400A2000260A300A1C0A), '2015-04-14 09:06:50.290')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 49, convert(nvarchar(max), 0x260A3F0A350A380A), '2015-04-14 09:06:50.290')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 50, convert(nvarchar(max), 0x2E0A390A400A280A3E0A), '2015-04-14 09:06:50.290')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 51, convert(nvarchar(max), 0x380A3E0A320A), '2015-04-14 09:06:50.290')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 52, convert(nvarchar(max), 0x050A170A320A3E0A), '2015-04-14 09:06:50.290')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 53, convert(nvarchar(max), 0x060A2A0A230A470A2000320A3F0A700A170A2000260A400A20001A0A4B0A230A2000150A300A4B0A20001C0A400A), '2015-04-14 09:06:58.300')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 54, convert(nvarchar(max), 0x2E0A300A260A), '2015-04-14 09:06:58.300')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 55, convert(nvarchar(max), 0x140A300A240A), '2015-04-14 09:06:58.300')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 56, convert(nvarchar(max), 0x1B0A710A210A4B0A), '2015-04-14 09:06:58.300')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 57, convert(nvarchar(max), 0x150A3F0A130A380A150A2000390A3E0A080A2C0A300A280A470A1F0A20002E002E002E002E00), '2015-04-14 07:45:47.230')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 58, convert(nvarchar(max), 0x350A3F0A710A1A0A20002C0A3E0A390A300A20001F0A3E0A080A2E0A3F0A700A170A20002E002E002E00), '2015-04-14 09:06:02.710')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 59, convert(nvarchar(max), 0x380A410A060A170A240A2000390A480A), '2015-04-14 09:06:08.287')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 60, convert(nvarchar(max), 0x380A3C0A410A300A420A2000150A300A280A2000320A080A2000070A710A150A20002D0A3E0A380A3C0A3E0A2000260A400A20001A0A4B0A230A2000150A300A4B0A), '2015-04-14 09:06:08.290')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 61, convert(nvarchar(max), 0x070A710A150A20001A0A4B0A230A2000260A400A20001A0A4B0A230A2000150A300A4B0A20001C0A400A), '2015-04-14 09:06:13.717')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 62, convert(nvarchar(max), 0x1C0A280A2E0A2000260A470A2000060A2A0A230A470A20002E0A390A400A280A470A20001A0A410A230A4B0A20001C0A400A), '2015-04-14 09:07:02.547')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 63, convert(nvarchar(max), 0x380A3C0A410A300A420A2000150A300A280A2000320A080A2C002000390A470A200A20001A0A4B0A230A2000260A400A2000070A710A150A2000260A400A20001A0A4B0A230A2000150A300A4B0A20001C0A400A2C00), '2015-04-14 09:06:18.250')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 64, convert(nvarchar(max), 0x380A410A060A170A240A2000390A480A), '2015-04-14 09:06:22.753')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 65, convert(nvarchar(max), 0x280A420A700A2000070A710A150A2000280A700A2C0A300A2000260A400A2000150A3F0A380A2E0A2000260A400A20001A0A4B0A230A2000150A300A4B0A20001C0A400A), '2015-04-14 09:07:19.660')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 66, convert(nvarchar(max), 0x050A170A320A3E0A), '2015-04-14 09:07:31.083')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 67, convert(nvarchar(max), 0x390A470A200A2000380A350A3E0A320A2000260A470A20001C0A350A3E0A2C0A2000150A300A4B0A20001C0A400A), '2015-04-14 09:07:31.083')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 68, convert(nvarchar(max), 0x1B0A710A210A4B0A), '2015-04-14 09:07:31.083')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 69, convert(nvarchar(max), 0x070A710A150A20001A0A4B0A230A2000260A400A20001A0A4B0A230A2000150A300A4B0A20001C0A400A), '2015-04-14 09:07:24.610')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 70, convert(nvarchar(max), 0x070A380A2000350A470A320A470A2000090A2A0A320A710A2C0A270A2000380A300A350A470A160A230A), '2015-04-14 09:07:24.610')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 71, convert(nvarchar(max), 0x1C0A280A2E0A2000260A470A2000380A3E0A320A2000260A470A2000060A2A0A230A470A2000260A400A20001A0A4B0A230A2000150A300A4B0A20001C0A400A), '2015-04-14 09:07:14.820')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 72, convert(nvarchar(max), 0x200A400A150A2000390A480A), '2015-04-14 09:07:14.820')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 73, convert(nvarchar(max), 0x260A470A2000090A710A2A0A300A2000260A3E0A2000150A4B0A080A), '2015-04-14 09:07:14.820')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 74, convert(nvarchar(max), 0x380A3E0A320A2000320A710A2D0A3F0A060A2000280A3E0A), '2015-04-14 09:07:14.820')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 75, convert(nvarchar(max), 0x300A3F0A380A480A2A0A380A3C0A280A2000280A3E0A320A2000380A700A2A0A300A150A2000150A300A4B0A20001C0A400A), '2015-04-14 09:07:14.820')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 82, convert(nvarchar(max), 0x380A410A280A470A390A470A), '2015-04-14 07:44:42.747')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 83, convert(nvarchar(max), 0x050A170A320A3E0A), '2015-04-14 07:44:42.747')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 84, convert(nvarchar(max), 0x1C0A280A2E0A2000260A470A2000380A3E0A320A2000260A470A2000060A2A0A230A470A2000260A400A20001A0A4B0A230A2000150A300A4B0A20001C0A400A), '2015-04-14 09:07:07.180')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 85, convert(nvarchar(max), 0x050A170A320A3E0A), '2015-04-14 09:07:07.180')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (22, 101, convert(nvarchar(max), 0x2A0A700A1C0A3E0A2C0A400A2000260A470A), '')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 1, convert(nvarchar(max), 0x860A970AAE0AA80A), '2015-04-14 07:28:52.830')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 2, convert(nvarchar(max), 0xB80AB00ACD0AB50AC70A), '2015-04-14 07:28:52.830')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 3, convert(nvarchar(max), 0xB80ABE0A870A9F0A2000AE0AC70AAA0A), '2015-04-14 07:28:52.830')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 4, convert(nvarchar(max), 0xA80ABF0AAE0AA30AC20A820A950A2000950AB00AC00A), '2015-04-14 07:28:52.833')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 5, convert(nvarchar(max), 0xA40AB00AC00A950AC70A2000A80ABF0AAE0AA30AC20A820A950A), '2015-04-14 07:27:08.793')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 6, convert(nvarchar(max), 0x960ABE0AA40AB00AC00A), '2015-04-14 07:27:08.797')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 7, convert(nvarchar(max), 0xB00AA60A2000950AB00ACB0A), '2015-04-14 07:27:08.797')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 8, convert(nvarchar(max), 0xAE0ABE0AAB0A2000950AB00AB60ACB0A2000850AAE0AC70A2000A40AAE0ABE0AB00AC00A2000B50ABF0A970AA40ACB0A2000B60ACB0AA70AB50ABE0A2000AE0ABE0A9F0AC70A2000850AB80AAE0AB00ACD0AA50A20009B0AC70A), '2015-04-14 07:27:08.797')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 9, convert(nvarchar(max), 0xB00ABF0AB80AC70AAA0ACD0AB60AA80A2000B80A820AAA0AB00ACD0A950A2000950AB00ACB0A2000850AA50AB50ABE0A2000AB0AB00AC00A2000AA0ACD0AB00AAF0ABE0AB80A2000950AB00AB50ABE0A2000AE0ABE0A9F0AC70A2000A80AC00A9A0AC70A2000AC0A9F0AA80A2000950ACD0AB20ABF0A950A2000950AB00ACB0A2000950AC30AAA0ABE0A2000950AB00AC00AA80AC70A), '2015-04-14 07:27:08.797')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 10, convert(nvarchar(max), 0xAB0AB00AC00AA50AC00A2000AA0ACD0AB00AAF0AA40ACD0AA80A2000950AB00ACB0A), '2015-04-14 07:27:08.797')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 11, convert(nvarchar(max), 0xAC0AA70ABE0A2000AA0AC10AB70ACD0A9F0ABF0A), '2015-04-14 07:27:08.797')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 12, convert(nvarchar(max), 0xAA0ACD0AB00ABE0AB00A820AAD0ABF0A950A2000860A970AAE0AA80A), '2015-04-14 07:27:08.797')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 13, convert(nvarchar(max), 0xAE0ABE0AAB0A2000950AB00AB60ACB0A2C002000A40AAE0ABE0AB00AC00A2000AE0AC10AB20ABE0A950ABE0AA40AAE0ABE0A820A2000850AAE0AC70A2000A40AAE0ABE0AB00AC00A2000AE0AC10AB20ABE0A950ABE0AA40AAE0ABE0A820A2000AA0AB90AC70AB20ABE0A820A2000230023002000AE0ABF0AA80ABF0A9F0A2000B80AC10AA70AC00A2000AE0ABE0A820A2000A40AAE0AC70A20009A0A950ABE0AB80AC00A2000850AB80AAE0AB00ACD0AA50A2000B90ACB0AAF0A2C0020002300230020009B0AC70A2E002000AA0A9B0AC00AA50AC00A2000AB0AB00AC00A2000AA0ACD0AB00AAF0AA40ACD0AA80A2000950AB00ACB0A2E00), '2015-04-14 07:27:08.797')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 14, convert(nvarchar(max), 0x850A820AA40AAE0ABE0A820A2000860A970AAE0AA80A), '2015-04-14 07:27:08.797')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 15, convert(nvarchar(max), 0xAE0ABE0AAB0A2000950AB00AB60ACB0A2C002000230023002000A80ABE0A2000A40AAE0ABE0AB00AC00A2000AE0AC10AB20ABE0A950ABE0AA40AAE0ABE0A820A2000B80AAE0AAF0A2000AA0AB80ABE0AB00A2000A50A880A2000970AAF0ACB0A20009B0AC70A2000850AA80AC70A2000850AAE0AC70A2000A40AAE0AA80AC70A2000A40AC70A20009A0A950ABE0AB80AB50ABE0A2000AE0ABE0A9F0AC70A2000850AB80AAE0AB00ACD0AA50A20009B0AC70A2E002000B00ABF0AB80AC70AAA0ACD0AB60AA80A2000B80A820AAA0AB00ACD0A950A2000950AB00ACB0A), '2015-04-14 07:27:08.797')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 16, convert(nvarchar(max), 0x930A950AC70A), '2015-04-14 07:27:08.797')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 17, convert(nvarchar(max), 0xAE0ABE0AAB0A2000950AB00AB60ACB0A21002000A40AAE0ABE0AB00AC00A2000B50ABF0AA80A820AA40ABF0A2000AA0AB00A2000AA0ACD0AB00A950ACD0AB00ABF0AAF0ABE0A2000950AB00AB50ABE0AAE0ABE0A820A2000850A950ACD0AB70AAE0A2E00), '2015-04-14 07:27:08.797')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 18, convert(nvarchar(max), 0xB00ABF0AB80AC70AAA0ACD0AB60AA80A2000AE0ABE0A9F0AC70A20009C0ABE0AA30A2000950AB00ACB0A), '2015-04-14 07:27:08.797')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 19, convert(nvarchar(max), 0x8F0A950A2000AA0AB80A820AA60AC00AA60ABE0A2000A60ABF0AB50AB80A2000AA0AB80A820AA60A2000950AB00ACB0A), '2015-04-14 07:26:40.170')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 20, convert(nvarchar(max), 0xAE0ABE0AAB0A2000950AB00AB60ACB0A2000850AAE0AC70A2000A40AAE0ABE0AB00AC00A2000B50ABF0A970AA40ACB0A2000B60ACB0AA70AB50ABE0A2000AE0ABE0A9F0AC70A2000850AB80AAE0AB00ACD0AA50A20009B0AC70A), '2015-04-14 07:26:40.170')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 21, convert(nvarchar(max), 0xB00ABF0AB80AC70AAA0ACD0AB60AA80A2000B80A820AAA0AB00ACD0A950A2000950AB00ACB0A2000850AA50AB50ABE0A2000AB0AB00AC00A2000AA0ACD0AB00AAF0ABE0AB80A2000950AB00AB50ABE0A2000AE0ABE0A9F0AC70A2000A80AC00A9A0AC70A2000AC0A9F0AA80A2000950ACD0AB20ABF0A950A2000950AB00ACB0A2000950AC30AAA0ABE0A2000950AB00AC00AA80AC70A), '2015-04-14 07:26:40.170')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 22, convert(nvarchar(max), 0xAB0AB00AC00AA50AC00A2000AA0ACD0AB00AAF0AA40ACD0AA80A2000950AB00ACB0A), '2015-04-14 07:26:40.170')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 23, convert(nvarchar(max), 0xB80ACD0AB50ABE0A970AA40A), '2015-04-14 07:26:40.170')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 24, convert(nvarchar(max), 0xA40AAE0ABE0AB00AC00A2000B50ABF0AA80A820AA40ABF0A2000960ABE0AA40AB00AC00A2000950AC30AAA0ABE0A2000950AB00AC00AA80AC70A), '2015-04-14 07:27:25.297')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 25, convert(nvarchar(max), 0x960ABE0AA40AB00AC00A), '2015-04-14 07:27:25.297')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 26, convert(nvarchar(max), 0xB00AA60A2000950AB00ACB0A), '2015-04-14 07:27:25.297')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 27, convert(nvarchar(max), 0x960ABE0AA40AC70A), '2015-04-14 07:27:25.297')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 28, convert(nvarchar(max), 0xA40AAE0ABE0AB00AC00A20009C0AA80ACD0AAE0A2000A60ABF0AB50AB80A2000AA0AB80A820AA60A2000950AB00ACB0A), '2015-04-14 07:29:39.303')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 29, convert(nvarchar(max), 0x890AAA0AB20AAC0ACD0AA70A2000950ACB0A880A2000B80ACD0AB20ACB0A9F0ACD0AB80A), '2015-04-14 07:26:46.983')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 30, convert(nvarchar(max), 0xAE0ABE0AAB0A2000950AB00AB60ACB0A21002000A40AAE0ABE0AB00AC00A2000B50ABF0AA80A820AA40ABF0A2000AA0AB00A2000AA0ACD0AB00A950ACD0AB00ABF0AAF0ABE0A2000950AB00AB50ABE0AAE0ABE0A820A2000850A950ACD0AB70AAE0A2E00), '2015-04-14 07:27:35.870')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 31, convert(nvarchar(max), 0xB00ABF0AB80AC70AAA0ACD0AB60AA80A2000AE0ABE0A9F0AC70A20009C0ABE0AA30A2000950AB00ACB0A), '2015-04-14 07:27:35.870')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 32, convert(nvarchar(max), 0x930A950AC70A), '2015-04-14 07:27:35.870')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 33, convert(nvarchar(max), 0xB80AAE0ABE0AAA0ACD0AA40A), '2015-04-14 07:27:48.240')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 34, convert(nvarchar(max), 0x860AAD0ABE0AB00A), '2015-04-14 07:27:48.240')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 35, convert(nvarchar(max), 0xA40AAE0ABE0AB00ABE0A2000860A970AAE0AA80A2000AA0AC10AB70ACD0A9F0ABF0A20009B0AC70A), '2015-04-14 07:27:48.240')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 36, convert(nvarchar(max), 0xB90ABE0A), '2015-04-14 07:27:48.240')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 37, convert(nvarchar(max), 0x950ACB0A880A), '2015-04-14 07:27:48.240')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 38, convert(nvarchar(max), 0xA40AAE0AC70A2000B80AB00ACD0AB50AC70A2000B20AC70AB50ABE0A2000AE0ABE0A820A970ACB0A20009B0ACB0A3F00), '2015-04-14 07:27:48.240')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 39, convert(nvarchar(max), 0xB80AAE0ABE0AAA0ACD0AA40A), '2015-04-14 07:27:56.873')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 40, convert(nvarchar(max), 0xA40AAE0ABE0AB00AC00A2000AE0AC10AB20ABE0A950ABE0AA40AAE0ABE0A820A2000B50ABF0AA80A820AA40AC00A2000AE0ABE0A9F0AC70A), '2015-04-14 07:27:56.873')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 41, convert(nvarchar(max), 0x860AAD0ABE0AB00A), '2015-04-14 07:27:56.873')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 42, convert(nvarchar(max), 0x960ABE0AA40AC70A), '2015-04-14 07:27:56.873')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 43, convert(nvarchar(max), 0x860AAD0ABE0AB00A), '2015-04-14 07:28:04.787')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 44, convert(nvarchar(max), 0xB80AAE0ABE0AAA0ACD0AA40A), '2015-04-14 07:28:04.787')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 45, convert(nvarchar(max), 0xA40AAE0ABE0AB00ABE0A2000B80AB00ACD0AB50AC70A2000B00AC70A950ACB0AB00ACD0AA10A2000950AB00AB50ABE0AAE0ABE0A820A2000860AB50ACD0AAF0ACB0A20009B0AC70A), '2015-04-14 07:28:04.787')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 46, convert(nvarchar(max), 0xA40AAE0ABE0AB00ABE0A2000AE0ABE0AA40ACD0AB00A2000460049005200530054004E0041004D0045002000A80ABE0A2000AA0ACD0AB00AA50AAE0A2000850A950ACD0AB70AB00A2000AA0AB80A820AA60A2000950AB00ACB0A), '2015-04-14 07:29:45.470')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 47, convert(nvarchar(max), 0xA40AAE0ABE0AB00ABE0A2000850A9F0A950A2000A80ABE0A2000AA0ACD0AB00AA50AAE0A2000850A950ACD0AB70AB00A2000AA0AB80A820AA60A2000950AB00ACB0A), '2015-04-14 07:29:50.743')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 48, convert(nvarchar(max), 0x9C0AA80ACD0AAE0A2000A40ABE0AB00AC00A960A2000A60ABE0A960AB20A), '2015-04-14 07:30:13.717')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 49, convert(nvarchar(max), 0xA60ABF0AB50AB80A), '2015-04-14 07:30:13.717')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 50, convert(nvarchar(max), 0xAE0AB90ABF0AA80ACB0A), '2015-04-14 07:30:13.717')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 51, convert(nvarchar(max), 0xB50AB00ACD0AB70A), '2015-04-14 07:30:13.717')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 52, convert(nvarchar(max), 0x860A970ABE0AAE0AC00A), '2015-04-14 07:30:13.717')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 53, convert(nvarchar(max), 0xA40AAE0ABE0AB00ABE0A2000B20ABF0A820A970A2000AA0AB80A820AA60A2000950AB00ACB0A), '2015-04-14 07:30:24.120')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 54, convert(nvarchar(max), 0xAA0AC10AB00AC10AB70A), '2015-04-14 07:30:24.120')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 55, convert(nvarchar(max), 0xB80ACD0AA40ACD0AB00AC00A), '2015-04-14 07:30:24.120')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 56, convert(nvarchar(max), 0x53006B0069007000), '2015-04-14 07:30:24.120')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 57, convert(nvarchar(max), 0x950ABF0A930AB80ACD0A950A2000680069006200650072006E006100740069006E00670020002E002E002E002E00), '2015-04-14 07:28:14.593')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 58, convert(nvarchar(max), 0xAE0ABE0A820A2000860A890A9F0A2000B80AAE0AAF0A20002E002E002E00), '2015-04-14 07:28:58.527')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 59, convert(nvarchar(max), 0xAE0ABE0A9F0AC70A2000B80ACD0AB50ABE0A970AA40A), '2015-04-14 07:29:13.430')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 60, convert(nvarchar(max), 0xB60AB00AC20A2000950AB00AB50ABE0A2000AE0ABE0A9F0AC70A20008F0A950A2000AD0ABE0AB70ABE0A2000AA0AB80A820AA60A), '2015-04-14 07:29:13.430')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 61, convert(nvarchar(max), 0x8F0A950A2000B50ABF0A950AB20ACD0AAA0A2000AA0AB80A820AA60A2000950AB00ACB0A), '2015-04-14 07:29:19.937')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 62, convert(nvarchar(max), 0xA40AAE0ABE0AB00AC00A20009C0AA80ACD0AAE0A2000AE0AB90ABF0AA80AC70A2000AA0AB80A820AA60A2000950AB00ACB0A), '2015-04-14 07:30:31.073')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 63, convert(nvarchar(max), 0xB60AB00AC20A2000950AB00AB50ABE0A2000AE0ABE0A9F0AC70A2C002000A80AC00A9A0AC70AA80ABE0A2000B50ABF0A950AB20ACD0AAA0ACB0AAE0ABE0A820AA50AC00A2000950ACB0A880A20008F0A950A2000AA0AB80A820AA60A2000950AB00ACB0A), '2015-04-14 07:29:25.847')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 64, convert(nvarchar(max), 0xAE0ABE0A9F0AC70A2000B80ACD0AB50ABE0A970AA40A), '2015-04-14 07:29:31.270')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 65, convert(nvarchar(max), 0x8F0A950A2000B80ACD0AB20ACB0A9F0A2000AA0ACD0AB00A950ABE0AB00A2000AA0AB80A820AA60A2000950AB00ACB0A), '2015-04-14 07:31:00.830')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 66, convert(nvarchar(max), 0x860A970ABE0AAE0AC00A), '2015-04-14 07:31:14.173')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 67, convert(nvarchar(max), 0xA80AC00A9A0AC70AA80ABE0A2000AA0ACD0AB00AB60ACD0AA80ACB0AA80ABE0A20009C0AB50ABE0AAC0A2000950AC30AAA0ABE0A2000950AB00AC00AA80AC70A), '2015-04-14 07:31:14.173')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 68, convert(nvarchar(max), 0x53006B0069007000), '2015-04-14 07:31:14.173')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 69, convert(nvarchar(max), 0x8F0A950A2000B50ABF0A950AB20ACD0AAA0A2000AA0AB80A820AA60A2000950AB00ACB0A), '2015-04-14 07:31:06.740')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 70, convert(nvarchar(max), 0xB90ABE0AB20AAE0ABE0A820A2000850AA80AC10AAA0AB20AAC0ACD0AA70A2000B80AB00ACD0AB50AC70A950ACD0AB70AA30A), '2015-04-14 07:31:06.740')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 71, convert(nvarchar(max), 0xA40AAE0ABE0AB00AC00A20009C0AA80ACD0AAE0A2000B50AB00ACD0AB70A2000AA0AB80A820AA60A2000950AB00ACB0A), '2015-04-14 07:30:47.977')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 72, convert(nvarchar(max), 0x930A950AC70A), '2015-04-14 07:30:47.977')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 73, convert(nvarchar(max), 0x890AAA0AB00AA80ABE0A2000950ACB0A880A2000A80AB90AC00A820A), '2015-04-14 07:30:47.977')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 74, convert(nvarchar(max), 0xB50AB00ACD0AB70AC70A2000AE0AB30ACD0AAF0ACB0A2000A80AA50AC00A), '2015-04-14 07:30:47.977')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 75, convert(nvarchar(max), 0xB00ABF0AB80AC70AAA0ACD0AB60AA80A2000B80A820AAA0AB00ACD0A950A2000950AB00ACB0A), '2015-04-14 07:30:47.977')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 82, convert(nvarchar(max), 0xB80A820AA60AC70AB60ABE0A), '2015-04-14 07:27:16.850')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 83, convert(nvarchar(max), 0x860A970ABE0AAE0AC00A), '2015-04-14 07:27:16.850')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 84, convert(nvarchar(max), 0xA40AAE0ABE0AB00AC00A20009C0AA80ACD0AAE0A2000B50AB00ACD0AB70A2000AA0AB80A820AA60A2000950AB00ACB0A), '2015-04-14 07:30:37.700')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 85, convert(nvarchar(max), 0x860A970ABE0AAE0AC00A), '2015-04-14 07:30:37.700')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (23, 102, convert(nvarchar(max), 0x970AC10A9C0AB00ABE0AA40AC00A), '')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (24, 103, convert(nvarchar(max), 0x49007200610071006900), '')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (25, 104, convert(nvarchar(max), 0x4B00750072006400690073006800), '')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 1, convert(nvarchar(max), 0x4806310648062F06), '2015-04-14 07:20:00.240')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 2, convert(nvarchar(max), 0x2806310631063306CC06), '2015-04-14 07:20:00.240')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 3, convert(nvarchar(max), 0x4606420634064706200033062706CC062A06), '2015-04-14 07:20:00.240')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 4, convert(nvarchar(max), 0x310627062000270646062A06350627062806), '2015-04-14 07:20:00.240')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 5, convert(nvarchar(max), 0x42063106270631062000450644062706420627062A062000280631062706CC06), '2015-04-14 07:17:59.183')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 6, convert(nvarchar(max), 0x2A062706CC06CC062F06), '2015-04-14 07:17:59.183')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 7, convert(nvarchar(max), 0x44063A0648062000A90631062F064606), '2015-04-14 07:17:59.187')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 8, convert(nvarchar(max), 0x28062706200039063106360620007E064806320634062000450627062000420627062F06310620002806470620007E06CC062F0627062000A90631062F06460620002706370644062706390627062A0620002E0648062F062000A9062706450644062000A9064606CC062F06), '2015-04-14 07:17:59.187')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 9, convert(nvarchar(max), 0x440637064106270620007E063006CC063106340620002A0645062706330620002806AF06CC063106CC062F06200048062000CC0627062000280627062000A9064406CC06A906200028063106200031064806CC0620002F06A9064506470620003206CC06310620004806200045062C062F062F0627062000270645062A062D06270646062000A9064606CC062F06), '2015-04-14 07:17:59.187')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 10, convert(nvarchar(max), 0x2F064806280627063106470620002A064406270634062000A9064606), '2015-04-14 07:17:59.187')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 11, convert(nvarchar(max), 0x2A06A9063106270631062000470645064706), '2015-04-14 07:17:59.187')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 12, convert(nvarchar(max), 0x4806310648062F062000320648062F06200047064606AF0627064506), '2015-04-14 07:17:59.190')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 13, convert(nvarchar(max), 0x28062706200039063106360620007E064806320634060C062000270646062A0635062706280620002E0648062F062000310627062000230023002000270633062A060C062000450627062000420627062F06310620002806470620002E0648062F0620003106270620008606A9062000A9064606CC062F0620002F06310620002A06270620002300230020002F064206CC064206470620004206280644062000270632062000480642062A0620002E0648062F062000310627062E00200044063706410627062000280639062F06270620002F06480628062706310647062000270645062A062D06270646062000A9064606CC062F062E00), '2015-04-14 07:17:59.190')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 14, convert(nvarchar(max), 0x4806310648062F0620002706480627062E063106), '2015-04-14 07:17:59.190')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 15, convert(nvarchar(max), 0x28062706200039063106360620007E064806320634060C06200032064506270646062000270646062A0635062706280620002E0648062F062000310627062000270632062000230023002000AF06300634062A0647062000270633062A06200048062000450627062000420627062F06310620002806470620002E0648062F0620003106270620008606A9062000A9064606CC062F062E002000440637064106270620007E063006CC063106340620002A06450627063306), '2015-04-14 07:17:59.190')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 16, convert(nvarchar(max), 0x2806270634064706), '2015-04-14 07:17:59.190')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 17, convert(nvarchar(max), 0x28062706200039063106360620007E0648063206340621002000420627062F06310620002806470620007E0631062F0627063206340620002F0631062E064806270633062A0620003406450627062E00), '2015-04-14 07:17:59.190')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 18, convert(nvarchar(max), 0x440637064106270620002806470620007E063006CC06310634062000AF063206270631063406), '2015-04-14 07:17:59.190')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 19, convert(nvarchar(max), 0x44063706410627062000CC06A90620003106480632062000270631062C062D062000270646062A062E06270628062000A9064606CC062F06), '2015-04-14 07:11:34.327')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 20, convert(nvarchar(max), 0x28062706200039063106360620007E064806320634062000450627062000420627062F06310620002806470620007E06CC062F0627062000A90631062F06460620002706370644062706390627062A0620002E0648062F062000A9062706450644062000A9064606CC062F06), '2015-04-14 07:11:34.327')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 21, convert(nvarchar(max), 0x440637064106270620007E063006CC063106340620002A0645062706330620002806AF06CC063106CC062F06200048062000CC0627062000280627062000A9064406CC06A906200028063106200031064806CC0620002F06A9064506470620003206CC06310620004806200045062C062F062F0627062000270645062A062D06270646062000A9064606CC062F06), '2015-04-14 07:11:34.327')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 22, convert(nvarchar(max), 0x2F064806280627063106470620002A064406270634062000A9064606), '2015-04-14 07:11:34.327')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 23, convert(nvarchar(max), 0x2E06480634062000220645062F06), '2015-04-14 07:11:34.327')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 24, convert(nvarchar(max), 0x440637064106270620002F0631062E064806270633062A0620002E0648062F0620003106270620002A062306CC06CC062F06), '2015-04-14 07:18:17.767')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 25, convert(nvarchar(max), 0x2A062706CC06CC062F06), '2015-04-14 07:18:17.770')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 26, convert(nvarchar(max), 0x44063A0648062000A90631062F064606), '2015-04-14 07:18:17.770')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 27, convert(nvarchar(max), 0x2F063106), '2015-04-14 07:18:17.770')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 28, convert(nvarchar(max), 0x4406370641062706200031064806320620002A06480644062F0620002E0648062F062000310627062000270646062A062E06270628062000A9064606CC062F06), '2015-04-14 07:20:51.363')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 29, convert(nvarchar(max), 0x28062F064806460620003406A90627064106470627062000450648062C0648062F06), '2015-04-14 07:11:41.530')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 30, convert(nvarchar(max), 0x28062706200039063106360620007E0648063206340621002000420627062F06310620002806470620007E0631062F0627063206340620002F0631062E064806270633062A0620003406450627062E00), '2015-04-14 07:18:32.300')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 31, convert(nvarchar(max), 0x440637064106270620002806470620007E063006CC06310634062000AF063206270631063406), '2015-04-14 07:18:32.300')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 32, convert(nvarchar(max), 0x2806270634064706), '2015-04-14 07:18:32.300')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 33, convert(nvarchar(max), 0x7E062706CC0627064606), '2015-04-14 07:18:47.877')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 34, convert(nvarchar(max), 0x45062A063406A90631064506), '2015-04-14 07:18:47.877')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 35, convert(nvarchar(max), 0x4806310648062F06200034064506270620002A062706CC06CC062F062000270633062A06), '2015-04-14 07:18:47.877')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 36, convert(nvarchar(max), 0x280644064706), '2015-04-14 07:18:47.877')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 37, convert(nvarchar(max), 0x28062F0648064606), '2015-04-14 07:18:47.877')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 38, convert(nvarchar(max), 0x2206CC062706200034064506270620004506CC0620002E06480627064706CC062F0620003106270620002806470620002806310631063306CC061F06), '2015-04-14 07:18:47.877')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 39, convert(nvarchar(max), 0x7E062706CC0627064606), '2015-04-14 07:18:57.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 40, convert(nvarchar(max), 0x280631062706CC0620002F0631062E064806270633062A06200042063106270631062000450644062706420627062A0620002E0648062F06), '2015-04-14 07:18:57.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 41, convert(nvarchar(max), 0x45062A063406A90631064506), '2015-04-14 07:18:57.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 42, convert(nvarchar(max), 0x2F063106), '2015-04-14 07:18:57.057')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 43, convert(nvarchar(max), 0x45062A063406A90631064506), '2015-04-14 07:19:18.417')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 44, convert(nvarchar(max), 0x7E062706CC0627064606), '2015-04-14 07:19:18.417')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 45, convert(nvarchar(max), 0x2806310631063306CC06200034064506270620002B0628062A06200034062F0647062000270633062A06), '2015-04-14 07:19:18.417')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 46, convert(nvarchar(max), 0x440637064106270620002D0631064106200027064806440620004606270645062000A90648068606A90620002E0648062F062000310627062000270646062A062E06270628062000A9064606CC062F06), '2015-04-14 07:20:57.337')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 47, convert(nvarchar(max), 0x440637064106270620002D06310641062000270648064406200046062706450620002E0627064606480627062F06AF06CC0620002E0648062F062000310627062000270646062A062E06270628062000A9064606CC062F06), '2015-04-14 07:21:02.773')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 48, convert(nvarchar(max), 0x2A0627063106CC062E0620002A06480644062F0620003106270620004806270631062F062000A9064606CC062F06), '2015-04-14 07:22:22.560')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 49, convert(nvarchar(max), 0x310648063206), '2015-04-14 07:22:22.563')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 50, convert(nvarchar(max), 0x450627064706), '2015-04-14 07:22:22.563')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 51, convert(nvarchar(max), 0x330627064406), '2015-04-14 07:22:22.563')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 52, convert(nvarchar(max), 0x280639062F06), '2015-04-14 07:22:22.563')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 53, convert(nvarchar(max), 0x440637064106270620002C0646063306CC062A0620002E0648062F062000310627062000270646062A062E06270628062000A9064606CC062F06), '2015-04-14 07:22:44.660')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 54, convert(nvarchar(max), 0x46063106), '2015-04-14 07:22:44.663')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 55, convert(nvarchar(max), 0x32064606), '2015-04-14 07:22:44.663')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 56, convert(nvarchar(max), 0x2C0633062A062000480620002E06CC0632062000A90631062F064606), '2015-04-14 07:22:44.663')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 57, convert(nvarchar(max), 0xA906CC0648063306A9062000480069006200650072006E006100740069006E00670020002E002E002E002E00), '2015-04-14 07:19:50.053')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 58, convert(nvarchar(max), 0x32064506270646062000280646062F06CC0620002F06310620002E002E002E00), '2015-04-14 07:20:05.990')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 59, convert(nvarchar(max), 0x2E06480634062000220645062F06CC062F06200028064706), '2015-04-14 07:20:12.030')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 60, convert(nvarchar(max), 0x270646062A062E0627062806200032062806270646062000280631062706CC0620003406310648063906), '2015-04-14 07:20:12.030')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 61, convert(nvarchar(max), 0x44063706410627062000CC06A9062000AF063206CC06460647062000310627062000270646062A062E06270628062000A9064606CC062F06), '2015-04-14 07:20:35.280')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 62, convert(nvarchar(max), 0x4406370641062706200045062706470620002A06480644062F0620002E0648062F062000310627062000270646062A062E06270628062000A9064606CC062F06), '2015-04-14 07:22:51.817')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 63, convert(nvarchar(max), 0x280631062706CC06200034063106480639060C06200044063706410627062000CC06A906CC062000270632062000AF063206CC0646064706200047062706CC0620003206CC0631062000310627062000270646062A062E06270628062000A9064606CC062F06), '2015-04-14 07:20:40.390')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 64, convert(nvarchar(max), 0x2E06480634062000220645062F06CC062F06200028064706), '2015-04-14 07:20:45.690')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 65, convert(nvarchar(max), 0x44063706410627062000CC06A906200046064806390620002D0627064106380647062000310627062000270646062A062E06270628062000A9064606CC062F06), '2015-04-14 07:23:13.827')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 66, convert(nvarchar(max), 0x280639062F06), '2015-04-14 07:23:28.423')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 67, convert(nvarchar(max), 0x44063706410627062000280647062000330648062706440627062A0620003206CC06310620007E06270633062E06), '2015-04-14 07:23:28.427')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 68, convert(nvarchar(max), 0x2C0633062A062000480620002E06CC0632062000A90631062F064606), '2015-04-14 07:23:28.427')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 69, convert(nvarchar(max), 0x44063706410627062000CC06A9062000AF063206CC06460647062000310627062000270646062A062E06270628062000A9064606CC062F06), '2015-04-14 07:23:19.573')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 70, convert(nvarchar(max), 0x460638063106330646062C06CC0620002F06310620002D062706440620002D0627063606310620002F06310620002F0633062A063106330620004606CC0633062A06), '2015-04-14 07:23:19.573')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 71, convert(nvarchar(max), 0x4406370641062706200033062706440620002A06480644062F0620002E0648062F062000310627062000270646062A062E06270628062000A9064606CC062F06), '2015-04-14 07:23:07.490')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 72, convert(nvarchar(max), 0x2806270634064706), '2015-04-14 07:23:07.490')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 73, convert(nvarchar(max), 0x4706CC068606A9062F0627064506200027063206200045064806270631062F062000410648064206), '2015-04-14 07:23:07.490')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 74, convert(nvarchar(max), 0x3306270644062000CC06270641062A062000460634062F06), '2015-04-14 07:23:07.490')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 75, convert(nvarchar(max), 0x440637064106270620007E063006CC063106340620002A06450627063306), '2015-04-14 07:23:07.490')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 82, convert(nvarchar(max), 0x7E06CC0627064506), '2015-04-14 07:18:08.157')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 83, convert(nvarchar(max), 0x280639062F06), '2015-04-14 07:18:08.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 84, convert(nvarchar(max), 0x4406370641062706200033062706440620002A06480644062F0620002E0648062F062000310627062000270646062A062E06270628062000A9064606CC062F06), '2015-04-14 07:22:58.350')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 85, convert(nvarchar(max), 0x280639062F06), '2015-04-14 07:22:58.353')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (26, 105, convert(nvarchar(max), 0x46006100720073006900), '')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 1, convert(nvarchar(max), 0xB50BB00BC10B950BC80B), '2015-04-14 06:58:19.963')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 2, convert(nvarchar(max), 0x9A0BB00BCD0BB50BC70B), '2015-04-14 06:58:19.963')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 3, convert(nvarchar(max), 0xA40BB30B2000B50BB00BC80BAA0B9F0BAE0BCD0B), '2015-04-14 06:58:19.963')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 4, convert(nvarchar(max), 0xA80BBF0BAF0BAE0BA90BAE0BCD0B20009A0BC60BAF0BCD0BAF0B), '2015-04-14 06:58:19.963')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 5, convert(nvarchar(max), 0xA80BBF0BAF0BAE0BA90B990BCD0B950BB30BCD0B), '2015-04-14 06:56:55.143')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 6, convert(nvarchar(max), 0x890BB10BC10BA40BBF0BAA0BCD0BAA0B9F0BC10BA40BCD0BA40BB50BC10BAE0BCD0B), '2015-04-14 06:56:55.143')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 7, convert(nvarchar(max), 0xB00BA40BCD0BA40BC10B), '2015-04-14 06:56:55.143')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 8, convert(nvarchar(max), 0xAE0BA90BCD0BA90BBF0B950BCD0B950BB50BC10BAE0BCD0B2C002000A80BBE0B990BCD0B950BB30BCD0B2000890B990BCD0B950BB30BCD0B2000B50BBF0BB50BB00B990BCD0B950BB30BC80B2000950BA30BCD0B9F0BC10BAA0BBF0B9F0BBF0B950BCD0B950B2000AE0BC10B9F0BBF0BAF0BB50BBF0BB20BCD0BB20BC80B), '2015-04-14 06:56:55.143')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 9, convert(nvarchar(max), 0xB50BB00BB50BC70BB10BCD0BAA0BC10B2000A40BC60BBE0B9F0BB00BCD0BAA0BC10B950BC60BBE0BB30BCD0BB30BB50BC10BAE0BCD0B2000850BB20BCD0BB20BA40BC10B2000AE0BC00BA30BCD0B9F0BC10BAE0BCD0B2000AE0BC10BAF0BB10BCD0B9A0BBF0B950BCD0B950BB50BC10BAE0BCD0B2000950BC00BB40BC70B2000890BB30BCD0BB30B2000AA0BC60BBE0BA40BCD0BA40BBE0BA90BC80B2000950BBF0BB30BBF0B950BCD0B20009A0BC60BAF0BCD0BAF0BB50BC10BAE0BCD0B2000A40BAF0BB50BC10B20009A0BC60BAF0BCD0BA40BC10B), '2015-04-14 06:56:55.143')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 10, convert(nvarchar(max), 0xAE0BC00BA30BCD0B9F0BC10BAE0BCD0B2000AE0BC10BAF0BB10BCD0B9A0BBF0B950BCD0B950BB50BC10BAE0BCD0B), '2015-04-14 06:56:55.143')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 11, convert(nvarchar(max), 0x850BA90BC80BA40BCD0BA40BC10B2000890BB10BC10BA40BBF0BAA0BCD0BAA0B9F0BC10BA40BCD0BA40BB50BC10BAE0BCD0B), '2015-04-14 06:56:55.143')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 12, convert(nvarchar(max), 0x860BB00BAE0BCD0BAA0B2000B50BB00BC10B950BC80BAF0BC80B), '2015-04-14 06:56:55.143')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 13, convert(nvarchar(max), 0xAE0BA90BCD0BA90BBF0B950BCD0B950BB50BC10BAE0BCD0B2C002000890B990BCD0B950BB30BCD0B20009A0BA80BCD0BA40BBF0BAA0BCD0BAA0BC10B2000A80BBE0B990BCD0B950BB30BCD0B2000890B990BCD0B950BB30BCD0B20009A0BA80BCD0BA40BBF0BAA0BCD0BAA0BC10B2000AE0BC10BA90BCD0B2000230023002000A80BBF0BAE0BBF0B9F0B990BCD0B950BB30BCD0B2000B50BB00BC80B2000A80BC00B990BCD0B950BB30BCD0B2000AA0BBE0BB00BCD0B950BCD0B950BB20BBE0BAE0BCD0B2000AE0BC10B9F0BBF0BAF0BB50BBF0BB20BCD0BB20BC80B2C002000230023002000860B950BBF0BB10BA40BC10B2E002000AA0BBF0BA90BCD0BA90BB00BCD0B2000AE0BC00BA30BCD0B9F0BC10BAE0BCD0B2000AE0BC10BAF0BB10BCD0B9A0BBF0B950BCD0B950BB50BC10BAE0BCD0B2E00), '2015-04-14 06:56:55.143')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 14, convert(nvarchar(max), 0xA40BBE0BAE0BA40BAE0BBE0B950B2000B50BA80BCD0BA40BC10B), '2015-04-14 06:56:55.143')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 15, convert(nvarchar(max), 0xAE0BA90BCD0BA90BBF0B950BCD0B950BB50BC10BAE0BCD0B2C002000230023002000890B990BCD0B950BB30BCD0B2000A80BBF0BAF0BAE0BA90BAE0BCD0B2000950BBE0BB20BAE0BCD0B2000950B9F0BA80BCD0BA40BC10BB50BBF0B9F0BCD0B9F0BA40BC10B2000AE0BB10BCD0BB10BC10BAE0BCD0B2000A80BBE0BAE0BCD0B2000A80BC00B990BCD0B950BB30BCD0B2000AA0BBE0BB00BCD0B950BCD0B950B2000AE0BC10B9F0BBF0BAF0BB50BBF0BB20BCD0BB20BC80B2E002000B50BB00BB50BC70BB10BCD0BAA0BC10B2000A40BC60BBE0B9F0BB00BCD0BAA0BC10B2000950BC60BBE0BB30BCD0BB30BB50BC10BAE0BCD0B), '2015-04-14 06:56:55.143')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 16, convert(nvarchar(max), 0x9A0BB00BBF0B), '2015-04-14 06:56:55.143')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 17, convert(nvarchar(max), 0xAE0BA90BCD0BA90BBF0B950BCD0B950BB50BC10BAE0BCD0B21002000890B990BCD0B950BB30BCD0B2000950BC70BBE0BB00BBF0B950BCD0B950BC80BAF0BC80B20009A0BC60BAF0BB20BCD0BAA0B9F0BC10BA40BCD0BA40B2000AE0BC10B9F0BBF0BAF0BB50BBF0BB20BCD0BB20BC80B2E00), '2015-04-14 06:56:55.143')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 18, convert(nvarchar(max), 0xB50BB00BB50BC70BB10BCD0BAA0BC10B2000A40BC60BB00BBF0BB50BBF0B950BCD0B950B2000A40BAF0BB50BC10B20009A0BC60BAF0BCD0BA40BC10B), '2015-04-14 06:56:55.143')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 19, convert(nvarchar(max), 0xB50BBF0BB00BC10BAA0BCD0BAA0BAA0BCD0BAA0B9F0BCD0B9F0B2000A80BBE0BB30BCD0B2000A40BC70BB00BCD0BA80BCD0BA40BC60B9F0BC10B950BCD0B950BB50BC10BAE0BCD0B), '2015-04-14 06:56:25.280')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 20, convert(nvarchar(max), 0xAE0BA90BCD0BA90BBF0B950BCD0B950BB50BC10BAE0BCD0B2C002000A80BBE0B990BCD0B950BB30BCD0B2000890B990BCD0B950BB30BCD0B2000B50BBF0BB50BB00B990BCD0B950BB30BC80B2000950BA30BCD0B9F0BC10BAA0BBF0B9F0BBF0B950BCD0B950B2000AE0BC10B9F0BBF0BAF0BB50BBF0BB20BCD0BB20BC80B), '2015-04-14 06:56:25.280')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 21, convert(nvarchar(max), 0xB50BB00BB50BC70BB10BCD0BAA0BC10B2000A40BC60BBE0B9F0BB00BCD0BAA0BC10B950BC60BBE0BB30BCD0BB30BB50BC10BAE0BCD0B2000850BB20BCD0BB20BA40BC10B2000AE0BC00BA30BCD0B9F0BC10BAE0BCD0B2000AE0BC10BAF0BB10BCD0B9A0BBF0B950BCD0B950BB50BC10BAE0BCD0B2000950BC00BB40BC70B2000890BB30BCD0BB30B2000AA0BC60BBE0BA40BCD0BA40BBE0BA90BC80B2000950BBF0BB30BBF0B950BCD0B20009A0BC60BAF0BCD0BAF0BB50BC10BAE0BCD0B2000A40BAF0BB50BC10B20009A0BC60BAF0BCD0BA40BC10B), '2015-04-14 06:56:25.280')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 22, convert(nvarchar(max), 0xAE0BC00BA30BCD0B9F0BC10BAE0BCD0B2000AE0BC10BAF0BB10BCD0B9A0BBF0B950BCD0B950BB50BC10BAE0BCD0B), '2015-04-14 06:56:25.280')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 23, convert(nvarchar(max), 0xB50BB00BB50BC70BB10BCD0B950BBF0BB10BC70BBE0BAE0BCD0B), '2015-04-14 06:56:25.280')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 24, convert(nvarchar(max), 0x890B990BCD0B950BB30BCD0B2000B50BC70BA30BCD0B9F0BC10B950BC70BBE0BB30BC80B2000890BB10BC10BA40BBF0B20009A0BC60BAF0BCD0B950B), '2015-04-14 06:57:15.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 25, convert(nvarchar(max), 0x890BB10BC10BA40BBF0BAA0BCD0BAA0B9F0BC10BA40BCD0BA40BB50BC10BAE0BCD0B), '2015-04-14 06:57:15.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 26, convert(nvarchar(max), 0xB00BA40BCD0BA40BC10B), '2015-04-14 06:57:15.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 27, convert(nvarchar(max), 0xAE0BA30BBF0B950BCD0B950BC10B), '2015-04-14 06:57:15.160')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 28, convert(nvarchar(max), 0x890B990BCD0B950BB30BA40BC10B2000AA0BBF0BB10BA80BCD0BA40B2000A80BBE0BB30BCD0B2000A40BC70BB00BCD0BA80BCD0BA40BC60B9F0BC10B950BCD0B950BB50BC10BAE0BCD0B), '2015-04-14 07:07:33.260')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 29, convert(nvarchar(max), 0x950BBF0B9F0BC80B950BCD0B950BC10BAE0BCD0B20008E0BA80BCD0BA40B2000870B9F0B990BCD0B950BB30BCD0B), '2015-04-14 06:56:33.827')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 30, convert(nvarchar(max), 0xAE0BA90BCD0BA90BBF0B950BCD0B950BB50BC10BAE0BCD0B21002000890B990BCD0B950BB30BCD0B2000950BC70BBE0BB00BBF0B950BCD0B950BC80BAF0BC80B20009A0BC60BAF0BB20BCD0BAA0B9F0BC10BA40BCD0BA40B2000AE0BC10B9F0BBF0BAF0BB50BBF0BB20BCD0BB20BC80B2E00), '2015-04-14 06:57:28.950')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 31, convert(nvarchar(max), 0xB50BB00BB50BC70BB10BCD0BAA0BC10B2000A40BC60BB00BBF0BB50BBF0B950BCD0B950B2000A40BAF0BB50BC10B20009A0BC60BAF0BCD0BA40BC10B), '2015-04-14 06:57:28.950')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 32, convert(nvarchar(max), 0x9A0BB00BBF0B), '2015-04-14 06:57:28.950')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 33, convert(nvarchar(max), 0xAA0BBF0BA90BBF0BB70BCD0B), '2015-04-14 06:57:40.783')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 34, convert(nvarchar(max), 0xA80BA90BCD0BB10BBF0B), '2015-04-14 06:57:40.783')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 35, convert(nvarchar(max), 0x890B990BCD0B950BB30BCD0B2000B50BB00BC10B950BC80BAF0BC80B2000890BB10BC10BA40BBF0B), '2015-04-14 06:57:40.783')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 36, convert(nvarchar(max), 0x860BAE0BCD0B), '2015-04-14 06:57:40.783')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 37, convert(nvarchar(max), 0x870BB20BCD0BB20BC80B), '2015-04-14 06:57:40.783')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 38, convert(nvarchar(max), 0xA80BC00B990BCD0B950BB30BCD0B2000950BA30B950BCD0B950BC60B9F0BC10BAA0BCD0BAA0BC10B20008E0B9F0BC10B950BCD0B950B2000B50BC70BA30BCD0B9F0BC10BAE0BCD0B3F00), '2015-04-14 06:57:40.783')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 39, convert(nvarchar(max), 0xAA0BBF0BA90BBF0BB70BCD0B), '2015-04-14 06:57:51.877')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 40, convert(nvarchar(max), 0x890B990BCD0B950BB30BCD0B20009A0BA80BCD0BA40BBF0BAA0BCD0BAA0BC10B2000950BC70BBE0BB00BBF0B950BCD0B950BC80B), '2015-04-14 06:57:51.877')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 41, convert(nvarchar(max), 0xA80BA90BCD0BB10BBF0B), '2015-04-14 06:57:51.877')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 42, convert(nvarchar(max), 0xAE0BA30BBF0B950BCD0B950BC10B), '2015-04-14 06:57:51.877')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 43, convert(nvarchar(max), 0xA80BA90BCD0BB10BBF0B), '2015-04-14 06:57:59.993')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 44, convert(nvarchar(max), 0xAA0BBF0BA90BBF0BB70BCD0B), '2015-04-14 06:57:59.993')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 45, convert(nvarchar(max), 0x890B990BCD0B950BB30BCD0B2000950BA30B950BCD0B950BC60B9F0BC10BAA0BCD0BAA0BC10B2000AA0BA40BBF0BB50BC10B), '2015-04-14 06:57:59.993')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 46, convert(nvarchar(max), 0x890B990BCD0B950BB30BCD0B2000AE0B9F0BCD0B9F0BC10BAE0BCD0B2000460049005200530054004E0041004D0045002000AE0BC10BA40BB20BCD0B2000950B9F0BBF0BA40BAE0BCD0B2000A40BC70BB00BCD0BA80BCD0BA40BC60B9F0BC10B950BCD0B950BB50BC10BAE0BCD0B), '2015-04-14 07:07:39.117')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 47, convert(nvarchar(max), 0x890B990BCD0B950BB30BCD0B2000950BC10B9F0BC10BAE0BCD0BAA0B2000AA0BC60BAF0BB00BC80B2000AE0BC10BA40BB20BCD0B2000950B9F0BBF0BA40BAE0BCD0B2000A40BC70BB00BCD0BA80BCD0BA40BC60B9F0BC10B950BCD0B950BB50BC10BAE0BCD0B), '2015-04-14 07:09:46.930')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 48, convert(nvarchar(max), 0xAA0BBF0BB10BA80BCD0BA40B2000A40BC70BA40BBF0B2000890BB30BCD0BB30BBF0B9F0BB50BC10BAE0BCD0B), '2015-04-14 07:10:01.070')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 49, convert(nvarchar(max), 0xA80BBE0BB30BCD0B), '2015-04-14 07:10:01.070')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 50, convert(nvarchar(max), 0xAE0BBE0BA40BAE0BCD0B), '2015-04-14 07:10:01.070')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 51, convert(nvarchar(max), 0x860BA30BCD0B9F0BC10B), '2015-04-14 07:10:01.070')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 52, convert(nvarchar(max), 0x850B9F0BC10BA40BCD0BA40B), '2015-04-14 07:10:01.070')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 53, convert(nvarchar(max), 0x890B990BCD0B950BB30BCD0B2000AA0BBE0BB20BBF0BA90BAE0BCD0B2000A40BC70BB00BCD0BB50BC10B20009A0BC60BAF0BCD0B950B), '2015-04-14 07:10:19.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 54, convert(nvarchar(max), 0x860BA30BCD0B), '2015-04-14 07:10:19.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 55, convert(nvarchar(max), 0xAA0BC60BA30BCD0B), '2015-04-14 07:10:19.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 56, convert(nvarchar(max), 0xAE0BBE0BB10BCD0BB10BC10B950B), '2015-04-14 07:10:19.320')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 57, convert(nvarchar(max), 0x950BBF0BAF0BC70BBE0BB80BCD0B950BCD0B2000B50BC70BA30BBE0BAE0BBE0B20002E002E002E002E00), '2015-04-14 06:58:09.460')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 58, convert(nvarchar(max), 0x850BB50BC10B9F0BCD0B9F0BBF0BB20BCD0B2000A80BC70BB00B20002E002E002E00), '2015-04-14 06:58:28.037')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 59, convert(nvarchar(max), 0xB50BB00BB50BC70BB10BCD0B950BBF0BB10BC70BBE0BAE0BCD0B), '2015-04-14 07:05:31.113')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 60, convert(nvarchar(max), 0xA40BC60BBE0B9F0B990BCD0B950B2000B50BC70BA30BCD0B9F0BBF0BAF0B2000AE0BC60BBE0BB40BBF0BAF0BC80B2000A40BC70BB00BCD0BB50BC10B), '2015-04-14 07:05:31.113')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 61, convert(nvarchar(max), 0x920BB00BC10B2000B50BBF0BB00BC10BAA0BCD0BAA0BA40BCD0BA40BC80B2000A40BC70BB00BCD0BA80BCD0BA40BC60B9F0BC10B990BCD0B950BB30BCD0B), '2015-04-14 07:05:38.453')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 62, convert(nvarchar(max), 0xAA0BBF0BB10BA80BCD0BA40B2000AE0BBE0BA40BA40BCD0BA40BC80B2000A40BC70BB00BCD0BA80BCD0BA40BC60B9F0BC10B950BCD0B950BB50BC10BAE0BCD0B), '2015-04-14 07:10:25.523')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 63, convert(nvarchar(max), 0xA40BC60BBE0B9F0B990BCD0B950BC10BB50BA40BB10BCD0B950BC10B2C002000950BC00BB40BC70B2000B50BBF0BB00BC10BAA0BCD0BAA0B990BCD0B950BB30BBF0BB20BCD0B2000920BA90BCD0BB10BC80B2000A40BC70BB00BCD0BB50BC10B20009A0BC60BAF0BCD0BAF0BB50BC10BAE0BCD0B), '2015-04-14 07:07:12.703')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 64, convert(nvarchar(max), 0xB50BB00BB50BC70BB10BCD0B950BBF0BB10BC70BBE0BAE0BCD0B), '2015-04-14 07:07:25.767')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 65, convert(nvarchar(max), 0x920BB00BC10B2000B80BCD0BB20BBE0B9F0BCD0B2000B50B950BC80BAF0BC80BA40BCD0B2000A40BC70BB00BCD0BB50BC10B20009A0BC60BAF0BCD0B950B), '2015-04-14 07:10:52.650')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 66, convert(nvarchar(max), 0x850B9F0BC10BA40BCD0BA40B), '2015-04-14 07:11:06.943')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 67, convert(nvarchar(max), 0xAA0BBF0BA90BCD0BB50BB00BC10BAE0BCD0B2000950BC70BB30BCD0BB50BBF0B950BB30BC10B950BCD0B950BC10B2000AA0BA40BBF0BB20BCD0B20009A0BC60BBE0BB20BCD0BB20BC10B990BCD0B950BB30BCD0B), '2015-04-14 07:11:06.943')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 68, convert(nvarchar(max), 0xAE0BBE0BB10BCD0BB10BC10B950B), '2015-04-14 07:11:06.943')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 69, convert(nvarchar(max), 0x920BB00BC10B2000B50BBF0BB00BC10BAA0BCD0BAA0BA40BCD0BA40BC80B2000A40BC70BB00BCD0BB50BC10B20009A0BC60BAF0BCD0B950B), '2015-04-14 07:10:59.073')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 70, convert(nvarchar(max), 0xA40BB10BCD0BAA0BC70BBE0BA40BC10B2000950BBF0B9F0BC80B950BCD0B950BB50BBF0BB20BCD0BB20BC80B2000950BB00BC10BA40BCD0BA40BBE0BAF0BCD0BB50BC10B), '2015-04-14 07:10:59.073')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 71, convert(nvarchar(max), 0xAA0BBF0BB10BA80BCD0BA40B2000860BA30BCD0B9F0BC80B2000A40BC70BB00BCD0BB50BC10B20009A0BC60BAF0BCD0B950B), '2015-04-14 07:10:42.767')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 72, convert(nvarchar(max), 0x9A0BB00BBF0B), '2015-04-14 07:10:42.767')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 73, convert(nvarchar(max), 0xAE0BC70BB20BC70B20008E0BA40BC10BB50BC10BAE0BCD0B), '2015-04-14 07:10:42.767')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 74, convert(nvarchar(max), 0x860BA30BCD0B9F0BC10B2000950BBF0B9F0BC80B950BCD0B950BB50BBF0BB20BCD0BB20BC80B), '2015-04-14 07:10:42.767')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 75, convert(nvarchar(max), 0xB50BB00BB50BC70BB10BCD0BAA0BC10B2000A40BC60BBE0B9F0BB00BCD0BAA0BC10B2000950BC60BBE0BB30BCD0BB30BB50BC10BAE0BCD0B), '2015-04-14 07:10:42.767')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 82, convert(nvarchar(max), 0x9A0BC60BAF0BCD0BA40BBF0B950BB30BCD0B), '2015-04-14 06:57:04.210')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 83, convert(nvarchar(max), 0x850B9F0BC10BA40BCD0BA40B), '2015-04-14 06:57:04.210')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 84, convert(nvarchar(max), 0xAA0BBF0BB10BA80BCD0BA40B2000860BA30BCD0B9F0BC80B2000A40BC70BB00BCD0BB50BC10B20009A0BC60BAF0BCD0B950B), '2015-04-14 07:10:32.130')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 85, convert(nvarchar(max), 0x850B9F0BC10BA40BCD0BA40B), '2015-04-14 07:10:32.130')
insert into [PatientFlow].[Translation] ([LanguageId], 
[TranslationRefId], [TranslationText],Modified) values (27, 106, convert(nvarchar(max), 0xA40BAE0BBF0BB40BCD0B), '')


update [PatientFlow].[Language] set [TranslationRefId]= 86 where [LanguageId]=7;
update [PatientFlow].[Language] set [TranslationRefId]= 87 where [LanguageId]=8;
update [PatientFlow].[Language] set [TranslationRefId]= 88 where [LanguageId]=9;
update [PatientFlow].[Language] set [TranslationRefId]= 89 where [LanguageId]=10;
update [PatientFlow].[Language] set [TranslationRefId]= 90 where [LanguageId]=11;
update [PatientFlow].[Language] set [TranslationRefId]= 91 where [LanguageId]=12;
update [PatientFlow].[Language] set [TranslationRefId]= 92 where [LanguageId]=13;
update [PatientFlow].[Language] set [TranslationRefId]= 93 where [LanguageId]=14;
update [PatientFlow].[Language] set [TranslationRefId]= 94 where [LanguageId]=15;
update [PatientFlow].[Language] set [TranslationRefId]= 95 where [LanguageId]=16;
update [PatientFlow].[Language] set [TranslationRefId]= 96 where [LanguageId]=17;
update [PatientFlow].[Language] set [TranslationRefId]= 97 where [LanguageId]=18;
update [PatientFlow].[Language] set [TranslationRefId]= 98 where [LanguageId]=19;
update [PatientFlow].[Language] set [TranslationRefId]= 99 where [LanguageId]=20;
update [PatientFlow].[Language] set [TranslationRefId]= 100 where [LanguageId]=21;
update [PatientFlow].[Language] set [TranslationRefId]= 101 where [LanguageId]=22;
update [PatientFlow].[Language] set [TranslationRefId]= 102 where [LanguageId]=23;
update [PatientFlow].[Language] set [TranslationRefId]= 103 where [LanguageId]=24;
update [PatientFlow].[Language] set [TranslationRefId]= 104 where [LanguageId]=25;
update [PatientFlow].[Language] set [TranslationRefId]= 105 where [LanguageId]=26;
update [PatientFlow].[Language] set [TranslationRefId]= 106 where [LanguageId]=27;





