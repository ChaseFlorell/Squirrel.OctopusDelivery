<#
    The file may look quite verbose, but it'll get you setup with everything you need for a solid build script.

    Below is the [hashtable] use to store all of the required variables. Feel free to add custom variables to $this.
#>
#region Variable Block
    $rootDir  = resolve-path ".\.."
    $script:this = @{
        project = 'Squirrel.OctopusDelivery'
        rootDir = Resolve-Path "$rootDir"
        path = Resolve-Path "$rootDir\build"
        modulesDir = "$rootDir\build\modules"
        environmentsDir = "$rootDir\build\environments"
        visualStudioVersion = "12.0"
        artifactsDir = @{
            rootDir = "$rootDir\build-artifacts"
            logsDir = "$rootDir\build-artifacts\logs"
            outputDir =  "$rootDir\build-artifacts\output"
            resultsDir = "$rootDir\build-artifacts\results"
            publishDir = "$rootDir\build-artifacts\publish"
            workingDir = "$rootDir\build-artifacts\working"
        }
        packagesDir = "$rootDir\packages"
        nuspecDir = "$rootDir\nuspec"
        solutionFile = "$rootDir\Squirrel.OctopusDelivery.sln"
        rootNamespace = 'Squirrel.OctopusDelivery'
        Squirrel.OctopusDelivery = @{
                namespace = "Squirrel.OctopusDelivery"
                solution = "$rootDir\src\Squirrel.OctopusDelivery\Squirrel.OctopusDelivery.csproj"
                projectDir = "$rootDir\src\Squirrel.OctopusDelivery"
                testAssemblyFile = "$rootDir\build-artifacts\output\Squirrel.OctopusDelivery.Tests.dll"
                unitTestNamespace = 'Squirrel.OctopusDelivery.Tests.Unit'
                nuspecFile = "$rootDir\nuspec\Squirrel.OctopusDelivery.nuspec"
            }
    }
#endregion VariableBlock
Import-Module "$($this.packagesDir)\poshBAR.*\tools\modules\poshBAR" -force
# Import any other custom module you might need.

#default task (required by psake)
task default -depends compile, test, package
task compile -depends CompileSquirrel.OctopusDelivery
task test -depends TestSquirrel.OctopusDelivery
task package -depends PackageSquirrel.OctopusDelivery


task CompileSquirrel.OctopusDelivery -depends Init {
    Update-AssemblyVersions $this.version $this.buildNumber $this.informationalVersion $this.Squirrel.OctopusDelivery.projectDir
    Update-AssemblyVersions $this.version $this.buildNumber $this.informationalVersion $this.Squirrel.OctopusDelivery.projectDir
    Invoke-MSBuild $this.artifactsDir.outputDir `
               $this.solutionFile `
               -logPath $this.artifactsDir.logsDir `
               -namespace $this.rootNamespace `
               -visualStudioVersion $this.visualStudioVersion
}

task TestSquirrel.OctopusDelivery -depends CompileSquirrel.OctopusDelivery {
    Invoke-NUnit $this.Squirrel.OctopusDelivery.testAssemblyFile $this.artifactsDir.resultsDir $this.Squirrel.OctopusDelivery.unitTestNamespace
}

task PackageSquirrel.OctopusDelivery -depends CompileSquirrel.OctopusDelivery {
    Update-XmlConfigValues $this.Squirrel.OctopusDelivery.nuspecFile "//*[local-name() = 'version']" $this.version
    Update-XmlConfigValues $this.Squirrel.OctopusDelivery.nuspecFile "//*[local-name() = 'summary']" "$($this.Squirrel.OctopusDelivery.namespace) $($this.informationalVersion)"

    New-NugetPackage $this.Squirrel.OctopusDelivery.nuspecFile "$($this.version).$($this.buildNumber)" $this.artifactsDir.publishDir
}

# the init task simply finishes setting everything up.
task Init -depends MakeBuildDir, SetupPaths {
    $this.version = $version
    $this.environment = $buildEnvironment
    $this.buildNumber = $buildNumber
    $this.informationalVersion = $informationalVersion
    $this.includeCoverage = $includeCoverage
    $script:environmentSettings = Get-EnvironmentSettings $this.environment "//environmentSettings" $this.environmentsDir
    Framework '4.0'
}

task MakeBuildDir {
    Write-Host "[re]Generating build-artifacts directory."
    # iterate over all of the nested artifacts directories, deleting the contents and re-creating the directory
    $this.artifactsDir.keys | %{ 
        remove-item -force -recurse $this.artifactsDir[$_] -ErrorAction SilentlyContinue 
        New-Item -ItemType Directory -Force -Path $this.artifactsDir[$_] | Out-Null
    }
    # uncomment if you want MSBUILD to clean the build output directory.
    # Invoke-CleanMSBuild $this.solutionFile
}

task SetupPaths {
    # used to add a custom tool to your $env:PATH if it's not in a standard location.
    # $envPATH += ";$rootDir\tools\someTool"
}

<#
    Improves the default FormatTaskName that comes with PSake 
    Leave this at the bottom of your script.
#>
FormatTaskName {
    param($taskName)
    Format-TaskNameToHost $taskName
}
