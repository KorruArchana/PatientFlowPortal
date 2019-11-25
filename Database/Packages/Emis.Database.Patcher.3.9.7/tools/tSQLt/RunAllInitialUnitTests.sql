
exec sp_addextendedproperty 
		@name =		N'tSQLt.ResultsFormatter',
		@value =	N'tSQLt.ResultsFormatterNoSuccessfulTestsInSummary',
		@level0type = N'SCHEMA',
		@level0name = N'tSQLt', 
		@level1type = N'PROCEDURE',
		@level1name = N'Private_OutputTestResults';

-- Run the first set of unit tests upfront (note: this is generally policy tests).
exec tSQLt.RunAllBasedOffExecutionPoint @GetTestClassToRunBeforeFunctionalTests = 1;

exec sp_dropextendedproperty 
		@name =		N'tSQLt.ResultsFormatter',
		@level0type = N'SCHEMA',
		@level0name = N'tSQLt', 
		@level1type = N'PROCEDURE',
		@level1name = N'Private_OutputTestResults';