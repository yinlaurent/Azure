Login-AzureRmAccount

Get-AzureRMSubscription | Sort SubscriptionName | Select SubscriptionName

$subscr="Azure subscription"
Get-AzureRmSubscription –SubscriptionName $subscr | Select-AzureRmSubscription

Register-AzureRmResourceProvider -ProviderNamespace Microsoft.ClassicInfrastructureMigrate

Get-AzureRmResourceProvider -ProviderNamespace Microsoft.ClassicInfrastructureMigrate

Get-AzurePublishSettingsFile
Import-AzurePublishSettingsFile

Add-AzureAccount

Get-AzureSubscription | Sort SubscriptionName | Select SubscriptionName

$subscr="Azure subscription"
Get-AzureSubscription –SubscriptionName $subscr | Select-AzureSubscription

Get-AzureService | ft Servicename

$serviceName = "cloud-vm-01"
$deployment = Get-AzureDeployment -ServiceName $serviceName
$deploymentName = $deployment.DeploymentName

# new VNET
Move-AzureService -Prepare -ServiceName $serviceName -DeploymentName $deploymentName -CreateNewVirtualNetwork

# existing VNET
$existingVnetRGName = "Migration"
$vnetName = "migration-vnet"
$subnetName = "migration-snet"
Move-AzureService -Prepare -ServiceName $serviceName -DeploymentName $deploymentName -UseExistingVirtualNetwork -VirtualNetworkResourceGroupName $existingVnetRGName -VirtualNetworkName $vnetName -SubnetName $subnetName

$vmName = "cloud-vm-00"
$vm = Get-AzureVM -ServiceName $serviceName -Name $vmName
$migrationState = $vm.VM.MigrationState

#Move-AzureService -Commit -ServiceName $serviceName -DeploymentName $deploymentName

$vnetName = "vnet-1"
Move-AzureVirtualNetwork -Prepare -VirtualNetworkName $vnetName

#Move-AzureVirtualNetwork -Abort -VirtualNetworkName $vnetName

#Move-AzureVirtualNetwork -Commit -VirtualNetworkName $vnetName

$storageAccountName = "0rportalvhds7dwdcx9g9321"

Move-AzureStorageAccount -Prepare -StorageAccountName $storageAccountName

Move-AzureStorageAccount -Abort -StorageAccountName $storageAccountName

Move-AzureStorageAccount -Commit -StorageAccountName $storageAccountName