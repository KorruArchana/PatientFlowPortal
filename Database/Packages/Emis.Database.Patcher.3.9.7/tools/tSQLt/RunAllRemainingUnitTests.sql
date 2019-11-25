
exec sp_addextendedproperty 
		@name =		N'tSQLt.ResultsFormatter',
		@value =	N'tSQLt.ResultsFormatterNoSuccessfulTestsInSummary',
		@level0type = N'SCHEMA',
		@level0name = N'tSQLt', 
		@level1type = N'PROCEDURE',
		@level1name = N'Private_OutputTestResults';

-- Run the remainder of the unit tests.
exec tSQLt.RunAllBasedOffExecutionPoint @GetTestClassToRunBeforeFunctionalTests = 0;

exec sp_dropextendedproperty 
		@name =		N'tSQLt.ResultsFormatter',
		@level0type = N'SCHEMA',
		@level0name = N'tSQLt', 
		@level1type = N'PROCEDURE',
		@level1name = N'Private_OutputTestResults';