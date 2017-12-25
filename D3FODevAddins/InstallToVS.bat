@IF NOT DEFINED _echo @ECHO OFF

SET InstallingAssembly=%1
SET ExtDir=

for /F "eol=! tokens=2*" %%I IN ('reg query HKEY_CURRENT_USER\Software\Microsoft\VisualStudio\14.0\ExtensionManager\EnabledExtensions /v "DynamicsRainierVSTools-52924ab1-c4fa-47fc-a90d-a5d1e69986bb,1.0"') do set ExtDir=%%J

if exist "%ExtDir%extension.vsixmanifest" (
  pushd "%ExtDir%AddinExtensions"
  echo.
  echo.
  echo "Copying %InstallingAssembly% to Dynamics Addins folder %ExtDir%AddinExtensions."
  echo.
  call xcopy /q /y %InstallingAssembly% .
  echo.
  popd
) else (
  echo "Rainier tools not installed.
  echo.
)