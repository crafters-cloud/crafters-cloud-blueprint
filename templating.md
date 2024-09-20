## Install the template locally from source (run in solution root)

``dotnet new install .\``

## Build the nuget package (run in solution root)

``dotnet pack CraftersCloud.Core.Blueprint.Template.csproj -c Release``

## Install the template locally from nupkg file

``dotnet new -i "bin\Release\CraftersCloud.Core.Blueprint.Template.3.3.0.nupkg"``

## Install the template from NuGet.org

``dotnet new install CraftersCloud.Core.Blueprint.Template``
If you are not authenticated automatically, add the --interactive argument.

## Install specific version of the template from NuGet.org

``dotnet new install CraftersCloud.Core.Blueprint.Template::VERSION``

where VERSION should be replaced with the specific version you want to install, e.g. 2.0.1

## Deploy a new solution based on the template:

``dotnet new blueprint --name Customer.Project --projectName Customer.Project --appProjectName customer-project --friendlyName "Customer Project" --allow-scripts yes``

## Uninstall the template (when installed from nupkg)

``dotnet new uninstall CraftersCloud.Core.Blueprint.Template``

## Uninstall the template (when installed from local source)

``dotnet new uninstall <path to solution e.g. D:\Projects\Enigmatry\enigmatry-entry-blueprint-template>``
