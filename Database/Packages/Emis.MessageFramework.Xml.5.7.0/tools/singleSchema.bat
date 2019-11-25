cls

rem echo batchfile dir= %~dp0

rem echo input xsd = %1
rem echo input ns = %2
rem echo ouput dir = %3
rem echo rootelement = %4

rem pause

if not exist %3 echo ** Output directory does not exist -> %3 **
if not exist %3 goto end

rem echo -xs=%1 -ns="%2" -out=%3 -lang=CS /20 -m="CodeXS.Schema.StandardCodeModifier.dll","SchemaDocumentation.dll" -esplit=0 -eparse=1 -root=%4
rem pause


"%~dp0\cxsc.exe" -xs=%1 -ns="%2" -out=%3 -lang=CS /20 -m="CodeXS.Schema.StandardCodeModifier.dll","SchemaDocumentation.dll" -esplit=0 -eparse=1 -root=%4

:end
