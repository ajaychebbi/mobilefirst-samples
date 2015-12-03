# windows8Native
This contains a sample project in Native C#/XAML Windoes 8.1 app 

TODO: detailed instructions

##Steps
1. Install MFP CLI and create a project

2. Add a windows8 API to the project

mfp add api news-reader -e windows8

3. Change the References in this project to point to the DLLs in that folder

a. Change the CPU to say X86
b. add references to Newtonsoft.Json.dll, SharpCompress.dll, 
buildtarget\x86\worklight-windows8.dll and AuthWinRT.winmd

4. Copy the wlclient.properties from the MFP project to the C# project

5. create a SampleHTTPAdapter

mfp add adapter SampleAdapter -t http
