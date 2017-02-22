cd "C:\Program Files (x86)\Microsoft SDKs\Azure\AzCopy"

$sourceTable = "https://myazuretable.table.core.windows.net/WADMetricsPT1MP10DV2S20160902/"
$sourceKey = "{storage-key}"

Get-Date
./AzCopy.exe /Source:$sourceTable /Dest:D:\tmp\ /SourceKey:$sourceKey /PayloadFormat:CSV
Get-Date
