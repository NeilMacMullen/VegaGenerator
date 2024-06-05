

[CmdletBinding()]
param(

    [Parameter(Mandatory = $true, HelpMessage = "version for package")]
    [string] $version,
    [Parameter(HelpMessage = "api key (if publising nuget packages)")]
    [string] $api,

    [Parameter(HelpMessage = "test only")]
    [switch] $skipBuild
)

if (-not $skipBuild) {
    #force rebuild
    Get-ChildItem -r bin | Remove-Item -r
    Get-ChildItem -r obj | Remove-Item -r

    dotnet build -c Release
    Get-ChildItem -r *.nupkg | Remove-Item -r
    
    #make nuget packages
    dotnet pack   -p:PackageVersion=$version .\VegaGenerator\VegaGenerator.csproj
       
    #remove pdbs
    get-ChildItem -recurse -path .\publish\ -include *.pdb | remove-item

}

if (-not ($api -like '') ) {
    dotnet nuget push .\VegaGenerator\bin\Release\VegaGenerator.$($version).nupkg --api-key $api --source https://api.nuget.org/v3/index.json
}

get-ChildItem -r *.nupkg | % FullName

