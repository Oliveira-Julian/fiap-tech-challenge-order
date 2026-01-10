#!/bin/bash

set -e

echo "Iniciando na porta 5000"

kubectl port-forward -n fiap-tech-challenge service/food-challenge-api 5000:5000 
