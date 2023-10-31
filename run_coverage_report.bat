@echo off

coverlet .\tests\GoodFood.Tests\bin\Debug\net8.0\GoodFood.Tests.dll --target "dotnet" --targetargs "test --no-build" -f OpenCover
reportgenerator -reports:"coverage.opencover.xml" -targetdir:"coverage-results" -reportTypes:HtmlInline
start "" "coverage-results\Index.html"
