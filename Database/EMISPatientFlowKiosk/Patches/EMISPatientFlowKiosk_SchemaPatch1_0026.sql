/*
Description: Adding new column snowmed code  in Questionnaire.
Author: Preethi
Patch Number: 1.0026
Dependant Patch Number: 1.0002
*/

alter table PatientFlow.QuestionAnswerOptions
add SnomedCode bigint null 

drop type [PatientFlow].[QuestionOption]

create type [PatientFlow].[QuestionOption] as table
(
    [QuestionId] int null,
    [OptionId] int not null,
    [OptionValue] varchar (50) null,
    [NestedQuestionId] int not null,
    [OptionCode] varchar (50) null,
    [SnomedCode] bigint null,
    primary key clustered ([OptionId] asc)
);


alter table PatientFlow.Member
add JobCategory varchar(100) null