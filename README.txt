BUILD:

msbuild ~\VendingMachineKata\VendingMachineKata\VendingMachineKata.sln

RUN TESTS:

cd ~\VendingMachineKata\VendingMachineKata\packages\NUnit.ConsoleRunner.3.6.1\tools
nunit3-console.exe ~\VendingMachineKata\VendingMachineKata\VendingMachineKataTests\VendingMachineKataTests.csproj