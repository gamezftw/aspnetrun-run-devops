#!/usr/bin/env bash
az group create --name myResourceGroup --location westeurope
az acr create --resource-group myResourceGroup --name shoppingappacr123 --sku basic
az acr update -n shoppingappacr123 --admin-enabled true
az acr list --resource-group myResourceGroup --query "[].loginServer" --output table
docker tag shoppingapi:latest shoppingappacr123.azurecr.io/shoppingapi:v1
docker tag shoppingclient:latest shoppingappacr123.azurecr.io/shoppingclient:v1
docker push shoppingappacr123.azurecr.io/shoppingapi:v1
docker push shoppingappacr123.azurecr.io/shoppingclient:v1
az acr repository list --name shoppingappacr123 --output table
az aks create --resource-group myResourceGroup --name myAKSCluster --node-count 1 --generate-ssh-keys --attach-acr shoppingappacr123

az aks create --resource-group myResourceGroup --name myAKSCluster --node-count 1 --generate-ssh-keys --attach-acr shoppingappacr123 --node-vm-size Standard_D2as_v5
az aks install-cli

kubectl create secret docker-registry acr-secret --docker-server=shoppingappacr123.azurecr.io --docker-username=shoppingappacr123 --docker-password=nDfUBAEnTw4qmXvhI/oJY1Go7LloYKAx --docker-email=nikola.stojkovic@htecgroup.com
