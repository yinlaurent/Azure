$subscr="SubscriptionName"
Get-AzureRmSubscription –SubscriptionName $subscr | Select-AzureRmSubscription

Get-AzureRmContext

cd "D:\"

$rg = "CloudRg"
$vmname = "CloudVmName"

Get-AzureRmVM -ResourceGroupName $rg -Name $vmName
Stop-AzureRmVM -ResourceGroupName $rg -Name $vmName


Set-AzureRmVm -ResourceGroupName $rg -Name $vmName -Generalized

$vm = Get-AzureRmVM -ResourceGroupName $rg -Name $vmName -status
$vm.Statuses

Save-AzureRmVMImage -ResourceGroupName $rg -Name $vmname -DestinationContainerName images -VHDNamePrefix $vmname -Path "D:\userImageLinux\azureVmGeneralized.json"

New-AzureRmResourceGroupDeployment -Name VMSSupdate -ResourceGroupName "CloudVmss" -TemplateFile "azuredeploy.json" -TemplateParameterFile "azuredeploy.parameters.json" -Verbose