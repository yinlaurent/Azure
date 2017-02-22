Login-AzureRmAccount
$storageAccountName = "storageAccountName"
$rg = Get-AzureRmResourceGroup -Name Cloud-Rg
$storage = $rg | Get-AzureRmStorageAccount -Name $storageAccountName
$container = $storage | Get-AzureStorageContainer vhds
$blob = $container | Get-AzureStorageBlob source.vhd
Start-AzureStorageBlobCopy -SrcBlob $blob

$storage | Start-AzureStorageBlobCopy -SrcBlob Cloud-VM-source.vhd -SrcContainer vhds -DestContainer vhdcopy
$storage | Get-AzureStorageBlobCopyState -Blob Cloud-VM-source.vhd -Container vhdcopy