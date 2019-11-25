set DatabasePatcherVersion=3.9.7
"%~dp0..\..\Utilities\NuGet\NuGet.exe" install Emis.Database.Patcher -Version %DatabasePatcherVersion% -OutputDirectory "%~dp0..\Packages" -Source "http://tfs-pcc-store.devtest.emishosting.com/nuget/"
cd "%~dp0..\Packages\Emis.Database.Patcher.%DatabasePatcherVersion%\tools"

start DBPatcher.UI.exe -c "..\..\..\EMISPatientFlowKiosk\NDOController.xml"