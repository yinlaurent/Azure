Login-AzureRmAccount

New-AzureRmResourceGroup -Name S2s-arm -Location northeurope

New-AzureRmResourceGroupDeployment -Name deployment01 -ResourceGroupName S2s-arm -TemplateFile azuredeploy.json -TemplateParameterFile azuredeploy.parameters.json

Get-AzureRmPublicIpAddress | ?{$_.Name -eq "s2s-arm-gwPubIp"}

Add-AzureAccount

Get-AzureVNetConfig -ExportToFile "classicvnets.netcfg"

Set-AzureVNetConfig -ConfigurationPath "classicvnets.netcfg"

Get-AzureVNetGatewayKey -VNetName s2s-vnet-asm -LocalNetworkSiteName vnet02

Set-AzureVNetGatewayKey -VNetName s2s-vnet-asm -LocalNetworkSiteName vnet02 -SharedKey abc1234

$vnet01gateway = Get-AzureRmLocalNetworkGateway -Name vnet01 -ResourceGroupName S2s-arm
$vnet02gateway = Get-AzureRmVirtualNetworkGateway -Name s2s-arm-gw -ResourceGroupName S2s-arm

New-AzureRmVirtualNetworkGatewayConnection -Name arm-asm-s2s-connection `
    -ResourceGroupName S2s-arm -Location "North Europe" -VirtualNetworkGateway1 $vnet02gateway `
    -LocalNetworkGateway2 $vnet01gateway -ConnectionType IPsec `
    -RoutingWeight 10 -SharedKey 'abc1234'

Get-AzureVM

#via le portail ARM
Get-AzureVNetGatewayKey -VNetName s2s-vnet-asm -LocalNetworkSiteName vnet03

Set-AzureVNetGatewayKey -VNetName s2s-vnet-asm -LocalNetworkSiteName vnet03 -SharedKey abc1234

$vnet01gateway = Get-AzureRmLocalNetworkGateway -Name vnet01 -ResourceGroupName S2s-arm3
$vnet02gateway = Get-AzureRmVirtualNetworkGateway -Name s2s-arm3-gw -ResourceGroupName S2s-arm3

New-AzureRmVirtualNetworkGatewayConnection -Name arm-asm-s2s-connection2 `
    -ResourceGroupName S2s-arm -Location "North Europe" -VirtualNetworkGateway1 $vnet02gateway `
    -LocalNetworkGateway2 $vnet01gateway -ConnectionType IPsec `
    -RoutingWeight 10 -SharedKey 'abc1234'