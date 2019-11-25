if object_id('[PatientFlow].[SaveKioskConfiguration]') is not null
	drop procedure [PatientFlow].[SaveKioskConfiguration];
go

create procedure [PatientFlow].[SaveKioskConfiguration] @Language varchar(max),
	@Module varchar(max),
	@PatientMatch varchar(max),
	@Status varchar(max),
	@Questionnaire varchar(max),
	@KioskArrival varchar(max),
	@KioskSetting varchar(max),
	@Organisation varchar(max),
	@Logo varbinary(max) = null,
	@KioskId varchar(max)
as
begin
	set nocount on;
	set transaction isolation level read committed;

	begin try
			;

		begin transaction;

		if not exists (
				select 1
				from [patientFlow].[KioskConfiguration]
				where KioskId = @KioskId
					and ConfigType = 'Status'
				)
		begin
			insert into [patientflow].[KioskConfiguration] (
				ConfigType,
				Value,
				KioskId
				)
			values (
				'Language',
				@Language,
				@KioskId
				);

			insert into [patientflow].[KioskConfiguration] (
				ConfigType,
				Value,
				KioskId
				)
			values (
				'Modules',
				@Module,
				@KioskId
				);

			insert into [patientflow].[KioskConfiguration] (
				ConfigType,
				Value,
				KioskId
				)
			values (
				'PatientMatch',
				@PatientMatch,
				@KioskId
				);

			insert into [patientflow].[KioskConfiguration] (
				ConfigType,
				Value,
				KioskId
				)
			values (
				'Status',
				@Status,
				@KioskId
				);

			insert into [patientflow].[KioskConfiguration] (
				ConfigType,
				Value,
				KioskId
				)
			values (
				'Questionnaire',
				@Questionnaire,
				@KioskId
				);

			insert into [patientflow].[KioskConfiguration] (
				ConfigType,
				Value,
				KioskId
				)
			values (
				'KioskArrival',
				@KioskArrival,
				@KioskId
				);

			insert into [patientflow].[KioskConfiguration] (
				ConfigType,
				Value,
				KioskId
				)
			values (
				'KioskSettings',
				@KioskSetting,
				@KioskId
				);

			insert into [patientflow].[KioskConfiguration] (
				ConfigType,
				Value,
				KioskId
				)
			values (
				'Organisation',
				@Organisation,
				@KioskId
				);

			if @Logo is not null
			begin
				insert into [patientflow].[KioskLogo] (
					Logo,
					KioskId
					)
				values (
					@Logo,
					@KioskId
					);
			end
		end
		else
		begin
			update PatientFlow.KioskConfiguration
			set Value = @Language
			where ConfigType = 'Language'
				and KioskId = @KioskId;

			update PatientFlow.KioskConfiguration
			set Value = @Module
			where ConfigType = 'Modules'
				and KioskId = @KioskId;

			update PatientFlow.KioskConfiguration
			set Value = @PatientMatch
			where ConfigType = 'PatientMatch'
				and KioskId = @KioskId;

			update PatientFlow.KioskConfiguration
			set Value = @Status
			where ConfigType = 'Status'
				and KioskId = @KioskId;

			update PatientFlow.KioskConfiguration
			set Value = @Questionnaire
			where ConfigType = 'Questionnaire'
				and KioskId = @KioskId;

			update PatientFlow.KioskConfiguration
			set Value = @KioskArrival
			where ConfigType = 'KioskArrival'
				and KioskId = @KioskId;

			update PatientFlow.KioskConfiguration
			set Value = @KioskSetting
			where ConfigType = 'KioskSettings'
				and KioskId = @KioskId;

			update PatientFlow.KioskConfiguration
			set Value = @Organisation
			where ConfigType = 'Organisation'
				and KioskId = @KioskId;

			if @Logo is not null
			begin
				if not exists (
						select 1
						from [patientflow].[KioskLogo]
						where KioskId = @KioskId
						)
					insert into [patientflow].[KioskLogo] (
						Logo,
						KioskId
						)
					values (
						@Logo,
						@KioskId
						);
				else
					update PatientFlow.KioskLogo
					set Logo = @Logo
					where KioskId = @KioskId;
			end
		end

		commit transaction;
	end try

	begin catch
			;

		declare @Error int,
			@Message varchar(4000);

		select 
			@Error = error_number(),
			@Message = error_message();

		if xact_state() <> 0
		begin
			rollback transaction;
		end

		raiserror (
				'SaveKioskConfiguration : %d: %s',
				16,
				1,
				@Error,
				@Message
				);
	end catch;
end
