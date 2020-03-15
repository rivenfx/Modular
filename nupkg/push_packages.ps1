. ".\common.ps1"

$apiKey = $args[0]

# 获取版本
[xml]$commonPropsXml = Get-Content (Join-Path $rootFolder "common.props")
$version = $commonPropsXml.Project.PropertyGroup.Version
$versionStr = $version[0].Trim()

# 发布所有包
foreach($project in $projects) {
    $projectName = $project
    $packageFullPath = Join-Path $packOutputFolder ($projectName + "." + $versionStr + ".nupkg")

    $packageFullPath

    & dotnet nuget push $packageFullPath -s https://api.nuget.org/v3/index.json --api-key "$apiKey"
}

# 返回脚本执行目录
Set-Location $packFolder
