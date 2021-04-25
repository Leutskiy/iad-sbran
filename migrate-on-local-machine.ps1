# update ef tool #
# dotnet tool update --global dotnet-ef

# cmd tool description
# https://ss64.com/nt/cmd.html

# about connection for powershell script
# https://mcpmag.com/articles/2018/12/10/test-sql-connection-with-powershell.aspx

Param(
  [Parameter(Position=1)][string]$action,
  [Parameter(Position=2)][string]$migrationName
)

$projectPath = "Sbran.Domain\Sbran.Domain.csproj"
$migrationFolder = "Migrations"
$startupProject = "Sbran.WebApp"

$username = "iadsbras"
$password = "p@ssw0rd"
$hostname = "localhost"
$port = "5432"
$database = "IadSbrasDB"

$connectionString = '"User ID={0};Password={1};Host={2};Port={3};Database={4}"' -f $username, $password, $hostname, $port, $database

$domainContextName = "DomainContext"
$systemContextName = "SystemContext"

# add new migration
$addCommand = "dotnet ef migrations add {0} -p {1} -o {2}" -f $migrationName, $projectPath, $migrationFolder
# remove all database migrations
$removeCommand = "dotnet ef migrations remove -p {0}" -f  $projectPath
# update database scheme for both domain and system contexts
$updateCommandTemplate = "dotnet ef database update $migrationName -p $projectPath --startup-project $startupProject --connection $connectionString --context {0}"
$updateDomainCommand = $updateCommandTemplate -f $domainContextName
$updateSystemCommand = $updateCommandTemplate -f $systemContextName

Switch ($action)
{
   'add' {
        cmd.exe /c $addCommand;
   }
   'remove' {
        cmd.exe /c $removeCommand;
   }
   'update' { 
        cmd.exe /c $updateDomainCommand;
        cmd.exe /c $updateSystemCommand;
   }
}
