Param(
  [Parameter(Position=1)][string]$action,
  [Parameter(Position=2)][string]$migrationName
)

$projectPath = "Sbran.Domain\Sbran.Domain.csproj"
$migrationFolder = "Migrations"
$startupProject = "Sbran.WebApp"

Switch ($action)
{
   'add' { cmd.exe /c "dotnet ef migrations add $migrationName -p $projectPath -o $migrationFolder" }
   'remove' { cmd.exe /c "dotnet ef migrations remove -p $projectPath"}
   'update' { cmd.exe /c "dotnet ef database update $migrationName -p $projectPath --startup-project $startupProject --connection ""User ID=scientist;Password=P@ssw0rd;Host=test.iad-sbras.ru;Port=5432;Database=IadSbran"" --context DomainContext && dotnet ef database update $migrationName -p $projectPath --startup-project $startupProject --connection ""User ID=scientist;Password=P@ssw0rd;Host=test.iad-sbras.ru;Port=5432;Database=IadSbran"" --context SystemContext"}
}
