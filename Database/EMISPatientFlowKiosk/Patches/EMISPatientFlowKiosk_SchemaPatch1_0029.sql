/*
Description: Updating the QuestionOption value to 200 characters
Author: Archana
Patch Number: 1.0029
Dependant Patch Number: 1.0002
*/

drop type [PatientFlow].[QuestionOption]

create type [PatientFlow].[QuestionOption] as table
(
    [QuestionId] int null,
    [OptionId] int not null,
    [OptionValue] varchar (200) null,
    [NestedQuestionId] int not null,
    [OptionCode] varchar (50) null,
    [SnomedCode] bigint null,
    primary key clustered ([OptionId] asc)
);

go
