# Taken from psake https://github.com/psake/psake

<#
.SYNOPSIS
  This is a helper function that runs a scriptblock and checks the PS variable $lastexitcode
  to see if an error occcured. If an error is detected then an exception is thrown.
  This function allows you to run command-line programs without having to
  explicitly check the $lastexitcode variable.
.EXAMPLE
  exec { svn info $repository_trunk } "Error executing SVN. Please verify SVN command-line client is installed"
#>
function Exec
{
    [CmdletBinding()]
    param(
        [Parameter(Position = 0, Mandatory = 1)][scriptblock]$cmd,
        [Parameter(Position = 1, Mandatory = 0)][string]$errorMessage = ($msgs.error_bad_command -f $cmd)
    )
    & $cmd
    if ($lastexitcode -ne 0)
    {
        throw ("Exec: " + $errorMessage)
    }
}

$artifacts = ".\artifacts"

if (Test-Path $artifacts)
{
    Remove-Item $artifacts -Force -Recurse
}

exec { & dotnet clean -c Release CraftersCloud.Blueprint.sln }

exec { & dotnet build -c Release CraftersCloud.Blueprint.sln }

exec { & dotnet test -c Release CraftersCloud.Blueprint.sln --no-build -l trx --verbosity=normal }

exec { & dotnet pack .\src\CraftersCloud.Core\dotnet pack .\CraftersCloud.Blueprint.Template\CraftersCloud.Blueprint.Template.Template.csproj -c Release -o $artifacts --no-build }



