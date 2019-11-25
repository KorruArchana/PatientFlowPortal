-- PatientFlow.Survey
alter table PatientFlow.Survey drop constraint PK_PatientFlow_Survey_SurveyId
alter table PatientFlow.Survey drop column SurveyId
alter table PatientFlow.Survey add SurveyId bigint identity(1,1) not null  constraint PK_PatientFlow_Survey_SurveyId primary key (SurveyId)