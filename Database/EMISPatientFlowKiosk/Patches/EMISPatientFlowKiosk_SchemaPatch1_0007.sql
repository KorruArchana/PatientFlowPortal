update [PatientFlow].[KioskScreenControl] set ControlLabel='Sorry, your appointment is ##, we are unable to check you in until ## minutes before your appointment.  Please try again later.'
where UniqueId ='EARLYARR_TEXT'
update [PatientFlow].[KioskScreenControl] set ControlLabel='Sorry, your appointment time of ## has passed and we are unable to check you in.  Please contact reception'
where UniqueId ='LATEARR_TEXT'
update [PatientFlow].[Translation] set [TranslationText]=N'Sorry, your appointment is ##, we are unable to check you in until ## minutes before your appointment.  Please try again later.'
where [TranslationRefId] =13 and [LanguageId] = 1
update [PatientFlow].[Translation] set [TranslationText]=N'Lo sentimos, su cita es ##, no podemos comprobar que en hasta ## minutos antes de su cita. Inténtalo de nuevo más tarde.'
where [TranslationRefId] =13 and [LanguageId] = 2
update [PatientFlow].[Translation] set [TranslationText]=N'Désolé, votre rendez-vous est ##, nous sommes incapables de vous enregistrer jusqu''à ## minutes avant votre rendez-vous. Se il vous plaît réessayer plus tard.'
where [TranslationRefId] =13 and [LanguageId] = 3
update [PatientFlow].[Translation] set [TranslationText]=N'Leider haben wir den Termin ##, wir nicht imstande sind, im Zimmer bis ## Minuten vor dem Termin. Bitte versuchen Sie es später noch einmal.'
where [TranslationRefId] =13 and [LanguageId] = 4
update [PatientFlow].[Translation] set [TranslationText]=N'К сожалению, ваш назначение ##, мы не можем не проверять вас, пока ## минут до назначенного времени. Пожалуйста, повторите попытку позже.'
where [TranslationRefId] =13 and [LanguageId] = 5
update [PatientFlow].[Translation] set [TranslationText]=N'Üzgünüz, randevu biz atanmadan önce ## dakika kadar sizi kontrol edemiyoruz, ## olduğunu. Lütfen daha sonra tekrar deneyiniz.'
where [TranslationRefId] =13 and [LanguageId] = 6
update [PatientFlow].[Translation] set [TranslationText]=N'Sorry, your appointment time of ## has passed and we are unable to check you in.  Please contact reception'
where [TranslationRefId] =15 and [LanguageId] = 1
update [PatientFlow].[Translation] set [TranslationText]=N'Lo siento, la hora de la cita de ## ha pasado y estamos no se puede comprobar. Por favor póngase en contacto con la recepción'
where [TranslationRefId] =15 and [LanguageId] = 2
update [PatientFlow].[Translation] set [TranslationText]=N'Désolé, votre rendez-vous de ## a passé et nous sommes incapables de vérifier en vous. Se il vous plaît contacter la réception'
where [TranslationRefId] =15 and [LanguageId] = 3
update [PatientFlow].[Translation] set [TranslationText]=N'Leider ist Ihr Termin von ## vergangen ist, und wir sind nicht in der Lage, im Zimmer. Bitte kontaktieren Sie die Rezeption'
where [TranslationRefId] =15 and [LanguageId] = 4
update [PatientFlow].[Translation] set [TranslationText]=N'К сожалению, ваше время назначение ## прошло, и мы не можем проверить вас. Пожалуйста, свяжитесь с администрацией'
where [TranslationRefId] =15 and [LanguageId] = 5
update [PatientFlow].[Translation] set [TranslationText]=N'Üzgünüz, ## ve randevu zamanı geçti ve biz sizi kontrol edemiyoruz. Resepsiyona başvurun'
where [TranslationRefId] =15 and [LanguageId] = 6