exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capture anonymous survey feedback', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'AnonymousSurvey'; 

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Master table to store appointment status', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'AppointmentStatus'; 
 
exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Records changes made in database', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'AuditTrail'; 

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Captures all the configuaration information related to kiosk', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'KioskConfiguration'; 

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Store logo related to kiosk', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'KioskLogo'; 

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capture questionaires related to kiosk', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'KioskQuestionnaire'; 

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capture all screens used in kiosk for language translation', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'KioskScreen'; 

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Mater table to hold all supported languages', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Language'; 

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Log of all errors and informations related to kiosk and sync services', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Log'; 

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capture member information', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Member'; 

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capture the date when a patient submit questionaire feedback', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'NonAnonymousSurveyFrequency'; 

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capature patient information', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Patient'; 

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'User information required for api call', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'PatientFlowUser'; 

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capture all question related information from kiosk configurator', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Question'; 

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capture all questionnaire related information from kiosk configurator', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Questionnaire'; 

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Log related to sync service', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'SynchronisationLog'; 

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capture all language translation text', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Translation'; 

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capture all appointment information for arrival', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Appointment'; 

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capture session information for booking appointment', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'AppointmentSession'; 

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'master table to hold all screen controls in kiosk', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'KioskScreenControl'; 

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capture all answer options related to question', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'QuestionAnswerOptions'; 

insert [PatientFlow].[KioskScreen] ([ScreenId], [ScreenCode], [ScreenName], [IsConfigurable], [TranslationRefId]) values (27, N'P_MESSAGES', N'Arrival Messages', 0, null)

insert [PatientFlow].[KioskScreenControl] ([ControlId], [ScreenId], [UniqueId], [ControlLabel], [TranslationRefId]) values (74, 27, N'MSG_TITLETEXT', N'Messages', null)

insert [PatientFlow].[KioskScreenControl] ([ControlId], [ScreenId], [UniqueId], [ControlLabel], [TranslationRefId]) values (75, 27, N'MSG_BTN_NEXT', N'Next', null)