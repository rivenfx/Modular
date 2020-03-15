# 路径
$packFolder = (Get-Item -Path "./" -Verbose).FullName   # 当前路径
$rootFolder = Join-Path $packFolder "../"               # 项目根目录
$packOutputFolder = Join-Path $packFolder "framework"   # 输出nuget package 目录



# 所有的项目名称
$projects = (
    "Riven.Modular"
)
