alter table [PatientFlow].[QuestionAnswerOptions] drop constraint FK_PatientFlow_QuestionAnswerOptions_QuestionId
alter table [PatientFlow].[Question] drop constraint FK_PatientFlow_Question_QuestionnaireId
alter table [PatientFlow].[KioskQuestionnaire] drop constraint FK_PatientFlow_KioskQuestionnaire_QuestionnaireId
truncate table [PatientFlow].[QuestionAnswerOptions]
truncate table [PatientFlow].[Question]
truncate table [PatientFlow].[KioskQuestionnaire]
truncate table [PatientFlow].[Questionnaire]
alter table [PatientFlow].[QuestionAnswerOptions] add constraint FK_PatientFlow_QuestionAnswerOptions_QuestionId
foreign key(QuestionId) references [PatientFlow].[Question](QuestionId)
alter table [PatientFlow].[Question] 
add constraint FK_PatientFlow_Question_QuestionnaireId 
foreign key(QuestionnaireId) references [PatientFlow].[Questionnaire](QuestionnaireId)
alter table [PatientFlow].[KioskQuestionnaire] add constraint FK_PatientFlow_KioskQuestionnaire_QuestionnaireId
foreign key(QuestionnaireId) references [PatientFlow].[Questionnaire](QuestionnaireId)

alter table [PatientFlow].[Questionnaire] add [OrganisationId] [int] not null 
alter table [PatientFlow].[Questionnaire]
add constraint FK_PatientFlow_Questionnaire_OrganisationId foreign key(OrganisationId) references [PatientFlow].[Organisation](OrganisationId)

delete from PatientFlow.SiteMenu where  parentmenuid=7
delete from PatientFlow.SiteMenu where nodeTypeId=7 and parentmenuid=0