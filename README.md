# S3Uploader

Author: Vadim Popov & lots of gus from google & stack overflow who have left lots of useful articles on this.

-------------------------
Description
-------------------------
COM object with method of upload file to S3 compatible with .net 4.5
Was needed to integrate Directum system with S3
Directum will be calling COM method that in turn will call Amazon.S3 .NET SDK to put files into S3 storage

COM interface method:

public interface S3UploaderInterface
    {
        string SendFile(string p_filename,
                        string p_accessKey,
                        string p_secretKey,
                        string p_serviceURL,
                        string p_bucketName,
                        string p_subDirectoryInBucket,
                        string p_fileNameInS3);

Example of VB program that calls method with registered COM object:
Sub test1()

    Dim S3Uploader As Object
    Set S3Uploader = CreateObject("S3.Upload")

    Call MsgBox(S3Uploader.SendFile("F:\scan.pdf", _
        "...replace with access key...", _
        "...replace with sectet key...", _
        "https://s3.cloud.lmru.tech", _
        "t-tmp-stor", _
        "", _
        "S3FileName33.zip"), 0, "A Message")

End Sub

You can use free S3 browser program to check the results.

Installation instructions
-------------------------
Register example:
1) place files of Release folder into specified folder - <release>
2) find location of Microsoft.NET framework regasm.exe utility and gacutil utility (C:\Windows\Microsoft.NET\Framework64\v4.0.30319\regasm.exe 
and C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools)
3) run cmd as system administrator
4) issue commands:

cd <path1>
gacutil -I <Path2>\S3Uploader.dll
cd <path3>
<path3>/regasm.exe <release>/S3Uploader.dll tlb:S3Uploader.tlb

Register example:
cd C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools
gacutil -I C:\Users\60006703\source\repos\S3Uploader\S3Uploader\bin\Release\S3Uploader.dll
cd C:\Windows\Microsoft.NET\Framework64\v4.0.30319
regasm C:\Users\60006703\source\repos\S3Uploader\S3Uploader\bin\Debug\S3Uploader.dll tlb:S3Uploader.tlb

De-Installation instructions
-------------------------

Need to de-install dlls in order to update with newer version or so.

Unregister:
1) run CMD as system administrator
2) issue commands:
cd <path1>
gacutil -U S3Uploader
cd <path3>
<path3>/regasm.exe <release>/S3Uploader.dll /unregister


Unregister example:
cd C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools
gacutil -u S3Uploader
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\regasm.exe C:\Users\60006703\source\repos\S3Uploader\S3Uploader\bin\Debug\S3Uploader.dll /unregister
